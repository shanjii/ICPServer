using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UFCServer.Server;
using UFCServer.Utils;
using vJoy.Wrapper;

namespace UFCServer
{
    class MainApp
    {
        public void Startup()
        {
            SetVjoy();
            IHost Host = HostBuilder(Common.GetSettings().Port);

            Host.Start();
        }

        private void SetVjoy()
        {
            VirtualJoystick joystick = new(1);
            joystick.Aquire();
        }

        private IHost HostBuilder(string Port)
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseUrls($"http://*:{Port}");
                webHostBuilder.UseStartup<ServerConfig>();
            }).Build();
        }

        private class ServerConfig
        {
            public static void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
            }

            public static void Configure(IApplicationBuilder app)
            {
                app.UseRouting();

                app.UseEndpoints(routes =>
                {
                    routes.MapControllers();
                });
            }
        }
    }
}
