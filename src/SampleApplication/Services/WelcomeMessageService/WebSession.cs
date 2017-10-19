namespace SampleApplication.Services.WelcomeMessageService
{
    using System.Web;

    public interface IWebSession
    {
    }

    public class WebSession : IWebSession
    {
        private readonly HttpSessionStateBase _httpSession;

        public WebSession(HttpSessionStateBase httpSession)
        {
            _httpSession = httpSession;
        }
    } 
}