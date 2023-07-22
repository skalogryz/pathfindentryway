using System;
using PathFindPlugin;
using L1PathFinder;
using System.Diagnostics;
using System.Collections.Generic;


namespace L1PathFinderPlugin
{
    public class L1PathFinderAlg : IPathFindAlg
    {
        public void FindPath(PathFindData input, PathFindResult result, Stopwatch sw)
        {
            int[,] grid = new int[input.Height, input.Width];
            int i = 0;
            for (int y = 0; y < input.Height; y++)
                for (int x = 0; x < input.Width; x++)
                {
                    grid[y, x] = input.Field[i];
                    i++;
                }

            var planner = L1PathPlanner.CreatePlanner(grid);
            Point start = new Point(input.Start.x, input.Start.y);
            Point target = new Point(input.End.x, input.End.y);

            sw.Start();
            var dist = planner.Search(target, start, out List<Point> path);
            sw.Stop();
            List<Coord> resPath = new List<Coord>();
            foreach (Point p in path)
                resPath.Add(new Coord(p.X, p.Y));
            result.Path = resPath;
        }
    }
}
