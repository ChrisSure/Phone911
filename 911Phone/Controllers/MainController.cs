using Microsoft.AspNetCore.Mvc;


namespace Phone.Controllers
{
    /// <summary>
    /// Main class of controllers
    /// <summary>
    public class MainController : ControllerBase
    {
        /// <summary>
        /// Method overload Base api url
        /// <summary>
        /// <returns>string</returns>
        public virtual string BaseApiUrl
        {
            get
            {
                var request = ControllerContext.HttpContext.Request;
                return request.Scheme + "://" + request.Host + request.Path;
            }
        }
    }
}
