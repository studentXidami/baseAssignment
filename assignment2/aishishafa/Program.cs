using System;
using System.Collections.Generic;

namespace PrimeNumberSieve
{
    class Program
    {
        static void Main(string[] args)
        {
            int upperLimit = 100;
            List<int> primes = SieveOfEratosthenes(upperLimit);
            PrintPrimes(primes);
        }

        /// <summary>
        /// 使用埃氏筛法找出2到上限之间的所有素数
        /// </summary>
        /// <param name="upperLimit">素数的上限</param>
        /// <returns>素数的列表</returns>
        static List<int> SieveOfEratosthenes(int upperLimit)
        {
            bool[] isPrime = new bool[upperLimit + 1];
            for (int i = 2; i <= upperLimit; i++)
            {
                isPrime[i] = true;
            }

            for (int i = 2; i * i <= upperLimit; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= upperLimit; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            List<int> primes = new List<int>();
            for (int i = 2; i <= upperLimit; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        /// <summary>
        /// 打印素数列表
        /// </summary>
        /// <param name="primes">素数的列表</param>
        static void PrintPrimes(List<int> primes)
        {
            Console.WriteLine("2到100以内的素数有：");
            foreach (int prime in primes)
            {
                Console.Write(prime + " ");
            }
            Console.WriteLine();
        }
    }
}