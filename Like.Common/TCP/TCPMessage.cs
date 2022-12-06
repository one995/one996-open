using Like.Common.Common;
using Like.Common.Helper;
using Like.Model;
using Like.Model.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Like.Common.TCP
{
    public class TCPMessage
    {
        public static UnifyResult<string> SendMsg(string ToUser,string Sdata ,string MsgType )
        {
            var res = new UnifyResult<string>
            {
                Message = "转发完成",
                StatusCode = ResultCode.Success
            };
            TcpClient client = new TcpClient();
            try
            {
                //连接消息中心
                //数据加密
                //消息转发

              
                client.Connect(Appsettings.GetTCPIP(),int.Parse(Appsettings.GetTCPPort()));
                client.Client.ReceiveTimeout = 10000;
                if (client.Connected)
                {
                    Model.TCPSendMsg tCPSend = new Model.TCPSendMsg();
                    tCPSend.data=Sdata;
                    tCPSend.ActionName=MsgType;
                    tCPSend.ToUser=ToUser;
                    tCPSend.FromUser="likeyouapi";
                    tCPSend.ClinetId="likeyouapi";
                    //先发登录

                    Model.TCPSendMsg LiogintCPSend = new Model.TCPSendMsg();
                    LiogintCPSend.data=JsonConvert.SerializeObject( new { userName= "likeyouapi" });
                    LiogintCPSend.ActionName=ActionType.Login.ToString();
                    client.Client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(LiogintCPSend)));
                    byte[] idreclogin = new byte[1024*1024];
                    Task.Delay(2000).Wait();
                    int length = client.Client.Receive(idreclogin);
                 
                    string result=Encoding.UTF8.GetString(idreclogin,0,length);

                    if (result.Contains("登录成功"))
                    {
                        length=0;

                        client.Client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tCPSend)));
                        byte[] idrec = new byte[1024*1024];
                        length = client.Client.Receive(idrec);
                        string repose = Encoding.UTF8.GetString(idrec, 0, length);
                        res.Data=repose;
                    }      
                }
                else
                {
                    res.StatusCode = ResultCode.ValidError;
                    res.Message="服务器不在线";
                }
            }catch(Exception ex)
            {
                Serilog.Log.Error("转发TCP消息异常:"+ex);
                res.Message="转发消息异常";
            }
            finally
            {
                client?.Close();
                client=null;    
            }
            return res;
        }
    }
}
