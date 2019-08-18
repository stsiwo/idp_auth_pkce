using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Command
{
    public class CreateCartCommand : IRequest<int>
    {
        public string SampleField { get; set; }
    }
}
