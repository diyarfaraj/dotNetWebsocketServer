using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocketServer.Middleware
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            string ConnId = Guid.NewGuid().ToString();

            _sockets.TryAdd(ConnId, socket);
            Console.WriteLine("connection added " + ConnId.ToString());

            return ConnId;

        }
    }
}
