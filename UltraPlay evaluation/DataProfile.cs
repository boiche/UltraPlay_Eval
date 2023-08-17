using AutoMapper;
using UltraPlay_evaluation.Data.Entities;
using UltraPlay_evaluation.Models;

namespace UltraPlay_evaluation
{
    public class DataProfile : Profile
    {
        public DataProfile() 
        {
            CreateMap<XmlSportsSport, Sport>();
            CreateMap<XmlSportsSportEvent, Event>();
            CreateMap<XmlSportsSportEventMatch, Data.Entities.Match>();
            CreateMap<XmlSportsSportEventMatchBet, Bet>();
            CreateMap<XmlSportsSportEventMatchBetOdd, Odd>();
            CreateMap<Data.Entities.Match, Models.Match>();
        }
    }
}
