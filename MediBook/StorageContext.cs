//using System;
//using ExtCore.Data.EntityFramework;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace MediBook
//{
//  public class StorageContext : StorageContextBase
//  {
//    public StorageContext(IOptions<StorageContextOptions> options) : base(options) { }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//      base.OnConfiguring(optionsBuilder);

//      if (string.IsNullOrEmpty(this.MigrationsAssembly))
//        optionsBuilder.UseSqlServer(this.ConnectionString);

//      else optionsBuilder.UseSqlServer(
//        this.ConnectionString,
//        options =>
//        {
//          options.MigrationsAssembly(this.MigrationsAssembly);
//        }
//     );

//      optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
//    }
//  }
//}

