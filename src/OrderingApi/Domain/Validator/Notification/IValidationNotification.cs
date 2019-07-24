using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Notification
{
    public interface IValidationNotification
    {
        void Add(ValidationError validationError);

        bool HasValidationError();

        IEnumerable<ValidationError> GetErrors();
    }
}
