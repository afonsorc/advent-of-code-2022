using System;
using System.Collections.Generic;


namespace day12{
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
            return hillClimbingAlgorithm(input, false);
        }


        static int partTwo(string[] input){
            return hillClimbingAlgorithm(input, true);
        }


        static int hillClimbingAlgorithm(string[] input, bool isStartingSpotFixed){
            int width = input[0].Length;
            char[] grid = new char[width * input.Length];
            int start = 0;
            int end = 0;

            // process input
            for(int i = 0; i < input.Length; i++){
                for(int j = 0; j < input[i].Length; j++){
                    grid[i * width + j] = input[i][j];
                    if(grid[i * width + j] == 'S'){
                        start = i * width + j;
                        grid[start] = 'a';
                    }else if(grid[i * width + j] == 'E'){
                        end = i * width + j;
                        grid[end] = 'z';
                    }
                }
            }

            // init visited array
            int[] visited = new int[width * input.Length];
            for(int i = 0; i < visited.Length; i++){
                visited[i]= -1;
            }

            // bfs
            int[] step = new int[4]{1, -1, -width, width};
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(end);
            visited[end] = end;
            while (queue.Count > 0){
                
                int spot = queue.Dequeue();
                if(spot == start && !isStartingSpotFixed){
                    break;
                }else if(grid[spot] == 'a' && isStartingSpotFixed){
                    start = spot;
                    break;
                }

                List<int> neighbours = new List<int>();

                if(spot % width != width - 1){
                    if(grid[spot] - grid[spot + step[0]] < 2) neighbours.Add(spot + step[0]);
                }
                if(spot % width != 0){
                    if(grid[spot] - grid[spot + step[1]] < 2) neighbours.Add(spot + step[1]);
                }
                if(spot >= width){
                    if(grid[spot] - grid[spot + step[2]] < 2) neighbours.Add(spot + step[2]);
                }
                if(spot < width * input.Length - width){
                    if(grid[spot] - grid[spot + step[3]] < 2) neighbours.Add(spot + step[3]);
                }

                foreach (int n in neighbours){
                    if(visited[n] == -1){
                        queue.Enqueue(n);
                        visited[n] = spot;
                    }
                }

                neighbours.Clear();
            }

            /// get shortest path
            int shortestPath = 0;
            int current = start;
            while(current != end){
                current = visited[current];
                shortestPath++;
            }

            return shortestPath;
        }
    }
}