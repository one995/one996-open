using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Result
{
    public class UnifyResult
    {
        public UnifyResult()
        {
            StatusCode = ResultCode.Success;
            Message = StatusCode;
        }

        public UnifyResult(ResultCode code, object message = null)
        {
            StatusCode = code;
            Message = message ?? code;
        }

        public UnifyResult(object message, ResultCode code = ResultCode.Success)
        {
            Message = message;
            StatusCode = code;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        /// 状态值
        /// </summary>
        public ResultCode StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>

        public static UnifyResult GetUnifyResult(string message="操作成功",ResultCode code =ResultCode.Success)
        {
            return new UnifyResult { Message = message, StatusCode = code };
        }

      

        /// <summary>
        /// 是否成功
        /// </summary>
        /// <param name="success"></param>
        public static UnifyResult GetUnifyResult(bool success)
        {
            return new UnifyResult
            {
                StatusCode = ResultCode.Success
                ,
                Message = success ? ResultCode.Success : ResultCode.ValidError
            };
        }

        /// <summary>
        /// 异常消息
        /// </summary>
        /// <param name="exception"></param>
        public static UnifyResult GetUnifyResult(Exception exception)
        {
            return new UnifyResult
            {
                StatusCode = ResultCode.Error
                ,
                Message = exception.Message
            };
        }

        /// <summary>
        /// 枚举消息
        /// </summary>
        /// <param name="code"></param>
        public static UnifyResult GetUnifyResult(ResultCode code)
        {
            return new UnifyResult
            {
                StatusCode = code
                ,
                Message = code
            };
        }
    }
}
