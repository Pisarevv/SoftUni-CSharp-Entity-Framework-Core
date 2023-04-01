namespace Boardgames.DataProcessor
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using ProductShop.Utilities;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var creators = context.Creators
                           .Where(c => c.Boardgames.Count > 0)
                           .ProjectTo<ExportCreatorDto>(mapper.ConfigurationProvider)
                           .ToArray()
                           .OrderByDescending(c => c.Boardgames.Count())
                           .ThenBy(c => c.CreatorName)
                           .ToArray();

            var result = xmlHelper.Serialize(creators, "Creators");

            return result.TrimEnd();
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            

            var sellers = context.Sellers
                          .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
                          .ToList()
                          .Select(s => new 
                          {
                              Name = s.Name,
                              Website = s.Website,
                              Boardgames = s.BoardgamesSellers
                                           .Where(s => s.Boardgame.YearPublished >= year && s.Boardgame.Rating <= rating)
                                           .Select(g => new 
                                           {
                                               Name = g.Boardgame.Name,
                                               Rating = g.Boardgame.Rating,
                                               Mechanics = g.Boardgame.Mechanics,
                                               Category = g.Boardgame.CategoryType.ToString()
                                           })
                                           .OrderByDescending(g => g.Rating)
                                           .ThenBy(g => g.Name)
                                           .ToList()
                          })
                          .OrderByDescending(s => s.Boardgames.Count())
                          .ThenBy(s => s.Name)
                          .Take(5)
                          .ToList();

            var result = JsonConvert.SerializeObject(sellers, Formatting.Indented);

            return result;
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BoardgamesProfile>();
            }));
        }
    }
}