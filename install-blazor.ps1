$vsixPath = "$($env:USERPROFILE)\Microsoft.VisualStudio.BlazorExtension.vsix"
copy Microsoft.VisualStudio.BlazorExtension.vsix $vsixPath
"`"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\VSIXInstaller.exe`" /q /a $vsixPath" | out-file ".\install-vsix.cmd" -Encoding ASCII
& .\install-vsix.cmd