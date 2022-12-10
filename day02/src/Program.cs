using System;


namespace day02{
    class Program{
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }

        const int myOffset = 88;
        const int opOffset = 65;


        static int partOne(string[] input){
            
            const int lose = 0;
            const int draw = 3;
            const int win = 6;
            int score = 0;

            // first index is my play, second is opponent's play: 0 rock, 1 paper, 2 scissors
            int[,] resultScore = {{draw, lose, win}, {win, draw, lose}, {lose, win, draw}};

            // add scores from the result and the shape
            foreach(string line in input){
                int myPlay = line[2] - myOffset;
                int opPlay = line[0] - opOffset;

                score += resultScore[myPlay, opPlay] + myPlay + 1;
            }
          
            return score;
        }


        static int partTwo(string[] input){

            const int rock = 1;
            const int paper = 2;
            const int scissors = 3;
            int score = 0;

            // first index is the result: 0 lose, 1 draw, 2 win, second is opponent's play: 0 rock, 1 paper, 2 scissors
            int[,] shapeScore = {{scissors, rock, paper}, {rock, paper, scissors}, {paper, scissors, rock}};

            // add scores from the result and the shape
            foreach(string line in input){
                int result = line[2] - myOffset;
                int opPlay = line[0] - opOffset;
                
                score += shapeScore[result, opPlay] + result * 3;
            }
          
            return score;
        }
    }
}