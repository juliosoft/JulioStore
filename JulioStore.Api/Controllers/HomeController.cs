using Microsoft.AspNetCore.Mvc;

namespace JulioStore.Api.Controllers
{
    
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public object Get()
        {
            return new { version = "0.0.2"};
        }
        
        [HttpGet]
        [Route("Error")]
        public string Error()
        {
            throw new System.Exception("Ocorreu algum erro");
            return "erro";    
        }
    }
}
