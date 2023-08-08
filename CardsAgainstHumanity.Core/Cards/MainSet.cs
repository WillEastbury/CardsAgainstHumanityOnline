using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsAgainstHumanity.Core.Cards
{
    public class MainSet : CardSet
    {
        protected override Dictionary<string, int> BlackCards => new Dictionary<string, int>()
        {
            { "Why can't I sleep at night?", 1 },
            { "I got 99 problems but _ ain't one.", 1 },
            { "What's a girl's best friend?", 1 },
            { "What's that smell?", 1 },
            { "This is the way the world ends / This is the way the world ends / Not with a bang but with _.", 1 },
            { "What is Batman's guilty pleasure?", 1 },
            { "TSA guidelines now prohibit _ on airplanes.", 1 
        };

        protected override List<string> WhiteCards => new List<string>()
        {
            "Vigorous jazz hands.",
            "Flightless birds.",
            "Doing the right thing.",
            "The violation of our most basic human rights.",
            "A balanced breakfast.",
            "The Big Bang.",
            "Being marginalized.",
            "Cuddling."
        };
    }
}