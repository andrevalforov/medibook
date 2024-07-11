using System;
using ExtCore.Data.Abstractions;
using ExtCore.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platformus.WebApplication.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace MediBook
{
  public class Startup
  {
    private IConfiguration configuration;
    private string extensionsPath;

    public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
      this.configuration = configuration;

      if (!string.IsNullOrEmpty(this.configuration["Extensions:Path"]))
        this.extensionsPath = hostingEnvironment.ContentRootPath + this.configuration["Extensions:Path"];
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<StorageContextOptions>(options =>
      {
        options.ConnectionString = this.configuration.GetConnectionString("Default");
      });

      services.AddPlatformus(this.extensionsPath);
    }

    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment hostingEnvironment)
    {
      if (hostingEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
      }

      //applicationBuilder.UseStatusCodePagesWithRedirects("~{0}.html");
      // applicationBuilder.UseMiddleware<ExceptionMiddleware>();

      //applicationBuilder.Use(async (context, next) =>
      //{
      //  if (context.Request.Path.Value.Contains("/ru/"))
      //    context.Response.Redirect(context.Request.Path.Value.Replace("/ru/", "/uk/"));

      //  await next();
      //});

      applicationBuilder.UsePlatformus();
    }
  }
}