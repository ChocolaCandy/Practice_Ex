namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Dijkstra dijkstra = new Dijkstra(GraphData.Graph_3);
            dijkstra.Run(0, 7);
        }   
    }
}

