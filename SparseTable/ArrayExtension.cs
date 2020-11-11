using System;
using System.Collections.Generic;
using System.Text;

namespace SparseTable
{
    public static class ArrayExtension
    {
        public static int FindIndex(this int[] arr, int element)
        {
            int ind = -1;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == element)
                {
                    ind = i;
                    break;
                }
            }
            return ind;
        }

        public static void CopyFrom<T>(this T[] arr, T[] toCopyArr)
        {
            if (arr.Length < toCopyArr.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            for(int i = 0; i < toCopyArr.Length; i++)
            {
                arr[i] = toCopyArr[i];
            }
        }

        public static void PushBack<T>(ref T[]arr,T element)
        {
            T[] temp = arr;
            arr = new T[temp.Length + 1];
            arr.CopyFrom(temp);
            arr[temp.Length] = element;
        }
    }
}
