using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Like.Model;
using Ubiety.Dns.Core;
using Pipelines.Sockets.Unofficial;
using System.Diagnostics;
using Serilog;
using Like.Common.Helper;

namespace Like.TCPServer.TCP
{
    public class TCPServerHP
    {
        private Socket ?_socket;
        
        public string ?IP { get; set; }

        public int Port { get; set; }

        private EndPoint endPoint;
        private Dictionary<string,Socket> ClientConnectionItems=new Dictionary<string, Socket>();
        private int ConntionCount = 20;
        private int stopre = 9527;
        private string cID;

        private Dictionary<string, string> LoginUser = new Dictionary<string, string>();

        public TCPServerHP( int port)
        {
   
            Port=port;
            _socket=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            endPoint=new IPEndPoint(IPAddress.Any, Port);
            _socket.Bind(endPoint);
            _socket.Listen(ConntionCount);
            Serilog.Log.Information("创建TCP服务...");
            Task.Run(() => { Satrt(); });
        }

        private void Satrt()
        {
            Serilog.Log.Information("开启监听...");
            Socket connection = null;
            while (true)
            {
                try
                {
                    connection= _socket?.Accept();
                    byte[] idrec = new byte[1024*1024];

                    //_socket.BeginReceive(data,0, _socket.ReceiveBufferSize,SocketFlags.None,clallback, stopre);

                    int length = connection.Receive(idrec);
                    string cID = Encoding.UTF8.GetString(idrec, 0, length);
                    TCPResponse response = new TCPResponse();
                    //登录解析
                    if (!string.IsNullOrEmpty(cID))
                    {
                        IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;

                        Console.WriteLine("\r\n[客户端\""+clientIP.ToString() + "\"建立连接成功！ 客户端数量：" + ClientConnectionItems.Count + "]");
                        Serilog.Log.Information("\r\n[客户端\""+clientIP.ToString() + "\"建立连接成功！ 客户端数量：" + ClientConnectionItems.Count + "]");
                        if(JsonHelper.IsJson(cID))
                        {
                            Model.Models.TCPPullData tCPSend = JsonConvert.DeserializeObject<Model.Models.TCPPullData>(cID);
                            if (tCPSend?.ActionName==ActionType.Login.ToString())
                            {
                                Serilog.Log.Information("\r\n[客户端\""+clientIP.ToString() + "\"] 登录处理");
                                //登录处理
                                if (!string.IsNullOrEmpty(tCPSend.Data))
                                {
                                    dynamic dy = JsonConvert.DeserializeObject(tCPSend.Data);

                                    if (!ClientConnectionItems.ContainsKey(dy.userName.ToString()))
                                    {
                                        ClientConnectionItems.Add(dy.userName.ToString(), connection);

                                        //显示与客户端连接情况

                                        //获取客户端的IP和端口号  

                                        //string sendmsg = "[" + "本端：" + dy.userName.ToString() + " 连接服务端成功！]";
                                        Console.WriteLine("\r\n[客户端\""+clientIP.ToString() + "\"建立连接成功！ 客户端数量2：" + ClientConnectionItems.Count + "]");
                                        Serilog.Log.Information("\r\n[客户端\""+dy.userName.ToString() + "\"] 登录成功");
                                    }
                                    else
                                    {
                                        string val = dy.userName.ToString();

                                        IPAddress oldclientIP = (ClientConnectionItems[val].RemoteEndPoint as IPEndPoint).Address;
                                        //如果两次连接的TCP IP不一样，踢掉前面的那个
                                        if (oldclientIP.ToString()!=clientIP.ToString())
                                        {
                                            if(ClientConnectionItems.ContainsKey(val))
                                                ClientConnectionItems[val].Close();
                                                ClientConnectionItems.Remove(val);
                                                ClientConnectionItems.Add(val, connection);
                                        }
                                    }
                                   
                                    response.code = 1;
                                    response.message="登录成功";
                                    connection.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                   
                                }
                                else
                                {
                                    Serilog.Log.Information("\r\n[客户端\""+clientIP.ToString() + "\"] 登录失败");
                                    response.code = 0;
                                    response.message="登录失败";
                                    connection.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                    
                                }
                            }
                            else
                            {
                                Serilog.Log.Information("\r\n[客户端\""+clientIP.ToString() + "\"] 未知指令");
                                Console.WriteLine(tCPSend?.ActionName);
                                response.code = 0;
                                response.message="未知指令";
                                connection.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                   
                            }
                        }
                        else
                        {
                            Serilog.Log.Information("\r\n[客户端\""+clientIP.ToString() + "\"数据格式错误]");
                            response.code = 0;
                            response.message="数据格式错误";
                            connection.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                   
                        }

                        
                    }
                    Task.Run(() => { recv(connection); });
                }
                catch(Exception ex)
                {
                    Serilog.Log.Information("监听异常:"+ex);
                    break;
                }
               
                            
            }
        }

       private  void recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;
            TCPResponse response = new TCPResponse();
            int count = 0;
            //DateTime Heartcount = DateTime.Now;
            while (true)
            {
                try
                {
                    byte[] arrServerRecMsg = new byte[1024 * 1024];
                    int length = socketServer.Receive(arrServerRecMsg);
                    if (length==0)
                    {
                        count++;
                        if (count>500)
                        {
                            //ClientConnectionItems.Remove(ClientConnectionItems.First().Key);
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\n" + (socketServer.RemoteEndPoint as IPEndPoint).Address+"异常断开连接");
                            break;
                        }
                
                    }
                    if (!socketServer.Connected)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\n" + (socketServer.RemoteEndPoint as IPEndPoint).Address+"断开连接");
                        break;
                    }
                    //将机器接受到的字节数组转换为人可以读懂的字符串     
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\n" + strSRecMsg);
                    Log.Information(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\n" + strSRecMsg);
                    if (!string.IsNullOrEmpty(strSRecMsg))
                    {
                        Serilog.Log.Information("监听:"+strSRecMsg);
                        count=0;
                        //处理心跳
                        if (strSRecMsg.Contains("Heart"))
                        {
                      
                            //Heartcount=DateTime.Now;
                            //心跳回复
                            socketServer.Send(Encoding.UTF8.GetBytes("Heart"));
                        }
                        else
                        {
                            if (JsonHelper.IsJson(strSRecMsg))
                            {
                                Model.Models.TCPPullData pull = JsonConvert.DeserializeObject<Model.Models.TCPPullData>(strSRecMsg);
                                if (pull!=null&&!string.IsNullOrEmpty(pull.ClinetId)&&!string.IsNullOrEmpty(pull.ActionName)&&!string.IsNullOrEmpty(pull.Data))
                                {
                                    if (ActionType.Login.ToString()==pull.ActionName)
                                    {
                                        //登录处理
                                        if (!string.IsNullOrEmpty(pull.Data))
                                        {
                                            dynamic dy = JsonConvert.DeserializeObject(pull.Data);
                                            if (!ClientConnectionItems.ContainsKey(dy.userName.ToString()))
                                            {
                                                ClientConnectionItems.Add(dy.userName.ToString(), socketServer);
                                                //显示与客户端连接情况
                                                Console.WriteLine("\r\n[客户端\"" + dy.userName.ToString() + "\"建立连接成功！ 客户端数量：" + ClientConnectionItems.Count + "]");
                                                Serilog.Log.Information("\r\n[客户端\""+dy.userName.ToString() + "\"] 登录成功");
                                                //获取客户端的IP和端口号  
                                                IPAddress clientIP = (socketServer.RemoteEndPoint as IPEndPoint).Address;
                                                int clientPort = (socketServer.RemoteEndPoint as IPEndPoint).Port;
                                                response.code = 1;
                                                response.message="登录成功";
                                                socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                            }
                                            else
                                            {
                                                response.code = 102;
                                                response.message="用户已经登录";
                                                socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //判断是否包含这个客户端
                                        bool contains = ClientConnectionItems.ContainsKey(pull.ClinetId);
                                        if (contains)
                                        {

                                            if (ActionType.SendData.ToString()==pull.ActionName)
                                            {
                                                //服务器端消息转发处理
                                                //Model.TCPSendMsg dy = JsonConvert.DeserializeObject<Model.TCPSendMsg>(pull.Data);
                                                if (!string.IsNullOrWhiteSpace(pull.ToUser)&&!string.IsNullOrWhiteSpace(pull.Data))
                                                {
                                                    response = new TCPResponse();
                                                    if (pull.ToUser==pull.FromUser)
                                                    {
                                                        response.code = 0;
                                                        response.message="不能转发给自己!";
                                                        socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                                    }
                                                    else
                                                    {
                                                        if (ClientConnectionItems.ContainsKey(pull.ToUser))
                                                        {
                                                            //转发给对应的人员
                                                            var tocon = ClientConnectionItems[pull.ToUser];
                                                            if (tocon.Connected)
                                                            {
                                                                ClientConnectionItems[pull.ToUser].Send(Encoding.UTF8.GetBytes(pull.Data));
                                                                response.code = 1;
                                                                response.message="转发完成";
                                                                socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                                            }
                                                            else
                                                            {
                                                                ClientConnectionItems.Remove(pull.ToUser);
                                                                response = new TCPResponse();
                                                                response.code = 0;
                                                                response.message="用户不在线";
                                                                socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                                            }

                                                        }
                                                        else
                                                        {
                                                            response = new TCPResponse();
                                                            response.code = 0;
                                                            response.message="用户不在线";
                                                            socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    response.code = 0;
                                                    response.message="转发人员不能为空!";
                                                    socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                                }

                                            }
                                            if (ActionType.Logout.ToString()==pull.ActionName)
                                            {
                                                //string temp = ClientConnectionItems.First().Key;
                                                ////提示套接字监听异常  
                                                //Console.WriteLine("\r\n[客户端\"" + socketServer.RemoteEndPoint + "\"已经下线！ 客户端数量：" + ClientConnectionItems.Count + "]");
                                                //ClientConnectionItems.Remove(ClientConnectionItems.First().Key);
                                                //Console.WriteLine("\r\n[客户端\"" + temp + "\"已经下线！ 客户端数量：" + ClientConnectionItems.Count + "]");
                                            }
                                            if (ActionType.ReSendData.ToString()==pull.ActionName)
                                            {
                                            }
                                            //ClientConnectionItems[ccID].Send(Encoding.UTF8.GetBytes(hc));
                                        }
                                        else
                                        {
                                            response = new TCPResponse();
                                            response.code = 0;
                                            response.message="用户不在线";
                                            socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                            //socketServer.Send(Encoding.UTF8.GetBytes("用户不在线"));
                                            Console.WriteLine("输入有误，不予转发\r\n");
                                        }
                                    }

                                    arrServerRecMsg.DefaultIfEmpty();
                                }
                                else
                                {

                                    response.code = 0;
                                    response.message="未知指令";
                                    socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                                    //socketServer.Send(Encoding.UTF8.GetBytes("用户不在线"));
                                    Console.WriteLine("输入有误，不予转发\r\n");
                                }
                            }
                            else
                            {
                                Serilog.Log.Information("\r\n[客户端\""+(socketServer.RemoteEndPoint as IPEndPoint).Address.ToString() + "\"数据格式错误]");
                                response.code = 0;
                                response.message="数据格式错误";
                                socketServer.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                            }

                            
                        }

                         
                    }
                   
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex.ToString());
                    //提示套接字监听异常  
                    Console.WriteLine("\r\n[客户端\"" + socketServer.RemoteEndPoint + "\"已经中断连接！ 客户端数量：" + ClientConnectionItems.Count + "]");
                    socketServer?.Close();

                    break;
                }
            }
        
        }

        public void Close()
        {
            _socket?.Close();
        }
    }

}
