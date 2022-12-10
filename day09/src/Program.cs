using System;
using System.Collections.Generic;

namespace day09 {
    class Program {

        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

            // write solution to part one
            int solutionOne = partOne(input); 
            System.Console.WriteLine("--------------\nPart1: {0}\n--------------", solutionOne);

            // write solution to part two
            int solutionTwo = partTwo(input);
            System.Console.WriteLine("--------------\nPart2: {0}\n--------------", solutionTwo);
            return;
        }


        public struct Coords{
            
            public int x;
            public int y;

            public Coords(int X, int Y){
                x = X;
                y = Y;
            }
        }

        static int partOne(string[] input){

            Coords head = new Coords(0, 0);
            Coords tail = new Coords(0, 0);

            // count starting tail position
            HashSet<string> visited = new HashSet<string>();        
            visited.Add(tail.x + ":" + tail.y);

            foreach(string i in input){
                
                string[] move = i.Split(' ');
                int step = Convert.ToInt32(move[1]);
                
                for(int s = 0; s < step; s++){

                    // move head
                    if(move[0] == "R") head.x++;
                    else if(move[0] == "L") head.x--;
                    else if(move[0] == "U") head.y++;
                    else if(move[0] == "D") head.y--;

                    // get distance from head to tail
                    int dx = head.x - tail.x;
                    int dy = head.y - tail.y;

                    // distance bigger than sqrt(2) requires tail move
                    if(Math.Sqrt(dx * dx + dy * dy) > 1.5){
                        tail.x += Math.Sign(dx);
                        tail.y += Math.Sign(dy);

                        // add new tail position
                        visited.Add(tail.x + ":" + tail.y);
                    }
                }
            }
            return visited.Count;
        }


        static int partTwo(string[] input){
            
            int knots = 10;
            Coords[] rope = new Coords[knots];

            // count starting tail position
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

                    for(int k = 1; k < knots; k++){
                        // get distance from head to tail
                        int dx = rope[k - 1].x - rope[k].x;
                        int dy = rope[k - 1].y - rope[k].y;

                        // distance bigger than sqrt(2) requires tail move
                        if(Math.Sqrt(dx * dx + dy * dy) > 1.5){
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