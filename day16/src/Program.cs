using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace day16{
    class Program {
        static void Main(string[] args){
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");
            var watch = new Stopwatch();
            watch.Start();
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return;
        }


        public struct Node{

            public int flow;
            public Dictionary<string, int> neighbours;

            public Node(int f){
                flow = f;
                neighbours = new Dictionary<string, int>();
            }
        }


        static int partOne(string[] input){
            
            int maxWeight = 1000;
            string startValve = "AA";
            HashSet<string> openValves = new HashSet<string>();
            string[] charsToDelete = new string[]{"Valve ", "has flow rate=", "; tunnels lead to ", "; tunnel leads to ", ",", "valves", "valve"};
            Dictionary<string, Node> valves = new Dictionary<string, Node>();

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, string.Empty);
                }
                string[] line = input[i].Split(' ');
                
                // add flow to node
                Node valve = new Node(int.Parse(line[1]));

                // add neightbours to node
                Dictionary<string, int> neighbours = new Dictionary<string, int>();
                for(int n = 2; n < line.Length; n++)
                   valve.neighbours.Add(line[n], 1);

                // add node to valves dict
                valves.Add(line[0], valve);
            }

            // remove 0 flow valves except for starting valve
            foreach(var v in valves){
                if(v.Value.flow == 0 && v.Key != startValve){
                    // if valve has 0 flow, iterate thourgh each of its neighbours to add all other neighbours
                    foreach(var n in v.Value.neighbours){ 
                        foreach(var o in v.Value.neighbours){
                            if(n.Key != o.Key && !valves[n.Key].neighbours.ContainsKey(o.Key)){
                                valves[n.Key].neighbours.Add(o.Key, o.Value + n.Value);
                                valves[n.Key].neighbours.Remove(v.Key);
                                valves[o.Key].neighbours.Add(n.Key, o.Value + n.Value);
                                valves[o.Key].neighbours.Remove(v.Key);
                                valves.Remove(v.Key);
                            }
                        }
                    }
                }
            }

            // add all others with infinite cost
            foreach(var v in valves){
                foreach(var w in valves){    
                    if(v.Key != w.Key && !valves[v.Key].neighbours.ContainsKey(w.Key)){
                        v.Value.neighbours.Add(w.Key, maxWeight);
                    }               
                }
            }

            // floyd warshall
            foreach(var v in valves){
                foreach(var n in v.Value.neighbours){
                    foreach(var o in v.Value.neighbours){
                        if(n.Key != o.Key && valves[n.Key].neighbours[o.Key] > n.Value + o.Value){
                            valves[n.Key].neighbours[o.Key] = n.Value + o.Value;
                        }
                    }
                }
            }

          // print graph
            /*foreach(var v in valves){
                System.Console.WriteLine(v.Key);
                System.Console.WriteLine(v.Value.flow);
                foreach(var n in v.Value.neighbours){
                    System.Console.WriteLine(n);
                }
                System.Console.WriteLine();
            }*/


            Dictionary<string, int> paths = new Dictionary<string, int>();
            int maxPressure = dfs(valves, startValve, openValves, 0, 0, "", paths, 30);
            System.Console.WriteLine(count);
            return maxPressure;
        }


        static int count = 0;











        





       
        static int partTwo(string[] input){
            
            count = 0;
            int maxWeight = 1000;
            string startValve = "AA";
            HashSet<string> openValves = new HashSet<string>();
            string[] charsToDelete = new string[]{"Valve ", "has flow rate=", "; tunnels lead to ", "; tunnel leads to ", ",", "valves", "valve"};
            Dictionary<string, Node> valves = new Dictionary<string, Node>();

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, string.Empty);
                }
                string[] line = input[i].Split(' ');
                
                // add flow to node
                Node valve = new Node(int.Parse(line[1]));

                // add neightbours to node
                Dictionary<string, int> neighbours = new Dictionary<string, int>();
                for(int n = 2; n < line.Length; n++)
                   valve.neighbours.Add(line[n], 1);

                // add node to valves dict
                valves.Add(line[0], valve);
            }

            // remove 0 flow valves except for starting valve
            foreach(var v in valves){
                if(v.Value.flow == 0 && v.Key != startValve){
                    // if valve has 0 flow, iterate thourgh each of its neighbours to add all other neighbours
                    foreach(var n in v.Value.neighbours){ 
                        foreach(var o in v.Value.neighbours){
                            if(n.Key != o.Key && !valves[n.Key].neighbours.ContainsKey(o.Key)){
                                valves[n.Key].neighbours.Add(o.Key, o.Value + n.Value);
                                valves[n.Key].neighbours.Remove(v.Key);
                                valves[o.Key].neighbours.Add(n.Key, o.Value + n.Value);
                                valves[o.Key].neighbours.Remove(v.Key);
                                valves.Remove(v.Key);
                            }
                        }
                    }
                }
            }

            // add all others with infinite cost
            foreach(var v in valves){
                foreach(var w in valves){    
                    if(v.Key != w.Key && !valves[v.Key].neighbours.ContainsKey(w.Key)){
                        v.Value.neighbours.Add(w.Key, maxWeight);
                    }               
                }
            }

            // floyd warshall
            foreach(var v in valves){
                foreach(var n in v.Value.neighbours){
                    foreach(var o in v.Value.neighbours){
                        if(n.Key != o.Key && valves[n.Key].neighbours[o.Key] > n.Value + o.Value){
                            valves[n.Key].neighbours[o.Key] = n.Value + o.Value;
                        }
                    }
                }
            }

          // print graph
            /*foreach(var v in valves){
                System.Console.WriteLine(v.Key);
                System.Console.WriteLine(v.Value.flow);
                foreach(var n in v.Value.neighbours){
                    System.Console.WriteLine(n);
                }
                System.Console.WriteLine();
            }*/


            Dictionary<string, int> paths = new Dictionary<string, int>();
            int pressure = dfs(valves, startValve, openValves, 0, 0, "", paths, 26);
            System.Console.WriteLine(count);

            List<string> pathsList = paths.Keys.ToList();


            // list of paths
            for(int i = 0; i < pathsList.Count; i++){
                if(pathsList[i].Length < 4){
                    pathsList.RemoveAt(i);
                    continue;
                }
                //System.Console.WriteLine(pathsList[i]);
            }



            int maximumPressure = 0;
            // elephant dfs for each human path
            openValves.Clear();
            System.Console.WriteLine(pathsList.Count);
            for(int i = 0; i < pathsList.Count; i++){
                if(i % 1000 == 0) System.Console.WriteLine(i);
                string[] humanOpenValves = pathsList[i].Split(" ");
                for(int j = 1; j < humanOpenValves.Length - 1; j++){
                    openValves.Add(humanOpenValves[j]);
                }
                //System.Console.WriteLine(pathsList[i]);
                //System.Console.WriteLine(paths[pathsList[i]]);
                int elliePressure = dfs(valves, startValve, openValves, 0, 0, "", paths, 26);
                //System.Console.WriteLine(elliePressure);
                //System.Console.WriteLine();
                openValves.Clear();
                if(maximumPressure < elliePressure + paths[pathsList[i]]) maximumPressure = elliePressure + paths[pathsList[i]];
            }

            /*
            count = 0;
            maxPressure = 0;
            for(int i = 0; i < pathsList.Count; i++){
                string[] pathHuman = pathsList[i].Split(" ");
                for(int j = i + 1; j < pathsList.Count; j++){
                    string[] pathElephant = pathsList[j].Split(" ");
                    var intersect = pathHuman.Intersect(pathElephant).ToList();
                    if(intersect.Count < 3){
                        //System.Console.WriteLine(pathsList[i]);
                        //System.Console.WriteLine(pathsList[j]);
                        //System.Console.WriteLine();
                        count++;
                        //if(maxPressure < paths[pathsList[i]] + paths[pathsList[j]]){
                        //    maxPressure = paths[pathsList[i]] + paths[pathsList[j]];
                        //}
                    }
                }
            }

            System.Console.WriteLine(pathsList.Count);
            System.Console.WriteLine(count);*/
            return maximumPressure;
        }


        static int dfs(Dictionary<string, Node> valves, string currentValve, HashSet<string> openValves, int pressure, int maxPressure, string currentPath, Dictionary<string, int> paths, int time){
            
            // count pressure
            pressure += valves[currentValve].flow * time;

            // visited
            openValves.Add(currentValve);
            currentPath += currentValve + " ";
            if(!paths.ContainsKey(currentPath)) paths.Add(currentPath, pressure);
            else if(pressure > paths[currentPath]) paths[currentPath] = pressure;
            //System.Console.Write(currentPath);
            //System.Console.Write(" : {0}\n", pressure);


            // if there is still time
            // search for neighbour closed valves
            // dfs from them
            bool isFinalValve = true;
            foreach(var n in valves[currentValve].neighbours){
                if(!openValves.Contains(n.Key) && time > n.Value){
                    isFinalValve = false;
                    break;
                }
            }

            // if no time
            // compare pressure with max pressure
            // update max pressure if better
            if(isFinalValve){
                if(maxPressure < pressure){
                    maxPressure = pressure;
                }
                openValves.Remove(currentValve);
                count++;
                return maxPressure;
            }

            // travel to closed valve
            // subtract the time
            foreach(var n in valves[currentValve].neighbours){
                if(!paths.ContainsKey(currentPath + currentValve + " ") && !openValves.Contains(n.Key)){
                    time -= n.Value + 1;
                    maxPressure = dfs(valves, n.Key, openValves, pressure, maxPressure, currentPath, paths, time);
                    time += n.Value + 1;
                }
            }

            openValves.Remove(currentValve);
            return maxPressure;
        }

    }
}