using Chatle.Blazor.Services;
using Microsoft.AspNetCore.Blazor.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Chatle.Blazor.Test.Services
{
    public class UriHelperExtentionsTest
    {
        [Fact]
        public void GetParameterTest()
        {
            var mock = new Mock<IUriHelper>();
            mock.Setup(m => m.GetAbsoluteUri()).Returns("http://test?test=test&a=" + Uri.EscapeDataString("/test"));
            var sut = mock.Object;
            Assert.Equal("test", sut.GetParameter("test"));
            Assert.Equal("/test", sut.GetParameter("a"));
        }
    }
}
