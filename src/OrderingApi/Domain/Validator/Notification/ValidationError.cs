using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Notification
{
    public struct ValidationError
    {
        public readonly string Message;

        public readonly string Field;

        public ValidationError(string message, string field)
        {
            Message = message;
            Field = field;
        }

        public override string ToString()
        {
            return string.Format("({0}) - {1}", Field, Message); 
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ValidationError)) return false;
            return Equals((ValidationError)obj); 
        }

        public bool Equals(ValidationError obj)
        {
            return Equals(obj.Message, Message) && Equals(obj.Field, Field);
        }

        // need to review: i don't know how this works
        public override int GetHashCode()
        {
            return (Message.GetHashCode() * 397) ^ Field.GetHashCode(); 
        }

        public static bool operator ==(ValidationError left, ValidationError right) => left.Equals(right);

        public static bool operator !=(ValidationError left, ValidationError right) => !left.Equals(right);
    }
}
