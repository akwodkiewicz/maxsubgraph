using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Taio
{
    class Program
    {
        static void Main(string[] args)
        {
            var delimiter = ',';
            if (args.Length < 2)
            {
                Console.WriteLine("usage: solver <input_file_1> <input_file_2>[<delimiter>]");
                return;
            }
            if (args.Length >= 3)
            {
                delimiter = args[2][0];
            }

            var graph1 = DeserializeGraphFromCsv(args[0], delimiter);
            var graph2 = DeserializeGraphFromCsv(args[1], delimiter);

            var result = McSplitAlgorithm.McSplit(graph1, graph2);

            PrintResult(result);

        }


        static Graph DeserializeGraphFromCsv(string csvPath, char separator)
        {
            var file = File.ReadAllLines(csvPath);

            var nodesNumber = (uint)file.Length;
            var matrix = new bool[nodesNumber, nodesNumber];

            for (int i = 0; i < nodesNumber; i++)
            {
                var row = file[i].Split(separator);
                if (row.Length != nodesNumber)
                {
                    throw new ArgumentException("Macierz sąsiedztwa nie jest kwadratowa!");
                }

                for (int j = 0; j < nodesNumber; j++)
                {
                    matrix[i, j] = row[j] == "1";
                }
            }

            return new Graph(nodesNumber, matrix);
        }

        static void PrintResult(List<(uint, uint)> result)
        {
            Console.WriteLine("=== Maximum common induced subgraph ===");
            Console.WriteLine("G      H");
            Console.WriteLine("--------");
            foreach (var mapping in result)
            {
                Console.WriteLine($"{mapping.Item1 + 1} <==> {mapping.Item2 + 1}");
            }
        }
    }
}

