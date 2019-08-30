using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types
{
    public class GQLRequestQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        // don't use "Inputs" use "JObject" and convert to "Inputs" when assigning to Executor in Controller 
        public JObject Variables { get; set; }
    }
}
