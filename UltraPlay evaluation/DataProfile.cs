using AutoMapper;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation
{
    public class DataProfile : Profile
    {
        public DataProfile() 
        {
            CreateMap<XmlSportsSport, Sport>();
            CreateMap<XmlSportsSportEvent, Event>();
            CreateMap<XmlSportsSportEventMatch, Match>();
            CreateMap<XmlSportsSportEventMatchBet, Bet>();
            CreateMap<XmlSportsSportEventMatchBetOdd, Odd>();
            CreateMap<string, Data.Entities.MatchType>()
                .ForMember(x => x.ID, x => x.MapFrom(t => Enum.Parse(typeof(MatchTypes), t)))
                .ForMember(x => x.Name, x => x.MapFrom(t => t));
        }
    }
}
