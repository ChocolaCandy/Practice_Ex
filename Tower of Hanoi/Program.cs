using System;
using System.Security.Cryptography;

namespace Hanoi
{
    class Hanoi
    {
        public List<List<int>> Hanoi_Board;
        bool Is_init = false;
        int Maxsize = 0;
        public Hanoi(int size)
        {
            CreateBoard();
            Init(size, 0);
        }

        private void CreateBoard()
        {
            Hanoi_Board = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                Hanoi_Board.Add(new List<int>());
            }
            Console.WriteLine("Create Hanoi_Board.");
        }

        public void Init(int size, int start)
        {
            for(int i = 0; i < 3; i++)
            {
                Hanoi_Board[i].Clear();
            }
            for (int i = size; i > 0; i--)
            {
                Hanoi_Board[start].Add(i);
            }
            Maxsize = size;
            Is_init = true;
            Console.WriteLine("Init Hanoi_Board.");
        }
        private void Moving(int size, int from, int to, ref int moveCount)
        {
            Hanoi_Board[from].Remove(size);
            Hanoi_Board[to].Add(size);
            moveCount++;
            Console.WriteLine($"{moveCount} Moving.");
            PrintResult();
        }
        private void Move(int size, int from, int tmp ,int to, ref int moveCount)
        {
            if (size == 1)
            {
                Moving(size, from, to, ref moveCount);
            }
            else
            {
                Move(size - 1, from, to, tmp, ref moveCount);
                Moving(size, from, to, ref moveCount);
                Move(size - 1, tmp, from, to, ref moveCount);
            }
        }

        private int Select_tmp(int from, int to)
        {
            List<int> list = new List<int>() { 0, 1, 2 };
            list.Remove(from);
            list.Remove(to);
            return list[0];
        }

        private void PrintResult()
        {
            for (int i = 0; i < 3; i++) {
                Console.Write($"[{i}]");
                foreach (int item in Hanoi_Board[i])
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void hanoi(int from, int to)
        {
            if (from == to)
            {
                Console.WriteLine("Start and End is same");
                return;
            }
            if(!Is_init || from != 0) Init(Maxsize, from);
            int tmp = Select_tmp(from, to);
            int moveCount = 0;

            Console.WriteLine("Before Move");
            PrintResult();
            Move(Maxsize, from, tmp, to, ref moveCount);
            Console.WriteLine("After Move");
            PrintResult();
        }

    }
    internal class Hanoi_Ex
    {
        static void Main(string[] args)
        {
            Hanoi hanoi = new Hanoi(16);
            hanoi.hanoi(0, 2); //Move 65535
        }
    }
}
