using System.Text;
using System.Xml.Linq;

namespace BPTreeFive
{

    // A BTree node
    public class BTreeNode
    {
        public int[] keys; // An array of keys
        public int t;     // Minimum degree (defines the range for number of keys)
        public  BTreeNode[] C; // An array of child pointers
        public int n;     // Current number of keys
        public bool leaf; // Is true when node is leaf. Otherwise false

        public BTreeNode(int t, bool leaf) // Constructor
        {
            this.t = t;
            this.leaf = leaf;

            // Allocate memory for maximum number of possible keys
            // and child pointers
            keys = new int[2 * t - 1];
            C = new BTreeNode[2 * t];

            // Initialize the number of keys as 0
            n = 0;
        }

        // A function to search a key in subtree rooted with this node.
        // Recursive.
        public BTreeNode Search(int k)
        {
            if (leaf)
                return this;

            // Find the first key greater than or equal to k
            int i = 0;
            while (i < n && k > keys[i])
                i++;

            // Go to the appropriate child
            return C[i].Search(k);
        }


        // Is key in leaf node?
        public bool Contains(int key)
        {
            for (int i = 0; i < n; i++)
            {
                if (keys[i] == key)
                    return true;
            }

            return false;
        }

        // A function that returns the index of the first key that is greater
        // or equal to k
        public int FindKey(int k)
        {
            int idx = 0;
            while (idx < n && keys[idx] < k)
                idx++;
            return idx;
        }

        // A utility function to insert a new key in the subtree rooted with
        // this node. The assumption is, the node must be non-full when this
        // function is called
        public void InsertNonFull(int k)
        {
            // Initialize index as index of rightmost element
            int i = n - 1;

            // If this is a leaf node
            if (leaf)
            {
                // The following loop does two things
                // a) Finds the location of new key to be inserted
                // b) Moves all greater keys to one place ahead
                while (i >= 0 && keys[i] > k)
                {
                    keys[i + 1] = keys[i];
                    i--;
                }

                // Insert the new key at found location
                keys[i + 1] = k;
                n = n + 1;
            }
            else // If this node is not leaf
            {
                // Find the child which is going to have the new key
                while (i >= 0 && keys[i] > k)
                    i--;

                // See if the found child is full
                if (C[i + 1].n == 2 * t - 1)
                {
                    // If the child is full, then split it
                    SplitChild(i + 1, C[i + 1]);

                    // After split, the middle key of C[i] goes up and
                    // C[i] is splitted into two. See which of the two
                    // is going to have the new key
                    if (keys[i + 1] < k)
                        i++;
                }
                C[i + 1].InsertNonFull(k);
            }
        }

        // A utility function to split the child y of this node
        // Note that y must be full when this function is called
        public void SplitChild(int i, BTreeNode y)
        {
            // Create a new node which is going to store (t-1) keys
            // of y
            BTreeNode z = new BTreeNode(y.t, y.leaf);
            z.n = t - 1; 

            // Copy the last (t-1) keys of y to z
            for (int j = 0; j < t - 1; j++)
                z.keys[j] = y.keys[j + t];

            // Clear upper keys in y 
            for (int j = t; j < y.keys.Length; j++)
                y.keys[j] = 0;

            if (!y.leaf)
            {
                // Copy the last t children of y to z
                for (int j = 0; j < t; j++)
                    z.C[j] = y.C[j + t];

                // Clear upper children in y 
                for (int j = t; j < y.C.Length; j++)
                    y.C[j] = null;
            }

            // Reduce the number of keys in y
            y.n = t;  

            // Since this node is going to have a new child,
            // create space of new child
            for (int j = n; j >= i + 1; j--)
                C[j + 1] = C[j];

            // Link the new child to this node
            C[i + 1] = z;

            // A key of y will move to this node. Find location of
            // new key and move all greater keys one space ahead
            for (int j = n - 1; j >= i; j--)
                keys[j + 1] = keys[j];

            // Copy the middle key of y to this node
            keys[i] = y.keys[t - 1];

            // Increment count of keys in this node
            ++n;
        }

        // A function to remove the key k from the sub-tree rooted with this node
        public void Remove(int k)
        {
            int idx = FindKey(k);

            // The key to be removed is present in this node
            if (idx < n && keys[idx] == k)
            {
                // If the node is a leaf node - removeFromLeaf is called
                // Otherwise, removeFromNonLeaf function is called
                if (leaf)
                {
                    RemoveFromLeaf(idx);
                    return;
                }
                else
                {
                    RemoveFromNonLeaf(idx);
                }
            }
            else
            {
                // If this node is a leaf node, then the key is not present in tree
                if (leaf)
                {
                    Console.WriteLine("The key " + k + " does not exist in the tree");
                    return;
                }

                // The key to be removed is present in the sub-tree rooted with this node
                // The flag indicates whether the key is present in the sub-tree rooted
                // with the last child of this node
                bool flag = (idx == n);

                // If the child where the key is supposed to exist has less that t keys,
                // we fill that child
                if (C[idx].n < t)
                    Fill(idx);

                // If the last child has been merged, it must have merged with the previous
                // child and so we recurse on the (idx-1)th child. Else, we recurse on the
                // (idx)th child which now has at least t keys
                if (flag && idx > n)
                    C[idx - 1].Remove(k);
                else
                    C[idx].Remove(k);
            }
        }

        // A function to remove the idx-th key from this node - which is a leaf node
        private void RemoveFromLeaf(int idx)
        {
            // Move all the keys after the idx-th pos one place backward
            for (int i = idx + 1; i < n; i++)
                keys[i - 1] = keys[i];

            // Reduce the count of keys
            n--;

            Clean();
        }

        // A function to remove the idx-th key from this node - which is a non-leaf node
        private void RemoveFromNonLeaf(int idx)
        {
            int k = keys[idx];

            // If the child that precedes k (C[idx]) has at least t keys,
            // find the predecessor 'pred' of k in the subtree rooted at
            // C[idx]. Replace k by pred. Recursively delete pred
            // in C[idx]
            if (C[idx].n >= t)
            {
                int pred = GetPred(idx);
                keys[idx] = pred;
                if (!C[idx].leaf)
                {
                    C[idx].Remove(pred);
                }
                C[idx].Remove(k);
            }

            // If the child C[idx] has less that t keys, examine C[idx+1].
            // If C[idx+1] has at least t keys, find the successor 'succ' of k in
            // the subtree rooted at C[idx+1]
            // Replace k by succ
            // Recursively delete succ in C[idx+1]
            else if (C[idx + 1].n >= t)
            {
                int succ = GetSucc(idx);
                keys[idx] = succ;
                if (!C[idx + 1].leaf)
                {
                    C[idx + 1].Remove(succ);
                }
                C[idx].Remove(k);
            }

            // If both C[idx] and C[idx+1] has less that t keys,merge k and all of C[idx+1]
            // into C[idx]
            // Now C[idx] contains 2t-1 keys
            // Free C[idx+1] and recursively delete k from C[idx]
            else
            {
                Merge(idx);
                C[idx].Remove(k);
            }

        }

        // A function to get predecessor of keys[idx]
        private int GetPred(int idx)
        {
            // Keep moving to the right most node until we reach a leaf
            BTreeNode cur = C[idx];
            while (!cur.leaf)
                cur = cur.C[cur.n];

            // Return the last key of the leaf
            return cur.keys[cur.n - 1];
        }

        // A function to get the successor of the key- where the key
        // is present in the idx-th position in the node
        private int GetSucc(int idx)
        {
            // Keep moving the left most node starting from C[idx+1] until we reach a leaf
            BTreeNode cur = C[idx + 1];
            while (!cur.leaf)
                cur = cur.C[0];

            // Return the first key of the leaf
            return cur.keys[0];
        }

        // A function to fill child C[idx] which has less than t-1 keys
        private void Fill(int idx)
        {
            // If the previous child(C[idx-1]) has more than t-1 keys, borrow a key
            // from that child
            if (idx != 0 && C[idx - 1].n >= t)
                BorrowFromPrev(idx);

            // If the next child(C[idx+1]) has more than t-1 keys, borrow a key
            // from that child
            else if (idx != n && C[idx + 1].n >= t)
                BorrowFromNext(idx);

            // Merge C[idx] with its sibling
            // If C[idx] is the last child, merge it with its previous sibling
            // Otherwise merge it with its next sibling
            else
            {
                if (idx != n)
                    Merge(idx);
                else
                    Merge(idx - 1);
            }
        }

        // A function to borrow a key from C[idx-1] and insert it
        // into C[idx]
        private void BorrowFromPrev(int idx)
        {
            BTreeNode child = C[idx];
            BTreeNode sibling = C[idx - 1];

            // The last key from C[idx-1] goes up to the parent and key[idx-1]
            // from parent is inserted as the first key in C[idx]. Thus, the loses
            // sibling one key and child gains one key

            // Moving all key in C[idx] one step ahead
            for (int i = child.n - 1; i >= 0; --i)
                child.keys[i + 1] = child.keys[i];

            // If C[idx] is not a leaf, move all its child pointers one step ahead
            if (!child.leaf)
            {
                for (int i = child.n; i >= 0; --i)
                    child.C[i + 1] = child.C[i];
            }

            // Setting child's first key equal to keys[idx-1] from the current node
            child.keys[0] = keys[idx - 1];

            // Moving sibling's last child as C[idx]'s first child
            if (!child.leaf)
                child.C[0] = sibling.C[sibling.n];

            // Moving the key from the sibling to the parent
            // This reduces the number of keys in the sibling
            keys[idx - 1] = sibling.keys[sibling.n - 1];

            child.n += 1;
            sibling.n -= 1;
        }

        // A function to borrow a key from the C[idx+1] and place
        // it in C[idx]
        private void BorrowFromNext(int idx)
        {
            BTreeNode child = C[idx];
            BTreeNode sibling = C[idx + 1];

            // keys[idx] is inserted as the last key in C[idx]
            child.keys[(child.n)] = keys[idx];

            // Sibling's first child is inserted as the last child
            // into C[idx]
            if (!child.leaf)
                child.C[(child.n) + 1] = sibling.C[0];

            //The first key from sibling is inserted into keys[idx]
            keys[idx] = sibling.keys[0];

            // Moving all keys in sibling one step behind
            for (int i = 1; i < sibling.n; ++i)
                sibling.keys[i - 1] = sibling.keys[i];

            // Moving the child pointers one step behind
            if (!sibling.leaf)
            {
                for (int i = 1; i <= sibling.n; ++i)
                    sibling.C[i - 1] = sibling.C[i];
            }

            // Increasing and decreasing the key count of C[idx] and C[idx+1]
            // respectively
            child.n += 1;
            sibling.n -= 1;
        }

        // A function to merge C[idx] with C[idx+1]
        // C[idx+1] is freed after merging
        private void Merge(int idx)
        {
            BTreeNode child = C[idx];
            BTreeNode sibling = C[idx + 1];

            // Pulling a key from the current node and inserting it into (t-1)th
            // position of C[idx]
            child.keys[t - 1] = keys[idx];

            // Copying the keys from C[idx+1] to C[idx] at the end
            for (int i = 0; i < sibling.n; ++i)
                child.keys[i + t] = sibling.keys[i];  // debug i + t

            // Copying the child pointers from C[idx+1] to C[idx]
            if (!child.leaf)
            {
                for (int i = 0; i <= sibling.n; ++i)
                    child.C[i + t] = sibling.C[i];
            }

            // Moving all keys after idx in the current node one step before -
            // to fill the gap created by moving keys[idx] to C[idx]
            for (int i = idx + 1; i < n; ++i)
                keys[i - 1] = keys[i];

            // Moving the child pointers after (idx+1) in the current node one
            // step before
            for (int i = idx + 2; i <= n; ++i)
                C[i - 1] = C[i];

            // Updating the key count of child and the current node
            child.n += sibling.n;
            n--;

            Clean();
        }

        // Clean the unnecessary keys and children.
        public void Clean()
        {
            // clear keys
            for (int i = n; i < keys.Length; i++)
                keys[i] = 0;

            if (leaf)
            {
                // clear children
                for (int i = 0; i < C.Length; i++)
                    C[i] = null;
            }
        }

        // Traverse the node.  Print every key.
        public void Traverse()
        {
            if (leaf)
            {
                for (int i = 0; i < n; i++)
                {
                   Console.Write(keys[i] + " ");
                }
            }
            else
            {
                // index
                for (int i = 0; i <= n; i++)
                {
                    if (C[i] != null)
                    {
                        C[i].Traverse();
                    }
                }
            }
        }

        // Get all descendants of a B+ tree node.
        public IEnumerable<BTreeNode> Descendants()
        {
            yield return this;
            for (int i = 0; i <= n; i++)
            {
                if (leaf == false && C[i] != null)
                { 
                    foreach (var item in C[i].Descendants())
                       yield return item;
                }
            }
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (leaf)
                sb.Append("L ");
            else
                sb.Append("I ");

            sb.Append("K");
            sb.Append(n + " ");
            for (int i = 0; i < keys.Length; i++)
            {
                sb.Append(keys[i] + " ");
            }

            return sb.ToString();
        }

    }
}
