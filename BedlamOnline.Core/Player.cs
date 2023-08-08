using System.Collections.Generic;

namespace BedlamOnline.Core
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string AccessToken { get; set; }
        public bool HasPlayedThisRound { get; set; }
        public List<WhiteCard> Hand { get; set; } = new List<WhiteCard>();
    }
}