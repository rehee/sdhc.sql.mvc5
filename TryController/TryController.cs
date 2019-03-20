using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TryControllers.Main
{
  public class TryController : Controller
  {
    public ActionResult Home()
    {
      return Content("123");
    }
  }
}
