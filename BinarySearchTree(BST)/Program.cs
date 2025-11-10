namespace BinarySearchTree
{
    class Node<T>(T item) where T : notnull
    {
        public readonly T Item = item;
        public Node<T>? LeftNode = null;
        public Node<T>? RightNode = null;
    }

    class BinarySearchTree<T> where T : notnull
    {
        #region Fields
        //루트 노드
        private Node<T>? _rootNode;
        //요소 수
        private int _count;
        #endregion

        #region Properties
        public int Count
        {
            get { return _count; }
        }
        #endregion

        #region Constructor
        public BinarySearchTree()
        {
            _rootNode = null;
            _count = 0;
        }

        public BinarySearchTree(Node<T> node)
        {
            _rootNode = node;
            _count = CountNode(node);
        }
        #endregion

        #region PublicMethods
        //트리 노드 전체 삭제 메서드
        public void Clear()
        {
            foreach (Node<T> node in RemoveList())
            {
                node.LeftNode = null;
                node.RightNode = null;
            }
            _rootNode = null;
            _count = 0;
        }

        //해당 요소 트리 포함 여부 메서드
        public bool Contains(T item)
        {
            ExceptionArgumentNull(item);

            return IsContains(item);
        }

        //해당 요소 추가 메서드
        public void Insert(T item)
        {
            ExceptionArgumentNull(item);

            if (_rootNode == null)
            {
                InsertNode(ref _rootNode, item);
                return;
            }

            if (IsContains(item))
            {
                Console.WriteLine($"{item} already in tree.");
                return;
            }

            Node<T> targetNode = _rootNode;
            int maxSearchCount = TreeLevel();
            while (maxSearchCount > 0)
            {
                if (Comparer<T>.Default.Compare(targetNode.Item, item) > 0)
                {
                    if (targetNode.LeftNode == null)
                    {
                        InsertNode(ref targetNode.LeftNode, item);
                        return;
                    }
                    targetNode = targetNode.LeftNode;
                }
                else
                {
                    if (targetNode.RightNode == null)
                    {
                        InsertNode(ref targetNode.RightNode, item);
                        return;
                    }
                    targetNode = targetNode.RightNode;
                }
                maxSearchCount--;
            }
            Console.WriteLine("Insert failed - Unexpected error");
        }

        //해당 요소 삭제 메서드
        public void Remove(T item)
        {
            ExceptionArgumentNull(item);

            if (_rootNode == null || !IsContains(item)) { return; }

            (Node<T>?, bool) targetParentNode;
            Node<T> targetNode;
            GetTargetParantAndTarget();

            (Node<T>?, bool) replaceParentNode;
            Node<T> replaceNode;
            GetReplaceParantAndReplace();

            replaceNode.LeftNode = targetNode.LeftNode;
            replaceNode.RightNode = targetNode.RightNode;
            targetNode.LeftNode = null;
            targetNode.RightNode = null;

            if (replaceParentNode.Item1 != null)
            {
                if (!replaceParentNode.Item2)
                {
                    replaceParentNode.Item1.LeftNode = null;
                }
                else
                {
                    replaceParentNode.Item1.RightNode = null;
                }
            }

            if (targetParentNode.Item1 == null)
            {
                _rootNode = replaceNode;
            }
            else
            {
                if (!targetParentNode.Item2)
                {
                    targetParentNode.Item1.LeftNode = replaceNode;
                }
                else
                {
                    targetParentNode.Item1.RightNode = replaceNode;
                }
            }

            void GetTargetParantAndTarget()
            {
                Queue<Node<T>> nodes = new Queue<Node<T>>();
                nodes.Enqueue(_rootNode);

                targetParentNode = (null, false);
                while (nodes.TryDequeue(out targetNode!))
                {
                    int compare = Comparer<T>.Default.Compare(targetNode.Item, item);
                    if (compare == 0) { break; }
                    if (compare > 0)
                    {
                        if (targetNode.LeftNode != null)
                        {
                            nodes.Enqueue(targetNode.LeftNode);
                            targetParentNode = (targetNode, false);
                        }
                    }
                    else
                    {
                        if (targetNode.RightNode != null)
                        {
                            nodes.Enqueue(targetNode.RightNode);
                            targetParentNode = (targetNode, true);
                        }
                    }
                }
            }

            void GetReplaceParantAndReplace()
            {
                replaceParentNode = targetParentNode;
                replaceNode = targetNode;
                if (replaceNode.LeftNode != null)
                {
                    replaceParentNode = (replaceNode, false);
                    replaceNode = replaceNode.LeftNode;
                    while (replaceNode.RightNode != null)
                    {
                        replaceParentNode = (replaceNode, true);
                        replaceNode = replaceNode.RightNode;
                    }
                }
                else if (replaceNode.RightNode != null)
                {
                    replaceParentNode = (replaceNode, true);
                    replaceNode = replaceNode.RightNode;
                    while (replaceNode.LeftNode != null)
                    {
                        replaceParentNode = (replaceNode, false);
                        replaceNode = replaceNode.LeftNode;
                    }
                }
            }
        }

        //해당 요소 검색 메서드
        public void Search(T item)
        {
            if (_rootNode == null)
            {
                Console.WriteLine("Root node is null. first Insert node.");
                return;
            }

            Queue<(Node<T>, int)> nodes = new Queue<(Node<T>, int)>();
            nodes.Enqueue((_rootNode, 1));

            (Node<T>, int) targetNode;
            while (nodes.TryDequeue(out targetNode!))
            {
                int compare = Comparer<T>.Default.Compare(targetNode.Item1.Item, item);
                if (compare == 0)
                {
                    string? leftNode = (targetNode.Item1.LeftNode == null) ? "null" : targetNode.Item1.LeftNode.Item.ToString(); 
                    string? rightNode = (targetNode.Item1.RightNode == null) ? "null" : targetNode.Item1.RightNode.Item.ToString(); 
                    Console.WriteLine($"Find_Node_Info : TargetNode - {targetNode.Item1.Item}, LeftNode - {leftNode}, RightNode - {rightNode}, Level[{targetNode.Item2}]");
                    break;
                }
                if (compare > 0)
                {
                    if (targetNode.Item1.LeftNode != null)
                    {
                        nodes.Enqueue((targetNode.Item1.LeftNode, targetNode.Item2 + 1));
                    }
                }
                else
                {
                    if (targetNode.Item1.RightNode != null)
                    {
                        nodes.Enqueue((targetNode.Item1.RightNode, targetNode.Item2 + 1));
                    }
                }
            }
        }

        //트리 요소 배열 반환 메서드
        public T[] ToArray()
        {
            if (_rootNode == null) { return Array.Empty<T>(); }

            int arraySize = (int)MathF.Pow(2, TreeLevel()) - 1;
            T[] array = new T[arraySize];

            int treeLevel = TreeLevel();

            Queue<(Node<T>?, int)> nodes = new Queue<(Node<T>?, int)>();
            nodes.Enqueue((_rootNode, 1));
            for (int index = 0; index < arraySize; index++)
            {
                (Node<T>?, int) targetNode = nodes.Dequeue();
                int level = targetNode.Item2;

                array[index] = (targetNode.Item1 != null) ? targetNode.Item1.Item : default!;

                if (level < treeLevel)
                {
                    nodes.Enqueue((targetNode.Item1?.LeftNode, level + 1));
                    nodes.Enqueue((targetNode.Item1?.RightNode, level + 1));
                }
            }
            return array;
        }

        //트리 레벨 반환 메서드
        public int TreeLevel()
        {
            if (_rootNode == null) { return 0; }

            int level = 1;
            Queue<(Node<T>, int)> nodes = new Queue<(Node<T>, int)>();
            nodes.Enqueue((_rootNode, level));
            while (nodes.TryDequeue(out (Node<T>, int) targetTuple))
            {
                level = targetTuple.Item2;
                if (targetTuple.Item1.LeftNode != null)
                {
                    nodes.Enqueue((targetTuple.Item1.LeftNode, level + 1));
                }
                if (targetTuple.Item1.RightNode != null)
                {
                    nodes.Enqueue((targetTuple.Item1.RightNode, level + 1));
                }
            }
            return level;
        }
        #endregion

        #region PrivateMethods
        //해당 노드의 총 노드 수 반환 메서드
        private static int CountNode(Node<T> node)
        {
            if (node == null) { return 0; }

            int count = 0;
            Queue<Node<T>> nodes = new Queue<Node<T>>();
            nodes.Enqueue(node);

            Node<T> targetNode;
            while (nodes.TryDequeue(out targetNode!))
            {
                if (targetNode.LeftNode != null)
                    nodes.Enqueue(targetNode.LeftNode);
                if (targetNode.RightNode != null)
                    nodes.Enqueue(targetNode.RightNode);
                count++;
            }
            return count;
        }

        //해당 요소 트리 포함 여부 내부 메서드
        private bool IsContains(T item)
        {
            if (_rootNode == null) { return false; }

            Queue<Node<T>> nodes = new Queue<Node<T>>();
            nodes.Enqueue(_rootNode);

            Node<T> targetNode;
            while (nodes.TryDequeue(out targetNode!))
            {
                int compare = Comparer<T>.Default.Compare(targetNode.Item, item);
                if (compare == 0) { return true; }
                if (compare > 0)
                {
                    if (targetNode.LeftNode != null)
                    {
                        nodes.Enqueue(targetNode.LeftNode);
                    }
                }
                else
                {
                    if (targetNode.RightNode != null)
                    {
                        nodes.Enqueue(targetNode.RightNode);
                    }
                }
            }
            return false;
        }
      
        //해당 트리 노드 위치 해당 요소 노드 생성/추가 메서드
        private void InsertNode(ref Node<T>? node, T item)
        {
            node = new Node<T>(item);
            _count++;
        }
       
        //자식 노드 가진 부모 노드 배열 반환 메서드
        private Node<T>[] RemoveList()
        {
            if (_rootNode == null) { return Array.Empty<Node<T>>(); }

            Queue<Node<T>> nodes = new Queue<Node<T>>();
            Stack<Node<T>> removeList = new Stack<Node<T>>();
            nodes.Enqueue(_rootNode);

            Node<T> targetNode;
            while (nodes.TryDequeue(out targetNode!))
            {
                if (targetNode.LeftNode != null)
                {
                    if (!removeList.Contains(targetNode))
                    {
                        removeList.Push(targetNode);
                    }
                    nodes.Enqueue(targetNode.LeftNode);
                }

                if (targetNode.RightNode != null)
                {
                    if (!removeList.Contains(targetNode))
                    {
                        removeList.Push(targetNode);
                    }
                    nodes.Enqueue(targetNode.RightNode);
                }
            }
            return removeList.ToArray();
        }
        #endregion

        #region Exceptions
        //T 인자 널 예외처리 메서드
        private static void ExceptionArgumentNull(T item)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(item);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Argument cannot be null.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> BST = new BinarySearchTree<int>();
            BST.Insert(8);
            BST.Insert(4);
            BST.Insert(12);
            BST.Insert(2);
            BST.Insert(6);
            BST.Insert(10);
            BST.Insert(14);
            BST.Insert(1);
            BST.Insert(3);
            BST.Insert(5);
            BST.Insert(7);
            BST.Insert(9);
            BST.Insert(11);
            BST.Insert(13);
            BST.Insert(15);
            BST.Search(4);
        }
    }
}
