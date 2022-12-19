using System;
using System.Linq;


namespace day01{
    class Program{
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static int partOne(string[] input){
            return calorieCounting(input, 1);
        }


        static int partTwo(string[] input){
            return calorieCounting(input, 3);
        }
            

        static int calorieCounting(string[] input, int numberOfElves){

            int calories = 0;
            int[] elves = new int[numberOfElves];
            
            foreach(string line in input){
                if(line == ""){
                    if(calories > elves.Min()) elves[Array.IndexOf(elves, elves.Min())] = calories;
                    calories = 0;
                }
                else calories += Convert.ToInt32(line);
            }

            return elves.Sum();
        }
    }
}