namespace Like.AuthorizedServiceCenter.Models
{
    public class LogoutRequest
    {
        public string? userName { get; set; }
        public string? Token { get; set; }
    }
    public class LogoutResponse
    {
        public bool IsSuccess { get; set; }
    }

}
