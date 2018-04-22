using System;

namespace Chatle.Blazor
{
    public static class OnKeyUp
    {
        public static Action<string> Action { get; set; }
        public static void Handler(string value)
        {
            Action?.Invoke(value);
        }
    }
}
