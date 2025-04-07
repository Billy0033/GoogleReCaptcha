Поделючение GoogleReCaptcha и GoogleReCaptcha v3. 

GoogleReCaptcha v2 - ждет нажатия пользователя, чтобы проверить что он не бот.
GoogleReCaptcha v3 - автоматически проверяет что форму заполнил не бот.

1. Сначала перейдем к нашей форме (файл index.cshtml) => для GoogleReCaptcha v2 и GoogleReCaptcha v3
2. Далее напишем метод для проверки GoogleReCaptcha (файл GoogleReCaptcha.cs) => для GoogleReCaptcha v2 и GoogleReCaptcha v3
3. Далее вызовем метод в Контроллере (файл HomeController.cs) => для GoogleReCaptcha v2 и GoogleReCaptcha v3

4. В Program.cs нужно зарегестрировать наш GoogleReCaptcha следующим образом:
builder.Services.Configure<GoogleReCaptchaSettings>(
    builder.Configuration.GetSection("GoogleReCaptcha"));
    
5. В appsettings.json должно выглядеть все так:

"GoogleReCaptcha": {
    "SiteKey": "Ваш ключ сайта",
    "SecretKey": "Ваш секретный ключ", 
    "VerificationUrl": "https://www.google.com/recaptcha/api/siteverify" //остается такой же, в документации Google дается
}

*Чтобы получить ключи перейдите на сайт: https://developers.google.com/recaptcha?hl=ru или в поисковик вбить GoogleReCaptcha, выбераем версию => v2 или v3 и копируем ключи в appsettings.json

