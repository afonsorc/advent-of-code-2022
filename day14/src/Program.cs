using System;
using System.Linq;


namespace day14{
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
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
            
            int sandBlocks = 0;

            string[] charsToDelete = new string[]{" -> ", ","};
            int solution = 0;

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, " ");
                }
            }

            Coords[] edges = new Coords[2];
            edges[0] = new Coords(1000, 0);
            
            foreach(var i in input){
                int[] rocks = i.Split(' ').Select(int.Parse).ToArray(); 
                for(int r = 0; r < rocks.Length; r+=2){
                    if(rocks[r] < edges[0].x){
                        edges[0].x = rocks[r];
                    }

                    if(rocks[r] > edges[1].x){
                        edges[1].x = rocks[r];
                    }
                    if(rocks[r + 1] > edges[1].y){
                        edges[1].y = rocks[r + 1];
                    }
                }
            }
            
            edges[0].x--;
            edges[1].x++;
            int height = edges[1].y - edges[0].y + 1;
            int width = edges[1].x - edges[0].x + 1;
            char[,] image = new char[height, width];

            for(int y = 0; y < height; y++){
                for(int x = 0; x < width; x++){
                    image[y, x] = '.';
                }
            }

            Coords sandOrigin = new Coords(500 - edges[0].x, 0 - edges[0].y);

            // draw rocks
            foreach(var i in input){
                int[] rocks = i.Split(' ').Select(int.Parse).ToArray(); 
                for(int r = 2; r < rocks.Length; r+=2){
                    if(rocks[r + 1] == rocks[r - 1]){
                        int start = rocks[r]- edges[0].x;
                        int end = rocks[r - 2] - edges[0].x;
                        if(start > end){
                            int temp = end;
                            end = start;
                            start = temp;
                        }

                        for(int j = start; j <= end; j++){
                            image[rocks[r + 1], j] = '#';
                        }
                    }else if(rocks[r] == rocks[r - 2]){
                        int start = rocks[r + 1];
                        int end = rocks[r - 1];
                        if(start > end){
                            int temp = end;
                            end = start;
                            start = temp;
                        }

                        for(int j = start; j <= end; j++){
                            image[j, rocks[r] - edges[0].x] = '#';
                        }
                    }
                }
            }

    	    // draw sand fall
            image[sandOrigin.y, sandOrigin.x] = '+';

            // main loop
            int restCounter = 0;
            Coords sand = new Coords(sandOrigin.x, sandOrigin.y);
            while(restCounter < height - 1){
               if(restCounter == 0){
                sand.x = sandOrigin.x;
                sand.y = sandOrigin.y;
               }
               
                // directly down then diagonals
                if(image[sand.y + 1, sand.x] == '.' && sand.x < width && sand.x >= 0){
                    image[sand.y, sand.x] = '.';
                    image[++sand.y, sand.x] = 'o';
                    restCounter++;
                }else if(image[sand.y + 1, sand.x - 1] == '.' && sand.x < width && sand.x >= 0){
                    image[sand.y, sand.x] = '.';
                    image[++sand.y, --sand.x] = 'o';
                    restCounter++;
                }else if(image[sand.y + 1, sand.x + 1] == '.' && sand.x < width && sand.x >= 0){
                    image[sand.y, sand.x] = '.';
                    image[++sand.y, ++sand.x] = 'o';
                    restCounter++;
                }else{ 
                    // cant move, is now resting  
                    sandBlocks++;
                    restCounter = 0;
                }
                image[sandOrigin.y, sandOrigin.x] = '+';
            }

            return sandBlocks;
        }


        static int partTwo(string[] input){
            
            int sandBlocks = 0;

            string[] charsToDelete = new string[]{" -> ", ","};
            int solution = 0;

            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, " ");
                }
            }

            Coords[] edges = new Coords[2];
            edges[0] = new Coords(1000, 0);
            
            foreach(var i in input){
                int[] rocks = i.Split(' ').Select(int.Parse).ToArray(); 
                for(int r = 0; r < rocks.Length; r+=2){
                    if(rocks[r] < edges[0].x){
                        edges[0].x = rocks[r];
                    }

                    if(rocks[r] > edges[1].x){
                        edges[1].x = rocks[r];
                    }
                    if(rocks[r + 1] > edges[1].y){
                        edges[1].y = rocks[r + 1];
                    }
                }
            }
            
            int height = edges[1].y - edges[0].y + 3;

            if(500 - edges[0].x < height + 1) edges[0].x -= height + 1;
            if(edges[1].x - 500 < height + 1) edges[1].x += height + 1;

            int width = edges[1].x - edges[0].x;
            System.Console.WriteLine(width);

            char[,] image = new char[height, width];

            for(int y = 0; y < height; y++){
                for(int x = 0; x < width; x++){
                    image[y, x] = '.';
                    if(y == height - 1){
                        image[y, x] = '#';
                    }
                }
            }

            Coords sandOrigin = new Coords(500 - edges[0].x, 0 - edges[0].y);

            // draw rocks
            foreach(var i in input){
                int[] rocks = i.Split(' ').Select(int.Parse).ToArray(); 
                for(int r = 2; r < rocks.Length; r+=2){
                    if(rocks[r + 1] == rocks[r - 1]){
                        int start = rocks[r]- edges[0].x;
                        int end = rocks[r - 2] - edges[0].x;
                        if(start > end){
                            int temp = end;
                            end = start;
                            start = temp;
                        }

                        for(int j = start; j <= end; j++){
                            image[rocks[r + 1], j] = '#';
                        }
                    }else if(rocks[r] == rocks[r - 2]){
                        int start = rocks[r + 1];
                        int end = rocks[r - 1];
                        if(start > end){
                            int temp = end;
                            end = start;
                            start = temp;
                        }

                        for(int j = start; j <= end; j++){
                            image[j, rocks[r] - edges[0].x] = '#';
                        }
                    }
                }
            }

            
    	    // draw sand fall
            image[sandOrigin.y, sandOrigin.x] = '+';
            

            // main loop
            int restCounter = 0;
            Coords sand = new Coords(sandOrigin.x, sandOrigin.y);
            while(image[sandOrigin.y, sandOrigin.x] != 'o'){
               if(restCounter == 0){
                sand.x = sandOrigin.x;
                sand.y = sandOrigin.y;
               }
               
                // directly down then diagonals
                if(image[sand.y + 1, sand.x] == '.' && sand.x < width && sand.x >= 0){
                    if(image[sand.y, sand.x] != '+') image[sand.y, sand.x] = '.';
                    image[++sand.y, sand.x] = 'o';
                    restCounter++;
                }else if(image[sand.y + 1, sand.x - 1] == '.' && sand.x < width && sand.x >= 0){
                    if(image[sand.y, sand.x] != '+')image[sand.y, sand.x] = '.';
                    image[++sand.y, --sand.x] = 'o';
                    restCounter++;
                }else if(image[sand.y + 1, sand.x + 1] == '.' && sand.x < width && sand.x >= 0){
                    if(image[sand.y, sand.x] != '+')image[sand.y, sand.x] = '.';
                    image[++sand.y, ++sand.x] = 'o';
                    restCounter++;
                }else{ 
                    // cant move, is now resting
                    if(sand.y == sandOrigin.y && sand.x == sandOrigin.x) image[sand.y, sand.x] = 'o'; 
                    sandBlocks++;
                    restCounter = 0;
                }
            }

            //print image
            for(int y = 0; y < height; y++){
                System.Console.Write("{0} ", y);
                for(int x = 0; x < width; x++){
                    System.Console.Write(image[y,x]);
                }
                System.Console.WriteLine();
            }

            return sandBlocks;
        }
    }
}