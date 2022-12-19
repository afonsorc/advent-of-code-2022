using System;
using System.Linq;

namespace day10{
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2:\n");
            char[] output = partTwo(input);
            for(int i = 0; i < 6; i++){
                for(int j = 0; j < 40; j++){
                    System.Console.Write(output[i*40+j]);
                }
                System.Console.WriteLine();
            }   
            return;
        }


        const int initialCycle = 20;
        const int betweenCycle = 40;


        static int partOne(string[] input){

            int cycle = 0;
            int X = 1;
            int[] signal = new int[6];

            foreach(string line in input){
                string[] cmd = line.Split(' ');
                if(String.Equals(cmd[0], "addx")){
                    cycle++;
                    
                    if(cycle % betweenCycle == initialCycle){
                        signal[cycle / betweenCycle] += X * cycle;
                    }

                    cycle++;


                    if(cycle % betweenCycle == initialCycle){
                        signal[cycle / betweenCycle] += X * cycle;
                    }

                    X += Convert.ToInt32(cmd[1]);

                }else if(String.Equals(cmd[0], "noop")){
                    cycle++;
                    if(cycle % betweenCycle == initialCycle){
                        signal[cycle / betweenCycle] += X * cycle;
                    }
                }
            }

            return signal.Sum();
        }


        static char[] partTwo(string[] input){
            
            char[] image = new char[40*6];

            int cycle = 0;
            int X = 1;
            int spritePos = X - 1;
            int spriteLen = 3;
            int imageWidth = 40;

            foreach(string line in input){
                string[] cmd = line.Split(' ');
                if(String.Equals(cmd[0], "addx")){

                    if((cycle % imageWidth) >= spritePos && (cycle % imageWidth) < spritePos + spriteLen) image[cycle] = '#';
                    else image[cycle] = '.';

                    cycle++;
                    if((cycle % imageWidth) >= spritePos && (cycle % imageWidth) < spritePos + spriteLen) image[cycle] = '#';
                    else image[cycle] = '.';

                    cycle++;
                    X += Convert.ToInt32(cmd[1]);
                    spritePos = X - 1;
                }
                else if(String.Equals(cmd[0], "noop")){
                    if((cycle % imageWidth) >= spritePos && (cycle % imageWidth) < spritePos + spriteLen) image[cycle] = '#';
                    else image[cycle] = '.';

                    cycle++;
                }
            }

            return image;
        }
    }
}