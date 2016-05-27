using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Json;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace NancyAzureTest
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat(@"Nancy/Views/", viewName));
      JsonSettings.MaxJsonLength = int.MaxValue;

    }

    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      base.ConfigureConventions(nancyConventions);

      nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("build"));
    }

    protected override NancyInternalConfiguration InternalConfiguration
    {
      get
      {
        return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = new[]
                {
                    typeof(ViewProcessor),
                    typeof(JsonProcessor),
                    typeof(XmlProcessor)
                });
      }
    }

   

    protected override IRootPathProvider RootPathProvider
    {
      get { return new SelfHostRootPathProvider(); }
    }

   

   
  }

  public class SelfHostRootPathProvider : IRootPathProvider
  {
    public string GetRootPath()
    {
      return StaticConfiguration.IsRunningDebug
          ? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."))
          : AppDomain.CurrentDomain.BaseDirectory;
    }
  }
}