# Ninject.Web.Mvc

[![Build status](https://ci.appveyor.com/api/projects/status/7af63sr9x1mwuhd8?svg=true)](https://ci.appveyor.com/project/Ninject/ninject-web-mvc)
[![NuGet Version](http://img.shields.io/nuget/v/Ninject.Mvc5.svg?style=flat)](https://www.nuget.org/packages/Ninject.Mvc5/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Ninject.Mvc5.svg?style=flat)](https://www.nuget.org/packages/Ninject.Mvc5/)

This extension allows integration between the [Ninject](http://github.com/ninject/ninject/)
and [ASP.NET MVC](http://www.asp.net/mvc/) projects. To use it, just make your HttpApplication
(typically in Global.asax.cs) extend NinjectHttpApplication:

```C#
public class YourWebApplication : NinjectHttpApplication
{
  public override void OnApplicationStarted()
  {
    // This is only needed in MVC1
    RegisterAllControllersIn("Some.Assembly.Name");
  }

  public override IKernel CreateKernel()
  {
    return new StandardKernel(new SomeModule(), new SomeOtherModule(), ...);
    
    // OR, to automatically load modules:
    
    var kernel = new StandardKernel();
    kernel.AutoLoadModules("~/bin");
    return kernel;
  }
}
```

Once you do this, your controllers will be activated via Ninject, meaning you can expose dependencies on
their constructors (or properties, or methods) to request injections.