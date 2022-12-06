using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Common.SerilogDao
{
    public class SeriLogHelper
    {
        LoggerConfiguration logger = new LoggerConfiguration();

        #region Serilog 相关设置

        /// <summary>
        /// 初始化默认方式的serilog
        /// </summary>
        /// <param name="logPath">输出txt日志路径</param>
        public static void InitLoggerDefault(string logPath)
        {
            //掉用方式: Application_Start 中 Logger.InitLoggerDefault(System.Web.Hosting.HostingEnvironment.MapPath("~/logs/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/log_.log"));

            //Serilog.Log.Logger = new LoggerConfiguration()
            //    // 将配置传给 Serilog 的提供程序 
            //    //.ReadFrom.Configuration(Configuration)
            //    .Enrich.With(new DateTimeNowEnricher())
            //    .Enrich.FromLogContext()//记录相关上下文信息 
            //    .MinimumLevel.Debug()//最小记录级别
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//对其他日志进行重写,除此之外,目前框架只有微软自带的日志组件
            //                                                                  //.WriteTo.Console()//输出到控制台 WriteTo.Async重写了
            //                                                                  //.WriteTo.File() //输出到本地文件 WriteTo.Async重写了
            //    .w(o =>
            //    {
            //        o.Console();//输出到控制台。mvc中可能配置此方式不太好用，暂无修正
            //        o.File(logPath, //System.Web.Hosting.HostingEnvironment.MapPath("~/logs/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/log_.log" //在程序跟目录下/logs/2020/11/30/log_20201130.log
            //            restrictedToMinimumLevel: LogEventLevel.Error, //最小写入级别
            //            rollingInterval: RollingInterval.Day/*按天生成日志文件*/);
            //    })
            //    .CreateLogger();
        }

        public class DateTimeNowEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "DateTimeNow", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
        }
        #endregion


        /// <summary>
        /// 默认方式使用log
        /// </summary>
        public class Default
        {
            public static void Info(string msg, params object[] args)
            {
                Serilog.Log.Information(msg, args);
            }

            public static void Info(string msg, Exception err)
            {
                Serilog.Log.Information(msg, err);
            }


            public static void Debug(string msg, params object[] args)
            {
                Serilog.Log.Debug(msg, args);
            }

            public static void Debug(string msg, Exception err)
            {
                Serilog.Log.Debug(msg, err);
            }

            public static void Warning(string msg, params object[] args)
            {
                Serilog.Log.Warning(msg, args);
            }

            public static void Warning(string msg, Exception err)
            {
                Serilog.Log.Warning(msg, err);
            }

            public static void Error(string msg, params object[] args)
            {
                Serilog.Log.Error(msg, args);
            }

            public static void Error(string msg, Exception err)
            {
                Serilog.Log.Error(msg, err);
            }

            public static void Fatal(string msg, params object[] args)
            {
                Serilog.Log.Fatal(msg, args);
            }

            public static void Fatal(string msg, Exception err)
            {
                Serilog.Log.Fatal(msg, err);
            }
        }


    }
}
