namespace ShortestPath
{
    public static class GraphData
    {
        private const int NULL = 9999;

        public static readonly int[,] Graph_1 =
        {
            {0, 7, NULL, NULL, 3, 10, NULL },
            {7, 0, 4, 10, 2, 6, NULL },
            {NULL, 4, 0, 2, NULL, NULL, NULL },
            {NULL, 10, 2, 0, 11, 9, 4 },
            {3, 2, NULL, 11, 0, NULL, 5 },
            {10, 6, NULL, 9, NULL, 0, NULL },
            {NULL, NULL, NULL, 4, 5, NULL, 0 }
        };

        public static readonly int[,] Graph_2 = 
        {
            {0, 2, 5, 1, NULL, NULL },
            {2, 0, 3, 2, NULL, NULL },
            {5, 3, 0, 3, 1, 5 },
            {1, 2, 3, 0, 1, NULL },
            {NULL, NULL, 1, 1, 0, 2 },
            {NULL, NULL, 5, NULL, 2, 0 }
        };

        public static readonly int[,] Graph_3 =
        {
            {0, 3, 4, NULL, NULL, NULL, NULL, NULL },
            {3, 0, 5, 10, NULL, 9, NULL, NULL },
            {4, 5, 0, 8, 5, NULL, NULL, NULL },
            {NULL, 10, 8, 0, 6, 10, 7, 3 },
            {NULL, NULL, 5, 6, 0, NULL, 4, NULL },
            {NULL, 9, NULL, 10, NULL, 0, NULL , 2 },
            {NULL, NULL, NULL, 7, 4, NULL, 0, 5 },
            {NULL, NULL, NULL, 3, NULL, 2, 5, 0 },
        };
    }
}
