using System.Collections.Generic;

namespace BedlamOnline.Core
{
    public class Round
    {
        public BlackCard BlackCard { get; set; }
        public Dictionary<Player, WhiteCard> PlayerCards { get; set; } = new Dictionary<Player, WhiteCard>();
        public Player Judge { get; set; }
      
    }
}
