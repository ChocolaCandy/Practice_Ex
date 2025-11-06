namespace Queue
{
    enum Side
    {
        Front,
        Rear
    }

    class Deque<T>
    {
        #region Fields
        //기본 덱(데크) 크기
        private readonly int defaultSize = 5;
        //덱(데크) 배열
        private T[] _array;
        //덱(데크) 헤드
        private int _front;
        //덱(데크) 테일
        private int _rear;
        //덱(데크) 요소 수
        private int _count;
        #endregion

        #region Constructors
        public Deque()
        {
            _array = new T[defaultSize];
            _front = defaultSize / 2;
            _rear = defaultSize / 2;
            _count = 0;
        }

        public Deque(int size)
        {
            _array = new T[size];
            _front = size / 2;
            _rear = size / 2;
            _count = 0;
        }

        public Deque(Deque<T> deque)
        {
            _array = new T[deque._array.Length];
            _front = deque._front;
            _rear = deque._rear;
            _count = deque._count;
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return _count; }
        }
        #endregion 

        #region PublicMethods
        //요소 전체 삭제 메서드
        public void Clear()
        {
            for (int index = _front; index <= _rear; index++)
            {
                _array[index] = default!;
            }
            _front = _array.Length / 2;
            _rear = _array.Length / 2;
            _count = 0;
        }

        //해당 요소 포함 여부 메서드
        public bool Contains(T item)
        {
            if (_count == 0)
            {
                return false;
            }
            for (int index = _front; index <= _rear; index++)
            {
                if (EqualityComparer<T>.Default.Equals(item, _array[index])) return true;
            }
            return false;
        }

        //처음 위치 요소 출력 메서드
        public T DequeueFront()
        {
            IsEmptyDeque();
            return Dequeue(_front);
        }

        //마지막 위치 요소 출력 메서드
        public T DequeueRear()
        {
            IsEmptyDeque();
            return Dequeue(_rear);
        }

        //처음 위치 해당 요소 삽입 메서드
        public void EnqueueFront(T item)
        {
            if (_count == 0)
            {
                AddItem(_front, item);
                return;
            }
            if (_count == _array.Length)
            {
                if(!Resize(_count + 1, Side.Front)) { return; }
            }
            int newFront = _front - 1;
            if (newFront < 0) 
            { 
                MoveToRear();
            }
            else
            {
                _front = newFront;
            }
            AddItem(_front, item);
        }

        //마지막 위치 해당 요소 삽입 메서드
        public void EnqueueRear(T item)
        {
            if (_count == 0)
            {
                AddItem(_rear, item);
                return;
            }
            if (_count == _array.Length)
            {
                if (!Resize(_count + 1, Side.Rear)) { return; }
            }
            int newRear = _rear + 1;
            if (newRear >= _array.Length)
            {
                MoveToFront();
            }
            else
            {
                _rear = newRear;
            }
            AddItem(_rear, item);
        }

        //처음 위치 요소 확인 메서드
        public T PeekFront()
        {
            IsEmptyDeque();
            return _array[_front];
        }

        //마지막 위치 요소 확인 메서드
        public T PeekRear()
        {
            IsEmptyDeque();
            return _array[_rear];
        }

        //덱(데크) 요소 프린트 메서드
        public void Print()
        {
            foreach (T item in _array)
            {
                if (EqualityComparer<T>.Default.Equals(item, default))
                {
                    Console.Write($"[null]");
                }
                else
                {
                    Console.Write($"[{item}]");
                }
            }
            Console.WriteLine();
        }

        //덱(데크) 배열 반환 메서드
        public T[] ToArray()
        {
            T[] newArray = new T[_count];
            Array.Copy(_array, _front, newArray, 0, _count);
            return newArray;
        }

        //덱(데크) 여백 제거 메서드
        public T[] TrimToSize()
        {
            T[] newArray = new T[_count];
            Array.Copy(_array, _front, newArray, 0, _count);
            return newArray;
        }

        //처음 위치 요소 출력 가능 여부 메서드(참조 인자 출력)
        public bool TryDequeueFront(out T result)
        {
            return TryDequeue(_front, out result);
        }

        //마지막 위치 요소 출력 가능 여부 메서드(참조 인자 출력)
        public bool TryDequeueRear(out T result)
        {
            return TryDequeue(_rear, out result);
        }

        //처음 위치 요소 확인 가능 여부 메서드(참조 인자 출력)
        public bool TryPeekFront(out T result)
        {
            return TryPeek(_front, out result);
        }

        //마지막 위치 요소 확인 가능 여부 메서드(참조 인자 출력)
        public bool TryPeekRear(out T result)
        {
            return TryPeek(_rear, out result);
        }
        #endregion

        #region PrivateMethods
        //해당 위치 해당 요소 삽입 메서드
        private void AddItem(int index, T item)
        {
            _array[index] = item;
            _count++;
        }

        //해당 위치 요소 출력 메서드
        private T Dequeue(int index)
        {
            T item = _array[index];
            _array[index] = default!;
            if (index == _front) { _front++; }
            else if (index == _rear) { _rear--; }
            _count--;
            return item;
        }

        //덱(데크) 배열 요소 한칸 앞 이동 메서드
        private void MoveToFront()
        {
            Array.Copy(_array, _front, _array, _front - 1, _count);
            _array[_rear] = default!;
            _front--;
        }

        //덱(데크) 배열 요소 한칸 뒤 이동 메서드
        private void MoveToRear()
        {
            Array.Copy(_array, _front, _array, _front + 1, _count);
            _array[_front] = default!;
            _rear++;
        }

        //덱(데크) 배열 크기 조정 메서드
        private bool Resize(int length, Side side)
        {
            if (_array.Length == Array.MaxLength)
            {
                Console.WriteLine("Array length is already max.");
                return false;
            }

            int newCapacity = _array.Length;
            do
            {
                newCapacity *= 2;
                if (newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;
            }
            while (newCapacity < length);
            T[] newArray = new T[newCapacity];
            int newFront = newFront = ((newArray.Length - _array.Length) / 2);
            if (side == Side.Front) { newFront++; }
            Array.Copy(_array, _front, newArray, newFront, _count);
            _array = newArray;
            _front = newFront;
            _rear = _front + (_count - 1);
            return true;
        }

        //해당 위치 요소 출력 가능 여부 메서드(참조 인자 출력)
        private bool TryDequeue(int index, out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = Dequeue(index);
            return true;
        }

        //해당 위치 요소 확인 가능 여부 메서드(참조 인자 출력)
        private bool TryPeek(int index, out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = _array[index];
            return true;
        }
        #endregion

        #region ExceptionMethods
        //빈 덱(데크) 확인 메서드
        private void IsEmptyDeque()
        {
            try
            {
                if (_count == 0) throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Queue is empty.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }
}
