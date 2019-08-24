using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Notification
{
    public abstract class ValidationNotificationBase : IValidationNotification
    {
        private readonly IList<ValidationError> _validationNotification = new List<ValidationError>();

        public void Add(ValidationError validationError)
        {
            _validationNotification.Add(validationError);
        }

        public IEnumerable<ValidationError> GetErrors()
        {
            foreach (var error in _validationNotification)
            {
                yield return error;
            }
        }

        public bool HasValidationError()
        {
            return (_validationNotification.Count != 0);
        }
    }
}
