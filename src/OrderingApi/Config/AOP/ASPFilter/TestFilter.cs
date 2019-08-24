using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OrderingApi.Config.AOP.ASPFilter.HateoasHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AOP.ASPFilter
{
    public class TestFilter : IResultFilter
    {
        private IHateoasHeader _hateoasHeader;

        public TestFilter(IHateoasHeader hateoasHeader)
        {
            _hateoasHeader = hateoasHeader;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("links", JsonConvert.SerializeObject(_hateoasHeader.GetLinks()));
        }
    }
}
