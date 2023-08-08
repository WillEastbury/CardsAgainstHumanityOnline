using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BedlamOnline.Core
{
    public class GameHost
    {
        private List<BlackCard> BaseBlackCards { get; set; } = new List<BlackCard>();
        private List<WhiteCard> BaseWhiteCards { get; set; } = new List<WhiteCard>();
        private List<Lobby> Lobbies { get; set; } = new List<Lobby>();
        public async Task Initialize()
        {
            // Load the base list of cards from the URL
            var deck = new CardDeck();
            await deck.LoadAndShuffleDeck();
            // Extract the base list of cards
            BaseBlackCards = deck.BlackCards.ToList();
            BaseWhiteCards = deck.WhiteCards.ToList();
        }
        public Lobby CreateLobby()
        {
            // Create a new CardDeck with a shuffled copy of the base list of cards
            var deck = new CardDeck
            {
                BlackCards = new Queue<BlackCard>(Shuffle(BaseBlackCards)),
                WhiteCards = new Queue<WhiteCard>(Shuffle(BaseWhiteCards))
            };
            // Create a new Lobby with the CardDeck
            var lobby = new Lobby(deck);
            Lobbies.Add(lobby);
            return lobby;
        }

        private IEnumerable<T> Shuffle<T>(IEnumerable<T> list)
        {
            var random = new Random();
            return list.OrderBy(x => random.Next()).ToList();
        }
    
        public List<Player> GetLeaderboard()
        {
            // Flatten the list of players from all lobbies
            var allPlayers = Lobbies.SelectMany(lobby => lobby.Players).ToList();

            // Sort the players by score in descending order
            var leaderboard = allPlayers.OrderByDescending(player => player.Score).ToList();

            return leaderboard;
        }
    }
}