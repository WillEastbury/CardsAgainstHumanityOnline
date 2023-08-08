using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BedlamOnline.Core
{
    public record BlackCard(string CardText);

    public record WhiteCard(string CardText, string CardDetailText);

    public class CardDeck
    {
        public Queue<BlackCard> BlackCards { get; set; } = new Queue<BlackCard>();
        public Queue<WhiteCard> WhiteCards { get; set; } = new Queue<WhiteCard>();

        public async Task LoadAndShuffleDeck()
        {
            string url = "https://raw.githubusercontent.com/kclemson/bedlam/main/bedlam-full-card-list.csv";

            using var httpClient = new HttpClient();
            var fileContents = await httpClient.GetStringAsync(url);

            var pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
            var regex = new Regex(pattern);

            using var reader = new StringReader(fileContents);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tokens = regex.Split(line);
                if (tokens[0] == "Black")
                {
                    var blackCard = new BlackCard(tokens[1].Trim('\"'));
                    BlackCards.Enqueue(blackCard);
                }
                else if (tokens[0] == "White")
                {
                    var whiteCard = new WhiteCard(tokens[1].Trim('\"'), tokens[2].Trim('\"'));
                    WhiteCards.Enqueue(whiteCard);
                }
            }

            // Shuffle the cards
            var random = new Random();
            BlackCards = new Queue<BlackCard>(BlackCards.OrderBy(x => random.Next()));
            WhiteCards = new Queue<WhiteCard>(WhiteCards.OrderBy(x => random.Next()));
        }

        public BlackCard DrawBlackCard()
        {
            return this.BlackCards.Dequeue();
        }

        public WhiteCard DrawWhiteCard()
        {
            return this.WhiteCards.Dequeue();
        }

        public void Recycle(BlackCard card)
        {
            this.BlackCards.Enqueue(card);
        }

        public void Recycle(WhiteCard card)
        {
            this.WhiteCards.Enqueue(card);
        }
    }
}
