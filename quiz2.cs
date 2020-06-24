using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Sort_game
    {
        static void Main(string[] args)
        {
            Console.WriteLine("간단한 정렬게임 시작!");
            Console.WriteLine("원하는 정수 5개를 입력하세요 (정수 한 개 입력 후 엔터치고 입력)");
            
            int a1 = int.Parse(Console.ReadLine());
            int a2 = int.Parse(Console.ReadLine());
            int a3 = int.Parse(Console.ReadLine());
            int a4 = int.Parse(Console.ReadLine());
            int a5 = int.Parse(Console.ReadLine());

            int[] arr = new int[5] { a1, a2, a3, a4, a5 };
            int i;

            Console.WriteLine("입력하신 숫자를 순서대로 나열하면 :");
     
            for (i = 0; i < 5; i++)
            {
                Console.WriteLine(arr[i]);
            }
            
            sort(arr, 5);

            Console.WriteLine("XSort의 결과 정렬된 배열은 아래와 같습니다:");
            
            for (i = 0; i < 5; i++)
                Console.WriteLine(arr[i]);

            Console.WriteLine("X에 들어갈 알맞은 말은?");
            Console.WriteLine("1번 Heap, 2번 Merge, 3번 Insertion, 4번 Bubble\n 정답을 작성하세요(정수 입력 후 엔터):");

            int ans = int.Parse(Console.ReadLine());

            if (ans == 3)
                Console.WriteLine("정답입니다! 포인트 10점을 획득하셨습니다!");
            else
                Console.WriteLine("정답이 아닙니다. 포인트 10점을 뺏기셨습니다");
        }
            

        static void sort(int[] data, int k)
        {
            int b, c;
            for (b = 1; b < k; b++)
            {
                int number = data[b];
                int start = 0;
                for (c = b - 1; c >= 0 && start != 1;)
                {

                    if (number < data[c])
                    {
                        data[c + 1] = data[c];
                        c--;
                        data[c + 1] = number;
                    } 
                    
                    //to check if the number is smaller than data[c]
                    
                    else start = 1;
                    //if not, continue
                }
            }
        }
    }
}