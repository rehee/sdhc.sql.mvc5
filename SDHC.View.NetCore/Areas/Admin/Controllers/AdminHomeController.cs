using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core31
{
  [Area("Admin")]
  public class AdminHomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
