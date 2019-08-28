using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Instrumentation;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderingApi.Application.DTO;
using OrderingApi.Config.AOP.ASPFilter;
using OrderingApi.UI.GQL;
using OrderingApi.UI.GQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly GQLSchema _gQLSchema;

        private readonly IDocumentExecuter _documentExecuter;

        private readonly DataLoaderDocumentListener _dataLoaderDocumentListener;
        public GraphQLController(GQLSchema gQLSchema, IDocumentExecuter documentExecuter, DataLoaderDocumentListener dataLoaderDocumentListener, ILogger<GraphQLController> logger)
        {
            _gQLSchema = gQLSchema;
            _documentExecuter = documentExecuter;
            _dataLoaderDocumentListener = dataLoaderDocumentListener;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(GQLRequestQuery query)
        {
            var start = DateTime.UtcNow;

            var result = await _documentExecuter.ExecuteAsync(_ =>
            {
                _.Schema = _gQLSchema;
                _.Query = query.Query;
                _.Inputs = query.Variables;
                _.EnableMetrics = true;
                _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                _.Listeners.Add(_dataLoaderDocumentListener);
                // only dev env
                _.ExposeExceptions = true;

            }).ConfigureAwait(false);

            /**
             * if this causes error, schema and its type configuration are wrong
             **/
            //result.EnrichWithApolloTracing(start);

            _logger.LogDebug(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            //_log.LogDebug(JsonConvert.SerializeObject(result, Formatting.Indented));

            return Ok(result);
        }
    }
}
