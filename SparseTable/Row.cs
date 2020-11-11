using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SparseTable
{
    public class Row
    {
        private float[] values;
        private int[] columnKeys;
        private static int colCount;
        public Row()
        {
        }
        public Row(float []val, int[]ck , int colCount)
        {
            Row.colCount = colCount;
            values = new float[val.Length];
            columnKeys = new int[ck.Length];
            values.CopyFrom(val);
            columnKeys.CopyFrom(ck);
        }

        public Row(Row row) // Copy Constructor
        {
            values = new float[row.values.Length];
            columnKeys = new int[row.columnKeys.Length];
            values.CopyFrom(row.values);
            columnKeys.CopyFrom(row.columnKeys);
        }

        public float Get(int key)
        {
            if(key < 0 || key >= colCount)
            {
                throw new ArgumentException();
            }
            if (EmptyRowCheck())
            {
                return 0f; 
            }
            int index = columnKeys.FindIndex(key); 
            if (index == -1 )
            {
                return 0f;
            }
            else
            {
                return values[index];
            }
        }

        public void Set(float value, int key)
        {
            if (key < 0 || key >= colCount)
            {
                throw new ArgumentException();
            }
            if (!EmptyRowCheck())
            {
                int index = columnKeys.FindIndex(key);
                if (index == -1)
                {
                    ArrayExtension.PushBack(ref columnKeys, key);
                    ArrayExtension.PushBack(ref values, value);
                }
                else
                {
                    values[index] = value;
                }
            }
            else
            {
                values = new float[1];
                columnKeys = new int[1];
                values[0] = value;
                columnKeys[0] = key;
            }
        }

        public void Merge(Row row)
        {
            for(int i=0; i < row.columnKeys.Length;i++)
            {
                Set(row.values[i], row.columnKeys[i]);
            }
        }

        private bool EmptyRowCheck()
        {
            if (values == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
