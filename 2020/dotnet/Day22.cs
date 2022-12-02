using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day22
    {
        private static Stopwatch stopWatch = new Stopwatch();

        public static void PartOne(IEnumerable<string> input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 22 - Part One ==========");

            var player1 = GetPlayerDeck(input, 1);
            var player2 = GetPlayerDeck(input, 2);

            var winningDeck = PlayGame(player1, player2);

            var result = CalculateScore(winningDeck);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(IEnumerable<string> input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 22 - Part Two ==========");

            var player1 = GetPlayerDeck(input, 1);
            var player2 = GetPlayerDeck(input, 2);

            var (player1deck, player2deck) = PlayRecursiveGame(player1, player2);

            var winningDeck = (player1deck.Any()) ? player1deck : player2deck;

            var result = CalculateScore(winningDeck);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }


        private static IEnumerable<int> GetPlayerDeck(IEnumerable<string> input, int player)
        {
            var data = input.ToList();

            var skip = data.FindIndex(d => d.Equals($"Player {player}:"));
            var take = data.FindIndex(skip, d => string.IsNullOrWhiteSpace(d));
            if (take < 0)
            {
                take = input.Count();
            }

            var deck = data.Skip(skip + 1).Take(take - 1).Select(x => int.Parse(x));

            return deck;
        }

        private static IEnumerable<int> PlayGame(IEnumerable<int> Player1, IEnumerable<int> Player2)
        {
            var player1deck = new Queue<int>(Player1);
            var player2deck = new Queue<int>(Player2);

            while (player1deck.Any() && player2deck.Any())
            {
                var player1Turn = player1deck.Dequeue();
                var player2Turn = player2deck.Dequeue();

                if (player1Turn > player2Turn)
                {
                    player1deck.Enqueue(player1Turn);
                    player1deck.Enqueue(player2Turn);
                }
                else if (player2Turn > player1Turn)
                {
                    player2deck.Enqueue(player2Turn);
                    player2deck.Enqueue(player1Turn);
                }
                else
                {
                    // Whut? Draw? There's no rule given for that.
                }
            }

            return (player1deck.Any()) ? player1deck : player2deck;
        }

        private static (IEnumerable<int>, IEnumerable<int>) PlayRecursiveGame(IEnumerable<int> Player1, IEnumerable<int> Player2, int index = 0)
        {
            index++;
            Console.WriteLine($"=== Game {index} ===");

            var player1deck = new Queue<int>(Player1);
            var player2deck = new Queue<int>(Player2);

            var player1GameHistory = new List<string>();
            var player2GameHistory = new List<string>();

            while (player1deck.Any() && player2deck.Any())
            {
                Console.WriteLine("\r");
                Console.WriteLine($"-- Round {player1GameHistory.Count + 1} (Game {index}) --");
                var stringDeck1 = string.Join(", ", player1deck);
                Console.WriteLine($"Player 1 deck: {stringDeck1}");
                var stringDeck2 = string.Join(", ", player2deck);
                Console.WriteLine($"Player 2 deck: {stringDeck2}");

                // Before either player deals a card, 
                // if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks, 
                // the game instantly ends in a win for player 1
                if (player1GameHistory.Any(gh => gh.Equals(stringDeck1)) ||
                    player2GameHistory.Any(gh => gh.Equals(stringDeck2)))
                {
                    Console.WriteLine("Player 1 wins because someone has had this deck before!");
                    return (player1deck.ToList(), Enumerable.Empty<int>());
                }
                player1GameHistory.Add(stringDeck1);
                player2GameHistory.Add(stringDeck2);

                // Play to card
                var player1Turn = player1deck.Dequeue();
                var player2Turn = player2deck.Dequeue();

                Console.WriteLine($"Player 1 plays: {player1Turn}");
                Console.WriteLine($"Player 2 plays: {player2Turn}");

                // If both players have at least as many cards remaining in their deck as the value of the card they just drew, 
                // the winner of the round is determined by playing a new game of Recursive Combat
                if (player1deck.Count >= player1Turn && player2deck.Count >= player2Turn)
                {
                    Console.WriteLine("Playing a sub-game to determine the winner... \r\n");
                    var (deck1, deck2) = PlayRecursiveGame(player1deck.Take(player1Turn).ToArray(), player2deck.Take(player2Turn).ToArray(), index);
                    if (deck1.Any())
                    {
                        Console.WriteLine($"The winner of game {index + 1} is player 1!");
                        Console.WriteLine($"\r\n ... anyway, back to game {index}.");
                        Console.WriteLine($"Player 1 wins round { player1GameHistory.Count} of game {index}!");
                        player1deck.Enqueue(player1Turn);
                        player1deck.Enqueue(player2Turn);
                    }

                    if (deck2.Any())
                    {
                        Console.WriteLine($"The winner of game {index + 1} is player 2!");
                        Console.WriteLine($"\r\n ... anyway, back to game {index}.");
                        Console.WriteLine($"Player 2 wins round { player2GameHistory.Count} of game {index}!");
                        player2deck.Enqueue(player2Turn);
                        player2deck.Enqueue(player1Turn);
                    }

                    continue;
                }

                // Otherwise, at least one player must not have enough cards left in their deck to recurse; 
                // the winner of the round is the player with the higher - value card.
                if (player1Turn > player2Turn)
                {
                    Console.WriteLine($"Player 1 wins round {player1GameHistory.Count} of game {index}!");
                    player1deck.Enqueue(player1Turn);
                    player1deck.Enqueue(player2Turn);
                }
                else if (player2Turn > player1Turn)
                {
                    Console.WriteLine($"Player 2 wins round {player2GameHistory.Count} of game {index}!");
                    player2deck.Enqueue(player2Turn);
                    player2deck.Enqueue(player1Turn);
                }
                else
                {
                    // Whut? Draw? There's no rule given for that.
                }
            }

            return (player1deck, player2deck);
        }

        private static long CalculateScore(IEnumerable<int> winningDeck)
        {
            var score = 0;

            var deck = winningDeck.Reverse().ToArray();

            foreach (var i in Enumerable.Range(1, winningDeck.Count()))
            {
                score += deck[i - 1] * i;
            }

            return score;
        }
    }
}
