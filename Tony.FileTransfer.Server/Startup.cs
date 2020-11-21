using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tony.FileTransfer.Server.DB;
using Tony.FileTransfer.Server.Services;

namespace Tony.FileTransfer.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options=>
            {
                options.MaxReceiveMessageSize = int.MaxValue;
                options.MaxSendMessageSize = int.MaxValue;
            });
           
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TonyFileTranster.db");
            //services.AddDbContext<ServerDBContext>(options => options.UseSqlite($"Data Source={fileName}"));
            services.AddSingleton<Func<ServerDBContext>>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ServerDBContext>();
                optionsBuilder.UseSqlite($"Data Source={fileName}");
                //如果有其他依赖的话，可以通过provider.GetService<XX>()来获取
                return new ServerDBContext(optionsBuilder.Options);
            });

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddLogging(loggerBuilder=>
            {
                loggerBuilder.AddConfiguration(config.GetSection("Logging"));
                loggerBuilder.AddConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<FileUploadService>();
                endpoints.MapGrpcService<UserService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
