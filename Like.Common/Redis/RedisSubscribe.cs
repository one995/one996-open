using HotChocolate.Types;
using StackExchange.Redis;

using System.Threading.Tasks;
using InitQ.Abstractions;
using Like.Model;

namespace Like.Common.Redis
{
    public class RedisSubscribe : IRedisSubscribe
    {
        [InitQ.Attributes.Subscribe(RedisMqKey.Loging)]
        private async Task SubRedisLoging(string msg)
        {
            //Console.WriteLine($"订阅者 1 从 队列{RedisKey.Loging} 消费到/接受到 消息:{msg}");
            await Task.CompletedTask;
        }
    }
}
