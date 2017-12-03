---
layout: post
title: MSVC Crossplatform Development Part 4
---
So far we didn't do anything crossplatformy, but that's gonna change now. In this part, we will create a native library for Android.
<!--more--> 

In the solution Explorer, Right-click the `Libraries` folder and choose:

`Add -> New Project ->Visual C++ -> Cross Platform -> Android -> Static Library (Android)`

Name the library `DemoTools.Android.Native`

![Screenshot]({{ "/images/msvc_part4_1.png" | absolute_url }})

Again, add the Shared Project by right-clicking on the project Refrences:

`Add Reference -> Shared Project -> DemoTools`

## Project Properties

Again, change the Project Properties (Note the extra `$Platform` variable in this case):

`Project Properties -> Configuration Properties -> General`

First select `All Configurations` and `All Platforms`. Change
- Output Directory: `$(ProjectDir)\$(Configuration)\$(Platform)\`
- Intermediate Directory: `$(ProjectDir)\$(Configuration)\$(Platform)\Intermediate\`
- Target Name: `libDemoTools`

Also, add `_ANDROID;` to the Preprocessor Definitions for all platforms and configurations. We need this for the conditional compilation in `PlatformID.cpp`.

## Compile

In the top menu, select

`Build -> Batch Build`

Select all configurations belonging to the Android project and build.
