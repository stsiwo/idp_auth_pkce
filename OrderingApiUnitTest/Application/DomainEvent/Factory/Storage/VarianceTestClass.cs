using Autofac.Features.Indexed;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.CovarianceStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitVarianceTest.Application.DomainEvent.Factory.Storage
{
    class VarianceTestClass
    {
        private readonly ITHandler<SaveCommand> _tHandler; 

        public VarianceTestClass(ITHandler<SaveCommand> tHandler)
        {
            _tHandler = tHandler;
        }

        public string Test()
        {
            return "hey"; 
        }
    }
}
