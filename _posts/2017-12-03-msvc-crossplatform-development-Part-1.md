---
layout: post
title: MSVC Crossplatform Development Part 1
---
This is the first part of a series about crossplatform development with Visual Studio. A small C++ library will be created and ported to all possible platforms. C# wrapper libraries will also be created and used.
<!--more--> 

The complete source code for this series can be found on [GitHub](https://github.com/yvanvds/yvanvds.github.io/tree/master/codeProjects/DemoTools).

## Setup

In this first part we'll setup a solution which will contain all projects. We'll start with a Shared Items Project, which will contain the C++ code of our library.

`File -> New -> Project...`

![Screenshot]({{ "/images/msvc_part1_1.png" | absolute_url }})

Before adding code, we will setup our solution structure. In the Solution Explorer, right click the solution:

`Add -> New Solution Folder`

Add two folders: `Libraries` and `Tests`. Next we will need some code files. In the Solution Explorer, right click the Shared Project:

`Add -> New Item`

Add 5 new files:
- Preprocessor.h
- Counter.h
- Counter.cpp
- PlatformID.h
- PlatformID.cpp

## Preprocessor

The header file `Preprocessor.h` that will make your life easier when creating a Windows DLL. This pays off when you're working with a larger project. It will add the necessary import and export declaration to the code when needed.

{% highlight c++ tabsize=4 %}
#pragma once

#ifdef _WINDOWS
#ifdef _USRDLL
#define API __declspec (dllexport)
#elif defined _IMPORTDEMOTOOLS 
#define API __declspec (dllimport)
#endif
#endif

#ifndef API
#define API
#endif
{% endhighlight %}

## Counter

The first class is a rather simple counter. It will be used to test if the library works on every platform. It also includes a so-called Functor. This is a typical C++ paradigm which I tend to use now and then. Functors are a bit like global objects, with the exception that they will be created when you first access them, instead of at the start of the program. Adding the Functor `MainCounter` will allow us to test how well this is portable to C#.

Another idea to test is operator overloading. We'll see how the `operator+` methods will behave later on.

Counter.h:
{% highlight c++ tabsize=2 %}
#pragma once
#include "PreProcessor.h"

namespace DemoTools {

	class API Counter
	{
	public:
		Counter();

		void Reset();
		void Increase();
		int  Get() const;

		Counter & operator+(int value);
		Counter & operator+(const Counter & other);

	private:
		int counter;
	};

	API Counter & MainCounter();
}
{% endhighlight %}

Counter.cpp
{% highlight c++ tabsize=4 %}
#include "Counter.h"

using namespace DemoTools;

Counter::Counter() : counter(0) {}

void Counter::Reset()
{
	counter = 0;
}

int Counter::Get() const
{
	return counter;
}

void Counter::Increase()
{
	counter++;
}

Counter & DemoTools::Counter::operator+(int value)
{
	counter += value;
	return *this;
}

Counter & DemoTools::Counter::operator+(const Counter & other)
{
	counter += other.counter;
	return *this;
}


Counter & DemoTools::MainCounter() {
	static Counter s;
	return s;
}
{% endhighlight %}

## PlatformID

A second class to implement is `PlatformID`. This class will be able to return a text telling us on what platform the application is running. (To be precise, it will return a text depending on the platform the library is compiled for.) The class will allow us to observe how platform dependent code is handled.

Equally important is that this class demonstrates the pointer implementation paradigm. When creating a crossplatform library, it is crucial to hide dependencies from the interface. Pointer implementations allow us to do just that. The internal objects of the class (in this case an `std::string`) will not be exposed in the header file. This way, an application doesn't have to know about `std::string` even though it is used in the library.

PlatformID.h
{% highlight c++ tabsize=4 %}
#pragma once

#include "PreProcessor.h"

namespace DemoTools {
	class PlatformIDImpl;

	class API PlatformID
	{
	public:
		PlatformID();
		~PlatformID();

		const char * Get();

	private:
		PlatformIDImpl * pimpl;
	};
}
{% endhighlight %}

PlatformID.cpp
{% highlight c++ tabsize=4 %}
#include "PlatformID.h"
#include <string>

using namespace DemoTools;

namespace DemoTools {
	class PlatformIDImpl {
	public:
		std::string content;
	};
}

PlatformID::PlatformID() : pimpl(new PlatformIDImpl())
{
#ifdef _WINDOWS
	pimpl->content = "Running on Windows";
#elif defined _ANDROID
	pimpl->content = "Running on Android";
#elif defined _IOS
	pimpl->content = "Running on iOS (Urgh!)";
#else
	pimpl->content = "Running on undefined Platform";
#endif
}

PlatformID::~PlatformID()
{
	delete pimpl;
}

const char * PlatformID::Get()
{
	return pimpl->content.c_str();
}
{% endhighlight %}

That's all the work we're gonna do on the actual library. At this point, your Solution Explorer should look like this:

![Screenshot]({{ "/images/msvc_part1_2.png" | absolute_url }})