using Microsoft.AspNetCore.Mvc;

namespace HeadmanBot
{
    public class TestController:Controller 
    {
        [HttpGet]
        ActionResult Test()
        {
            return Ok();
        }
    }
}