using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace SparseTable
{
    public class Dictionary
    {
        private Row[] rows;
        private int[] rowsNums;
        private int rowCount;
        private int colCount;

        public Dictionary(Row[] rows, int[] rN , int rowCount, int colCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.rows = new Row[rows.Length];
            rowsNums = new int[rN.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                this.rows[i] = new Row(rows[i]);
            }
            rowsNums.CopyFrom(rN);
        }

        public Dictionary(Dictionary dictionary) // Copy Constructor
        {
            rowCount = dictionary.rowCount;
            colCount = dictionary.colCount;
            rows = new Row[dictionary.rows.Length];
            rowsNums = new int[dictionary.rowsNums.Length];
            for(int i = 0; i < rows.Length; i++)
            {
                rows[i] = new Row(dictionary.rows[i]);
            }

            rowsNums.CopyFrom(dictionary.rowsNums);
        }

        public Row Get(int key)
        {
            if(key < 0 || key >= rowCount)
            {
                throw new ArgumentException();
            }
            int index = rowsNums.FindIndex(key);
            if (index == -1)
            {
                return new Row();
            }
            else
            {
                return rows[index];
            }
        }

        public Row Set(int key)
        {
            if(key < 0 || key >= rowCount)
            {
                throw new ArgumentException();
            }

            int index = rowsNums.FindIndex(key);
            if (index == -1)
            {
                ArrayExtension.PushBack(ref rowsNums, key);
                Row[] temp = rows;
                rows = new Row[temp.Length + 1];
                for(int i = 0; i < temp.Length; i++)
                {
                    rows[i] = new Row(temp[i]);
                }
                rows[temp.Length] = new Row();
                return rows[temp.Length];
            }
            else
            {
                return rows[index];
            }
        }

        public void MergeWith(Dictionary d)
        {
            if(d.rowCount != rowCount || d.colCount != colCount)
            {
                throw new ArgumentException();
            }
            for(int r = 0; r < d.rowsNums.Length; r++)
            {
                Set(d.rowsNums[r]).Merge(d.rows[r]);
            }
            
        }
    }
}
