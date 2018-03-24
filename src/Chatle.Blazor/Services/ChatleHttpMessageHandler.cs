using Microsoft.AspNetCore.Blazor.Browser.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Chatle.Blazor.Services
{
    public class ChatleHttpMessageHandler: DelegatingHandler
    {
        private readonly Settings _settings;
        private string _xhrf;

        public string XHRF => _xhrf;
        public ChatleHttpMessageHandler(Settings settings): base(new BrowserHttpMessageHandler())
        {
            _settings = settings;
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_xhrf))
            {
                await GetXhrf(cancellationToken);
            }

            request.Headers.Add("X-XSRF-TOKEN", _xhrf);

            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch
            {
                _xhrf = null;
                throw;
            }
        }

        private async Task GetXhrf(CancellationToken cancellationToken)
        {
            using (var getXhrfRequest = new HttpRequestMessage(HttpMethod.Get, _settings.ApiBaseUrl + "/xhrf"))
            {
                using (var response = await base.SendAsync(getXhrfRequest, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    _xhrf = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
