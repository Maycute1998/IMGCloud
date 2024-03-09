using IMGCloud.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IMGCloud.API.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public virtual IActionResult ExecuteResult<TOption>(TOption option)
        {
            return new ObjectResult(option);
        }

        [NonAction]
        public virtual IActionResult ExecuteResult(bool Status, string Message)
        {
            var Result = new BaseOption();
            try
            {
                if (Status)
                {
                    Result.Data = null;
                    Result.Status = Status;
                    Result.Message = Message;
                    Result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    Result.Data = null;
                    Result.Status = Status;
                    Result.Message = Message;
                    Result.StatusCode = HttpStatusCode.InternalServerError;
                }
                return new ObjectResult(Result);
            }
            catch (Exception ex)
            {
                Result.Data = null;
                Result.Status = false;
                Result.Message = string.Concat("Bad request", ex.Message);
                Result.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(Result);
            }
        }
    }
}
