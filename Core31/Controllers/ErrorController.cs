using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core31.Controllers
{
  public class ErrorController : Controller
  {
    [Route("error/{code:int}")]
    public IActionResult Index(int code)
    {
      return Content(code.ToString());
    }
  }
}