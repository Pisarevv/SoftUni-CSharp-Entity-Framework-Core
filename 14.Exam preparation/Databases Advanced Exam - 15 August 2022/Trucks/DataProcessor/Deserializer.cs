namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Castle.Core.Internal;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.Utilities;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            StringBuilder sb = new StringBuilder();

            var despatchers = xmlHelper.Deserialize<ImportDespatcherDto[]>(xmlString);

            var validDespatchers = new HashSet<Despatcher>();

            foreach(var  despatcherDto in despatchers)
            {
                if (!IsValid(despatcherDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (despatcherDto.Position.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher despatcher = mapper.Map<Despatcher>(despatcherDto);

                validDespatchers.Add(despatcher);

                var validTrucks = new HashSet<Truck>();

                foreach(var truckDto in despatcherDto.Trucks)
                {
                    if (!IsValid(truckDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (truckDto.VinNumber.IsNullOrEmpty())
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck truck = mapper.Map<Truck>(truckDto);
                    validTrucks.Add(truck);

                }

                despatcher.Trucks = validTrucks;

                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcher.Name, validTrucks.Count));
            }

            context.Despatchers.AddRange(validDespatchers);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportClient(TrucksContext context, string jsonString)
        {
            IMapper mapper = CreateMapper();

            StringBuilder sb = new StringBuilder();

            var clients = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);

            var validTruckIds = context.Trucks.Select(t => t.Id).ToHashSet();

            var validClients = new HashSet<Client>();

            foreach(var clientDto in clients)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (clientDto.Nationality.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(clientDto.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = mapper.Map<Client>(clientDto);

                validClients.Add(client);

                var validClientTrucks = new HashSet<ClientTruck>();

                foreach(var truckId in clientDto.Trucks.Distinct())
                {
                    if(!validTruckIds.Any(id => id == truckId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    ClientTruck clientTruck = new ClientTruck();
                    clientTruck.TruckId = truckId;

                    validClientTrucks.Add(clientTruck);

                }

                client.ClientsTrucks = validClientTrucks;

                sb.AppendLine(string.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));

            }

            context.AddRange(validClients);

            context.SaveChanges();

            return sb.ToString();
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
                cfg.AddProfile<TrucksProfile>();
            }));
        }
    }
}