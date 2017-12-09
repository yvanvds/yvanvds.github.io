---
layout: post
title: MSVC Crossplatform Development Part 15
---
**TODO** This is the end of this series for now. Let's go over what should be next.
<!--more-->
So far, we have finished the following:
- Native C++ library for Windows 
- Native C++ library for Android
- .NET framework library for Windows, which can be used in WPF projects
- .NET universal library for Windows, which can be used in UWP projects (Windows Only)
- .NET Android library
- .NET PCL library for use with Xamarin Forms

We've also put together some small applications for testing. It would be great to replace the lot of them with Unit Tests, but that is less urgent for me because that approach is a bit limited for my real target, being an audio library.

Next on the list should be:
- Native C++ libraries for Linux, MacOS en iOS.
- .NET core library with support for all platforms, if possible.
- .NET implementation for Windows IOT.
- Completing the Xamarin Forms approach for iOS and UWP.