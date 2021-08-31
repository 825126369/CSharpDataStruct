using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public class TreeNode
    {
        public int data;
        public TreeNode left;
        public TreeNode right;
        public TreeNode parent;

        public bool color;

        public TreeNode(int data)
        {
            this.data = data;
            this.color = false;
        }
    }

    class RB_Tree
    {
        public const bool RED = false;
        public const bool BLACK = true;

        public TreeNode root = null;
        public void Insert(int item)
        {
            if (root == null)
            {
                //注意，根节点为黑色
                root = new TreeNode(item);
                Black(root);
            }
            else
            {
                //这一步只完成插入操作，与二叉查找的插入是一样的
                var newnode = Inserts(item);
                //重点在于这一步，看节点是否需要调整
                InsertFixUp(newnode);
            }
        }
        public TreeNode Inserts(int item)
        {
            TreeNode node = root;
            TreeNode newnode = new TreeNode(item);
            while (true)
            {
                if (item > node.data)
                {
                    if (node.right == null)
                    {
                        //注意父子关系要确定
                        newnode.parent = node;
                        node.right = newnode;
                        break;
                    }
                    node = node.right;
                }
                else if (item < node.data)
                {
                    if (node.left == null)
                    {
                        newnode.parent = node;
                        node.left = newnode;
                        break;
                    }
                    node = node.left;
                }
            }
            return newnode;
        }
        private void InsertFixUp(TreeNode node)
        {
            TreeNode parent = node.parent;
            if (parent == null)
            {
                Black(node);
                return;
            }
            if (IsBlack(parent)) return;

            TreeNode uncle = Sibling(parent);
            TreeNode grand = parent.parent;
            if (IsRed(uncle))//叔叔节点是黑色
            {
                Black(parent);
                Black(uncle);
                InsertFixUp(Red(grand));
                return;//这里直接返回，因为下面处理的是祖父节点的调整，与自己这个节点已经无关了
            }
            if (IsLeftChild(parent))
            {
                if (IsLeftChild(node))
                {
                    Black(parent);
                    Red(grand);
                    RightRotate(grand);
                }
                else
                {
                    Black(node);
                    Red(grand);
                    LeftRotate(parent);
                    RightRotate(grand);
                }
            }
            else
            {
                if (IsLeftChild(node))
                {
                    Black(node);
                    Red(grand);
                    RightRotate(parent);
                    LeftRotate(grand);
                }
                else
                {
                    Black(parent);
                    Red(grand);
                    LeftRotate(grand);
                }
            }
        }
        public void Remove(int data)
        {
            TreeNode node = Find(data);
            Remove(node);
        }
        private void Remove(TreeNode node)
        {
            if (node == null)
                return;
            if (node.left != null && node.right != null)
            {
                TreeNode s = FindNext(node);
                node.data = s.data;
                node = s;
            }
            TreeNode replacement = node.left != null ? node.left : node.right;
            if (replacement != null)
            {
                replacement.parent = node.parent;
                if (node.parent == null)
                    root = replacement;
                else if (node == node.parent.left)
                    node.parent.left = replacement;
                else
                    node.parent.right = replacement;
                RemoveFixUp(node, replacement);
            }
            else if (node.parent == null)//是叶子结点，并且是根节点
            {
                root = null;
                //RemoveFixUp(node, null);
            }
            else//是叶子结点，但不是根节点
            {
                if (node == node.parent.right)
                    node.parent.right = null;
                else
                    node.parent.left = null;
                RemoveFixUp(node, null);
            }
        }
        private void RemoveFixUp(TreeNode node, TreeNode replacement)
        {
            //1、如果删除的是红色节点
            if (IsRed(node)) return;
            //2、如果删除的是黑色节点，用于取代node的节点是红色
            if (IsRed(replacement))
            {
                Black(replacement);
                return;
            }
            //3、如果删除的是黑色节点，孩子都是黑色（或者null）
            TreeNode parent = node.parent;
            //3.1、如果删除的是根节点，直接返回
            if (parent == null) return;
            //获取删除节点的兄弟节点
            bool left = parent.left == null || IsLeftChild(node);//判断删除的是父节点的左子节点还是右子节点
            TreeNode sibling = left ? parent.right : parent.left;
            if (left)//被删除的节点在右边，兄弟节点在左边
            {
                if (IsRed(sibling))//兄弟节点是红色情况
                {
                    Black(sibling);
                    Red(parent);
                    LeftRotate(parent);

                    sibling = parent.right;//重新指定兄弟节点
                }
                //能来到这里。兄弟节点必是黑色了，处理黑色兄弟的情况
                if (IsBlack(sibling.left) && IsBlack(sibling.right))
                {
                    bool parentBlack = IsBlack(parent);
                    Red(sibling);
                    Black(parent);
                    if (parentBlack)
                    {
                        RemoveFixUp(parent, null);
                    }
                }
                else//能来到这里，说明兄弟中至少有一个红色子节点
                {
                    if (IsBlack(sibling.right))
                    {
                        RightRotate(sibling);
                        sibling = parent.right;//兄弟节点变了，这里要重新转换
                    }
                    Color(sibling, ColorOf(parent));
                    Black(sibling.right);
                    Black(parent);
                    LeftRotate(parent);
                }
            }
            else
            {
                if (IsRed(sibling))//兄弟节点是红色情况
                {
                    Black(sibling);
                    Red(parent);
                    RightRotate(parent);

                    sibling = parent.left;//重新指定兄弟节点
                }
                //能来到这里。兄弟节点必是黑色了，处理黑色兄弟的情况
                if (IsBlack(sibling.left) && IsBlack(sibling.right))
                {
                    bool parentBlack = IsBlack(parent);
                    Red(sibling);
                    Black(parent);
                    if (parentBlack)
                    {
                        RemoveFixUp(parent, null);
                    }
                }
                else//能来到这里，说明兄弟中至少有一个红色子节点
                {
                    if (IsBlack(sibling.left))
                    {
                        LeftRotate(sibling);
                        sibling = parent.left;//兄弟节点变了，这里要重新转换
                    }
                    Color(sibling, ColorOf(parent));
                    Black(sibling.left);
                    Black(parent);
                    RightRotate(parent);
                }
            }
        }
        public TreeNode FindNext(TreeNode root)
        {
            if (root == null)
                return null;
            if (root.right == null)
                return null;
            TreeNode current = root.right;
            while (current != null)
            {
                if (current.left != null)
                {
                    current = current.left;
                }
                else
                    break;
            }
            return current;
        }
        public TreeNode Find(int data)
        {
            TreeNode current = root;
            while (current != null)
            {
                if (data < current.data)
                    current = current.left;
                else if (data > current.data)
                    current = current.right;
                else
                    return current;
            }
            return null;
        }
        private void LeftRotate(TreeNode node)
        {
            TreeNode parent = node.parent;
            TreeNode rightChild = node.right;
            TreeNode grandLeftChild = rightChild.left;

            node.parent = rightChild;
            rightChild.left = node;
            rightChild.parent = parent;
            if (parent != null)
            {
                if (parent.left == node)
                {
                    parent.left = rightChild;
                }
                else
                {
                    parent.right = rightChild;
                }
            }
            else//如果父节点是空，说明是根节点，让root指向旋转后的父节点
            {
                root = rightChild;
            }
            node.right = grandLeftChild;
            if (grandLeftChild != null)
            {
                grandLeftChild.parent = node;
            }
        }
        private void RightRotate(TreeNode node)
        {
            TreeNode parent = node.parent;
            TreeNode leftChild = node.left;
            TreeNode grandRightChild = leftChild.right;

            node.parent = leftChild;
            leftChild.right = node;
            leftChild.parent = parent;
            if (parent != null)
            {
                if (parent.left == node)
                {
                    parent.left = leftChild;
                }
                else
                {
                    parent.right = leftChild;
                }
            }
            else//如果父节点是空，说明是根节点，让root指向旋转后的父节点
            {
                root = leftChild;
            }
            node.left = grandRightChild;
            if (grandRightChild != null)
            {
                grandRightChild.parent = node;
            }
        }
        //染色
        private TreeNode Color(TreeNode node, bool color)
        {
            if (node == null)
                return node;
            node.color = color;
            return node;
        }
        //获取颜色
        private bool ColorOf(TreeNode node)
        {
            if (node == null)
                return BLACK;
            return node.color == RED ? RED : BLACK;
        }
        //染成红色
        private TreeNode Red(TreeNode node)
        {
            return Color(node, RED);
        }
        //染成黑色
        private TreeNode Black(TreeNode node)
        {
            return Color(node, BLACK);
        }
        //判断是否为红色
        private bool IsRed(TreeNode node)
        {
            return ColorOf(node) == RED;
        }
        //判断是否为黑色
        private bool IsBlack(TreeNode node)
        {
            return ColorOf(node) == BLACK;
        }
        //判断是不是父亲的左孩子
        private bool IsLeftChild(TreeNode node)
        {
            return node.parent != null && node.parent.left == node;
        }
        //判断是不是父亲的右孩子
        private bool IsRightChild(TreeNode node)
        {
            return node.parent != null && node.parent.right == node;
        }
        //获取兄弟节点
        private TreeNode Sibling(TreeNode node)
        {
            if (IsLeftChild(node))
            {
                return node.parent.right;
            }
            if (IsRightChild(node))
            {
                return node.parent.left;
            }
            return null;
        }
        public void PreOrder(TreeNode node)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = node;
            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    string color = current.color == RED ? "R" : "B";
                    Console.Write(color + current.data + "  ");
                    stack.Push(current);
                    current = current.left;
                }
                if (stack.Count > 0)
                {
                    current = stack.Pop().right;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 55, 87, 56, 74, 96, 22, 62, 20, 70, 68, 90, 50 };
            RB_Tree rb_tree = new RB_Tree();
            foreach (var item in a)
            {
                rb_tree.Insert(item);
            }
            for (int i = 0; i < a.Length; i++)
            {
                rb_tree.Remove(a[i]);
                Console.WriteLine("--------------------------------------");
                rb_tree.PreOrder(rb_tree.root);
                Console.WriteLine();
            }
            rb_tree.PreOrder(rb_tree.root);
            Console.ReadKey();
        }
    }
}
