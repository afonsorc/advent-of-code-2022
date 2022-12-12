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

            int solution = 0;
            int len = input[0].Length;
            char[] grid = new char[len * input.Length];
            int start = 0;
            int end = 0;

            for(int i = 0; i < input.Length; i++){
                for(int j = 0; j < input[i].Length; j++){
                    grid[i * len + j] = input[i][j];
                    if(grid[i*len+j] == 'S'){
                        start = i*len+j;
                        grid[start] = 'a';
                    }else if(grid[i*len+j] == 'E'){
                        end = i*len+j;
                        grid[end] = 'z';
                    }
                }
            }

            

            // print line
            for(int i = 0; i < input.Length; i++){
                for(int j = 0; j < input[i].Length; j++){
                    System.Console.Write(grid[i * len + j]);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();



            int path = 0;
            int[] visited = new int[len * input.Length];
            for(int i = 0; i < visited.Length; i++){
                visited[i]=-1;
            }


            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = start;

            while (queue.Count > 0){
                
                int element = queue.Dequeue();
                //System.Console.WriteLine(element);
                if(element == end)
                    break;

            
                
                List<int> neighbours = new List<int>();


                if(element % len != 0){
                    int climb = grid[element - 1] - grid[element];
                    if(climb < 2) neighbours.Add(element - 1);
                }
                if(element % len != len - 1){
                    int climb = grid[element + 1] - grid[element];
                    if(climb < 2) neighbours.Add(element + 1);
                }
                if(element - len >= 0){
                    int climb = grid[element - len] - grid[element];
                    if(climb < 2) neighbours.Add(element - len);
                }
                if(element + len < len * input.Length){
                    int climb = grid[element + len] - grid[element];
                    if(climb < 2) neighbours.Add(element + len);
                }
                //foreach(int n in neighbours) System.Console.Write("{0}|", n);
                //System.Console.WriteLine();
                if (neighbours == null)
                    continue;
                foreach (int n in neighbours)
                {
                    if(visited[n] == -1){
                        queue.Enqueue(n);
                        visited[n] = element;
                    }
                }
                neighbours.Clear();
            }

            int cur = end;
            while(cur != start){
                System.Console.WriteLine(cur);
                cur = visited[cur];
                path++;
            }

            return path;
        }


        static int partTwo(string[] input){
            
            int solution = 0;
            int len = input[0].Length;
            char[] grid = new char[len * input.Length];
            int start = 0;
            int end = 0;

            for(int i = 0; i < input.Length; i++){
                for(int j = 0; j < input[i].Length; j++){
                    grid[i * len + j] = input[i][j];
                    if(grid[i*len+j] == 'S'){
                        start = i*len+j;
                        grid[start] = 'a';
                    }else if(grid[i*len+j] == 'E'){
                        end = i*len+j;
                        grid[end] = 'z';
                    }
                }
            }

            

            // print line
            for(int i = 0; i < input.Length; i++){
                for(int j = 0; j < input[i].Length; j++){
                    System.Console.Write(grid[i * len + j]);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();



            int path = 0;
            int[] visited = new int[len * input.Length];
            for(int i = 0; i < visited.Length; i++){
                visited[i]=-1;
            }


            Queue<int> queue = new Queue<int>();
            queue.Enqueue(end);
            visited[end] = end;

            while (queue.Count > 0){
                
                int element = queue.Dequeue();
                System.Console.WriteLine(element);
                System.Console.WriteLine(grid[element]);
                if(grid[element] == 'a'){
                    start = element;
                    break;
                }


                
                List<int> neighbours = new List<int>();

                if(element % len != 0){
                    int climb = grid[element - 1] - grid[element];
                    if(climb > -2) neighbours.Add(element - 1);
                }
                if(element % len != len - 1){
                    int climb = grid[element + 1] - grid[element];
                    if(climb > -2) neighbours.Add(element + 1);
                }
                if(element - len >= 0){
                    int climb = grid[element - len] - grid[element];
                    if(climb > -2) neighbours.Add(element - len);
                }
                if(element + len < len * input.Length){
                    int climb = grid[element + len] - grid[element];
                    if(climb > -2) neighbours.Add(element + len);
                }
                foreach(int n in neighbours) System.Console.Write("{0}|", n);
                System.Console.WriteLine();
                if (neighbours == null)
                    continue;
                foreach (int n in neighbours)
                {
                    if(visited[n] == -1){
                        queue.Enqueue(n);
                        visited[n] = element;
                    }
                }
                neighbours.Clear();
            }

            int cur = start;
            while(cur != end){
                //System.Console.WriteLine(cur);
                cur = visited[cur];
                path++;
            }

            return path;
        }
    }
}