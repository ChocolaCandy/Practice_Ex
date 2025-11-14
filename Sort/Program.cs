namespace Sort
{
    static class Sort
    {
        //선택 정렬
        public static int[] SelectionSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();
            for (int targetIndex = 0; targetIndex < sortArray.Length - 1; targetIndex++)
            {
                int minValueIndex = targetIndex;
                for (int nextIndex = targetIndex + 1; nextIndex < sortArray.Length; nextIndex++)
                {
                    if (sortArray[targetIndex] > sortArray[nextIndex])
                    {
                        minValueIndex = nextIndex;
                    }
                }

                if (targetIndex != minValueIndex)
                {
                    (sortArray[targetIndex], sortArray[minValueIndex]) = (sortArray[minValueIndex], sortArray[targetIndex]);
                }
            }
            return sortArray;
        }

        //삽입 정렬
        public static int[] InsertionSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();
            for (int targetIndex = 1; targetIndex < sortArray.Length; targetIndex++)
            {
                int prevIndex, targetValue = sortArray[targetIndex];
                for (prevIndex = targetIndex - 1; prevIndex >= 0; prevIndex--)
                {
                    if (targetValue >= sortArray[prevIndex]) { break; }
                    sortArray[prevIndex + 1] = sortArray[prevIndex];
                }
                sortArray[prevIndex + 1] = targetValue;
            }
            return sortArray;
        }

        //버블 정렬
        public static int[] BubbleSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();
            for (int scan = 1; scan < sortArray.Length; scan++)
            {
                for (int targetIndex = 0; targetIndex < (sortArray.Length - scan); targetIndex++)
                {
                    if (sortArray[targetIndex] > sortArray[targetIndex + 1])
                    {
                        (sortArray[targetIndex], sortArray[targetIndex + 1]) = (sortArray[targetIndex + 1], sortArray[targetIndex]);
                    }
                }
            }
            return sortArray;
        }

        //쉘 정렬
        public static int[] ShellSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();

            for (int gap = (sortArray.Length / 2); gap > 0; gap = (gap / 2))
            {
                if ((gap % 2) == 0) { gap++; }

                for (int startIndex = 0; startIndex < gap; startIndex++)
                {
                    GapInsertionSort(startIndex, gap);
                }
            }
            return sortArray;

            void GapInsertionSort(int startIndex, int gap)
            {
                for (int targetIndex = startIndex + gap; targetIndex < sortArray.Length; targetIndex = targetIndex + gap)
                {
                    int prevIndex, targetValue = sortArray[targetIndex];
                    for (prevIndex = targetIndex - gap; prevIndex >= 0; prevIndex = prevIndex - gap)
                    {
                        if (targetValue > sortArray[prevIndex]) { break; }
                        sortArray[prevIndex + gap] = sortArray[prevIndex];
                    }
                    sortArray[prevIndex + gap] = targetValue;
                }
            }
        }

        //합병 정렬
        public static int[] MergeSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();

            DivideAndMarge(0, sortArray.Length - 1);
            return sortArray;

            void DivideAndMarge(int start, int end)
            {
                if (start >= end) { return; }
                int divideSize = (start + end) / 2;
                DivideAndMarge(start, divideSize);
                DivideAndMarge(divideSize + 1, end);
                Merge(start, divideSize, end);
            }

            void Merge(int startIndex, int divideSize, int endIndex)
            {
                int firstIndex = startIndex;
                int secondIndex = divideSize + 1;
                int insertIndex = 0;

                int[] newArray = new int[(endIndex - startIndex) + 1];

                while (firstIndex <= divideSize && secondIndex <= endIndex)
                {
                    if (sortArray[firstIndex] < sortArray[secondIndex])
                    {
                        newArray[insertIndex++] = sortArray[firstIndex++];
                    }
                    else
                    {
                        newArray[insertIndex++] = sortArray[secondIndex++];
                    }
                }
                while (firstIndex <= divideSize)
                {
                    newArray[insertIndex++] = sortArray[firstIndex++];
                }
                while (secondIndex <= endIndex)
                {
                    newArray[insertIndex++] = sortArray[secondIndex++];
                }
                Array.Copy(newArray, 0, sortArray, startIndex, newArray.Length);
            }
        }

        //퀵 정렬
        public static int[] QuickSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = (int[])array.Clone();

            QuickAndDivide(0, sortArray.Length - 1);
            return sortArray;

            void QuickAndDivide(int startIndex, int endIndex)
            {
                if (startIndex >= endIndex) { return; }

                int pivot = startIndex;
                int low = startIndex + 1, high = endIndex;
                while (low < high)
                {
                    while (sortArray[pivot] > sortArray[low] && low < endIndex)
                    {
                        low++;
                    }
                    while (sortArray[pivot] < sortArray[high] && high > startIndex)
                    {
                        high--;
                    }
                    if (low < high)
                    {
                        (sortArray[low], sortArray[high]) = (sortArray[high], sortArray[low]);
                    }
                }
                Divide(startIndex, endIndex, pivot, low, high);
            }

            void Divide(int startIndex, int endIndex, int pivot, int low, int high)
            {
                if (high == startIndex)
                {
                    QuickAndDivide(high + 1, endIndex);
                }
                else if (low == endIndex)
                {
                    (sortArray[pivot], sortArray[high]) = (sortArray[high], sortArray[pivot]);
                    QuickAndDivide(startIndex, high - 1);
                }
                else
                {
                    (sortArray[pivot], sortArray[high]) = (sortArray[high], sortArray[pivot]);
                    QuickAndDivide(startIndex, high - 1);
                    QuickAndDivide(high + 1, endIndex);
                }
            }
        }

        //힙 정렬
        public static int[] HeapSort(int[] array)
        {
            ArrayNullException(array);

            int[] sortArray = new int[array.Length];
            int count = 0;

            foreach (int i in array)
            {
                HeapInsert(i);
            }
            HeapSortinging();
            return sortArray;

            void HeapInsert(int a)
            {
                sortArray[count] = a;
                int compareIndex = count;
                while (compareIndex > 0)
                {
                    if (sortArray[(compareIndex - 1) / 2] < sortArray[compareIndex]) { break; }
                    (sortArray[(compareIndex - 1) / 2], sortArray[compareIndex]) = (sortArray[compareIndex], sortArray[(compareIndex - 1) / 2]);
                    compareIndex = (compareIndex - 1) / 2;
                }
                count++;
            }

            void HeapSortinging()
            {
                int index = 0;
                int[] tempArray = new int[array.Length];

                while (index < array.Length)
                {
                    tempArray[index] = sortArray[0];
                    (sortArray[0], sortArray[count - 1]) = (sortArray[count - 1], sortArray[0]);
                    sortArray[count - 1] = 0;
                    index++;
                    count--;

                    int parentIndex = 0;
                    while (parentIndex < count)
                    {
                        if (((2 * parentIndex) + 1) >= count) { break; }

                        int childIndex = 0;
                        if (((2 * parentIndex) + 1) < count && ((2 * parentIndex) + 2) < count)
                        {
                            childIndex = (sortArray[(2 * parentIndex) + 1] < sortArray[(2 * parentIndex) + 2]) ? (2 * parentIndex) + 1 : (2 * parentIndex) + 2;
                        }
                        else if (((2 * parentIndex) + 1) < count)
                        {
                            childIndex = (2 * parentIndex) + 1;
                        }

                        if (sortArray[childIndex] >= sortArray[parentIndex]) { break; }
                        (sortArray[parentIndex], sortArray[childIndex]) = (sortArray[childIndex], sortArray[parentIndex]);
                        parentIndex = childIndex;
                    }
                }
                sortArray = tempArray;
            }
        }

        //기수 정렬
        public static int[] RadixSort(int[] array)
        {
            ArrayNullException(array);

            Queue<int>[] stackArray = new Queue<int>[10];

            for (int i = 0; i < stackArray.Length; i++)
            {
                stackArray[i] = new Queue<int>();
            }

            int[] sortArray = (int[])array.Clone();
            int digit = 0;

            int maxNum = int.MinValue;
            foreach (int value in array)
            {
                maxNum = int.Max(maxNum, value);
            }
            string maxNumString = maxNum.ToString();
            int maxNumDigit = maxNumString.Length;

            while (digit < maxNumDigit)
            {
                foreach (int value in sortArray)
                {
                    stackArray[value / (int)MathF.Pow(10, digit) % 10].Enqueue(value);
                }

                int index = 0;
                foreach (Queue<int> queue in stackArray)
                {
                    while (queue.TryDequeue(out int value))
                    {
                        sortArray[index] = value;
                        index++;
                    }
                }
                digit++;
            }
            return sortArray;
        }

        private static void ArrayNullException(int[] array)
        {
            try
            {
                if (array == null || array.Length == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error : Array is null.");
                Environment.Exit(-1);
            }
        }
    }

    class Sort_EX
    {
        static void Main(string[] args)
        {
            int[] array = { 5, 3, 8, 4, 9, 1, 6, 2, 7 };
            int[] sortArray = Sort.RadixSort(array);
            Console.WriteLine(string.Join(", ", sortArray));
        }
    }
}
