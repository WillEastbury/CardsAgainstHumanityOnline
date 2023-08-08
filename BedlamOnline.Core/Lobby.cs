using System;
using System.Collections.Generic;
using System.Linq;

namespace BedlamOnline.Core
{
    public class Lobby
    {
        private const int MaxPlayers = 10;
        private const int CardsPerHand = 7;

        public List<Player> Players { get; set; } = new List<Player>();
        public List<Round> Rounds { get; set; } = new List<Round>();

        public CardDeck Deck { get; set; }
        
        public string LobbyName { get; set; }

        public Lobby(CardDeck deck)
        {
            Deck = deck;
            LobbyName = $"{Deck.DrawBlackCard().CardText}-{Guid.NewGuid()}";
        }

        public void Join(Player player)
        {
            if (Players.Count < MaxPlayers)
            {
                Players.Add(player);
                for (int i = 0; i < CardsPerHand; i++)
                {
                    player.Hand.Add(Deck.DrawWhiteCard());
                }
            }
            else
            {
                throw new Exception("The lobby is full.");
            }
        }

        public void StartRound()
        {
            var round = new Round
            {
                BlackCard = Deck.DrawBlackCard(),
                Judge = Players.OrderBy(x => Guid.NewGuid()).First() // Select a random judge
            };

            Rounds.Add(round);
        }

        public void PlayCard(Player player, WhiteCard card, string accessToken)
        {
            if (player.AccessToken != accessToken)
            {
                throw new Exception("Invalid access token.");
            }

            if (player.HasPlayedThisRound)
            {
                throw new Exception("Player has already played this round.");
            }

            if (player.Hand.Contains(card))
            {
                var round = Rounds.Last();
                if (!round.Judge.Equals(player))
                {
                    round.PlayerCards[player] = card;
                    player.Hand.Remove(card);
                    player.HasPlayedThisRound = true;
                }
                else
                {
                    throw new Exception("Judge can't play a card.");
                }
            }
        }

        public void ChooseWinner(Player judge, Player winner, string accessToken)
        {
            if (judge.AccessToken != accessToken)
            {
                throw new Exception("Invalid access token.");
            }

            var round = Rounds.Last();
            if (!round.Judge.Equals(judge))
            {
                throw new Exception("Only the judge can choose the winner.");
            }

            winner.Score++;
            foreach (var playerCard in round.PlayerCards)
            {
                Deck.Recycle(playerCard.Value);
            }

            round.PlayerCards.Clear();

            foreach (var player in Players)
            {
                if (!player.Equals(round.Judge))
                {
                    player.Hand.Add(Deck.DrawWhiteCard());
                }
                player.HasPlayedThisRound = false;
            }
        }
    }
}
