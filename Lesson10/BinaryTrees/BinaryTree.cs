using System;

namespace BinaryTrees
{
    public class TreeNode<T> : IComparable
        where T : IComparable
    {
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public T Value { get; }

        public TreeNode(T value, TreeNode<T> left, TreeNode<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public int CompareTo(TreeNode<T> other) => Value.CompareTo(other.Value);
        public int CompareTo(object obj)
        {
            if (obj.GetType() != GetType())
                throw new 
        }
    }

    public class BinaryTree<T>
    {
        public TreeNode<T> Root { get; private set; }

        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(value, null, null);
                return;
            }

            while (true)
            {
                if (value.CompareTo(root.Value) < 0)
                {
                    root.LeftSize++;
                    if (root.Left == null)
                    {
                        var tempNode = new Node(value);
                        tempNode.Parent = root;
                        root.Left = tempNode;
                        Count++;
                        return;
                    }
                    root = root.Left;
                }
                else
                {
                    if (root.Right == null)
                    {
                        var tempNode = new Node(value);
                        tempNode.Parent = root;
                        root.Right = tempNode;
                        Count++;
                        return;
                    }
                    root = root.Right;
                }
            }
        }


    }
}
