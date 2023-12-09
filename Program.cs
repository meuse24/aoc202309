using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace aoc202309 // Advent of Code 2023 - Tag 9 - Günther Meusburger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stopwatch=new Stopwatch(); stopwatch.Start();
            string path = @"C:\Users\guent\OneDrive\anp_GF07\aoc2023\C#\aoc202309\Input9.txt"; // Ersetzen Sie dies durch den Pfad zu Ihrer Datei
            var rows = new List<List<int>>(); var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                var numbers = new List<int>(); var values = line.Split(' ');
                foreach (var value in values) if (int.TryParse(value, out int number)) numbers.Add(number);
                rows.Add(numbers);
            }

            long part1 = 0; long part2 = 0;
            foreach (var row in rows) { part1 += extrapolateFwd(row); part2 += extrapolateBwd(row); }
            Console.WriteLine($"AoC 2023, Day 9: Part1: {part1}, Part2: {part2} ");
            stopwatch.Stop(); Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} mSec");

            int extrapolateFwd(List<int> r) // Extrapolation nach vorne
            {   // Rekursion, Ende, wenn alle Elemente der Liste den gleichen Wert haben
                if (r.All(x => x == r.Last())) return r.Last();
                else
                {   // an die Liste ein Int anhängen, der um die differenz der letzten liste erhöht ist
                    r.Add(r.Last() + extrapolateFwd(r.Zip(r.Skip(1), (a, b) => b - a).ToList()));
                    return r.Last();
                };
            }

            int extrapolateBwd(List<int> r) // Extrapolation nach vorne
            {   // Rekursion, Ende, wenn alle Elemente der Liste den gleichen Wert haben
                if (r.All(x => x == r.Last())) return r.Last();
                else
                {   // an die erste Stelle die Differenz aus der Vorliste vom ersten Wert abziehen und einfügen
                    r.Insert(0, r[0] - extrapolateBwd(r.Zip(r.Skip(1), (a, b) => b - a).ToList()));
                    return r[0];
                };
            }
        }
    }
}