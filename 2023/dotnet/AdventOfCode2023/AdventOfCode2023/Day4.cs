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

        public object ExecutePart2DumbWay()
        {
            //var testInput = new[] {
            //    "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            //    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            //    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            //    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            //    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            //    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
            //    };

            //Input = testInput;

            var playedScratchCards = new List<ScratchCard>();

            // Collect all scratch cards
            var scratchCards = new Queue<ScratchCard>();
            foreach (var index in Enumerable.Range(0, Input.Length))
            {
                var scratchCard = GetScratchCard(Input[index]);

                // Play the card
                var matchingNumbers = scratchCard.WinningNumbers.Intersect(scratchCard.CardNumbers);

                // Add newly won cards to the scratch card
                foreach (var i in Enumerable.Range(1, matchingNumbers.Count()))
                {
                    var newScratchCard = GetScratchCard(Input[(scratchCard.ID + i) - 1]);
                    scratchCards.Enqueue(newScratchCard);
                }

                scratchCards.Enqueue(scratchCard);
            }

            Console.WriteLine($"Scratch card count: {scratchCards.Count}");

            // Play every scratchcard on the queue
            while (scratchCards.Count > 0)
            {
                var scratchCard = scratchCards.Dequeue();

                var matchingNumbers = scratchCard.WinningNumbers.Intersect(scratchCard.CardNumbers);
                Console.WriteLine($"Won {matchingNumbers.Count()} new scratch cards");

                // Add newly won cards to the queue
                foreach (var i in Enumerable.Range(1, matchingNumbers.Count()))
                {
                    var newScratchCard = GetScratchCard(Input[(scratchCard.ID + i) - 1]);
                    scratchCards.Enqueue(newScratchCard);
                }

                playedScratchCards.Add(scratchCard);
                Console.WriteLine($"Scratch cards played: {playedScratchCards.Count}");
            }

            return playedScratchCards.Count;
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
