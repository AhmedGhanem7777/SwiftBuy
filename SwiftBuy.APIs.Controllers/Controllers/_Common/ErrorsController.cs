using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Errors;

namespace SwiftBuy.APIs.Controllers.Controllers._Common
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            return code switch
            {
                404 => NotFound(new ApiResponse(404)),
                _ => StatusCode(code, new ApiResponse(code))
            };
        }
    }
}
