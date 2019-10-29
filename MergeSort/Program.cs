﻿using System;
using System.Threading;

namespace MergeSort
{
    public class MergeSort
    {
        int[] input;

        public MergeSort(int[] data)
        {
            input = new int[data.Length];
            Array.Copy(data, input, data.Length);
        }

        public void printContent()
        {
            Console.WriteLine("Content of the array is:");
            for (int i = 0; i < input.Length; i++)
                Console.Write("data[{0}]={1} ;",i,input[i]);
        }
 
        public void sortSeq(int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                sortSeq(left, middle);
                sortSeq(middle + 1, right);

                merge(left, middle, right);
            }
        }

        public void merge(int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

    }
    class ExampleMergeSort
    {
        // This method is a simple implementation for exercise of week 2: Threads.
        // In the class this method should be removed and students are expected to implement this solution themselves.
        static void sortCon(int[] d)
        {
            MergeSort mergeSort = new MergeSort(d);

            int midPos = d.Length / 2;

            Thread leftSort = new Thread(()=>mergeSort.sortSeq(0,midPos));
            Thread rightSort = new Thread(() => mergeSort.sortSeq(midPos+1, d.Length-1));

            leftSort.Start();
            rightSort.Start();

            leftSort.Join();
            rightSort.Join();

            mergeSort.merge(0,midPos,d.Length-1);
            mergeSort.printContent();
        }

        static void Main(string[] args)
        {
            int[] arr = new int[10] { 1, 5, 4, 11, 20, 8, 2, 98, 90, 16 };

            MergeSort mergeSort = new MergeSort(arr);

            mergeSort.printContent();
            mergeSort.sortSeq(0, arr.Length - 1);
            mergeSort.printContent();

            Console.WriteLine("Now concurrent sort will be running ...");
            ExampleMergeSort.sortCon(arr);

        }
    }
}