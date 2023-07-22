using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindPlugin;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pathfind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathFindController : ControllerBase
    {
        // GET: api/<PathFindController>
        [HttpGet]
        //public IEnumerable<string> Get()
        public PluginsList Get()
        {
            // todo: enum plugins



            //return new string[] { "value1", "value2" };
            PluginsList pl = new PluginsList();
            pl.plugins = new Plugin[PluginsList.Main.Count];
            int i = 0;
            foreach(Plugin p in PluginsList.Main.Values)
            {
                pl.plugins[i] = p;
                i++;
            }
            return pl;
        }


        // POST api/<PathFindController>
        // PUT api/<PathFindController>/5
        [HttpPost("{id}")]
        [HttpPut("{id}")]
        public PathFindAPIResult AssessPath(string id, [FromBody] PathFindAPIRequest request)
        {
            PathFindAPIResult result = new PathFindAPIResult();

            result.request = request;
            IPathFindAlg alg = GetAlgorithm(id);
            if (alg == null)
            {
                result.SetError($"algorith {id} not found");
                return result;
            }
            PathFindData inp = request.ToFindData();
            PathFindResult findres = new PathFindResult();
            Stopwatch sw = new Stopwatch();
            try
            {
                alg.FindPath(inp, findres, sw);
                result.time = sw.Elapsed.TotalMilliseconds;
                result.AddPathResult(findres);
            } 
            catch(Exception x)
            {
                sw.Stop();
                result.SetError($"error file excuting the algorithm {id}: {x.Message} \r\n{x.StackTrace}");
            }
            result.time = sw.Elapsed.TotalMilliseconds;

            return result;
        }

        IPathFindAlg GetAlgorithm(string id)
        {
            Plugin p;
            if (!PluginsList.Main.TryGetValue(id, out p)) return null; 
            return p.alg;
        }


    }
}
