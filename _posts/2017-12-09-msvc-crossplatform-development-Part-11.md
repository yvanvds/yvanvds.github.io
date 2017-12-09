---
layout: post
title: MSVC Crossplatform Development Part 11
---
**Android .NET Library** The first step to getting this library to work with Xamarin Forms will be a .NET library for every platform. We'll start with Android.
<!--more--> 

In the solution Explorer, Right-click the `Libraries` folder and choose:

`Add -> New Project -> Visual C# -> Android -> Class Library`

Name the project `DemoTools.NET.Android`.

![Screenshot]({{ "/images/msvc_part11_1.png" | absolute_url }})

The previous .NET libraries were linked to the `DemoTools.Windows.Native` library. In order to use that library from within a .NET library, we had to add `Generate.NET/DemoTools_wrap.cxx` to the library. This library will be built upon the `DemoTools.Android.Native` library, so we will need to add the same file to this Android library.

Once that is done, the Android .NET libraray can be configured:

1. Delete `Class1.cs`.

2. Open `Project -> Project Dependencies...` and add `DemoTools.Android.Native` as a dependency.

3. Right-click the project references and a reference to the shared project `NetDemoTools`.

4. Add a folder called `lib` to the project, along with subfolder `arm`, `arm64`, `x86` and `x64`. Inside each subfolder, add the file `\DemoTools.Android.Native\Release\[Platform]\libDemoTools.so` as a link.

5. Set the properties for all four links:
    - **Build Action**: EmbeddedNativeLibrary
    - **Copy to Output Directory**: copy if newer

6. Save the project *(Save All)* and open the project file `DemoTools.NET.Android.csproj` with a text editor.

7. Make changes to add the ABI tags the embedded libraries so that they look like this:

{% highlight raw %}
<ItemGroup>
    <EmbeddedNativeLibrary Include="..\DemoTools.Android.Native\Release\ARM64\libDemoTools.so">
      <Link>lib\arm64\libDemoTools.so</Link>
      <ABI>arm64-v8a</ABI>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </EmbeddedNativeLibrary>
    <EmbeddedNativeLibrary Include="..\DemoTools.Android.Native\Release\ARM\libDemoTools.so">
      <Link>lib\arm\libDemoTools.so</Link>
      <ABI>armeabi</ABI>
      <ABI>armeabi-v7a</ABI>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </EmbeddedNativeLibrary>
    <EmbeddedNativeLibrary Include="..\DemoTools.Android.Native\Release\x64\libDemoTools.so">
      <Link>lib\x64\libDemoTools.so</Link>
      <ABI>x86_64</ABI>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </EmbeddedNativeLibrary>
    <EmbeddedNativeLibrary Include="..\DemoTools.Android.Native\Release\x86\libDemoTools.so">
      <Link>lib\x86\libDemoTools.so</Link>
      <ABI>x86</ABI>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </EmbeddedNativeLibrary>
    <None Include="Resources\AboutResources.txt" />
</ItemGroup>
{% endhighlight %}

Save the file and go back to visual studio. You will be prompted to reload the solution because the project file is changed outside visual studio. Do so. 

At this time the library can be built. Add all configurations to `Build -> Batch Build`. A library for *anyCPU* will be built, but it will contain native dll's for all platforms.

