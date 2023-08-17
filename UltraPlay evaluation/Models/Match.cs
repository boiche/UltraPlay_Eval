using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.Models
{
    public class Match
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<Bet> ActiveBets { get; set; }
        public List<Bet> InactiveBets { get; set; }
        public List<Bet> PreviewBets { get; set; }
    }
}
