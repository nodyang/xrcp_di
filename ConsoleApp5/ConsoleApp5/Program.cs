using System;
using System.Threading.Tasks;
using BeetleX.XRPC;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogType = BeetleX.EventArgs.LogType;

namespace ConsoleApp5
{
    class Program
    { 
        private static XRPCServer mXRPCServer;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
           
              var builder = new HostBuilder()
                            .ConfigureServices((hostContext, services) =>
                            {
                                services.AddTransient<Stu>();
                                services.UseXrpc(true,LogType.Debug,
                                typeof(HelloWorldService).Assembly);
                            });
                        builder.Build().Run();
            
        }
    }

  
    
   

}