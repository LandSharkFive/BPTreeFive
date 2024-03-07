namespace BPTreeFive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestOne();

            TestTwo();

            TestThree();
        }

        static public void TestOne()
        {
            BTree t = new BTree(3); // A B-Tree with minimum degree 3

            int[] array = { 1, 2, 3, 7, 10, 11, 12, 13, 14 };

            t.AddRange(array);

            foreach (var key in array)
            {
                Console.WriteLine("{0} {1}", key, t.Contains(key));
            }

            t.Traverse();
            t.Print();
        }

        static public void TestTwo()
        {
            BTree t = new BTree(3); // A B-Tree with minimum degree 3

            int[] array = { 1, 3, 7, 10, 11, 12, 13, 14, 15, 18, 16, 19, 24, 25, 26, 21, 4, 5, 20, 22, 2, 17, 12, 6 };

            t.AddRange(array);

            Console.WriteLine("tree");
            t.Traverse();
            Console.WriteLine();

            t.Remove(6);
            Console.WriteLine("tree after removing 6");
            t.Traverse();
            Console.WriteLine();

            t.Remove(13);
            Console.WriteLine("tree after removing 13");
            t.Traverse();
            Console.WriteLine();

            t.Remove(7);
            Console.WriteLine("tree after removing 7");
            t.Traverse();
            Console.WriteLine();

            t.Remove(4);
            Console.WriteLine("tree after removing 4");
            t.Traverse();
            Console.WriteLine();

            t.Remove(2);
            Console.WriteLine("tree after removing 2");
            t.Traverse();
            Console.WriteLine();

            t.Remove(16);
            Console.WriteLine("tree after removing 16");
            t.Traverse();
            Console.WriteLine();
        }


        static public void TestThree()
        {
            // Fix Remove 7.
            BTree t = new BTree(3); // A B-Tree with minimum degree 3

            int[] array = { 1, 3, 7, 10, 26, 21, 4, 5, 2 }; 

            t.AddRange(array);

            Console.WriteLine("tree");
            t.Traverse();
            t.Print();
            Console.WriteLine();

            t.Remove(7);
            Console.WriteLine("tree after removing 7");
            t.Traverse();
            t.Print();
            Console.WriteLine();

        }

    }
}