//Создаем данный файл в папке Service в вашем проекте (можно и в Helper, не критично)

using YourProject.Utils;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json.Nodes;

namespace YourProject.Helpers
{
    public class GoogleReCaptchaSettings
    {
        public string SiteKey { get; set; } //Храним SiteKey из appsetting.json
        public string SecretKey { get; set; } //Храним SecretKey из appsetting.json
        public string VerificationUrl {  get; set; } //Храним VerificationUrl из appsetting.json

        //Для GoogleReCaptcha v2

        //response - Отправка токена
        //secret - Секретный ключ
        //verificationUrl - URL для проверки прохождения GoogleReCaptcha
        public static async Task<bool> VerifyReCaptchaV2(string response, string secret, string verificationUrl)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", secret),
                    new KeyValuePair<string, string>("response", response)
                });

                var result = await client.PostAsync(verificationUrl, content);

                if (result.IsSuccessStatusCode)
                {
                    var strResponse = await result.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    return jsonResponse?["success"]?.GetValue<bool>() == true;
                }
            }

            return false;
        }

        //Для GoogleReCaptcha v3

        //response - Отправка токена
        //secret - Секретный ключ
        //verificationUrl - URL для проверки прохождения GoogleReCaptcha
        //public static async Task<bool> verifyReCaptchaV3(string response, string secret, string verificationUrl)
        //{
        //    if (string.IsNullOrWhiteSpace(response))
        //    {
        //        throw new ArgumentException("reCAPTCHA response is null or empty.", nameof(response));
        //    }

        //    if (string.IsNullOrWhiteSpace(secret))
        //    {
        //        throw new ArgumentException("reCAPTCHA secret is null or empty.", nameof(secret));
        //    }

        //    using (var client = new HttpClient())
        //    {
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(response ?? ""), "response");
        //        content.Add(new StringContent(secret ?? ""), "secret");

        //        var result = await client.PostAsync(verificationUrl, content);

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var strResponse = await result.Content.ReadAsStringAsync();
        //            Console.WriteLine(strResponse);

        //            var jsonResponse = JsonNode.Parse(strResponse);
        //            if (jsonResponse != null)
        //            {
        //                var success = jsonResponse["success"]?.GetValue<bool>();
        //                var score = jsonResponse["score"]?.GetValue<double>() ?? 0;
        //                return success == true && score > 0.5;
        //            }
        //        }
        //    }
        //    return false;
        //}

    }
}
