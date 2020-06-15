using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] seq = new string[4, 3];
            int row = 0;
            seq[row, 0] = "University";
            seq[row, 1] = "Kwangwoon";
            seq[row, 2] = "Incorrect";
            row++;
            seq[row, 0] = "Department";
            seq[row, 1] = "Software";
            seq[row, 2] = "Incorrect";
            row++;
            int numrows = 0;
            Start(seq, numrows);
            Console.Write("Continue : Press 'y'");
            string next = Console.ReadLine();
            if (next.CompareTo("y") == 0)
            {
                Console.Clear();
                numrows += 2;
                Start(seq, numrows);
            }
            Console.ReadLine();
        }

        static void Start(string[,] seq, int rows)
        {
            Console.WriteLine("Simple game start!");
            for (int i = rows; i < rows + 2; i++)
            {
                Console.Write("앞말에 들어가는건?'{0}':", seq[i, 0]);
                string ans = Console.ReadLine();
                if (seq[i, 1].ToLower().CompareTo(ans.ToString().ToLower()) == 0)
                    seq[i, 2] = "맞습니다";
                Console.WriteLine();
            }
            Console.WriteLine("Check your answer!!!");
            Console.WriteLine("단어\t\t\tAnswer\t\t\tResult");
            for (int i = rows; i < rows + 2; i++)
            {
                for (int j = 0; j < 3; j++)
                    Console.Write("{0}\t\t", seq[i, j]);
                Console.WriteLine();
            }
            Console.Read();
        }


    }
}
