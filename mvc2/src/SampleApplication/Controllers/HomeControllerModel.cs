namespace SampleApplication.Controllers
{
    public class HomeControllerModel : IHomeControllerModel
    {
        public string WelcomeMessage
        {
            get
            {
                return "Welcome to MVC";
            }
        }
    }
}