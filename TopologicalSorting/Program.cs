namespace TopologicalSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            TopologicalSorting topologicalSorting = new TopologicalSorting(GraphData.Graph_3);
            topologicalSorting.Run();
        }
    }
}
