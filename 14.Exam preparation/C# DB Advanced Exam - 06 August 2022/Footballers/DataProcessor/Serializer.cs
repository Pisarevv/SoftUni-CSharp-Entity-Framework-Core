namespace Footballers.DataProcessor
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using ProductShop.Utilities;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var coatches = context.Coaches
                          .Where(c => c.Footballers.Count() > 0)
                          .ProjectTo<ExportCoatchDto>(mapper.ConfigurationProvider)
                          .OrderByDescending(c => c.FootballersCount)
                          .ThenBy(c => c.CoachName)
                          .ToArray();
  

            var result = xmlHelper.Serialize<ExportCoatchDto>(coatches, "Coaches");

            return result;
                          
                      
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                        .Where(t => t.TeamsFootballers.Any(p => p.Footballer.ContractStartDate >= date))
                        .ToList()
                        .Select(t => new
                        {
                            Name = t.Name,
                            Footballers = t.TeamsFootballers.Where(p => p.Footballer.ContractStartDate >=  date)
                                .OrderByDescending(p => p.Footballer.ContractEndDate)
                                .ThenBy(p => p.Footballer.Name)
                                .Select(
                                p => new
                                {
                                    FootballerName = p.Footballer.Name,
                                    ContractStartDate = p.Footballer.ContractStartDate.ToString("d",CultureInfo.InvariantCulture),
                                    ContractEndDate = p.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                                    BestSkillType = p.Footballer.BestSkillType.ToString(),
                                    PositionType = p.Footballer.PositionType.ToString(),
                                })
                                .ToList()

                        })
                        .ToList()
                        .OrderByDescending(x => x.Footballers.Count)
                        .ThenBy(x => x.Name)
                        .Take(5)
                        .ToList();

            var result = JsonConvert.SerializeObject(teams, Formatting.Indented);

            return result.ToString();
                         
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FootballersProfile>();
            }));
        }
    }
}
