using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Utils
{
    public static class PropertyUtil
    {
        public static string GetPropertyDisplayOrNameFor<TObject>(this TObject type,
                                                       Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        public static string GetDisplayOrNameFor<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        private static string GetPropertyNameCore(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                MemberInfo propertyMember = memberExpr.Member;

                Object[] displayAttributes = propertyMember.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (displayAttributes.Length == 1)
                    return ((DisplayAttribute)displayAttributes[0]).Name;

                return propertyMember.Name;
            }

            throw new ArgumentException("No property reference expression was found.",
                             "propertyRefExpr");
        }
    }
}
