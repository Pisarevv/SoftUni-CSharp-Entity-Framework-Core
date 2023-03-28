namespace Footballers
{
    using AutoMapper;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using System.Globalization;

    // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
    public class FootballersProfile : Profile
    {
        public FootballersProfile()
        {
            new MapperConfiguration(cfg =>
             {
                 //Coach
                 this.CreateMap<ImportCoachDto, Coach>()
                 .ForMember(d => d.Name, obj => obj.MapFrom(s => s.Name))
                 .ForMember(d => d.Nationality, obj => obj.MapFrom(s => s.Nationality))
                 .ForMember(d => d.Footballers, act => act.Ignore());

                 this.CreateMap<ImportFootballerDto, Footballer>()
                  .ForMember(d => d.ContractStartDate, obj => obj.MapFrom(s => DateTime.ParseExact(s.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                  .ForMember(d => d.ContractEndDate, obj => obj.MapFrom(s => DateTime.ParseExact(s.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                  .ForMember(d => d.BestSkillType, obj => obj.MapFrom(s => (BestSkillType)s.BestSkillType))
                  .ForMember(d => d.PositionType, obj => obj.MapFrom(s => (BestSkillType)s.PositionType));

                 //Team
                 this.CreateMap<ImportTeamDto, Team>()
                 .ForMember(d => d.TeamsFootballers, act => act.Ignore());
 
             });
        }
    }
}
