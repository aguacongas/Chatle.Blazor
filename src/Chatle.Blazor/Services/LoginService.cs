using Chatle.Blazor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Services;

namespace Chatle.Blazor.Services
{
    public class LoginService
    {
        private readonly HttpClient _http;
        private readonly IUriHelper _uriHelper;
        private readonly Settings _settings;
        public string UserName { get; set; }

        public LoginService(HttpClient http, IUriHelper uriHelper, Settings settings)
        {
            _http = http;
            _uriHelper = uriHelper;
            _settings = settings;
        }

        public async Task<ExternalLoginProvider[]> GetExternalLoginProviders()
        {
            return await _http.GetJsonAsync<ExternalLoginProvider[]>(_settings.AccountApi + "/getExternalProviders");
        }

        public async Task<string> Validate(string userName)
        {
            try
            {
                if (await _http.GetJsonAsync<bool>($"{_settings.AccountApi}/exists?userName={userName}"))
                {
                    return $"{userName} already exists";
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return null;
        }

        public async Task<string> Register(string userName)
        {
            try
            {
                await _http.PutJsonAsync(_settings.AccountApi + "/spaExternalLoginConfirmation", new { userName });
                UserName = userName;
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return null;
        }
    }
}
