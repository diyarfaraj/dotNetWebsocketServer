using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace WebSocketServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                WriteRequestParam(context);
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("websocket connected");
                }
                else
                {
                    Console.WriteLine("hello from the second request delegate");

                    await next();
                }
            });

            app.Run(async context =>
            {
                Console.WriteLine("hello from the third request delegate");
                await context.Response.WriteAsync("hello from the async reponse");
            });
        }

        public void WriteRequestParam(HttpContext context)
        {
            Console.WriteLine("request method: " + context.Request.Method);
            Console.WriteLine("request protocol: " + context.Request.Protocol);

            if(context.Request.Headers != null)
            {
                foreach (var h in context.Request.Headers)
                {
                    Console.WriteLine("---> "+ h.Key+" : " + h.Value);
                }
            }

        }
    }
}
