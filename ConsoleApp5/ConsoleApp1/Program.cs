using System;
using System.Threading;
using System.Threading.Tasks;
using BeetleX.XRPC.Clients;
using Entity;
using EventNext;

namespace ConsoleApp1
{
    class Program
    {
        private static XRPCClient _client;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Random random=new Random();
            _client = new XRPCClient("127.0.0.1", 9090);
            _client.TimeOut = 10000;
            _client.Connect();
            _client.NetError = (c, e) =>
            {
                Console.WriteLine(e.Error.Message);
            };
            var service = _client.Create<IHelloWorld>();
            while(true)
            {
               
                var result = await service.Hello();
                Console.WriteLine(result+"  "+DateTime.Now.ToString("s"));
                Thread.Sleep(random.Next(100));
            }

            Console.ReadKey();
        }
    }
    
   
}
