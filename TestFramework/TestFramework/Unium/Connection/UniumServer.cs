using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Unium.QueryData;

namespace TestFramework.Unium.Connection
{
    public class UniumServer : IDisposable
    {
        private const string SCENE_CALL_ID = "scene";
        private ClientWebSocket _socket;
        private CancellationTokenSource _cancellationToken;
        private string _httpIp;
        private string _webSocketIp;
        private ArraySegment<byte> _rcvBuffer;
        public string HttpIP => _httpIp;

        public UniumServer(string address, string port,CancellationTokenSource cancellationToken)
        {
            _rcvBuffer = new ArraySegment<byte>(new byte[1024 * 1024]);
            _httpIp = $"http://{address}:{port}/";
            _webSocketIp = $"ws://{address}:{port}/ws";
            _cancellationToken = cancellationToken;
        }

        public bool IsConnected => _socket.State == WebSocketState.Open;

        public static async Task<UniumServer> CreateAndConnect(string address, string port,CancellationTokenSource cancellationToken)
        {
            var server = new UniumServer(address,port,cancellationToken);
            await server.ConnectAsync();
            return server;
        }

        public float TimeScale { get; set; } = 1f;

        public async Task ConnectAsync()
        {
            _socket = new ClientWebSocket();
            await _socket.ConnectAsync(new Uri(_webSocketIp), _cancellationToken.Token);
        }

        public async Task SendAsync(EventData d)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(d));
            var sendBuffer = new ArraySegment<byte>(sendBytes);
            await
                _socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true,
                    cancellationToken: _cancellationToken.Token);
        }


        public async Task<string> ReceiveAsync()
        {
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult rcvResult;
                do
                {
                    rcvResult = await _socket.ReceiveAsync(_rcvBuffer, _cancellationToken.Token);
                    ms.Write(_rcvBuffer.Array, _rcvBuffer.Offset, rcvResult.Count);
                } while (!rcvResult.EndOfMessage);

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public async Task<string> Get(string path,string id = SCENE_CALL_ID)
        {
            EventData e = new EventData() { q = path, id = id };
            //Console.WriteLine("Send {0}", e.q);
            await SendAsync(e);
            //Console.WriteLine("Responce  {0}", responceString);
            return await ReceiveAsync();
        }

        public async Task<T> Get<T>(string path,string id = SCENE_CALL_ID)
        {
            JObject obj = JObject.Parse(await Get(path,id));
            return obj["data"].ToObject<T>();
        }

        public async Task<string> GetByScenePath(string path)
        {
            return await Get("/q/scene/" + path);
        }
        
        public async Task<bool> GetValueFromMethod(string path,string id = SCENE_CALL_ID)
        {
            return await Get<bool>($"/{path}",id);
        }        

        public void Dispose()
        {
            _socket?.Dispose();
            _cancellationToken?.Dispose();
        }
    }
}