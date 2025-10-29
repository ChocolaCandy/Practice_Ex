namespace Stack
{
    public class StackByArray<T>
    {
        #region Fields
        //스택 크기 기본값
        private const int DefaultSize = 4;
        //스택 배열
        private T[] _stackArray;
        //스택 요소 수
        private int _count;
        //스택 탑 인덱스
        private int _topIndex;
        #endregion

        #region Properties
        //스택 요소 수
        public int Count
        {
            get { return _count; }
        }

        //스택 크기
        public int Size
        {
            get { return _stackArray.Length; }
        }

        //스택 비어있는지 여부
        public bool IsEmpty
        {
            get { return _count == 0; }
        }

        //스택 다 차있는지 여부
        public bool IsFull
        {
            get { return _count == _stackArray.Length; }
        }
        #endregion

        #region Constructors
        public StackByArray()
        {
            _stackArray = Array.Empty<T>();
            _count = 0;
            _topIndex = -1;
        }

        public StackByArray(int size)
        {
            _stackArray = new T[size];
            _count = 0;
            _topIndex = -1;
        }

        public StackByArray(StackByArray<T> stack)
        {
            CheckArgumrntNull();
            _stackArray = new T[stack._stackArray.Length];
            for (int count = 0; count < stack._count; count++)
            {
                T value = stack._stackArray[count];
                _stackArray[count] = value;
            }
            _count = stack._count;
            _topIndex = stack._topIndex;

            void CheckArgumrntNull()
            {
                try
                {
                    ArgumentNullException.ThrowIfNull(stack);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Constructor Parameter StackByArray<T> cannot be null.");
                    Environment.Exit(-1);
                }
            }
        }
        #endregion

        #region Methods 
        //전체 요소 삭제 메서드
        public void Clear()
        {
            if (_count == 0) return;
            Array.Clear(_stackArray, 0, _count);
            _count = 0;
            _topIndex = -1;
        }

        //해당 요소 포함 여부 확인 메서드
        public bool Contains(T value)
        {
            if (_count == 0) return false;
            return Array.IndexOf(_stackArray, value) != -1;
        }

        //탑 위치 요소 반환 메서드
        public T Peak()
        {
            CheckIsEmpty();
            return _stackArray[_topIndex];
        }

        //탑 위치 요소 반환 여부 및 반환 메서드
        public bool TryPeek(out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = _stackArray[_topIndex];
            return true;
        }

        //탑 위치 요소 추출 메서드
        public T Pop()
        {
            CheckIsEmpty();
            T value = _stackArray[_topIndex];
            _stackArray[_topIndex] = default!;
            _count--;
            _topIndex--;
            return value;
        }

        //탑 위치 요소 추출 여부 및 추출 메서드
        public bool TryPop(out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = _stackArray[_topIndex];
            _stackArray[_topIndex] = default!;
            _count--;
            _topIndex--;
            return true;
        }

        //해당 요소 삽입 메서드
        public void Push(T value)
        {
            if (IsFull) Resize(_stackArray.Length + 1);
            _stackArray[_count] = value;
            _count++;
            _topIndex++;
        }

        //스택 크기 조정 메서드
        public void Resize(int size)
        {
            CheckLessThanCount();
            int newSize = size <= DefaultSize ? DefaultSize : _stackArray.Length * 2;
            if (newSize > Array.MaxLength) newSize = Array.MaxLength;
            while (newSize < size) { newSize *= 2; }
            if (_stackArray.Length == newSize) return;
            Array.Resize(ref _stackArray, newSize);

            void CheckLessThanCount()
            {
                try
                {
                    ArgumentOutOfRangeException.ThrowIfLessThan(size, _count);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The argument size of Resize() is smaller than the number of Stack elements.");
                    Environment.Exit(-1);
                }
            }
        }

        //스택 상태 출력 메서드
        public void State()
        {
            CheckIsEmpty();
            int MaxLength = 0;
            foreach (T item in _stackArray)
            {
                int length = item!.ToString()!.Length;
                MaxLength = int.Max(MaxLength, length);
            }

            Console.WriteLine(new string('-', MaxLength));
            for (int index = _topIndex; index > -1; index--)
            {
                Console.WriteLine(_stackArray[index]);
                Console.WriteLine(new string('-', MaxLength));
            }
        }

        //스택 요소들 배열 반환 메서드
        public T[] ToArray()
        {
            if (_count == 0) return Array.Empty<T>();
            T[] newArray = new T[_count];
            for (int count = 0; count < _count; count++)
            {
                newArray[count] = _stackArray[count];
            }
            return newArray;
        }
        #endregion

        #region Exception
        private void CheckIsEmpty()
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfZero(_count);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The stack is empty.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }
}