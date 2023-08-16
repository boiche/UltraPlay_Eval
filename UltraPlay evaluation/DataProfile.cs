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
        }
    }
}
