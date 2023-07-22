using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindPlugin;

namespace pathfind
{

    public class Plugin
    {
        public IPathFindAlg alg;
        public string name { get; set; }
        public string version { get; set; }
        public string url { get; set; }
    }

    public class PluginsList
    {
        public Plugin[] plugins { get; set; }


        public static Dictionary<string, Plugin> Main = new Dictionary<string, Plugin>(StringComparer.InvariantCultureIgnoreCase);
        public static void Register(IPathFindAlg alg, string name)
        {
            if (alg == null) return;
            if (string.IsNullOrEmpty(name)) return;

            Plugin pl = new Plugin();
            pl.alg = alg;
            pl.name = name;
            Main[name] = pl;
        }
    }

}
