namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    using System;
    using System.Linq;

    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;

    public static class IConstructorArgumentSyntaxExtensions
    {
        public static AttributeValueSelector<TAttribute> FromActionAttribute<TAttribute>(this IConstructorArgumentSyntax syntax)
            where TAttribute : Attribute
        {
            return new AttributeValueSelector<TAttribute>(
                syntax.Context.Parameters.OfType<FilterContextParameter>().Single().ActionDescriptor // use existin impl
                .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().Single());
        }
    
        public static AttributeValueSelector<TAttribute> FromControllerAttribute<TAttribute>(this IConstructorArgumentSyntax syntax)
            where TAttribute : Attribute
        {
            return new AttributeValueSelector<TAttribute>(
                syntax.Context.Parameters.OfType<FilterContextParameter>().Single().ActionDescriptor.ControllerDescriptor
                .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().Single());
        }
    }

    public class AttributeValueSelector<TAttribute>
    {
        private readonly TAttribute attribute;

        public AttributeValueSelector(TAttribute attribute)
        {
            this.attribute = attribute;
        }

        public T SelectValue<T>(Func<TAttribute, T> valueSelector)
        {
            return valueSelector(this.attribute);
        }
    }
}