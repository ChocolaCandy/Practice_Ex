namespace Stack
{
    public class Node<T>
    {
        private Node<T>? _frontNode;
        private readonly T _value;

        public Node()
        {
            _frontNode = null;
            _value = default!;
        }

        public Node(T input)
        {
            _frontNode = null;
            _value = input;
        }

        public Node<T>? FrontNode 
        {
            get { return _frontNode; }
            set { _frontNode = value; }
        }

        public T Value { get { return _value; } }
    }

    public class StackByLinkedList<T>
    {
        #region Fields
        //스택 맨 위 노드
        private Node<T>? _topNode;
        //스택 요소 수
        private int _count;
        #endregion

        #region Properties
        //스택 요소 수
        public int Count
        {
            get { return _count; }
        }

        //스택 비어있는지 여부
        public bool IsEmpty
        {
            get { return _count == 0; }
        }
        #endregion
        
        #region Constructors
        public StackByLinkedList()
        {
            _topNode = null;
            _count = 0;
        }

        public StackByLinkedList(Node<T> node)
        {
            CheckNodeNull();
            Push(node.Value);

            void CheckNodeNull()
            {
                try
                {
                    ArgumentNullException.ThrowIfNull(node);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Constructor Parameter Node<T> cannot be null.");
                    Environment.Exit(-1);
                }
            }
        }

        public StackByLinkedList(StackByLinkedList<T> list)
        {
            CheckLinkedListNull();
            foreach (T value in list.ToArray())
            {
                Push(value);
            }

            void CheckLinkedListNull()
            {
                try
                {
                    ArgumentNullException.ThrowIfNull(list);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Constructor Parameter StackByLinkedList<T> cannot be null.");
                    Environment.Exit(-1);
                }
            }
        }
        #endregion

        #region Methods 
        //전체 노드 삭제 메서드
        public void Clear()
        {
            if (_topNode == null) return;
            Node<T>? frontNode;
            do
            {
                frontNode = _topNode.FrontNode;
                _topNode.FrontNode = null;
                _topNode = frontNode;
            } while (_topNode != null);
            _count = 0;
        }

        //해당 요소 값 노드 포함 여부 확인 메서드
        public bool Contains(T value)
        {
            if (_topNode == null) return false;
            Node<T>? targetNode = _topNode;
            while (targetNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(targetNode.Value, value)) return true;
                targetNode = targetNode.FrontNode;
            }
            return false;
        }

        //탑 위치 요소 반환 메서드
        public T Peak()
        {
            CheckIsEmpty();
            return _topNode!.Value;
        }

        //탑 위치 요소 반환 여부 및 반환 메서드
        public bool TryPeek(out T result)
        {
            if (_topNode == null)
            {
                result = default!;
                return false;
            }
            result = _topNode.Value;
            return true;
        }

        //탑 위치 요소 추출 메서드
        public T Pop()
        {
            CheckIsEmpty();
            return Pop_T();
        }

        //탑 위치 요소 추출 여부 및 추출 메서드
        public bool TryPop(out T result)
        {
            if (_topNode == null)
            {
                result = default!;
                return false;
            }
            result = Pop_T();
            return true;
        }

        //탑 위치 요소 추출 메서드
        private T Pop_T()
        {
            T value = _topNode!.Value;
            Node<T>? frontNode = _topNode.FrontNode;
            _topNode.FrontNode = null;
            _topNode = frontNode;
            _count--;
            return value;
        }

        //해당 요소 값 노드 삽입 메서드
        public void Push(T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.FrontNode = _topNode;
            _topNode = newNode;
            _count++;
        }

        //스택 상태 출력 메서드
        public void State()
        {
            if(_topNode == null)
            {
                Console.WriteLine("The stack is empty.");
                return;
            }

            int MaxLength = 0;
            foreach (T item in ToArray())
            {
                int length = item!.ToString()!.Length;
                MaxLength = int.Max(MaxLength, length);
            }

            Node<T>? taragetNode = _topNode;
            Console.WriteLine(new string('-', MaxLength));
            while (taragetNode != null)
            {
                Console.WriteLine(taragetNode.Value);
                taragetNode = taragetNode.FrontNode;
                Console.WriteLine(new string('-', MaxLength));
            }
        }

        //스택 요소들 배열 반환 메서드
        public T[] ToArray()
        {
            if (_topNode == null) return Array.Empty<T>();
            T[] newArray = new T[_count];

            Node<T>? tempNode = _topNode;
            int index = _count - 1;
            while (tempNode != null)
            {
                newArray[index] = tempNode.Value;
                tempNode = tempNode.FrontNode;
                index--;
            }
            return newArray;
        }
        #endregion
        
        #region Exception
        private void CheckIsEmpty()
        {
            try
            {
                ArgumentNullException.ThrowIfNull(_topNode);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The stack is empty.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }
}
