namespace Boardgames
{
    using AutoMapper;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.DataProcessor.ImportDto;

    public class BoardgamesProfile : Profile
    {
        // DO NOT CHANGE OR RENAME THIS CLASS!
        public BoardgamesProfile()
        {
            new MapperConfiguration(cfg =>
            {
                //Creator
                this.CreateMap<ImportCreatorDto, Creator>()
                .ForMember(d => d.Boardgames, obj => obj.Ignore());
                this.CreateMap<Creator,ExportCreatorDto>()
                .ForMember(d => d.CreatorName, obj => obj.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.BoardgamesCount, obj => obj.MapFrom(s => s.Boardgames.Count()))
                .ForMember(d => d.Boardgames, obj => obj.MapFrom(s => s.Boardgames.ToArray().OrderBy(b => b.Name).ToArray()));


                //BoardGame
                this.CreateMap<ImportBoardgameDto, Boardgame>();
                this.CreateMap<Boardgame, ExportBoardGameDto>();
                

                //Seller
                this.CreateMap<ImportSellerDto, Seller>()
                .ForMember(s => s.BoardgamesSellers, opt => opt.Ignore());

            });
        }
    }
}