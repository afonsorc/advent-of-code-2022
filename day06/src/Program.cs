using System;
using System.Collections.Generic;
using System.Linq;

namespace day06 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            int solutionOne = 0;
            int solutionTwo = 0;

            solutionOne = partOne(input); 
            System.Console.WriteLine("--------------\nPart1: {0}\n--------------", solutionOne);

            solutionTwo = partTwo(input);
            System.Console.WriteLine("--------------\nPart2: {0}\n--------------", solutionTwo);

            return;
        }


        static int partOne(string[] input){
                 
            const int packetSize = 4;
            Queue<char> packet = new Queue<char>();
            int[] currentPacket = new int[4];

            for(int i = 0; i < input[0].Length; i++){ 
                packet.Enqueue(input[0][i]);
                if(packet.Count == packetSize + 1){
                    packet.Dequeue();

                    int j = 0;
                    foreach(char c in packet){
                        currentPacket[j++] = c;
                    }
                    
                    if(new HashSet<int>(currentPacket).Count == currentPacket.Length){
                        return ++i;
                    }
                }
            }
            return -1;
        }


        static int partTwo(string[] input){
       
            const int packetSize = 14;
            Queue<char> packet = new Queue<char>();
            int[] currentPacket = new int[packetSize];

            for(int i = 0; i < input[0].Length; i++){ 
                packet.Enqueue(input[0][i]);
                if(packet.Count == packetSize + 1){
                    packet.Dequeue();

                    int j = 0;
                    foreach(char c in packet){
                        currentPacket[j++] = c;
                    }
                    
                    if(new HashSet<int>(currentPacket).Count == currentPacket.Length){
                        return ++i;
                    }
                }
            }
            return -1;
        }
    }
}