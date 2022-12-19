using System;
using System.Linq;
using System.Collections.Generic;

namespace day18{
    class Program {
        static void Main(string[] args){
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        public struct Cube{         
            public int X;
            public int Y;
            public int Z;

            public Cube(int x, int y, int z){
                X = x;
                Y = y;
                Z = z;
            }
        }


        static int partOne(string[] input){
            
            int area = 0;
            HashSet<Cube> cubes = new HashSet<Cube>();

            foreach(var i in input){
                int[] cube = i.Split(',').Select(int.Parse).ToArray(); 
                cubes.Add(new Cube(cube[0], cube[1], cube[2]));
            }

            foreach(Cube cube in cubes){
                Cube neighbour = new Cube(cube.X + 1, cube.Y, cube.Z);
                if(!cubes.Contains(neighbour)) area++;
                neighbour = new Cube(cube.X - 1, cube.Y, cube.Z);
                if(!cubes.Contains(neighbour)) area++;
                neighbour = new Cube(cube.X, cube.Y + 1, cube.Z);
                if(!cubes.Contains(neighbour)) area++;
                neighbour = new Cube(cube.X, cube.Y - 1, cube.Z);
                if(!cubes.Contains(neighbour)) area++;
                neighbour = new Cube(cube.X, cube.Y, cube.Z + 1);
                if(!cubes.Contains(neighbour)) area++;
                neighbour = new Cube(cube.X, cube.Y, cube.Z - 1);
                if(!cubes.Contains(neighbour)) area++;
            }

            return area;
        }


        static int partTwo(string[] input){
                                    
            int area = 0;
            int max = 0;
            
            // get max size of grid
            foreach(var i in input){
                int[] cube = i.Split(',').Select(int.Parse).ToArray(); 
                if(cube.Max() > max){
                    max = cube.Max();
                }
            }

            // increase length to avoid working on the borders
            int [, ,] cubes = new int[max + 3, max + 3, max + 3];
            
            // set input cubes
            foreach(var i in input){
                int[] cube = i.Split(',').Select(int.Parse).ToArray(); 
                cubes[cube[0] + 1, cube[1] + 1, cube[2] + 1] = 1;
            }

            // bfs
            Queue<Cube> queue = new Queue<Cube>();
            HashSet<Cube> visited = new HashSet<Cube>();
            queue.Enqueue(new Cube(0, 0, 0));
            visited.Add(new Cube(0, 0, 0));

            while(queue.Count > 0){
                Cube cur = queue.Dequeue();
                List<Cube> neighbours = new List<Cube>();
                if(cur.X < cubes.GetLength(0) - 1) neighbours.Add(new Cube(cur.X + 1, cur.Y, cur.Z));
                if(cur.X > 0) neighbours.Add(new Cube(cur.X - 1, cur.Y, cur.Z));
                if(cur.Y < cubes.GetLength(1) - 1) neighbours.Add(new Cube(cur.X, cur.Y + 1, cur.Z));
                if(cur.Y > 0) neighbours.Add(new Cube(cur.X, cur.Y - 1, cur.Z));
                if(cur.Z < cubes.GetLength(2) - 1) neighbours.Add(new Cube(cur.X, cur.Y, cur.Z + 1));
                if(cur.Z > 0) neighbours.Add(new Cube(cur.X, cur.Y, cur.Z - 1));

                foreach(Cube n in neighbours){
                    if(cubes[n.X, n.Y, n.Z] == 1) area++;
                    if(!visited.Contains(n) && cubes[n.X, n.Y, n.Z] == 0){
                        queue.Enqueue(n);
                        visited.Add(n);
                    }
                }
            }

            return area;
        }
    }
}