namespace AzW.AdventOfCode.Year2023
{
    public class Day4 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var cardScores = new List<long>();

            foreach (var card in Input)
            {
                var cardNumbers = card.Split(':').Last().Split('|');
                var winningNumbers = cardNumbers.First().Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).Select(int.Parse);
                var myNumbers = cardNumbers.Last().Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).Select(int.Parse);

                var matchingNumbers = winningNumbers.Intersect(myNumbers);

                var cardScore = 0;
                foreach (var i in Enumerable.Range(1, matchingNumbers.Count()))
                {
                    cardScore = cardScore == 0 ? 1 : cardScore * 2;
                }

                cardScores.Add(cardScore);
            }

            return cardScores.Sum();
        }

        public override object ExecutePart2()
        {
            var totalCards = new Dictionary<int, long>();
            var playedScratchCards = new List<ScratchCard>();

            foreach (var card in Input)
            {
                var scratchCard = GetScratchCard(card);

                // Play the card
                var matchingNumbers = scratchCard.WinningNumbers.Intersect(scratchCard.CardNumbers);
                scratchCard.Wins = matchingNumbers.Count();

                playedScratchCards.Add(scratchCard);

                totalCards.Add(scratchCard.ID, 1);
            }

            //PrintCards(totalCards);

            foreach (var card in totalCards)
            {
                var c = playedScratchCards[card.Key - 1];

                foreach (var i in Enumerable.Range(1, c.Wins))
                {
                    totalCards[c.ID + i] = totalCards[c.ID + i] + totalCards[c.ID];
                }

                //PrintCards(totalCards);
            }

            var t = totalCards.Sum(x => x.Value);
            return t;
        }

        private void PrintCards(Dictionary<int, long> cardDict)
        {
            foreach (var card in cardDict)
            {
                Console.WriteLine($"Card {card.Key} : {card.Value}");
            }
            Console.WriteLine('\n');
        }

        private static ScratchCard GetScratchCard(string input)
        {
            var card = input.Split(":");
            var cardId = int.Parse(card[0].Split(' ').Last());

            var cardNumbers = card.Last().Split('|');
            var winningNumbers = cardNumbers.First().Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).Select(int.Parse);
            var myNumbers = cardNumbers.Last().Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).Select(int.Parse);

            var scratchCard = new ScratchCard(cardId, winningNumbers.ToList(), myNumbers.ToList());

            return scratchCard;
        }

        private sealed record ScratchCard(int ID, List<int> WinningNumbers, List<int> CardNumbers)
        {
            public int Wins { get; set; } = 0;
        };
    }
}
