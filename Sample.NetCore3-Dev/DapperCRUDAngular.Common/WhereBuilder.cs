using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DapperCRUDAngular.Common
{
    public static class WhereBuilder
    {
        public static string ToSql<T>(this Expression<Func<T, bool>> expression)
        {
            return Recurse(expression.Body, true);
        }

        private static string Recurse(Expression expression, bool isUnary = false, bool quote = true)
        {
            if (expression is UnaryExpression)
            {
                var unary = (UnaryExpression)expression;
                var right = Recurse(unary.Operand, unary.IsLiftedToNull ? false : true);
                return "(" + NodeTypeToString(unary.NodeType, right == "NULL") + " " + right + ")";
            }
            if (expression is BinaryExpression)
            {
                var body = (BinaryExpression)expression;
                var right = Recurse(body.Right);

                if (!body.Left.ToString().StartsWith("value(") &&
                    body.Right.ToString().StartsWith("value(") && (right == "" || right == "NULL"))
                    return "";
                else
                {
                    var left = Recurse(body.Left);
                    if (left == "")
                        return right;
                    else if (right == "")
                        return left;
                    else return "(" + left + " " + NodeTypeToString(body.NodeType, right == "NULL") + " " + right + ")";
                }
                //var left = Recurse(body.Left);
                //if (left !="" && left != "NULL")
                //{
                //    var right = Recurse(body.Right);

                //    if (body.Left.ToString().StartsWith("(value"))
                //        return right;
                //    else if(right=="")
                //        return left;
                //    else return "(" + left + " " + NodeTypeToString(body.NodeType, right == "NULL") + " " + right + ")";
                //}
                //else return "";
            }
            if (expression is ConstantExpression)
            {
                var constant = (ConstantExpression)expression;
                return ValueToString(constant.Value, isUnary, quote);
            }
            if (expression is MemberExpression)
            {
                var member = (MemberExpression)expression;

                if (expression.ToString().StartsWith("value(") || member.ToString().StartsWith("DateTime."))
                    return ValueToString(GetValue(member), false, quote);

                if (member.Member is PropertyInfo)
                {
                    var property = (PropertyInfo)member.Member;

                    var colName = property.Name;
                    if (isUnary && member.Type == typeof(bool))
                    {
                        return "([" + colName + "] = 1)";
                    }
                    return "[" + colName + "]";
                }
                if (member.Member is FieldInfo)
                {
                    return ValueToString(GetValue(member), isUnary, quote);
                }
                throw new Exception($"Expression does not refer to a property or field: {expression}");
            }
            if (expression is MethodCallExpression)
            {
                var methodCall = (MethodCallExpression)expression;
                // LIKE queries:
                if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
                {
                    string propertyValue = Recurse(methodCall.Arguments[0], quote: false);

                    if (!string.IsNullOrEmpty(propertyValue) && propertyValue.ToUpper() != "NULL")
                        return "(" + Recurse(methodCall.Object) + " LIKE '%" + propertyValue + "%')";
                    else return "";
                }
                if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
                {
                    return "(" + Recurse(methodCall.Object) + " LIKE '" + Recurse(methodCall.Arguments[0], quote: false) + "%')";
                }
                if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
                {
                    return "(" + Recurse(methodCall.Object) + " LIKE '%" + Recurse(methodCall.Arguments[0], quote: false) + "')";
                }
                //if (methodCall.Method == typeof(string).GetMethod("IsNullOrWhiteSpace", new[] { typeof(string) }))
                //{
                //    var argumentValue = GetValue(methodCall.Arguments[0]);
                //    return ValueToString(argumentValue==null ? true : !String.IsNullOrWhiteSpace(argumentValue.ToString()), true, false);
                //}

                // IN queries:
                if (methodCall.Method.Name == "Contains")
                {
                    Expression collection;
                    Expression property;
                    if (methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 2)
                    {
                        collection = methodCall.Arguments[0];
                        property = methodCall.Arguments[1];
                    }
                    else if (!methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 1)
                    {
                        collection = methodCall.Object;
                        property = methodCall.Arguments[0];
                    }
                    else
                    {
                        throw new Exception("Unsupported method call: " + methodCall.Method.Name);
                    }
                    var values = (IEnumerable<object>)GetValue(collection);
                    var concated = "";
                    foreach (var e in values)
                    {
                        concated += ValueToString(e, false, true) + ", ";
                    }
                    if (concated == "")
                    {
                        return ValueToString(false, true, false);
                    }
                    return "(" + Recurse(property) + " IN (" + concated.Substring(0, concated.Length - 2) + "))";
                }
                throw new Exception("Unsupported method call: " + methodCall.Method.Name);
            }
            throw new Exception("Unsupported expression: " + expression.GetType().Name);
        }

        public static string ValueToString(object value, bool isUnary, bool quote)
        {
            if (value is bool)
            {
                if (isUnary)
                {
                    return (bool)value ? "(1=1)" : "(1=0)";
                }
                return (bool)value ? "1" : "0";
            }
            if(value != null && value.GetType() == typeof(DateTime))
            {
                return value == null ? "NULL" : (quote ? "'" + DateTime.Parse(value.ToString()).ToString("MM/dd/yyyy") + "'" : DateTime.Parse(value.ToString()).ToString("MM/dd/yyyy"));
            }
            return value == null ? "NULL" : (quote ? "'" + value.ToString() + "'" : value.ToString());
        }

        private static bool IsEnumerableType(Type type)
        {
            return type
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        private static object GetValue(Expression member)
        {
            if (member.Type.BaseType == typeof(System.Enum))
            {
                var objectMember = Expression.Convert(member, typeof(int));
                var getterLambda = Expression.Lambda<Func<int>>(objectMember);
                var getter = getterLambda.Compile();
                return getter();
            }
            else
            {
                // source: http://stackoverflow.com/a/2616980/291955
                var objectMember = Expression.Convert(member, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                var getter = getterLambda.Compile();
                return getter();
            }
        }

        private static object NodeTypeToString(ExpressionType nodeType, bool rightIsNull)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Equal:
                    return rightIsNull ? "IS" : "=";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Negate:
                    return "-";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Convert:
                    return "";
            }
            throw new Exception($"Unsupported node type: {nodeType}");
        }
    }
}
