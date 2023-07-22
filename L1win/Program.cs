using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using PathFindPlugin;
using L1PathFinderPlugin;

namespace L1win
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length  == 0)
            {
                Console.Error.WriteLine("please provide input file name");
                Environment.ExitCode = 1;
                return;
            }
            string s = File.ReadAllText(args[0]);
            PathFindAPIRequest request  = JsonConvert.DeserializeObject<PathFindAPIRequest>(s);

            PathFindAPIResult result = new PathFindAPIResult();

            result.request = request;
            IPathFindAlg alg = new L1PathFinderAlg();
            PathFindData inp = request.ToFindData();
            PathFindResult findres = new PathFindResult();
            Stopwatch sw = new Stopwatch();
            try
            {
                alg.FindPath(inp, findres, sw);
                result.time = sw.Elapsed.TotalMilliseconds;
                result.AddPathResult(findres);
            }
            catch (Exception x)
            {
                sw.Stop();
                result.SetError($"error while excuting: {x.Message} \r\n{x.StackTrace}");
            }
            result.time = sw.Elapsed.TotalMilliseconds;

            s = JsonConvert.SerializeObject(result);
            Console.Write(s);
        }
    }
}
