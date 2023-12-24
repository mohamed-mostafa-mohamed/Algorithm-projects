using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{ 
    public static class PlagiarismChecking
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given an UNDIRECTED Graph of matching pairs and a query pair, find the min number of connections between the nodes of the given pair (if any)
        /// </summary>
        /// <param name="edges">array of matching pairs</param>
        /// <param name="query">query pair</param>
        /// <returns>min number of connections between the nodes of the query pair (if any)</returns>
     public static   Dictionary<string, int> state = new Dictionary<string, int>();
     public static   Dictionary<string, int>queryDistance = new Dictionary<string, int>();
     public static string firstNode;
     public static string lastNode;
        public static int CheckPlagiarism(Tuple<string, string>[] edges, Tuple<string, string> query)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();

            //Dictionary<string, List<string>> graph = initializeGraph(edges,query);
            /*    bool flag= findDirectQuery(edges, query);
                  if (flag == true)
                      return 1;
                  else
                    return BFS_initalize_search(graph, query);
            */

      
            Dictionary<string, List<string>> graph = initializeGraph(edges, query);
            if (NotExistQuery(graph, query))
                return 0;
            Task<bool> findDirectQueryP = Task.Factory.StartNew(() => findDirectQuery(edges, query));
            Task<int> BFS_searchP = Task.Factory.StartNew(() => BFS_search(graph, query));
            if (findDirectQueryP.Result)
                return 1;
            else 
                return BFS_searchP.Result;
        }
        public static int BFS_search(Dictionary<string, List<string>> g, Tuple<string, string> q)
        {

            // Dictionary<string, int> state = new Dictionary<string, int>();
            //   Dictionary<string, int> range = new Dictionary<string, int>();


            /*   foreach (string k in g.Keys)
                  {

                    state[k] = -1;
                    // white not discoverd
                      range[k] = -1;

                  }
              */


            // state[firstNode] = 0;
            //gray discoverd
            // queryDistance[firstNode] = 0;
            // NotExistQuery(g, q);
           
            

            Queue<string> Q = new Queue<string>();
            firstNode = q.Item1;
            lastNode = q.Item2;
            Q.Enqueue(firstNode);
              while (Q.Any())
              {
                  string currentNode = Q.Dequeue();

                if (currentNode != lastNode)  
                {
                    foreach (string neighbor in g[currentNode])
                    {
                        if (state[neighbor] == -1)
                        //white
                        {
                            
                            state[neighbor] = 0;
                            // gray
                            queryDistance[neighbor] = queryDistance[currentNode] + 1;
                            Q.Enqueue(neighbor);
                     }
                        else
                            continue;
                    }

                   state[currentNode] = 1;
                    //black fully explored
                }
                else
                    return queryDistance[currentNode];
            }
              
                
                return 0; 
        }
        public static bool findDirectQuery(Tuple<string, string>[] e, Tuple<string, string> q) {
            firstNode = q.Item1;
            lastNode = q.Item2;
            for (int i = 0; i < e.Length; i++)
            {
                if ((firstNode == e[i].Item2 && lastNode == e[i].Item1) || firstNode == e[i].Item1 && lastNode == e[i].Item2)
                    return true;
            }
            return false;
            }
        public static Dictionary<string, List<string>> initializeGraph(Tuple<string, string>[] e, Tuple<string, string> q)
        {
            Dictionary<string, List<string>> g = new Dictionary<string, List<string>>();
            firstNode = q.Item1;
           // lastNode = q.Item2;
            for(int i=0;i<e.Length;i++)
            { 
               if (!g.ContainsKey(e[i].Item1))
                { 
                    state[e[i].Item1] = -1;
                    //white not discoverd
                    queryDistance[e[i].Item1] = -1;
                    g[e[i].Item1] = new List<string>();
                }
                if (!g.ContainsKey(e[i].Item2))
                { 
                    state[e[i].Item2] = -1;
                    //white not discoverd
                    queryDistance[e[i].Item2] = -1;
                    g[e[i].Item2] = new List<string>();
                }
                g[e[i].Item1].Add(e[i].Item2);
                g[e[i].Item2].Add(e[i].Item1);  
            }
            state[firstNode] = 0;
            //gray discoverd
            queryDistance[firstNode] = 0;

            return g;
        }
        public static bool NotExistQuery(Dictionary<string, List<string>> g, Tuple<string, string> q)
        {
            firstNode = q.Item1;
            lastNode = q.Item2;
            if (!g.ContainsKey(firstNode) || !g.ContainsKey(lastNode))
                return true;
            else
                return false;
        }
      

    #endregion
}
}
