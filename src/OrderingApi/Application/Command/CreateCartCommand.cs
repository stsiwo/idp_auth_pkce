using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Command
{
    public class CreateCartCommand : IRequest<int>
    {
        public string SampleField { get; set; }

        public CreateCartCommand(string sample)
        {
            SampleField = sample;
        }
    }
}
