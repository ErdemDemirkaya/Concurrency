using System;
using System.Threading;
//using SolutionMergeSort;

namespace MergeSort
{
    class MergeSort
    {

        static void Main(string[] args)
        {
            int[] arr = { 1, 5, 4, 11, 20, 8, 2, 98, 90, 16, 3 , 100, 83, 24, 18, 33, 44, 76 };

            
            SequentialMergeSort mergeSort = new SequentialMergeSort(arr);

            mergeSort.printContent();
            mergeSort.sortSeq(0, arr.Length - 1);
            mergeSort.printContent();

            //Console.WriteLine("\n Now concurrent sort will be running ...");
            //SolutionConcurrentMergeSort concMergeSort = new SolutionConcurrentMergeSort();
            //concMergeSort.sortCon(arr);

        }
    }
}
