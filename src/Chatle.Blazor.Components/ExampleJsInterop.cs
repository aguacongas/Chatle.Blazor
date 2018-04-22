using System;
using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace Chatle.Blazor.Components
{
    public class ExampleJsInterop
    {
        public static string Prompt(string message)
        {
            return RegisteredFunction.Invoke<string>(
                "Chatle.Blazor.Components.ExampleJsInterop.Prompt",
                message);
        }
    }
}
