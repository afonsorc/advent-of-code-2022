using System;
using System.IO;

namespace day02{
    class Program{
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            //partOne(input);
            
            partTwo(input);

        }


        static void partOne(string[] input){
            
            const int myOffset = 88;
            const int oppOffset = 65;

            const int lose = 0;
            const int draw = 3;
            const int win = 6;

            int[,] play =  {{draw, lose, win}, {win, draw, lose}, {lose, win, draw}};
            int score = 0;

            foreach (string round in input){

                int myPlay = round[2] - myOffset;    
                int oppPlay = round[0] - oppOffset;

                score += play[myPlay, oppPlay];
                score += myPlay + 1;

            }
          
            System.Console.WriteLine(score);
            return;
        }


        static void partTwo(string[] input){
            
            const int myOffset = 88;
            const int oppOffset = 65;

            const int rock = 1;
            const int paper = 2;
            const int scissors = 3;

            int[,] play = {{scissors, rock, paper}, {rock, paper, scissors}, {paper, scissors, rock}};
            int score = 0;

            foreach (string round in input){

                int result = round[2] - myOffset;   
                int oppPlay = round[0] - oppOffset;
                
                score += play[result, oppPlay] + result * 3;
            }
          
            System.Console.WriteLine(score);
            return;
        }
    }
}