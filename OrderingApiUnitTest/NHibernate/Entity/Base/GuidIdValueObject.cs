using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.Base
{
    public abstract class GuidIdValueObject : ValueObject
    {
        public virtual Guid Id { get; private set; }

        // for NHibernate (mandatory)
        public GuidIdValueObject()
        {

        }

        public GuidIdValueObject(Guid id)
        {
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}
