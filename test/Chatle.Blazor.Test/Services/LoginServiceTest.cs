using Chatle.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chatle.Blazor.Test.Services
{
    public class LoginServiceTest
    {
        [Fact]
        public async Task GetExternalProviders_should_deserialize_providers()
        {
            var sut = CreateSut((message, tokem) =>
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"displayName\":\"Facebook\",\"authenticationScheme\":\"Facebook\"},{\"displayName\":\"Twitter\",\"authenticationScheme\":\"Twitter\"},{\"displayName\":\"Google\",\"authenticationScheme\":\"Google\"}]")
                });
            });

            var result = await sut.GetExternalLoginProviders();
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public async Task Validate_should_return_error_when_user_exists()
        {
            var sut = CreateSut((message, tokem) =>
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("true")
                });
            });

            var result = await sut.Validate("test");
            Assert.NotEmpty(result);
            Assert.StartsWith("test", result);
        }

        [Fact]
        public async Task Validate_should_return_error_on_exception()
        {
            var sut = CreateSut((message, tokem) =>
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });
            });

            var result = await sut.Validate("test");
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Validate_should_return_null_when_user_does_not_exist()
        {
            var sut = CreateSut((message, tokem) =>
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("false")
                });
            });

            var result = await sut.Validate("test");
            Assert.Null(result);
        }

        private LoginService CreateSut(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
        {
            return new LoginService(new HttpClient(new DelegatingHandlerStub(handler))
            {
                BaseAddress = new Uri("http://test")
            }, null, new Settings
            {
                AccountApi = "/Account"
            });
        }
    }
}
