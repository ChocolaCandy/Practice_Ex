namespace ShortestPath
{
    public class Dijkstra
    {
        private readonly int[,] _graph;
        private readonly int[] _distance;
        private readonly bool[] _visited;

        public Dijkstra(int[,] graph)
        {
            CheckGraph(graph);
            _graph = (int[,])graph.Clone();
            _visited = Enumerable.Repeat(false, graph.GetLength(0)).ToArray();
            _distance = Enumerable.Repeat(int.MaxValue, graph.GetLength(0)).ToArray();
        }

        //다익스트라 알고리즘 실행 메서드
        public void Run(int start, int end)
        {
            int verticesCount = _visited.Length;
            OutOfRangeException(end, verticesCount);

            for (int vertex = 0; vertex < verticesCount; vertex++)
            {
                _distance[vertex] = _graph[start, vertex];
            }

            _visited[start] = true;

            for (int i = 0; i < verticesCount - 1; i++)
            {
                int visitVertex = ShortestVertex();
                _visited[visitVertex] = true;
                for (int j = 0; j < verticesCount; j++)
                {
                    if (_visited[j]) { continue; }
                    if (_distance[j] > _distance[visitVertex] + _graph[visitVertex, j])
                    {
                        _distance[j] = _distance[visitVertex] + _graph[visitVertex, j];
                    }
                }
            }

            Console.WriteLine($"{start} to {end} shortest distance : {_distance[end]}");
        }

        //방문하지 않은 최단경로 정점 반환 메서드
        private int ShortestVertex()
        {
            int shortestDistance = int.MaxValue;
            int shortestVertex = -1;
            for(int vertex = 0; vertex < _distance.Length; vertex++)
            {
                if (_visited[vertex]) { continue; }
                if(shortestDistance > _distance[vertex])
                {
                    shortestDistance = _distance[vertex];
                    shortestVertex = vertex;
                }
            }
            return shortestVertex;
        }

        //그래프 정상 여부 확인 메서드
        private static void CheckGraph(int[,] graph)
        {
            int rowLength = graph.GetLength(0);
            int colLength = graph.GetLength(1);
            try
            {
                if (rowLength != colLength) { throw new Exception(); }
                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = i; j < colLength; j++)
                    {
                        if (graph[i,j] != graph[j,i]) { throw new Exception(); }
                    }
                }
                Console.WriteLine("Input graph data successful.");
            }
            catch(Exception)
            {
                Console.WriteLine("Input graph data failed.");
                Environment.Exit(-1);
            }
        }

        //배열 범위 확인 메서드
        private static void OutOfRangeException(int value, int other)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, other);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("End index is out of range.");
                Environment.Exit(-1);
            }
        }
    }
}
