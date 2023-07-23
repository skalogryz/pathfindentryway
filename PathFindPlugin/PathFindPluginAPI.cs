using System;
using System.Collections.Generic;
using System.Text;

namespace PathFindPlugin
{
    /*

   * https://gamedev.ru/flame/forum/?id=277957&page=15&m=5774369#m220
     {
    "maze": [
      0, 1, 0, 0, 0, 0, 0,
      0, 1, 0, 1, 0, 0, 0,
      0, 1, 0, 1, 1, 1, 0,
      0, 1, 0, 1, 0, 0, 0,
      0, 1, 0, 1, 0, 0, 0,
      0, 1, 0, 1, 0, 0, 0,
      0, 1, 0, 1, 0, 1, 1,
      0, 0, 0, 1, 0, 0, 0
    ],
    "width": 7,
    "height": 8,
    "from": {"x":0, "y":0},
    "to": {"x":7, "y":6}
    }
     */
    // запрос 
    public class PathFindAPIRequest
    {
        public int[] maze { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Coord from { get; set; }
        public Coord to { get; set; }

        public PathFindData ToFindData()
        {
            PathFindData result = new PathFindData();
            result.Start = from;
            result.End = to;
            result.Field = maze;
            result.Width = width;
            result.Height = height;
            return result;
        }
    }

    // результат обработки 

    public class PathFindAPIResult
    {
        public PathFindAPIRequest request { get; set; }
        public int[] path { get; set; }

        // processing time
        public double time { get; set; }

        public string error { get; set; }

        public void SetError(string error)
        {
            this.error = error;
        }

        public void AddPathResult(PathFindResult res)
        {
            if (res == null) return;
            if (res.Path == null) return;

            List<int> r = new List<int>();
            foreach (Coord c in res.Path)
            {
                r.Add(c.x);
                r.Add(c.y);
            }
            path = r.ToArray();
        }
    }

}
