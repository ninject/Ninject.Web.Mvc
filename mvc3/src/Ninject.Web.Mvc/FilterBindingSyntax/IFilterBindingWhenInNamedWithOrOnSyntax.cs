namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    public interface IFilterBindingWhenInNamedWithOrOnSyntax<out T> :
        IFilterBindingWhenSyntax<T>,
        IFilterBindingInSyntax<T>,
        IFilterBindingNamedSyntax<T>,
        IFilterBindingWithSyntax<T>,
        IFilterBindingOnSyntax<T>
    {
        
    }
}