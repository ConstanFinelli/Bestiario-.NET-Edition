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
        private static readonly Lazy<string> _baseUrl = new(ResolveBaseUrl);

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
            // Leer URL base de configuración (cacheada una sola vez), si no existe usar localhost por defecto
            client.BaseAddress = new Uri(_baseUrl.Value);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(AuthToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
            }
        }

        private static string ResolveBaseUrl()
        {
            try
            {
                // 1. Cargar .env una sola vez sin sobrescribir variables de entorno del sistema
                DotNetEnv.Env.NoClobber().TraversePath().Load();
                string? envUrl = Environment.GetEnvironmentVariable("API_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                {
                    return envUrl;
                }

                string runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
                if (runtimeInfo.StartsWith("android"))
                {
                    return "http://10.0.2.2:5076/";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Error detectando plataforma: {ex.Message}");
            }

            // URL por defecto para Windows/otras plataformas
            return "http://localhost:5076/";
        }
    }
}