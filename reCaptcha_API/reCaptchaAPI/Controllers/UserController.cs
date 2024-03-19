using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reCaptchaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public UserController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        [AllowAnonymous]
        [HttpGet("Captcha")]
        public async Task<bool> GetreCaptchaResponse(string userResponse)
        {
            var reCaptchaSecretKey = _configuration["reCaptcha:SecretKey"];
            bool isSuccess = false;

            if(reCaptchaSecretKey != null && userResponse != null)
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"secret", reCaptchaSecretKey },
                    {"response", userResponse }
                });
                var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<reCaptchaResponse>();
                    //isSuccess = result.Success;
                }
            }
            return isSuccess;
        }

        public class reCaptchaResponse
        {
            public bool Success { get; set; }
            public string[] ErrorCodes { get; set; }
        }
    }
}
