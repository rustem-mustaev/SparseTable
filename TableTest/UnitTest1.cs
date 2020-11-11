using NUnit.Framework;
using SparseTable;
using System;

namespace TableTest
{
    public class Tests
    {
        private Table table;
        private int width = 5;
        private int height = 5;

        [SetUp]
        public void Setup()
        {
            
            float[] val1 = { 2, 3 };
            int[] cN1 = { 1, 3 };

            float[] val2 = { 4 };
            int[] cN2 = { 2 };

            float[] val3 = { 7, 1 };
            int[] cN3 = { 3, 4 };

            Row[] rows = { new Row(val1, cN1, width), new Row(val2, cN2, width), new Row(val3, cN3, width) };
            int[] rN = { 0, 2, 4 };

            table = new Table(new Dictionary(rows, rN, height, width));
        }

        [Test]
        public void GetTest()
        {
            Assert.IsTrue(table.Get(0, 1) == 2);
            Assert.IsTrue(table.Get(0, 3) == 3);
            Assert.IsTrue(table.Get(2, 2) == 4);
            Assert.IsTrue(table.Get(4, 3) == 7);
            Assert.IsTrue(table.Get(4, 4) == 1);
            Assert.IsTrue(table.Get(0, 0) == 0);
            Assert.IsTrue(table.Get(1, 0) == 0);
            Assert.Throws<ArgumentException>(() => table.Get(5, 0));
            Assert.Throws<ArgumentException>(() => table.Get(0, 5));
            Assert.Throws<ArgumentException>(() => table.Get(5, 5));
        }

        [Test]
        public void SetExceptionsTest()
        {
            Assert.Throws<ArgumentException>(() => table.Set(1, 5, 0));
            Assert.Throws<ArgumentException>(() => table.Set(1, 0, 5));
            Assert.Throws<ArgumentException>(() => table.Set(1, 5, 5));
        }

        [Test]
        public void SetToEmptyCellOnExistRow()
        {
            Assert.IsTrue(table.Get(0, 0) == 0);
            table.Set(2, 0, 0);
            Assert.IsTrue(table.Get(0, 0)==2);
        }

        [Test]
        public void SetToExistCellOnExistRow()
        {
            Assert.IsTrue(table.Get(4, 3) == 7);
            table.Set(9, 4, 3);
            Assert.IsTrue(table.Get(4, 3) == 9);
        }

        [Test]
        public void SetOnEmptyRow()
        {
            Assert.IsTrue(table.Get(3, 2) == 0);
            table.Set(1, 3, 2);
            Assert.IsTrue(table.Get(3, 2) == 1);
        }

        [Test]
        public void MergeTest()
        {
            Assert.IsTrue(table.Get(0, 2) == 0);

            float[] val1 = { 8, 4 };
            int[] cN1 = { 2, 3 };

            float[] val2 = { 0.5f };
            int[] cN2 = { 0 };

            Row[] rows = { new Row(val1, cN1, width), new Row(val2, cN2, width) };
            int[] rN = { 0, 1 };

            Table tableToMerge = new Table(new Dictionary(rows, rN, height, width));

            table.MergeTable(tableToMerge);
            Assert.IsTrue(table.Get(0, 1) == 2);
            Assert.IsTrue(table.Get(0, 2) == 8);
            Assert.IsTrue(table.Get(0, 3) == 4);
            Assert.IsTrue(table.Get(1, 0) == 0.5f);
            Assert.IsTrue(table.Get(2, 2) == 4);
        }
    }




}