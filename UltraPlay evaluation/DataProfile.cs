using AutoMapper;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation
{
    public class DataProfile : Profile
    {
        public DataProfile() 
        {
            CreateMap<Match, Models.Match>();
            CreateMap<GetPreviewBetsResult, Bet>()
                .ForMember(x => x.MatchID, x => x.MapFrom(s => s.MatchID))
                .ForMember(x => x.ID, x => x.MapFrom(s => s.BetID))
                .ForMember(x => x.IsActive, x => x.MapFrom(s => s.BetIsActive))
                .ForMember(x => x.IsLive, x => x.MapFrom(s => s.IsLive))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.BetName));

            CreateMap<GetPreviewBetsResult, Odd>()
                .ForMember(x => x.SpecialBetValue, x => x.MapFrom(s => s.SpecialBetValue))
                .ForMember(x => x.ID, x => x.MapFrom(s => s.OddID))
                .ForMember(x => x.IsActive, x => x.MapFrom(s => s.OddIsActive))
                .ForMember(x => x.Value, x => x.MapFrom(s => s.OddValue))
                .ForMember(x => x.Name, x => x.MapFrom(s => s.OddName));
        }
    }
}
