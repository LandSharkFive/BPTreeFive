using BPTreeFive;

namespace BTreeTest
{

    [TestClass]
    public class BPlusTreeTests
    {
        private const int ORDER = 3;

        [TestMethod]
        public void Test_InsertAndSearch_ValidKey()
        {
            var tree = new BTree(ORDER);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
        }

        [TestMethod]
        public void Test_InsertAndSearch_InvalidKey()
        {
            var tree = new BTree(ORDER);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

            Assert.IsFalse(tree.Contains(4));
        }

        [TestMethod]
        public void Test_Delete_ValidKey()
        {
            var tree = new BTree(ORDER);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

            tree.Remove(2);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsFalse(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
        }

        [TestMethod]
        public void Test_Delete_InvalidKey()
        {
            var tree = new BTree(ORDER);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

            tree.Remove(4);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(4));
        }

        [TestMethod]
        public void Test_Delete_Six()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 3, 7, 6, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++) 
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            tree.Remove(6);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
        }

        [TestMethod]
        public void Test_Delete_Thirteen()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 3, 7, 6, 13, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++)
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            tree.Remove(6);
            tree.Remove(13);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
        }

        [TestMethod]
        public void Test_Delete_Seven()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 3, 7, 6, 13, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++)
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            int[] junk = { 6, 13, 7 };

            tree.RemoveRange(junk);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
        }

        [TestMethod]
        public void Test_Delete_Four()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 2, 3, 7, 6, 13, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++)
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            int[] junk = { 6, 13, 7, 4 };

            tree.RemoveRange(junk);

            Assert.IsTrue(tree.Contains(1));
            tree.Traverse();
            tree.Print();
            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
        }

        [TestMethod]
        public void Test_Delete_Two()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 3, 7, 6, 13, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++)
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            int[] junk = { 6, 13, 7, 4, 2, 2 };

            tree.RemoveRange(junk);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(2));
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
        }

        [TestMethod]
        public void Test_Delete_Sixteen()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            int[] check = { 1, 2, 3, 4, 7, 6, 13, 16, 26, 20, 22, 2 };

            for (int i = 0; i < check.Length; i++)
            {
                Assert.IsTrue(tree.Contains(check[i]), "key " + check[i]);
            }

            int[] junk = { 6, 13, 7, 4, 2, 16 };

            tree.RemoveRange(junk);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(2));
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsFalse(tree.Contains(16));
        }

        [TestMethod]
        public void Test_Count_One()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);
            Assert.IsTrue(tree.Contains(4));
            Assert.IsTrue(tree.Contains(5));
            Assert.IsTrue(tree.Contains(7));
        }

        [TestMethod]
        public void Test_Count_Two()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            tree.Remove(6);

            Assert.AreEqual(tree.KeyCount(), 23);
            Assert.IsFalse(tree.Contains(6));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsTrue(tree.Contains(10));

        }

        [TestMethod]
        public void Test_Count_Three()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            tree.Remove(6);
            tree.Remove(13);

            Assert.AreEqual(tree.KeyCount(), 22);
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsTrue(tree.Contains(10));
        }

        [TestMethod]
        public void Test_Count_Four()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            tree.Remove(6);
            tree.Remove(13);
            tree.Remove(7);

            Assert.AreEqual(tree.KeyCount(), 21);
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsTrue(tree.Contains(10));
            Assert.IsTrue(tree.Contains(16));
            Assert.IsTrue(tree.Contains(19));

        }

        [TestMethod]
        public void Test_Count_Five()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            int[] junk = { 6, 13, 7, 4 };

            tree.RemoveRange(junk);

            Assert.AreEqual(tree.KeyCount(), 20);
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsFalse(tree.Contains(42));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(15));
            Assert.IsTrue(tree.Contains(16));
            Assert.IsTrue(tree.Contains(26));
        }

        [TestMethod]
        public void Test_Count_Six()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            int[] junk = { 6, 13, 7, 4, 2, 2 };

            tree.RemoveRange(junk);

            Assert.AreEqual(tree.KeyCount(), 18);
            Assert.IsFalse(tree.Contains(2));
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(15));
            Assert.IsTrue(tree.Contains(19));
        }

        [TestMethod]
        public void Test_Count_Seven()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            int[] junk = { 6, 13, 7, 4, 2 };

            tree.RemoveRange(junk);

            Assert.AreEqual(tree.KeyCount(), 19);
        }

        [TestMethod]
        public void Test_Count_Eight()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            int[] junk = { 6, 13, 7, 4, 2, 2, 16 };

            tree.RemoveRange(junk);

            Assert.AreEqual(tree.KeyCount(), 17);
            Assert.IsFalse(tree.Contains(2));
            Assert.IsFalse(tree.Contains(4));
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(7));
            Assert.IsFalse(tree.Contains(13));
            Assert.IsFalse(tree.Contains(16));
            Assert.IsTrue(tree.Contains(14));
            Assert.IsTrue(tree.Contains(15));
            Assert.IsTrue(tree.Contains(18));
        }

        [TestMethod]
        public void Test_Sort_One()
        {
            var tree = new BTree(ORDER);

            int[] array = { 1, 2, 3, 7, 10, 11, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 24);

            int[] a = tree.GetKeys().ToArray();

            Assert.AreEqual(24, a.Length);   
            Assert.IsTrue(Util.IsSorted(a));
        }

        [TestMethod]    
        public void Test_Sort_Two()
        {
            var tree = new BTree(ORDER);

            int[] array = { 9, 89, 58, 44, 17, 6, 68, 75, 84, 65, 3, 43, 99, 59, 73, 57, 85, 1, 34, 32 };

            tree.AddRange(array);

            Assert.AreEqual(tree.KeyCount(), 20);

            int[] a = tree.GetKeys().ToArray();

            Assert.AreEqual(a.Length, 20);
            Assert.IsTrue(Util.IsSorted(a));
        }
    }
}
