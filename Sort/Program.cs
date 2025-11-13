namespace Sort
{
    static class Sort
    {
        public static int[] SelectionSort(int[] array)
        {
            if (array == null || array.Length == 0) { return Array.Empty<int>(); }

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

        public static int[] InsertionSort(int[] array)
        {
            if (array == null || array.Length == 0) { return Array.Empty<int>(); }

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

        public static int[] BubbleSort(int[] array)
        {
            if (array == null || array.Length == 0) { return Array.Empty<int>(); }

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

        public static int[] ShellSort(int[] array)
        {
            if (array == null || array.Length == 0) { return Array.Empty<int>(); }

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

        public static int[] MergeSort(int[] array)
        {
            if (array == null || array.Length == 0) { return Array.Empty<int>(); }

            int[] sortArray = (int[])array.Clone();

            DivideAndMarge(0, sortArray.Length - 1);

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
            return sortArray;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
