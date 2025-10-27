namespace LinkedList
{
    public class Node<T>
    {
        private T _value;
        private Node<T> _prevNode;
        private Node<T> _nextNode;

        public Node(T inputValue)
        {
            _value = inputValue;
            _prevNode = this;
            _nextNode = this;
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Node<T>? FrontNode
        {
            get { return _prevNode; }
            set
            {
                if (value == null) _prevNode = this;
                else
                {
                    value._nextNode = this;
                    _prevNode = value;
                }
            }
        }

        public Node<T>? NextNode
        {
            get { return _nextNode; }
            set
            {
                if (value == null) _nextNode = this;
                else
                {
                    value._prevNode = this;
                    _nextNode = value;
                }
            }
        }
    }

    public class LinkedList<T>
    {
        #region Fields
        //연결리스트 헤드
        private Node<T>? _head;
        //연결리스트 노드 수
        private int _count;
        #endregion

        #region Properties
        //리스트 요소 수
        public int Count
        {
            get { return _count; }
        }

        //처음 노드
        public Node<T>? First
        {
            get { return _head; }
        }

        //마지막 노드
        public Node<T>? Last
        {
            get { return _head?.FrontNode; }
        }
        #endregion

        #region Constructors
        public LinkedList()
        {
            _head = null;
            _count = 0;
        }

        public LinkedList(LinkedList<T> linkedlist) : this()
        {
            CheckThrowIfNull(linkedlist);
            if (linkedlist._count == 0 || linkedlist._head == null) return;
            else
            {
                Node<T> node = linkedlist._head;
                for (int count = 0; count < linkedlist._count; count++)
                {
                    AddLast(node.Value);
                    if (node.NextNode == null || node.NextNode == linkedlist._head) break;
                    node = node.NextNode;
                }
            }
        }
        #endregion

        #region Methods
        //해당 노드 앞에 노드 추가 메서드(매개변수 값)
        public void AddAfter(Node<T>? node, T value)
        {
            CheckThrowIfNull(value, "AddAfter");
            Node<T> newNode = new Node<T>(value);
            AddAfter(node, newNode);
        }

        //해당 노드 앞에 노드 추가 메서드(매개변수 노드)
        public void AddAfter(Node<T>? node, Node<T> newNode)
        {
            IsContainsHeadNotNull(node, "AddAfter");
            CheckThrowIfNull(newNode, "AddAfter");
            _count++;
            if (_head == null) _head = newNode;
            else
            {
                newNode.NextNode = node!.NextNode;
                newNode.FrontNode = node;
            }
        }

        //해당 노드 뒤에 노드 추가 메서드(매개변수 값)
        public Node<T> AddBefore(Node<T>? node, T value)
        {
            CheckThrowIfNull(value, "AddBefore");
            Node<T> newNode = new Node<T>(value);
            return AddBefore(node, newNode);
        }

        //해당 노드 뒤에 노드 추가 메서드(매개변수 노드)
        public Node<T> AddBefore(Node<T>? node, Node<T> newNode)
        {
            IsContainsHeadNotNull(node, "AddBefore");
            CheckThrowIfNull(newNode, "AddBefore");
            _count++;
            if (_head == null) _head = newNode;
            else
            {
                newNode.FrontNode = node!.FrontNode;
                newNode.NextNode = node;
            }
            return newNode;
        }

        //처음에 노드 추가 메서드(매개변수 값)
        public void AddLast(T value)
        {
            AddAfter(_head?.FrontNode, value);
        }

        //처음에 노드 추가 메서드(매개변수 노드)
        public void AddLast(Node<T> node)
        {
            AddAfter(_head?.FrontNode, node);
        }

        //마지막에 노드 추가 메서드(매개변수 값)
        public void AddFirst(T value)
        {
            _head = AddBefore(_head, value);
        }

        //마지막에 노드 추가 메서드(매개변수 노드)
        public void AddFirst(Node<T> node)
        {
            _head = AddBefore(_head, node);
        }

        //노드 전부 삭제 메서드
        public void Clear()
        {
            if (_head == null) return;
            Node<T> node = _head.FrontNode!;
            while (node != _head)
            {
                Node<T> frontNode = node.FrontNode!;
                node.FrontNode = null;
                node.NextNode = null;
                frontNode.NextNode = _head;
                node = frontNode;
            }
            _head = null;
            _count = 0;
        }

        //노드 포함 여부 확인 메서드(매개변수 값)
        public bool Contains(T value)
        {
            return FindNodeByValueFromStart(value) != null;
        }

        //노드 포함 여부 확인 메서드(매개변수 노드)
        public bool Contains(Node<T>? node)
        {
            if (node == null) return false;
            Node<T>? searchNode = _head;
            do
            {
                if (searchNode == null) return false;
                if (searchNode == node) return true;
                searchNode = searchNode.NextNode;
            } while (searchNode != _head);
            return false;
        }

        //노드 값 배열 반환 메서드
        public T[] ToArray()
        {
            if (_head == null) return Array.Empty<T>();
            T[] newArray = new T[_count];
            int index = 0;
            Node<T> node = _head;
            for (int count = 0; count < _count; count++)
            {
                newArray[index++] = node.Value;
                node = node.NextNode!;
            }
            return newArray;
        }
       
        //처음부터 노드 찾기 메서드
        public Node<T>? FindNodeByValueFromStart(T value)
        {
            CheckThrowIfNull(value, "FindNodeByValue");
            Node<T>? searchNode = _head;
            do
            {
                //동일 기능 수행(박싱으로 인한 성능저하 발생)
                //if (searchNode == null || Object.Equals(searchNode.Value, value)) return searchNode;
                if (searchNode == null || EqualityComparer<T>.Default.Equals(searchNode.Value, value)) return searchNode;
                searchNode = searchNode.NextNode;
            } while (searchNode != _head);
            return null;
        }

        //처음부터 노드 찾기 메서드
        public Node<T>? FindNodeByValueFromEnd(T value)
        {
            CheckThrowIfNull(value, "FindLastNodeByValue");
            Node<T>? searchNode = _head?.FrontNode;
            do
            {
                //동일 기능 수행(박싱으로 인한 성능저하 발생)
                //if (searchNode == null || Object.Equals(searchNode.Value, value)) return searchNode;
                if (searchNode == null || EqualityComparer<T>.Default.Equals(searchNode.Value, value)) return searchNode;
                searchNode = searchNode.FrontNode;
            } while (searchNode != _head!.FrontNode);
            return null;
        }

        //노드 삭제 메서드
        public void RemoveNode(Node<T>? node)
        {
            IsContains(node);
            if (_head == null)
            {
                Console.WriteLine("LinkedList is Empty.");
                return;
            }
            if (node == _head)
            {
                RemoveFirst();
                return;
            }
            _count--;
            Node<T> frontNode = node!.FrontNode!;
            Node<T> nextNode = node.NextNode!;
            node.FrontNode = null;
            node.NextNode = null;
            frontNode.NextNode = nextNode;
        }

        //처음 값을 가진 노드 삭제 메서드
        public void RemoveNodeByValueFromStart(T value)
        {
            CheckThrowIfNull(value, "RemoveNodeByValueFromStart");
            Node<T>? findNode = FindNodeByValueFromStart(value);
            RemoveNode(findNode);
        }

        //마지막 값을 가진 노드 삭제 메서드
        public void RemoveNodeByValueFromEnd(T value)
        {
            CheckThrowIfNull(value, "RemoveNodeByValueFromEnd");
            Node<T>? findNode = FindNodeByValueFromEnd(value);
            RemoveNode(findNode);
        }

        //처음 노드 삭제 메서드
        public void RemoveFirst()
        {
            if (_head == null)
            {
                Console.WriteLine("LinkedList is Empty.");
                return;
            }
            _count--;
            if (_head.FrontNode == _head)
            {
                _head = null;
                return;
            }
            Node<T> first = _head;
            Node<T> nextNode = first.NextNode!;
            nextNode.FrontNode = _head.FrontNode;
            first.FrontNode = null;
            first.NextNode = null;
            _head = nextNode;
        }

        //마지막 노드 삭제 메서드
        public void RemoveLast()
        {
            if (_head == null)
            {
                Console.WriteLine("LinkedList is Empty.");
                return;
            }
            _count--;
            if (_head.FrontNode == _head)
            {
                _head = null;
                return;
            }
            Node<T> last = _head.FrontNode!;
            Node<T> frontNode = last.FrontNode!;
            last.FrontNode = null;
            last.NextNode = null;
            frontNode.NextNode = _head;
        }

        //노드 출력 메서드
        public void PrintList(bool order = true)
        {
            if (_head == null)
            {
                Console.WriteLine("LinkedList is Empty.");
                return;
            }
            if (order)
            {
                Node<T> point = _head;
                do
                {
                    Console.WriteLine($"전:{point.FrontNode!.Value} | 값:{point.Value} | 후:{point.NextNode!.Value}");
                    point = point.NextNode;
                }
                while (point != _head);
            }
            else
            {
                Node<T> point = _head.FrontNode!;
                do
                {
                    Console.WriteLine($"{point.Value} = {point.FrontNode!.Value} : {point.NextNode!.Value}");
                    point = point.FrontNode;
                }
                while (point != _head.FrontNode);
            }
        }
        #endregion

        #region CheckException
        private static void CheckThrowIfNull(T argument, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(argument);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"{methodName} argument T cannot be null.");
                Environment.Exit(-1);
            }
        }

        private static void CheckThrowIfNull(Node<T> argument, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(argument);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"{methodName} argument Node<T> cannot be null.");
                Environment.Exit(-1);
            }
        }

        private static void CheckThrowIfNull(LinkedList<T> argument)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(argument);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Constructor argument LinkedList<T> cannot be null.");
                Environment.Exit(-1);
            }
        }

        void IsContainsHeadNotNull(Node<T>? argument, string methodName)
        {
            try
            {
                if (!Contains(argument) && _head != null) throw new ArgumentNullException();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Method {methodName} argument Node<T> cannot be null.");
                Environment.Exit(-1);
            }
        }

        void IsContains(Node<T>? node)
        {
            try
            {
                if (!Contains(node)) throw new ArgumentNullException();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"{node} Node is not Contains in LinkedList.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }

    class LinkedList_Ex
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.PrintList();
        }

    }
}
