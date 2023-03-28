namespace Footballers.DataProcessor
{
    using AutoMapper;
    using Castle.Core.Internal;
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using ProductShop.Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            StringBuilder sb = new StringBuilder();

            var coaches = xmlHelper.Deserialize<ImportCoachDto[]>(xmlString);

            var validCoaches = new HashSet<Coach>();

            foreach (var coachDto in coaches)
            {
               
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (coachDto.Name.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = mapper.Map<Coach>(coachDto);

                var validFootballers = new HashSet<Footballer>();
                validCoaches.Add(coach);

                foreach (var footballerDto in coachDto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    if (footballerDto.ContractEndDate.IsNullOrEmpty() || footballerDto.ContractStartDate.IsNullOrEmpty())
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (footballerDto.ContractStartDate.IsNullOrEmpty() || footballerDto.ContractEndDate.IsNullOrEmpty())
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballContractStartDate;

                    bool isFootbalerContractStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out footballContractStartDate);

                    if (!isFootbalerContractStartDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballContractEndDate;

                    bool isFootbalerContractSEndDateValid = DateTime.TryParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out footballContractEndDate);

                    if (!isFootbalerContractSEndDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (footballContractStartDate >= footballContractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballer = mapper.Map<Footballer>(footballerDto);

                    validFootballers.Add(footballer);
                }

                coach.Footballers = validFootballers;

                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }

            context.Coaches.AddRange(validCoaches);
            context.SaveChanges();
            

            return sb.ToString().TrimEnd();

        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            IMapper mapper = CreateMapper();

            StringBuilder sb = new StringBuilder();


            var teams = JsonConvert.DeserializeObject <ImportTeamDto[] >(jsonString);
            var validTeams = new List<Team>();

            var validFootballPlayers = context.Footballers.ToList();

            foreach(var teamDto in teams)
            {
                if (!IsValid(teamDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (teamDto.Nationality.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(teamDto.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Team team = mapper.Map<Team>(teamDto);

                validTeams.Add(team);

                var validFootballers = context.Footballers.ToList();

                var validTeamFootballer = new List<TeamFootballer>(); 

                foreach (int footballPlayerId in teamDto.Footballers.Distinct())
                {
                    Footballer footballer = validFootballPlayers.Where(x => x.Id == footballPlayerId).FirstOrDefault();

                    if (footballer == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer teamFootballer = new TeamFootballer { 
                        Footballer = footballer
                    };

                    validTeamFootballer.Add(teamFootballer);

                }

                team.TeamsFootballers = validTeamFootballer;

                sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(validTeams);
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
                cfg.AddProfile<FootballersProfile>();
            }));
        }


    }
}

