using System;
using System.Collections.Generic;


namespace day07 {
    class Program {
    
        const int MAX_FILE_SIZE = 100000;
        const int TOTAL_DISK_SIZE = 70000000;
        const int NEEDED_DISK_SIZE = 30000000;

    
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

            int solution = 0;

            string currentDir = "/";
            TreeNode root = new TreeNode(currentDir, null);
            TreeNode node = root;

            foreach(string line in input){
                
                if(line[0] == '$'){
                    string[] command = line.Split(' ');
                    if(String.Equals(command[1], "cd")){
                        if(String.Equals(command[2], "..")){
                            node = node.parent;
                            currentDir = string.Copy(node.name);
                        }else{
                            foreach(TreeNode child in node.children){
                                if(String.Equals(child.name, command[2])){
                                    node = child;
                                    currentDir = string.Copy(node.name);
                                }
                            }
                        }
                    }
                    else if(String.Equals(command[1], "ls")){
                        continue;
                    }
                }
                else{
                    string[] file = line.Split(' ');
                    if(String.Equals(file[0], "dir")){
                        TreeNode newNode = new TreeNode(file[1], node);
                        node.children.AddLast(newNode);
                    }else{
                        node.size += Convert.ToInt32(file[0]);
                    }
                }
            }

            solution = correctSizes(root);
            solution = sumFolders(root, 0);
            return solution;
        }

        static int partTwo(string[] input){

            int solution = 0;

            string currentDir = "/";
            TreeNode root = new TreeNode(currentDir, null);
            TreeNode node = root;

            foreach(string line in input){
                
                if(line[0] == '$'){
                    string[] command = line.Split(' ');
                    if(String.Equals(command[1], "cd")){
                        if(String.Equals(command[2], "..")){
                            node = node.parent;
                            currentDir = string.Copy(node.name);
                        }else{
                            foreach(TreeNode child in node.children){
                                if(String.Equals(child.name, command[2])){
                                    node = child;
                                    currentDir = string.Copy(node.name);
                                }
                            }
                        }
                    }
                    else if(String.Equals(command[1], "ls")){
                        continue;
                    }
                }
                else{
                    string[] file = line.Split(' ');
                    if(String.Equals(file[0], "dir")){
                        TreeNode newNode = new TreeNode(file[1], node);
                        node.children.AddLast(newNode);
                    }else{
                        node.size += Convert.ToInt32(file[0]);
                    }
                }
            }

            int diskSize = correctSizes(root);
            int diskFreeSpace = TOTAL_DISK_SIZE - diskSize;
            int diskToFree = NEEDED_DISK_SIZE - diskFreeSpace;
            solution = findDir(root, diskToFree, diskSize);
            return solution;
        }
    
    
        static int correctSizes(TreeNode node){
            
            int subfolderSize = 0;
            foreach(TreeNode child in node.children){
                subfolderSize += correctSizes(child);
            }
            node.size += subfolderSize;
            return node.size;
        }

        static int sumFolders(TreeNode node, int size){
            
            foreach(TreeNode child in node.children){
                size = sumFolders(child, size);
            }

            if(node.size < MAX_FILE_SIZE){
                size += node.size;
            }
            return size;
        }

        static int findDir(TreeNode node, int diskToFree, int min){
            
            foreach(TreeNode child in node.children){
                min = findDir(child, diskToFree, min);
            }

            if(node.size > diskToFree && node.size < min){
                return node.size;
            }
            return min;
        }
    }
    public class TreeNode{

        public string name = String.Empty; 
        public int size = 0;
        public TreeNode parent = null;
        public LinkedList<TreeNode> children = new LinkedList<TreeNode>();

        public TreeNode(string nodeName, TreeNode nodeParent){
            name = nodeName;
            parent = nodeParent;
        }
    }
}