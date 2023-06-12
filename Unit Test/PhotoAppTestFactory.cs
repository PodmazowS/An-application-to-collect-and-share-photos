using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test
{
    public class PhotoAppTestFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        /* 
         
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<MongoDBContext>));//?
                services.Remove(dbContextDescriptor);
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                // services.AddSingleton<DbConnection>(container =>
                // {
                //     var connection = new SqliteConnection("Filename=:memory:");
                //     connection.Open();
                //     return connection;
                // });

                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<QuizDbContext>((container, options) =>
                    {
                        options.UseInMemoryDatabase("QuizTest").UseInternalServiceProvider(container);
                    });
            });
            builder.UseEnvironment("Development");
        }
        */
    }
}
