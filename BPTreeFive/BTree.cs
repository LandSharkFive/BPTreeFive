using BPTreeFive;

public class BTree
{
    private BTreeNode root; // Pointer to root node
    private int t; // Minimum degree

    // Constructor (Initializes tree as empty)
    public BTree(int t)
    {
        root = null;
        this.t = t;
    }

    // function to search a key in this tree
    public BTreeNode Search(int k)
    {
        if (root == null)
            return null;

        return root.Search(k);
    }

    // The main function that inserts a new key in this B-Tree
    public void Insert(int k)
    {
        // If tree is empty
        if (root == null)
        {
            // Allocate memory for root
            root = new BTreeNode(t, true);
            root.keys[0] = k; // Insert key
            root.n = 1; // Update number of keys in root
        }
        else // If tree is not empty
        {
            // If root is full, then tree grows in height
            if (root.n == 2 * t - 1)
            {
                // Allocate memory for new root
                BTreeNode s = new BTreeNode(t, false);

                // Make old root as child of new root
                s.C[0] = root;

                // Split the old root and move 1 key to the new root
                s.SplitChild(0, root);

                // New root has two children now. Decide which of the
                // two children is going to have new key
                int i = 0;
                if (s.keys[0] < k)
                    i++;
                s.C[i].InsertNonFull(k);

                // Change root
                root = s;
            }
            else // If root is not full, call insertNonFull for root
                root.InsertNonFull(k);
        }
    }

    // The main function that removes a new key in this B-Tree
    public void Remove(int k)
    {
        if (root == null)
        {
            Console.WriteLine("The tree is empty");
            return;
        }

        // Call the remove function for root
        root.Remove(k);

        // If the root node has 0 keys, make its first child as the new root
        // if it has a child, otherwise set root as NULL
        if (root.n == 0)
        {
            if (root.leaf)
                root = null;
            else
                root = root.C[0];
        }
    }

    public bool Contains(int k)
    {
        var node = root.Search(k);
        if (node == null)
            return false;

        return node.Contains(k);
    }

    // Print all keys in tree.
    public void Traverse()
    {
        if (root == null)
            return;

        root.Traverse();
        Console.WriteLine();
    }

    // Print all nodes in tree.
    public void Print()
    {
        foreach (var node in GetEnumerator())
        {
            Console.WriteLine(node.ToString());
        }
    }

    // Return a list of keys.
    public List<int> GetKeys()
    {
        var list = new List<int>();
        foreach (var node in GetEnumerator())
        {
            if (node.leaf)
            {
                for (int i = 0; i < node.n; i++)
                {
                    list.Add(node.keys[i]);
                }
            }
        }

        return list;
    }


    // Get Key Count.
    public int KeyCount()
    {
        int count = 0;
        foreach (var node in GetEnumerator())
        {
            if (node.leaf)
            {
                count += node.n;
            }
        }

        return count;
    }

    public IEnumerable<BTreeNode> GetEnumerator()
    {
        return root.Descendants();
    }

    public void AddRange(int[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            Insert(keys[i]);
        }
    }

    public void RemoveRange(int[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            Remove(keys[i]);
        }
    }


}