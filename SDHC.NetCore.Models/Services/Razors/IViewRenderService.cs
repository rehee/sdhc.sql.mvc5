﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace System
{
  public interface IViewRenderService
  {
    Task<string> RenderToStringAsync(string viewName, object model, RouteData routeData = null, ActionDescriptor action = null, ActionContext context = null);
  }

  public class ViewRenderService : IViewRenderService
  {
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IServiceProvider _serviceProvider;

    public ViewRenderService(IRazorViewEngine razorViewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider)
    {
      _razorViewEngine = razorViewEngine;
      _tempDataProvider = tempDataProvider;
      _serviceProvider = serviceProvider;
    }

    public async Task<string> RenderToStringAsync(string viewName, object model, RouteData routeData = null, ActionDescriptor action = null, ActionContext context = null)
    {
      var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
      var actionContext = context ?? new ActionContext(httpContext, routeData ?? new RouteData(), action ?? new ActionDescriptor());

      using (var sw = new StringWriter())
      {
        var viewResult = FindView(actionContext, viewName);

        if (viewResult == null)
        {
          throw new ArgumentNullException($"{viewName} does not match any available view");
        }

        var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
          Model = model
        };

        var viewContext = new ViewContext(
            actionContext,
            viewResult,
            viewDictionary,
            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
            sw,
            new HtmlHelperOptions()
        );
        try
        {
          await viewResult.RenderAsync(viewContext);

        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
        return sw.ToString();
      }
    }

    private IView FindView(ActionContext actionContext, string viewName)
    {
      var getViewResult = _razorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
      if (getViewResult.Success)
      {
        return getViewResult.View;
      }

      var findViewResult = _razorViewEngine.FindView(actionContext, viewName, isMainPage: true);
      if (findViewResult.Success)
      {
        return findViewResult.View;
      }

      var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
      var errorMessage = string.Join(
          Environment.NewLine,
          new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

      throw new InvalidOperationException(errorMessage);
    }
  }
}