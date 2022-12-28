using System;
using System.Collections.Generic;
using System.Linq;

namespace day20{
    class Program{
        static void Main(string[] args){
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static long partOne(string[] input){
            return decrypt(input, 1, 1);
        }


        static long partTwo(string[] input){
            return decrypt(input, 811589153, 10);
        }


        static long decrypt(string[] input, int decriptionKey, int mixingCount){
            
            var initialFile = new List<(long value, int index)>();
            for (int i = 0; i < input.Length; i++){
                initialFile.Add((long.Parse(input[i]) * decriptionKey, i));
            }

            var file = new List<(long value, int index)>(initialFile);

            for(int c = 0; c < mixingCount; c++){
                for (int i = 0; i < input.Length; i++){

                    var startIndex = file.IndexOf(initialFile[i]);
                    var endIndex = (startIndex + initialFile[i].value) % (input.Length - 1);

                    // loop around the list
                    if (endIndex < 0) endIndex = input.Length + endIndex - 1;

                    file.Remove(initialFile[i]);
                    file.Insert((int) endIndex, initialFile[i]);
                }
            }

            var zero = file.FindIndex(e => e.value == 0);
            var index1000 = (zero + 1000) % input.Length;
            var index2000 = (zero + 2000) % input.Length;
            var index3000 = (zero + 3000) % input.Length;

            return file[index1000].value + file[index2000].value + file[index3000].value;
        }
    }
}