using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chatle.Blazor.Services
{
    public static class UriHelperExtentions
    {
        public static string GetParameter(this IUriHelper uriHelper, string name)
        {
            var url = uriHelper.GetAbsoluteUri();
            var regex = new Regex($"[\\?&]{name}=([^&#]*)");
            var match = regex.Match(url);
            if (match != null && match.Groups.Count == 2)
            {
                return Uri.UnescapeDataString(match.Groups[1].Value);
            }

            return null;
        }
    }
}
