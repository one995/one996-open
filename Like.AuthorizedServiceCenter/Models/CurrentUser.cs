using Microsoft.AspNetCore.Http;

namespace Like.AuthorizedServiceCenter.Models
{
    public static class CurrentUser
    {

        private static IHttpContextAccessor _httpContext;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
         {
            _httpContext = httpContextAccessor;
        }
        public static int Id { get; set; }
        public static string UserName { get; set; }

        public static int ?Role { get; set; }

    }
}
