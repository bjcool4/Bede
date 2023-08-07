using Bede.Logic.Class;
using Bede.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bede.Logic
{
    public class SlotMachineService : ISlotMachineService
    {
        private double balance;

        public double DepositAmount { get; private set; }
        public double Balance => balance;
        // Define the symbols and their corresponding coefficients and probabilities
        public Dictionary<char, Symbol> symbols = new Dictionary<char, Symbol>
        {
            { 'A', new Symbol { Coefficient = 0.4, Probability = 0.45 } },
            { 'B', new Symbol { Coefficient = 0.6, Probability = 0.35 } },
            { 'P', new Symbol { Coefficient = 0.8, Probability = 0.15 } },
            { '*', new Symbol { Coefficient = 0, Probability = 0.05 } }
        };

        public List<char[]> Spin()
        {
            char[] result = new char[3];
            List<char[]> finalResult = new List<char[]>();
            Random random = new Random();
            for (int y = 0; y <= 3; y++)
            {
                result = new char[3];
                for (int i = 0; i < 3; i++)
                {
                    double rand = random.NextDouble();
                    double cumulativeProbability = 0;

                    foreach (var symbol in symbols)
                    {
                        cumulativeProbability += symbol.Value.Probability;
                        if (rand < cumulativeProbability)
                        {
                            result[i] = symbol.Key;
                            break;
                        }
                    }
                }
                finalResult.Add(result);
            }
            return finalResult;
        }

        public double CalculateWinAmount(char[] result, double stakeAmount)
        {
            Dictionary<char, int> symbolCount = new Dictionary<char, int>();
            foreach (char symbol in result)
            {
                if (symbolCount.ContainsKey(symbol))
                    symbolCount[symbol]++;
                else
                    symbolCount[symbol] = 1;
            }

            double winAmount = 0;
            foreach (var kvp in symbolCount)
            {
                char symbol = kvp.Key;
                int count = kvp.Value;
                winAmount += symbols[symbol].Coefficient * count * stakeAmount;
            }

            return winAmount;
        }

        public void Play()
        {
            Console.WriteLine("Welcome to the Slot Machine Game!");
            Console.Write("Enter your deposit amount: ");
            double depositAmount = Convert.ToDouble(Console.ReadLine());

            double balance = depositAmount;

            while (balance > 0)
            {
                Console.Write($"\nYour current balance is {balance}. Enter stake amount: ");
                double stakeAmount = Convert.ToDouble(Console.ReadLine());
                if (stakeAmount > balance)
                {
                    Console.WriteLine("You cannot stake more than your current balance.");
                    continue;
                }

                List<char[]> result = Spin();
                List<double> winAmount = new List<double>();
                foreach (var item in result)
                {
                    winAmount.Add(CalculateWinAmount(item, stakeAmount));
                }
                balance = (balance - stakeAmount) + winAmount.Sum(x => x);

                Console.WriteLine("\nResult:");
                Console.WriteLine("---------");
                foreach (var item in result)
                {
                    Console.WriteLine($" {item[0]} | {item[1]} | {item[2]} ");
                }
                Console.WriteLine("---------");
                Console.WriteLine($"\nYou won {winAmount.Sum(x => x)} with this spin.");
                Console.WriteLine($"Your total balance now is {balance}.\n");
            }

            Console.WriteLine("\nGame Over! Your balance is 0. You have run out of money.");

        }
    }
}
