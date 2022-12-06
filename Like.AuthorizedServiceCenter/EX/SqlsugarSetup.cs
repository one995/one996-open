using Like.Common.Common;
using Like.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;

namespace Like.AuthorizedServiceCenter.EX
{
    public static class SqlsugarSetup
    {
        static readonly DBConntionArr DB = Appsettings.app<DBConntionArr>(new string[] { "DB" })?.Find(S => S.IsUse==true);
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                DbType =DB.Type==0?DbType.MySql: DbType.SqlServer,
                ConnectionString =DB.Connstr,
                IsAutoCloseConnection = true,

                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (c, p) =>
                    {
                        if (DB.Type==0&&p.DataType == "varchar(max)")
                        {
                            p.DataType = "longtext";
                        }
                    }
                }
            },
                db =>
                {
                //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                    //Console.WriteLine(sql);//输出sql
                    };
                    //每次Sql执行前事件
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        //我可以在这里面写逻辑
                        Serilog.Log.Information(sql, pars);
                        Console.WriteLine(sql);
                    };
                    db.Aop.OnLogExecuted = (sql, p) =>
                    {
                        if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
                        {
                            //代码CS文件名
                            var fileName = db.Ado.SqlStackTrace.FirstFileName;
                            //代码行数
                            var fileLine = db.Ado.SqlStackTrace.FirstLine;
                            //方法名
                            var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                            //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息

                            Serilog.Log.Information($"OnLogExecuted:{fileName}-{fileLine}-{FirstMethodName}-usetime:{db.Ado.SqlExecutionTime.ToString()}");
                        }
                    };
                    db.Aop.OnError = (exp) =>//SQL报错
                    {
                        //exp.sql 这样可以拿到错误SQL
                        Serilog.Log.Error(exp.Sql);
                    };

                    db.Aop.DataExecuting = (oldValue, entityInfo) =>
                    {
                        //inset生效
                        if (entityInfo.PropertyName == "CreateTime" && entityInfo.OperationType == DataFilterType.InsertByObject)
                        {
                            entityInfo.SetValue(DateTime.Now);//修改CreateTime字段
                                                              //entityInfo有字段所有参数
                        }
                        //update生效        
                        if (entityInfo.PropertyName == "UpdateTime" && entityInfo.OperationType == DataFilterType.UpdateByObject)
                        {
                            entityInfo.SetValue(DateTime.Now);//修改UpdateTime字段
                        }
                    };


                }
                 
                );
            sqlSugar.CodeFirst.InitTables(typeof(Model.Models.AdminUser));
            sqlSugar.CodeFirst.InitTables(typeof(Model.User));
            sqlSugar.CodeFirst.InitTables(typeof(Model.Models.Role));

            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }
}
