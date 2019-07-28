using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.CustomType
{
    class BaseIdUserType<T> : IUserType
        where T : GuidIdValueObject
    {
        public SqlType[] SqlTypes
        {
            get
            {
                //We store our Uri in a single column in the database that can contain a string
                SqlType[] types = new SqlType[1];
                types[0] = new SqlType(DbType.Guid);
                return types;
            }
        }

        public Type ReturnedType
        {
            get { return typeof(T); }
        } 

        public bool IsMutable
        {
            get { return false; }
        } 

        public object Assemble(object cached, object owner)
        {
            return cached; 
        }

        public object DeepCopy(object value)
        {
            if (value == null) return null;
            //return new T((Guid)value);
            // should avoid using Reflection because of performance overhead
            return (T)Activator.CreateInstance(typeof(T), (Guid)value);
        }

        public object Disassemble(object value)
        {
            return value; 
        }

        public new bool Equals(object x, object y)
        {
            //Uri implements Equals it self by comparing the Uri's based 
            // on value so we use this implementation
            if (x == null)
            {
                return false;
            }
            else
            {
                return x.Equals(y);
            }
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length == 0)
                throw new InvalidOperationException("Expected at least 1 column");

            if (rs.IsDBNull(rs.GetOrdinal(names[0])))
                return null;

            object value = rs[names[0]];

            return value;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                var uintValue = (T)value;
                ((IDataParameter)cmd.Parameters[index]).Value = (Guid)uintValue.Id;
            }
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
