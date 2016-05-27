using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace NancyAzureTest.Nancy
{
  public class TestClass : NancyModule
  {
    public TestClass() : base("/api")
    {
      Get["/user"] = _ =>
      {
        return Response.AsJson("Test");
      };

      Get["/test"] = _ =>
      {
        List<int> li = new List<int>();

        for (var i = 0; i <= 1000000; i++)
        {
          li.Add(i);
        }
        return Response.AsJson(li);
      };
    }
  }
}