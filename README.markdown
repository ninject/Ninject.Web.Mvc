This extension allows integration between the [Ninject core](http://github.com/ninject/ninject/)
and [ASP.NET MVC](http://www.asp.net/mvc/) projects. To use it, just make your HttpApplication
(typically in Global.asax.cs) extend NinjectHttpApplication:

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

Once you do this, your controllers will be activated via Ninject, meaning you can expose dependencies on
their constructors (or properties, or methods) to request injections.