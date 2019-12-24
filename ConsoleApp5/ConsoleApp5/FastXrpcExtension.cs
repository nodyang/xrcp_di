using System;
using BeetleX.EventArgs;
using BeetleX.XRPC;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp5
{
    public static class FastXrpcExtension
    {
        public static IServiceCollection UseXrpc(this IServiceCollection service,
            bool logToConsole,LogType logType, params System.Reflection.Assembly[] assemblies)
        {
            XRpcSettingHandler settingHandler = new XRpcSettingHandler();
            settingHandler.Assemblies = assemblies;
            settingHandler.LogToConsole = logToConsole;
            settingHandler.LogTypeInfo = logType;
            settingHandler.Services = service;
            service.AddSingleton<XRpcSettingHandler>(settingHandler);
            ServiceCollection services = new ServiceCollection();
            return service.AddHostedService<XrpcServer>();
        }  
    }
}