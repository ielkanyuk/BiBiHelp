using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BiBiHelp.Models
{
    public class jQTemplate<T>
    {
        public string GetPropName(Expression<Func<T, object>> property)
        {

            if (property.Body is MemberExpression)
                return (property.Body as MemberExpression).Member.Name;
            else if (property.Body is UnaryExpression)
            {
                return ((property.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            }
            else
                throw new Exception("Unknown type");
        }
        //public T Model
        //{
        //    get { return }
        //}
        public string GetPropType<T>(Expression<Func<T, object>> property)
        {

            if (property.Body is MemberExpression)
                return (property.Body as MemberExpression).Type.FullName;
            else if (property.Body is UnaryExpression)
            {
                return ((property.Body as UnaryExpression).Operand as MemberExpression).Type.FullName;
            }
            else
                throw new Exception("Unknown type");
        }
    }
}