using YourProject.Abstracts;
using YourProject.Helpers;
using YourProject.Models;
using YourProject.Services;
using YourProject.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using WebVedProApi.Services;

namespace YourProject.Controllers
{
    [Route("[controller]")]
    public class HomeController : BaseController<IHomeService>
    {
        //Получаем наши ключи из appsettings.json
        private readonly GoogleReCaptchaSettings _recaptchaSettings;
        public HomeController(IOptions<GoogleReCaptchaSettings> options)
        {
            _recaptchaSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] HomeModel model)
        {
            string gRecaptchaResponse = Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(gRecaptchaResponse))
            {
                return BadRequest("reCAPTCHA response is missing.");
            }
            string secretKey = _recaptchaSettings.SecretKey;
            string verificationUrl = _recaptchaSettings.VerificationUrl;

            //Для GoogleReCaptcha v3
            //bool isValid = await GoogleReCaptchaSettings.verifyReCaptchaV3(gRecaptchaResponse, secretKey, verificationUrl);

            //Для GoogleReCaptcha v2
            bool isValid = await GoogleReCaptchaSettings.VerifyReCaptchaV2(gRecaptchaResponse, secretKey, verificationUrl);

            if (!isValid) 
            {
                return BadRequest("reCAPTCHA response is missing.");
            }

            return Ok();

        }
    }
}
