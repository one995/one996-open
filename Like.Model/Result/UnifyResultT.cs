using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Result
{
    /// <summary>
    /// 统一响应结果泛型
    /// </summary>
    /// <typeparam name="T">返回结果的数据类型</typeparam>
    public class UnifyResult<T> : UnifyResult
    {
        public UnifyResult()
        {

        }

        public UnifyResult(object data, ResultCode code = ResultCode.Success) : base(code)
        {
            Data = data;
        }

        //public UnifyResult(List<T> data, ResultCode code = ResultCode.Success) : base(code)
        //{
        //    ListData = data;
        //}
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        //public List<T> ListData { get; set; }

        /// <summary>
        /// 正常消息并返回数据（如果返回结果为string类型可替换为UnifiedResult<dynamic>）
        /// </summary>
        /// <param name="data"></param>
        public static implicit operator UnifyResult<T>(T data)
        {
            return new UnifyResult<T>
            {
                Data = data,
                StatusCode = ResultCode.Success,
                Message = ResultCode.Success
            };
        }

        /// <summary>
        /// 错误消息使用
        /// </summary>
        /// <param name="message"></param>
        public static implicit operator UnifyResult<T>((string, bool) message)
        {
            return new UnifyResult<T>
            {
                Message = message.Item1,
                StatusCode = message.Item2 ? ResultCode.Success : ResultCode.ValidError
            };
        }

        /// <summary>
        /// 异常消息
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator UnifyResult<T>(Exception exception)
        {
            return new UnifyResult<T>
            {
                Message = exception.Message,
                StatusCode = ResultCode.Error
            };
        }
    }
}
