namespace AzW.AdventOfCode.Year2023
{
    public class Day7 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var camelCardHands = new List<CamelCardHand>();

            foreach (var line in Input)
            {
                var hand = line.Split(' ');
                var camelCardHand = new CamelCardHand()
                {
                    Hand = hand[0],
                    Bid = int.Parse(hand[1])
                };

                camelCardHands.Add(camelCardHand);
            }

            // Sort the hands
            var sortedCamelCardHands = SortCamelCardHands(camelCardHands);

            // Calculate the winnings
            var winnings = new List<long>();
            foreach (var rank in Enumerable.Range(0, sortedCamelCardHands.Count))
            {                
                var multiplier = rank + 1;
                //Console.WriteLine($"Rank: {rank}; {sortedCamelCardHands[rank].Bid} * {multiplier}");
                winnings.Add(sortedCamelCardHands[rank].Bid * multiplier);
            }

            return winnings.Sum();
        }

        private List<CamelCardHand> SortCamelCardHands(List<CamelCardHand> camelCardHands)
        {
            var fiveOfAKind = SortByStrength(camelCardHands.Where(c => c.Hand.Distinct().Count() == 1).Select(c => c), "five of a kind");            
            var onePair = SortByStrength(camelCardHands.Where(c => c.Hand.Distinct().Count() == 4).Select(c => c), "one pair");
            var highCard = SortByStrength(camelCardHands.Where(c => c.Hand.Distinct().Count() == 5).Select(c => c), "high card");

            var twoValues = camelCardHands.Where(c => c.Hand.Distinct().Count() == 2).Select(c => c);
            var four = GetMultipleOfAKind(twoValues, 4);
            var full = twoValues.Except(four);

            var fourOfAKind = SortByStrength(four, "four of a kind");
            var fullHouse = SortByStrength(full, "full house");

            var threeValues = camelCardHands.Where(c => c.Hand.Distinct().Count() == 3).Select(c => c);
            var three = GetMultipleOfAKind(threeValues, 3);
            var two = threeValues.Except(three);

            var threeOfAKind = SortByStrength(three, "three of a kind");
            var twoPair = SortByStrength(two, "two pair");

            // Five of a kind > Four of a kind > Full house > Three of a kind > two pair > One pair > High card
            var sortedList = fiveOfAKind.Concat(fourOfAKind).Concat(fullHouse).Concat(threeOfAKind).Concat(twoPair).Concat(onePair).Concat(highCard).ToList();
            sortedList.Reverse();

            return sortedList;
        }

        private List<CamelCardHand> GetMultipleOfAKind(IEnumerable<CamelCardHand> camelCards, int multiple)
        {
            var threeOfAKind = new List<CamelCardHand>();
            foreach (var camelCard in camelCards)
            {
                foreach (var card in camelCard.Hand)
                {
                    if (camelCard.Hand.Count(c => c == card) == multiple)
                    {
                        threeOfAKind.Add(camelCard);
                        break;
                    }
                }
            }
            return threeOfAKind;
        }

        private List<CamelCardHand> SortByStrength(IEnumerable<CamelCardHand> camelCards, string type)
        {
            //  A > K > Q > J > T > 9 > 8 > 7 > 6 > 5 > 4 > 3 > 2
            var sortableCards = new List<KeyValuePair<List<int>, CamelCardHand>>();

            foreach (var camelCard in camelCards)
            {
                camelCard.Type = type;

                var score = new List<int>();
                
                foreach(var card in camelCard.Hand)
                {
                    //var value = GetCardValueP1(card);
                    var value = GetCardValueP2(card);

                    score.Add(value);
                }              

                sortableCards.Add(new KeyValuePair<List<int>, CamelCardHand>(score, camelCard));
            }

            var result = sortableCards.OrderBy(c => c.Key[0])
                                      .ThenBy(c => c.Key[1])
                                      .ThenBy(c => c.Key[2])
                                      .ThenBy(c => c.Key[3])
                                      .ThenBy(c => c.Key[4]);

            return result.Select(r => r.Value).Reverse().ToList();
        }

        private int GetCardValueP1(char c) => c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            '9' => 9,
            '8' => 8,
            '7' => 7,
            '6' => 6,
            '5' => 5,
            '4' => 4,
            '3' => 3,
            '2' => 2,
            _ => throw new NotImplementedException()
        };

        private int GetCardValueP2(char c) => c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            '9' => 9,
            '8' => 8,
            '7' => 7,
            '6' => 6,
            '5' => 5,
            '4' => 4,
            '3' => 3,
            '2' => 2,
            'T' => 1,
            _ => throw new NotImplementedException()
        };

        private string[] GetTestInput()
        {
            return
            [
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483"
            ];
        }

        private sealed record CamelCardHand()
        {
            public required int Bid { get; init; }
            public required string Hand { get; set; }
            public string? Type { get; set; }
        }
    }
}
