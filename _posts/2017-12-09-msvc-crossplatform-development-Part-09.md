---
layout: post
title: MSVC Crossplatform Development Part 9
---
**.NET Universal Library** In this part we will create another .NET library for Universal Windows Applications. This library can be used to create UWP applications. 
<!--more--> 

Because of the way SWIG handles pointers, it is not possible to use this approach on older Windows versions. The earliest Windows version supporting this is the Windows 10 Fall Creators update. (2017)

In the solution Explorer, Right-click the `Libraries` folder and choose:

`Add -> New Project -> Visual C# -> Windows Universal -> Class Library`

Name the project `DemoTools.NET.Universal`.

![Screenshot]({{ "/images/msvc_part9_1.png" | absolute_url }})

Set the target an minimum version to *Windows 10 Fall Creators Update* or newer. **(Note: you can only do this if you have these windows updated installed on your development machine.)**

Only a few extra steps are needed to create this library.

1. Add a reference to the shared project `NetDemoTools'.
2. Add `DemoTools.dll' from the project *Demotools.Windows.Native* as an existing item. *(Add as link)*
3. Open the properties of this link and select `Copy if Newer'.

We'll add this library to the batch build by opening `Project -> Batch Build` and selecting the relevant entries for this library. Try building it. The library should build without generating any errors at this point.