using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeetleX.XRPC;
using EventNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp5
{
    public class XRpcSettingHandler
    {
        public  bool LogToConsole { get; set; }

        public  BeetleX.EventArgs.LogType LogTypeInfo { get; set; }
        public System.Reflection.Assembly[] Assemblies { get; set; }

        public IServiceCollection Services { get; set; }
    }

    public class XrpcServer : BackgroundService
    {
        
        public XrpcServer(XRpcSettingHandler  handler)
        {
            _xRpcSettingHandler = handler;
        }

        private ServiceCollection _serviceCollection = new ServiceCollection();

        private IServiceProvider _serviceProvider;

        private XRpcSettingHandler  _xRpcSettingHandler;
        
        XRPCServer mXRPCServer;
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            mXRPCServer=new XRPCServer();
            mXRPCServer.RPCOptions.LogToConsole = _xRpcSettingHandler.LogToConsole;
            mXRPCServer.ServerOptions.LogLevel = _xRpcSettingHandler.LogTypeInfo;
            mXRPCServer.EventCenter.ServiceInstance += (o, e) =>
            {
                //  e.Service = _serviceProvider.GetService(typeof(XRPCServer));

               var item=_serviceProvider.GetService(e.Type);
               e.Service = item;

            };
            ServiceDescriptor[] items = new ServiceDescriptor[_xRpcSettingHandler.Services.Count];
            _xRpcSettingHandler.Services.CopyTo(items, 0);
            foreach (var item in items)
            {
                _serviceCollection.Insert(0,item);
            }

            foreach (Assembly item in _xRpcSettingHandler.Assemblies)
            {
                Type[] types = item.GetTypes();
                foreach (Type type in types)
                {
                    ServiceAttribute ca = type.GetCustomAttribute<ServiceAttribute>(false);
                    if (ca != null)
                    {
                        if (ca.SingleInstance)
                        {
                            _serviceCollection.AddSingleton(type);
                        }
                        else
                        {
                            _serviceCollection.AddScoped(type);
                        }
                    }
                  
                }

               
            }
            _serviceCollection.AddSingleton(mXRPCServer);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            if (_xRpcSettingHandler.Assemblies != null)
                mXRPCServer.Register(_xRpcSettingHandler.Assemblies);
            mXRPCServer.Open();
            return  Task.CompletedTask;
            
        }

            
    }
}