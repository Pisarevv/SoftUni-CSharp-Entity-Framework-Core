namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;
    using Castle.Core.Internal;
    using Newtonsoft.Json;
    using ProductShop.Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var sb = new StringBuilder();

            var countries = xmlHelper.Deserialize<ImportCountryDto[]>(xmlString);
            var validCountries = new HashSet<Country>();

            foreach (var countryDto in countries)
            {
                if (!IsValid(countryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var country = mapper.Map<Country>(countryDto);
                validCountries.Add(country);

                sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }

            context.Countries.AddRange(validCountries);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var sb = new StringBuilder();

            var manufacturers = xmlHelper.Deserialize<ImportManufacturerDto[]>(xmlString);

            var validManufacturers = new HashSet<Manufacturer>();

            foreach(var manufacturerDto in manufacturers)
            {
                if (!IsValid(manufacturerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufacturer = mapper.Map<Manufacturer>(manufacturerDto);

                if (validManufacturers.Any(m => m.ManufacturerName == manufacturer.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var foundedArgs = manufacturer.Founded.Split(", ",StringSplitOptions.RemoveEmptyEntries).ToArray();
                string foundedInfo = $"{foundedArgs[foundedArgs.Length - 2]}, {foundedArgs[foundedArgs.Length-1]}";
                validManufacturers.Add(manufacturer);

                sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, foundedInfo));
            }

            context.Manufacturers.AddRange(validManufacturers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var sb = new StringBuilder();

            var shells = xmlHelper.Deserialize<ImportShellDto[]>(xmlString);

            var validShells = new HashSet<Shell>();

            foreach (var shellsDto in shells)
            {
                if (!IsValid(shellsDto))
                {
                    sb.AppendLine(ErrorMessage); 
                    continue;
                }

                Shell shell = mapper.Map<Shell>(shellsDto);

                validShells.Add(shell);
                sb.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));

            }

            context.Shells.AddRange(validShells);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

 
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {

            IMapper mapper = CreateMapper();

            var sb = new StringBuilder();

            var guns = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);

            var validGuns = new HashSet<Gun>();

            foreach(var gunDto in guns)
            {
                if (!IsValid(gunDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (gunDto.GunType.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!Enum.IsDefined(typeof(GunType), gunDto.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Gun gun = mapper.Map<Gun>(gunDto);

                validGuns.Add(gun);

                var validCountriesGuns = new HashSet<CountryGun>();

                foreach (var countryId in gunDto.Countries)
                {
                    CountryGun countryGun = new CountryGun();
                    countryGun.CountryId = countryId.Id;

                    validCountriesGuns.Add(countryGun);
                }

                gun.CountriesGuns = validCountriesGuns;

                sb.AppendLine(string.Format(SuccessfulImportGun, gun.GunType, gun.GunWeight, gun.BarrelLength));
            }

            context.Guns.AddRange(validGuns);
            context.SaveChanges();


             return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArtilleryProfile>();
            }));
        }

    }

    
}