namespace ShortestPath
{
    class FloydWarshall
    {
        private const int INFINITY = int.MaxValue;
        private bool result;
        private readonly int[,] _graph;
        private readonly int _vertexCount;

        public FloydWarshall(int[,] graph)
        {
            GraphStaticClass.CheckGraph(graph);
            result = false;
            _graph = CreateGraph(graph);
            _vertexCount = _graph.GetLength(0);
        }

        //그래프 출력 메서드
        public void Print()
        {
            int count = 0;
            foreach (var graph in _graph)
            {
                Console.Write($"{graph} ");
                count++;
                if (count >= _graph.GetLength(1))
                {
                    count = 0;
                    Console.WriteLine();
                }
            }
        }

        //정점 사이 최소 비용 출력 메서드
        public void Result(int start, int end)
        {
            OutOfRangeException(start);
            OutOfRangeException(end);
            if (!result) { Run(); }
            Console.WriteLine($"[Floyd_Warshall] {start} to {end} shortest distance : {_graph[start,end]}");
        }

        //플로이드 워셜 알고리즘 실행 메서드 
        public void Run()
        {
            if (result) { return; }
            for (int pass = 0; pass < _vertexCount; pass++)
            {
                for (int start = 0; start < _vertexCount; start++)
                {
                    for (int end = 0; end < _vertexCount; end++)
                    {
                        if (_graph[start, pass] == INFINITY || _graph[pass, end] == INFINITY) { continue; }
                        if (_graph[start, end] > _graph[start, pass] + _graph[pass, end])
                        {
                            _graph[start, end] = _graph[start, pass] + _graph[pass, end];
                        }
                    }
                }
            }
            result = true;
        }

        //배열 범위 확인 메서드
        private void OutOfRangeException(int value)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, _vertexCount);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"{value} is out of range.");
                Environment.Exit(-1);
            }
        }

        //플로이드 워셜 그래프 생성 메서드
        private static int[,] CreateGraph(int[,] graph)
        {
            int[,] newGraph = new int[graph.GetLength(0), graph.GetLength(1)];
            for (int row = 0; row < graph.GetLength(0); row++)
            {
                for (int col = 0; col < graph.GetLength(1); col++)
                {
                    if (row == col)
                    {
                        newGraph[row, col] = 0;
                    }
                    else if (graph[row, col] == GraphData.NULL)
                    {
                        newGraph[row, col] = INFINITY;
                    }
                    else
                    {
                        newGraph[row, col] = graph[row, col];
                    }
                }
            }
            return newGraph;
        }
    }
}
