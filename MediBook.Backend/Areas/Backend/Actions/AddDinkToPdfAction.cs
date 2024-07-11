using DinkToPdf;
using DinkToPdf.Contracts;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace MediBook.Backend
{
  public class AddDinkToPdfAction : IConfigureServicesAction
  {
    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

      var context = new CustomAssemblyLoadContext();
      context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "libwkhtmltox.dylib" : "libwkhtmltox.dll"));
    }

    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
      public IntPtr LoadUnmanagedLibrary(string absolutePath)
      {
        return LoadUnmanagedDll(absolutePath);
      }
      protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
      {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
      }

      protected override Assembly Load(AssemblyName assemblyName)
      {
        throw new NotImplementedException();
      }
    }
  }
}
