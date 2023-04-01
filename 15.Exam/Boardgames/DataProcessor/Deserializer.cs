namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ImportDto;
    using Castle.Core.Internal;
    using Newtonsoft.Json;
    using ProductShop.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();
            StringBuilder sb = new StringBuilder();


            var creators = xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString);

            var validCreators = new HashSet<Creator>();

            foreach (var creatorDto in creators)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creator = mapper.Map<Creator>(creatorDto);
                validCreators.Add(creator);

                var validBoardgames = new HashSet<Boardgame>();

                foreach( var boardgameDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (boardgameDto.Name.IsNullOrEmpty())
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = mapper.Map<Boardgame>(boardgameDto);
                    validBoardgames.Add(boardgame);
                }

                creator.Boardgames = validBoardgames;

                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, validBoardgames.Count));
            }

            context.Creators.AddRange(validCreators);
            context.SaveChanges();


            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();
            StringBuilder sb = new StringBuilder();

            var sellers = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);

            var validSellers = new HashSet<Seller>();
            var validBoardgameIds = context.Boardgames.Select(b => b.Id).ToList();


            foreach(var sellerDto in sellers)
            {
                if(!IsValid(sellerDto))
                {
                    sb.AppendLine(ErrorMessage); 
                    continue;
                }

                if (sellerDto.Country.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(sellerDto.Website.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (sellerDto.Address.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = mapper.Map<Seller>(sellerDto);
                validSellers.Add(seller);

                var validBoardgameSellers = new HashSet<BoardgameSeller>();

                foreach(var boardgameId in sellerDto.Boardgames.Distinct())
                {
                    if(!validBoardgameIds.Any(id => id == boardgameId)){
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller boardgameSeller = new BoardgameSeller();
                    boardgameSeller.BoardgameId = boardgameId;

                    validBoardgameSellers.Add(boardgameSeller);
                }

                seller.BoardgamesSellers = validBoardgameSellers;
                sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count()));
            }

            context.Sellers.AddRange(validSellers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
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
