using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.NHMapper.CustomType
{
    // Enum <=> string 
    public class RmqPublishMessageStatusCustomType : IUserType
    {
        public new bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            // appType (Object) => dbType (String)
            var xdocX = nameof(x);
            var xdocY = nameof(y);

            return xdocY == xdocX;
        }

        public int GetHashCode(object x)
        {
            if (x == null)
                return 0;

            return x.GetHashCode();
        }

        public object DeepCopy(object value)
        {
            if (value == null)
                return null;

            //Serialized and Deserialized using json.net so that I don't
            //have to mark the class as serializable. Most likely slower
            //but only done for convenience. 

            // appType => dbType
            var serialized = nameof(value);

            // dbType => appType
            MessageStatusConstants messageStatus;
            Enum.TryParse(serialized, out  messageStatus);

            return messageStatus;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            var str = cached as string;

            if (string.IsNullOrWhiteSpace(str))
                return null;

            // dbType => appType
            MessageStatusConstants messageStatus;
            Enum.TryParse(str, out  messageStatus);
            return messageStatus; 
        }

        public object Disassemble(object value)
        {
            if (value == null)
                return null;

            // appType => dbType
            return nameof(value); 
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length != 1)
                throw new InvalidOperationException("Only expecting one column...");

            var val = rs[names[0]] as string;

            if (val != null && !string.IsNullOrWhiteSpace(val))
            {
                // dbType => appType
                MessageStatusConstants messageStatus;
                Enum.TryParse(val, out messageStatus);
                return messageStatus;
            }

            return null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = (DbParameter)cmd.Parameters[index];

            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                // appType => dbType
                parameter.Value = nameof(value);
            }
        }

        public SqlType[] SqlTypes
        {
            get
            {
                return new SqlType[] { new StringSqlType() };
            }
        }

        public Type ReturnedType
        {
            get { return typeof(MessageStatusConstants); }
        }

        public bool IsMutable
        {
            get { return true; }
        }
    }
}
