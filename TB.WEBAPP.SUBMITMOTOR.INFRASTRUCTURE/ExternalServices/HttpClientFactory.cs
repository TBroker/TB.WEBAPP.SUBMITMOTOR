using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;

namespace TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.ExternalServices
{
    public static class HttpClientFactory
    {
        public static void RegisterHttpClients(IServiceCollection services, string environmentName)
        {
            // อ่านการตั้งค่าจาก app settings
            services.Configure<CoreSystemSetting>(options => { });
            services.Configure<DataSetting>(options => { });
            services.Configure<ServiceSetting>(options => { });
            services.Configure<CreateFileSetting>(options => { });
            services.Configure<PaymentSetting>(options => { });

            // ลงทะเบียน TokenHandler
            //services.AddTransient<TokenHandler>();

            // กำหนด TLS protocols
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            // ตั้งค่า DataApi
            services.AddHttpClient("DataService", (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<DataSetting>>().Value;

                // สร้าง HttpClientHandler

                // ตั้งค่า SslProtocols
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // ตั้งค่า HttpClient
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(apiSettings.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .ConfigurePrimaryHttpMessageHandler(() => CreateHttpHandler(environmentName));
            //.AddHttpMessageHandler<TokenHandler>();

            // ตั้งค่า CoreSystem
            services.AddHttpClient("CoreSystemService", (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<CoreSystemSetting>>().Value;

                client.BaseAddress = new Uri(apiSettings.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .ConfigurePrimaryHttpMessageHandler(() => CreateHttpHandler(environmentName));
            //.AddHttpMessageHandler<TokenHandler>();

            // ตั้งค่า DataApi
            services.AddHttpClient("NotificationService", (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<ServiceSetting>>().Value;

                client.BaseAddress = new Uri(apiSettings.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .ConfigurePrimaryHttpMessageHandler(() => CreateHttpHandler(environmentName));
            //.AddHttpMessageHandler<TokenHandler>();

            // ตั้งค่า CreateFileService
            services.AddHttpClient("CreateFileService", (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<CreateFileSetting>>().Value;

                client.BaseAddress = new Uri(apiSettings.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .ConfigurePrimaryHttpMessageHandler(() => CreateHttpHandler(environmentName));
            //.AddHttpMessageHandler<TokenHandler>();

            // ตั้งค่า CreateFileService
            services.AddHttpClient("PaymentService", (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<PaymentSetting>>().Value;
                client.BaseAddress = new Uri(apiSettings.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .ConfigurePrimaryHttpMessageHandler(() => CreateHttpHandler(environmentName));
            //.AddHttpMessageHandler<TokenHandler>();
        }

        private static HttpClientHandler CreateHttpHandler(string environmentName)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 |
                              System.Security.Authentication.SslProtocols.Tls13,
                UseDefaultCredentials = true
            };

            // ข้าม SSL validation เฉพาะ Development และ UAT
            if (string.IsNullOrEmpty(environmentName) ||
                environmentName.Equals("Development", StringComparison.OrdinalIgnoreCase) ||
                environmentName.Equals("UAT", StringComparison.OrdinalIgnoreCase))
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            }

            return handler;
        }
    }

    public class TokenHandler(IServiceProvider serviceProvider) : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // ดึงข้อมูลการตั้งค่า
            var apiSettings = _serviceProvider.GetRequiredService<IOptions<CoreSystemSetting>>().Value;

            using (var scope = _serviceProvider.CreateScope())
            {
                var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                var tokenClient = httpClientFactory.CreateClient();
                tokenClient.BaseAddress = new Uri(apiSettings.ServiceUrl);

                // สร้างคำขอเพื่อดึง token
                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "/T2P_APP/api/proxy/auth")
                {
                    Content = new StringContent(JsonSerializer.Serialize(
                        new
                        {
                            username = apiSettings.Username,
                            password = apiSettings.Password
                        }
                        ), Encoding.UTF8, "application/json")
                };

                var tokenResponse = await tokenClient.SendAsync(tokenRequest, cancellationToken);
                tokenResponse.EnsureSuccessStatusCode();

                var tokenResult = await tokenResponse.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken: cancellationToken);
                var token = tokenResult?.Tokens?.AccessToken;

                // ตั้งค่า Authorization header ด้วย accessToken ที่ได้รับ
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }

        // โครงสร้างของ AuthResponse
        public class AuthResponse
        {
            public TokenData Tokens { get; set; } = default!;

            public class TokenData
            {
                [JsonPropertyName("accessToken")]
                public string AccessToken { get; set; } = default!;

                [JsonPropertyName("expiresIn")]
                public int ExpiresIn { get; set; }

                [JsonPropertyName("refreshToken")]
                public string RefreshToken { get; set; } = default!;

                [JsonPropertyName("tokenType")]
                public string TokenType { get; set; } = default!;
            }
        }
    }

}