namespace TopologicalSorting
{
    public class TopologicalSorting
    {
        private readonly int[,] _graph;
        private readonly List<int> _result;

        public TopologicalSorting(int[,] graph)
        {
            CheckInputGraph(graph);
            _graph = (int[,])graph.Clone();
            _result = new List<int>();
        }

        //알고리즘 수행 결과 출력 실행 메소드
        public void Run()
        {
            PrintResult(Sorting());
        }

        //결과 출력 메서드
        private void PrintResult(bool result)
        {
            if (result) { Console.WriteLine(string.Join(", ", _result)); }
            else { Console.WriteLine("Error : Run Failed."); }
        }

        //위상 정렬 실행 메서드
        private bool Sorting()
        {
            int[] _inDegree = CreateInDegreeGraph(_graph);
            CheckCycle(_inDegree);

            Queue<int> queue = new Queue<int>();

            for (int index = 0; index < _inDegree.Length; index++)
            {
                if (_inDegree[index] == 0) { queue.Enqueue(index); }
            }

            while (queue.TryDequeue(out int vertex))
            {
                _result.Add(vertex);

                for (int col = 0; col < _graph.GetLength(0); col++)
                {
                    if (_graph[vertex, col] > 0)
                    {
                        _inDegree[col] -= 1;
                        if (_inDegree[col] == 0) { queue.Enqueue(col); }
                        if (_inDegree[col] < 0) { return false; }
                    }
                }
            }

            foreach (int vertex in _inDegree)
            {
                if (vertex > 0) { return false; }
            }

            return true;
        }

        //진입 차수 그래프 사이클 확인 메서드
        private static void CheckCycle(int[] graph)
        {
            try
            {
                bool isCycle = true;
                foreach (int value in graph)
                {
                    if (value == 0) { isCycle = false; }
                }
                if (isCycle) { throw new Exception(); }
                Console.WriteLine("Check graph cycle : No");
            }
            catch(Exception)
            {
                Console.WriteLine("Check graph cycle : Yes");
                Environment.Exit(-1);
            }
        }

        //입력 그래프 정상 확인 메서드
        private static void CheckInputGraph(int[,] graph)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfZero(graph.Length);
                ArgumentOutOfRangeException.ThrowIfNotEqual(graph.GetLength(0), graph.GetLength(1));
                Console.WriteLine("Check input graph : Normal");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Check input graph : Abnormal");
                Environment.Exit(-1);
            }
        }

        //진입 차수 그래프 생성 메서드
        private static int[] CreateInDegreeGraph(int[,] graph)
        {
            int[] newGraph = new int[graph.GetLength(1)];
            for (int row = 0; row < graph.GetLength(1); row++)
            {
                int count = 0;
                for (int col = 0; col < graph.GetLength(0); col++)
                {
                    if (graph[col,row] > 0) { count++; }
                }
                newGraph[row] = count;
            }
            return newGraph;
        }
    }
}
