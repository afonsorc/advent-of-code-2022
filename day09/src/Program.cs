using System;
using System.Collections.Generic;

namespace day09 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static int partOne(string[] input){
            return ropeBridge(input, 2);
        }


        static int partTwo(string[] input){
            return ropeBridge(input, 10);
        }


        struct Coords{
            public int x;
            public int y;
        }


        static int ropeBridge(string[] input, int knots){
            
            Coords[] rope = new Coords[knots];
            
            HashSet<string> visited = new HashSet<string>();
            visited.Add(rope[knots - 1].x + ":" + rope[knots - 1].y);

            foreach(string i in input){
                string[] move = i.Split(' ');
                int step = Convert.ToInt32(move[1]);
                
                for(int s = 0; s < step; s++){

                    // move head
                    if(move[0] == "R") rope[0].x++;
                    else if(move[0] == "L") rope[0].x--;
                    else if(move[0] == "U") rope[0].y++;
                    else if(move[0] == "D") rope[0].y--;

                    // move other knots
                    for(int k = 1; k < knots; k++){

                        // get vector from current knot to previous knot
                        int dx = rope[k - 1].x - rope[k].x;
                        int dy = rope[k - 1].y - rope[k].y;

                        // distance bigger than sqrt(2) requires knot move
                        if(Math.Sqrt(dx * dx + dy * dy) > Math.Sqrt(2)){
                            rope[k].x += Math.Sign(dx);
                            rope[k].y += Math.Sign(dy);
                        }

                        // add new tail position
                        if(k == knots - 1) visited.Add(rope[k].x + ":" + rope[k].y);
                    }
                }
            }

            return visited.Count;
        }
    }
}