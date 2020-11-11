using System;
using System.Collections.Generic;
using System.Text;

namespace SparseTable
{
    public class Table
    {
        private Dictionary dict;

        public Table(Dictionary d)
        {
            dict = new Dictionary(d);
        }

        public Table(Table table) // Copy Constructor
        {
            dict = new Dictionary(table.dict);
        }

        public float Get(int x, int y)
        {
            return dict.Get(x).Get(y);
        }

        public void Set(float value, int x, int y)
        {
            dict.Set(x).Set(value, y);
        }

        public void MergeTable(Table table)
        {
            dict.MergeWith(table.dict);
        }
    }
}
