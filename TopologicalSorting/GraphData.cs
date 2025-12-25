namespace TopologicalSorting
{
    public static class GraphData
    {
        public readonly static int[,] Graph_1 =
        {
            { 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0 },
        };

        public readonly static int[,] Graph_2 =
        {
            { 0, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1, 1 },
            { 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        };

        public readonly static int[,] Graph_3 =
        {
            { 0, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 1, 0 },
        };
    }
}
