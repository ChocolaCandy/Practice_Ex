namespace ShortestPath
{
    internal static class GraphStaticClass
    {
        //그래프 정상 여부 확인 메서드
        internal static void CheckGraph(int[,] graph)
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
                        if (graph[i, j] != graph[j, i]) { throw new Exception(); }
                    }
                }
                Console.WriteLine("Input graph data successful.");
            }
            catch (Exception)
            {
                Console.WriteLine("Input graph data failed.");
                Environment.Exit(-1);
            }
        }
    }
}
