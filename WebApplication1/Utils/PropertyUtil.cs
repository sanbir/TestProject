using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Utils
{
    public static class PropertyUtil
    {
        public static string GetPropertyDisplayNameFor<TObject>(this TObject type,
                                                       Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameOrDisplayCore(propertyRefExpr.Body, true);
        }

        public static string GetDisplayNameFor<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameOrDisplayCore(propertyRefExpr.Body, true);
        }

        public static string GetPropertyNameFor<TObject>(this TObject type,
                                               Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameOrDisplayCore(propertyRefExpr.Body);
        }

        public static string GetNameFor<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameOrDisplayCore(propertyRefExpr.Body);
        }

        private static string GetPropertyNameOrDisplayCore(Expression propertyRefExpr, bool isDisplay = false)
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

            if (memberExpr == null || memberExpr.Member.MemberType != MemberTypes.Property)
                throw new ArgumentException("No property reference expression was found.", "propertyRefExpr");

            MemberInfo propertyMember = memberExpr.Member;

            if (isDisplay)
            {
                Object[] displayAttributes = propertyMember.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (displayAttributes.Length == 1)
                    return ((DisplayAttribute)displayAttributes[0]).Name;
            }

            return propertyMember.Name;
        }

    }
}
