using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApiUnitTest.MediatR.TestEntity
{
    public class TestClass
    {
        private IMediator _mediator;

        public TestClass(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Dispatch()
        {
            _mediator.Publish(new DEvent1()); 
        }
    }
}
