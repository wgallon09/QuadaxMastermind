using System;
using System.Collections.Generic;

namespace Quadax_Mastermind
{
    class Mastermind
    {
        private List<int> Solution { get; set; }

        private readonly int SolutionLength;
        private readonly int MaxTurns;
        private readonly int Min;
        private readonly int Max;

        public Mastermind(int solutionLength = 4, int maxTurns = 10, int min = 1, int max = 6)
        {
            this.SolutionLength = solutionLength;
            this.MaxTurns = maxTurns;
            this.Min = min;
            this.Max = max;
        }

        public void Play()
        {
            Console.WriteLine("Welcome to Mastermind!");
            do
            {
                this.GenerateSolution();
                Console.WriteLine();
                Console.WriteLine("Round Rules:");
                Console.WriteLine(string.Format("Guesses: {0}; Min Int: {1}; Max Int: {2}", this.MaxTurns, this.Min, this.Max));
                Console.WriteLine();
                for (int i = 0; i < this.MaxTurns; i++)
                {
                    Console.WriteLine(string.Format("You have {0} guesses remaining.", this.MaxTurns - i));
                    if (this.CheckAnswer(this.GuessAnswer()))
                    {
                        Console.WriteLine("Congratulations!  You found the code!");
                        break;
                    }
                    else if (i == this.MaxTurns - 1)
                    {
                        Console.WriteLine("Sorry, you ran out of guesses!  The correct answer was:");
                        foreach (int j in this.Solution)
                        {
                            Console.Write(j);
                        }
                        Console.WriteLine();
                    }
                }
            }
            while (this.PlayAgain());
        }

        private string GuessAnswer()
        {
            string guessMessage = string.Format("Please input a {0} digit combination to guess, then press enter.", this.SolutionLength);
            Console.WriteLine(guessMessage);
            string guess = Console.ReadLine();
            if (!int.TryParse(guess, out int num) || guess.Length != this.SolutionLength)
            {
                Console.WriteLine("You entered an invalid number");
                return this.GuessAnswer();
            }
            return guess;
        }

        private void GenerateSolution()
        {
            Random r = new Random();
            Solution = new List<int>();

            for (int i = 0; i < this.SolutionLength; i++)
            {
                this.Solution.Add(r.Next(this.Min, this.Max + 1));
            }
        }

        private bool CheckAnswer(string guess)
        {
            bool correct = true;
            string result = string.Empty;

            for (int i = 0; i < guess.Length; i++)
            {
                int currentDigit = Convert.ToInt32(Convert.ToString(guess[i]));
                if (Solution.Contains(currentDigit))
                {
                    if (currentDigit == Solution[i])
                    {
                        result += "+";
                    }
                    else
                    {
                        result += "-";
                        correct = false;
                    }
                }
                else
                {
                    result += " ";
                    correct = false;
                }
            }
            Console.WriteLine("Result:" + result);
            return correct;
        }

        private bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again? (Y/N): ");
            if (Console.ReadLine().ToUpperInvariant() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
