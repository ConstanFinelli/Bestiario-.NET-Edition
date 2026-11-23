using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        private static string? _globalAuthToken;
        private static readonly AsyncLocal<bool> _useScopedToken = new();
        private static readonly AsyncLocal<string?> _scopedAuthToken = new();

        public static string? AuthToken
        {
            get => _useScopedToken.Value ? _scopedAuthToken.Value : _globalAuthToken;
            set
            {
                if (_useScopedToken.Value)
                {
                    _scopedAuthToken.Value = value;
                }
                else
                {
                    _globalAuthToken = value;
                }
            }
        }

        public static void SetAuthToken(string? token) => AuthToken = token;

        public static void ClearAuthToken() => AuthToken = null;

        public static IDisposable UseScopedToken(string? token)
        {
            bool prevUse = _useScopedToken.Value;
            string? prevToken = _scopedAuthToken.Value;
            _useScopedToken.Value = true;
            _scopedAuthToken.Value = token;
            return new ScopedTokenReverter(prevUse, prevToken);
        }

        private sealed class ScopedTokenReverter : IDisposable
        {
            private readonly bool _prevUse;
            private readonly string? _prevToken;

            public ScopedTokenReverter(bool prevUse, string? prevToken)
            {
                _prevUse = prevUse;
                _prevToken = prevToken;
            }

            public void Dispose()
            {
                _useScopedToken.Value = _prevUse;
                _scopedAuthToken.Value = _prevToken;
            }
        }

        protected static async Task<HttpClient> CreateHttpClientAsync()
        {
            var client = new HttpClient();
            await ConfigureHttpClientAsync(client);
            return client;
        }

        protected static async Task ConfigureHttpClientAsync(HttpClient client)
        {
            await Task.CompletedTask;
            string baseUrl = GetBaseUrlFromConfig();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(AuthToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
            }
        }

        private static string GetBaseUrlFromConfig()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Intentando leer configuración...");

                DotNetEnv.Env.TraversePath().Load(); 
                string? envUrl = Environment.GetEnvironmentVariable("API_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] URL desde variable de entorno: {envUrl}");
                    return envUrl;
                }

                string runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Runtime: {runtimeInfo}");

                if (runtimeInfo.StartsWith("android"))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] Detectado Android - usando IP de emulador");
                    return "http://10.0.2.2:5076/";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Error detectando plataforma: {ex.Message}");
            }

            string defaultUrl = "http://localhost:5076/";
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Usando URL por defecto: {defaultUrl}");
            return defaultUrl;
        }
    }
}