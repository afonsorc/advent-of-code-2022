using System;
using System.Linq;

namespace day15{
    class Program {
        static void Main(string[] args){
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
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
            
            int index = 2000000;

            string[] charsToDelete = new string[]{"Sensor at x=", "y=", "closest beacon is at x=", ":", ","};

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, string.Empty);
                }
            }

            Coords[] sensors = new Coords[input.Length];
            Coords[] beacons = new Coords[input.Length];
            Coords[] edges = new Coords[2];
            edges[0] = new Coords(10000000, 10000000);
            int[] distanceToBeacon = new int[input.Length];

            for(int i = 0; i < input.Length; i++){
                int[] line = input[i].Split(' ').Select(int.Parse).ToArray();
                sensors[i] = new Coords(line[0], line[1]);
                beacons[i] = new Coords(line[2], line[3]);
                int distance = Math.Abs(line[0] - line[2]) + Math.Abs(line[1] - line[3]);
                distanceToBeacon[i] = distance;

                if(line[0] - distance < edges[0].x){
                    edges[0].x = line[0] - distance;
                }
                if(line[1] - distance < edges[0].y){
                    edges[0].y = line[1] - distance;
                }

                if(line[0] + distance > edges[1].x){
                    edges[1].x = line[0] + distance;
                }
                if(line[1] + distance > edges[1].y){
                    edges[1].y = line[1] + distance;
                }
            }

            int width = edges[1].x - edges[0].x;
            char[] image = new char[width];
            int noBeacon = 0;

            for(int x = 0; x < width; x++){
                image[x] = '.';
            }

            foreach(var s in sensors){
                if(s.y == index) image[s.x - edges[0].x] = 'S';
            }

            foreach(var b in beacons){
                if(b.y == index) image[b.x - edges[0].x] = 'B';
            }

            for(int s = 0; s < input.Length; s++){
                for(int x = sensors[s].x - distanceToBeacon[s]; x < sensors[s].x + distanceToBeacon[s]; x++){
                    if(Math.Abs(x - sensors[s].x) + Math.Abs(index - sensors[s].y) <= distanceToBeacon[s]){
                        if(image[x - edges[0].x] == '.'){
                            image[x - edges[0].x] = '#';
                            noBeacon++;
                        }
                    }
                }
            }

            return noBeacon;
        }


        static long partTwo(string[] input){
            
            string[] charsToDelete = new string[]{"Sensor at x=", "y=", "closest beacon is at x=", ":", ","};
            Coords[] sensors = new Coords[input.Length];
            Coords[] edges = new Coords[2];

            edges[0] = new Coords(10000000, 10000000);
            int[] distanceToBeacon = new int[input.Length];
            bool isBeaconFound = false;
            long frequency = 4000000;

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, string.Empty);
                }
            }

            for(int i = 0; i < input.Length; i++){
                int[] line = input[i].Split(' ').Select(int.Parse).ToArray();
                sensors[i] = new Coords(line[0], line[1]);
                distanceToBeacon[i] = Math.Abs(line[0] - line[2]) + Math.Abs(line[1] - line[3]);
            }

            for(int s = 0; s < input.Length; s++){
                for(int t = s + 1; t < input.Length; t++){
                    if(Math.Abs(sensors[s].x - sensors[t].x) + Math.Abs(sensors[s].y - sensors[t].y) == distanceToBeacon[s] + distanceToBeacon[t] + 2){

                        Coords start = new Coords(sensors[s].x + Math.Sign(sensors[t].x - sensors[s].x) * (distanceToBeacon[s] + 1), sensors[s].y);
                        Coords end = new Coords(sensors[s].x, sensors[s].y + Math.Sign(sensors[t].y - sensors[s].y) * (distanceToBeacon[s] + 1));
                        Coords beacon = new Coords(start.x, start.y);

                        for(beacon.x = start.x, beacon.y = start.y; beacon.x != end.x && beacon.y != end.y; beacon.x += Math.Sign(end.x - start.x), beacon.y += Math.Sign(end.y - start.y)){
                            isBeaconFound = true;
                            for(int u = 0; u < input.Length; u++){
                                // point is inside one of the sensors
                                if(Math.Abs(beacon.x - sensors[u].x) + Math.Abs(beacon.y - sensors[u].y) <= distanceToBeacon[u]){
                                    isBeaconFound = false;
                                }
                            }
                            if(isBeaconFound) return beacon.x * frequency + beacon.y;
                        }
                    }
                }
            }
            return -1;
        }
    }
}