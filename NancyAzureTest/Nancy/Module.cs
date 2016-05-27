using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Hosting.Aspnet;
namespace NancyAzureTest
{
  public class Home : NancyModule
  {
    public Home()
    {
      Get["/"] = _ => View["index"];;
    }
  }
}