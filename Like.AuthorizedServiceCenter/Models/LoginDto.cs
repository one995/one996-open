namespace Like.AuthorizedServiceCenter.Models
{
    public class LoginDto
    {
        /// <summary>
        ///是否成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        ///错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///时间戳
        /// </summary>
        public string Expired { get; set; }

        public string Role { get; set; }

        public string UserName { get; set; }



    }
}
