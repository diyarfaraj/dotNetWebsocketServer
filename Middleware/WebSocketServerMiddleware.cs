﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServer.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("websocket connected");

                await RecieveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("message recieved");
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("recieved close message");
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer,0,result.Count) }");
                        return;
                    }
                });
            }
            else
            {
                Console.WriteLine("hello from the second request delegate");

                await _next(context);
            }
        }
        private async Task RecieveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }
        }
    }
}
