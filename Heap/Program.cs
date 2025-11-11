namespace Heap
{
    //우선순위 열거형
    enum Priority
    {
        Min,
        Max
    }

    class Heap<T>
    {
        #region Fields
        //힙 배열 기본 크기
        private readonly int _defaultSize = 7;
        //힙 배열
        private T[] _heapArray;
        //힙 우선순위
        private Priority priority;
        //요소 수
        private int _count;
        #endregion

        #region Properties
        public int Count { get { return _count; } }
        #endregion

        #region Constructors
        public Heap()
        {
            _heapArray = new T[_defaultSize];
            priority = Priority.Max;
            _count = 0;
        }

        public Heap(Priority priority)
        {
            _heapArray = new T[_defaultSize];
            this.priority = priority;
            _count = 0;
        }

        public Heap(Heap<T> heap)
        {
            _heapArray = heap._heapArray;
            priority = heap.priority;
            _count = heap._count;
        }
        #endregion

        #region PublicMethods
        //힙 해당 요소 추출 메서드
        public T DeleteItem()
        {
            EmptyException();

            T item = _heapArray[0];
            _heapArray[0] = _heapArray[_count - 1];
            _heapArray[_count - 1] = default!;
            _count--;
            Siftdown(0);
            return item;

            void EmptyException()
            {
                try
                {
                    if(_count == 0) { throw new Exception(); }
                }
                catch (Exception)
                {
                    Console.WriteLine("Heap is Empty. First insert item.");
                    Environment.Exit(-1);
                }
            }
        }

        //힙 해당 요소 추가 메서드
        public void InsertItem(T item)
        {
            if (_count >= _heapArray.Length)
            {
                AddSize();
            }
            _heapArray[_count] = item;
            Siftup(_count++);
        }

        //힙 배열 프린트 메서드
        public void PrintArray()
        {
            List<T> printList = new List<T>();
            for (int i = 0; i < _count; i++)
            {
                printList.Add(_heapArray[i]);
            }
            Console.WriteLine(string.Join(' ', printList));
        }
        #endregion

        #region PrivateMethods
        //힙 배열 트리 레벨에 따른 크기 증가 메서드
        private void AddSize()
        {
            int level = GetLevel();
            int addSize = (int)MathF.Pow(2, level);
            T[] newArray = new T[_heapArray.Length + addSize];
            Array.Copy(_heapArray, newArray, _heapArray.Length);
            _heapArray = newArray;
        }

        //힙 배열 레벨 반환 메서드
        private int GetLevel()
        {
            return int.Log2(_heapArray.Length + 1);
        }

        //자식 노드 요소 비교 교환 메서드
        private void Siftdown(int index)
        {
            int leftChildIndex = ((2 * index) + 1) < _count ? (2 * index) + 1 : -1;
            int rightChildIndex = ((2 * index) + 2) < _count ? (2 * index) + 2 : -1;

            if (leftChildIndex == -1 && rightChildIndex == -1) { return; }

            int targetIndex;
            if (leftChildIndex == -1) { targetIndex = rightChildIndex; }
            else if (rightChildIndex == -1) { targetIndex = leftChildIndex; }
            else
            {
                if (priority == Priority.Max)
                {
                    targetIndex = (Comparer<T>.Default.Compare(_heapArray[leftChildIndex], _heapArray[rightChildIndex]) > 0) ? leftChildIndex : rightChildIndex;
                }
                else
                {
                    targetIndex = (Comparer<T>.Default.Compare(_heapArray[leftChildIndex], _heapArray[rightChildIndex]) < 0) ? leftChildIndex : rightChildIndex;
                }
            }

            if (priority == Priority.Max)
            {
                if (Comparer<T>.Default.Compare(_heapArray[targetIndex], _heapArray[index]) > 0)
                {
                    (_heapArray[index], _heapArray[targetIndex]) = (_heapArray[targetIndex], _heapArray[index]);
                    Siftdown(targetIndex);
                }
            }
            else
            {
                if (Comparer<T>.Default.Compare(_heapArray[targetIndex], _heapArray[index]) < 0)
                {
                    (_heapArray[index], _heapArray[targetIndex]) = (_heapArray[targetIndex], _heapArray[index]);
                    Siftdown(targetIndex);
                }
            }
        }

        //부모 노드 요소 비교 교환 메서드
        private void Siftup(int index)
        {
            if (index == 0) { return; }

            if (priority == Priority.Max)
            {
                if (Comparer<T>.Default.Compare(_heapArray[index], _heapArray[(index - 1) / 2]) > 0)
                {
                    (_heapArray[index], _heapArray[(index - 1) / 2]) = (_heapArray[(index - 1) / 2], _heapArray[index]);
                    Siftup((index - 1) / 2);
                }
            }
            else
            {
                if (Comparer<T>.Default.Compare(_heapArray[index], _heapArray[(index - 1) / 2]) < 0)
                {
                    (_heapArray[index], _heapArray[(index - 1) / 2]) = (_heapArray[(index - 1) / 2], _heapArray[index]);
                    Siftup((index - 1) / 2);
                }
            }
        }
        #endregion
    }

    class Heap_Ex
    {
        static void Main(string[] args)
        {
           Heap<int> maxHeap = new Heap<int>();
            for(int i = 0; i < 7; i++)
            {
                maxHeap.InsertItem(i);
            }
            Console.Write("Insert After : ");
            maxHeap.PrintArray();
            int deleteMaxItem = maxHeap.DeleteItem();
            Console.WriteLine($"{deleteMaxItem} deleted");
            Console.Write("Delete After : ");
            maxHeap.PrintArray();

            Console.WriteLine();
            
            Heap<int> minHeap = new Heap<int>(Priority.Min);
            for (int i = 7; i > 0; i--)
            {
                minHeap.InsertItem(i);
            }
            Console.Write("Insert After : ");
            minHeap.PrintArray();
            int deleteMinItem = minHeap.DeleteItem();
            Console.WriteLine($"{deleteMinItem} deleted");
            Console.Write("Delete After : ");
            minHeap.PrintArray();
        }
    }
}