using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace WebSocketServer.Middleware
{
    public static class WebSocketMiddlewareExtensions
    {
        public static class WebSocketServerMiddlewareExtensions
        {
            
        }
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleware>();
        }
    }
}
