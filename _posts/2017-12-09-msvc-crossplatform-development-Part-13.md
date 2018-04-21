---
layout: single
title: MSVC Crossplatform Development Part 13
toc: true
---
**Xamarin Forms Library** This library exposes the platform specific .NET libraries to Xamarin forms. We need several steps to do this.
<!--more--> 

## Step 1: Create a PCL Library

In the solution Explorer, Right-click the `Libraries` folder and choose:

`Add -> New Project ->Visual C# -> Cross-Platform -> Class Library`

Name the project `DemoTools.NET.PCL`.

![Screenshot]({{ "/images/msvc_part13_1.png" | absolute_url }})

This library is a bit different from the ones we created before. We would like to use our native libraries from a Xamarin Forms project, but it is not possible to use them directly. Because the libraries we created are native to each platform, they cannot be linked to Xamarin Forms.

One way to overcome this is to create an interface for the classes we would like to use. We don't reference any other project here. This will change the way we interact with our library, but all in all it is a rather clean way of dealing with this problem.

After creating the project, a file called `DemoTools.NET.PCL.cs` is created. We don't need that file. You can safely delete it.

### Counter Inferface

Start with creating an interface for the `Counter` class. Add a new interface called `ICounter.cs` to the project with this content:

{% highlight c# %}
namespace DemoTools
{
    public interface ICounter
    {
        void Reset();
        void Add(int value);
        void Add(ICounter other);

        int Value { get; }
    }
}
{% endhighlight %}

### PlatformID Interface

The same is needed for the class `PlatformID`, implemented in `IPlatformID.cs`:

{% highlight c# %}
namespace DemoTools
{
    public interface IPlatformID
    {
        string Value { get; }
    }
}
{% endhighlight %}

*Note that we replaced the `Get()` methods with read-only properties. We have to modify parts of out class anyway, so we might as well adapt more to the C# way of doing things with properties.*

### Manager Interface

Even with these interfaces, we cannot create platform specific objects in Xamarin Forms. Instead, we will create an extra interface for a manager object *(which does not exist)*. This manager object will be able to create the objects on our behalf, from inside the platform specific code.

Add a new file to this project, called `IManager.cs`, and add the following content:

{% highlight c# %}
namespace DemoTools
{
    public interface IManager
    {
        ICounter CreateCounter();
        IPlatformID CreatePlatformID();

        ICounter MainCounter(); // for the functor object
    }
}
{% endhighlight %}

This is all the code needed in the PCL. Add it to the batch build list and build everything once more. There should be no errors.

## Step 2: Implement the interfaces

The PCL library declares interfaces, but we still have to create classes to implement those. And we need another shared project to do this. Add it to your solution with the name `PCLDemoTools`.

![Screenshot]({{ "/images/msvc_part13_2.png" | absolute_url }})

### PlatformID

Add a new class `PlatformID.cs` to the shared project. The code in this class will be easy:

{% highlight c# %}
namespace PCLDemoTools
{
    class PlatformID : DemoTools.PlatformID, DemoTools.IPlatformID
    {
        public string Value => Get();
    }
}

{% endhighlight %}

Even though the code itself is easy, I want to draw your attention to a few things:

- A different namespace is used. `DemoTools` is already in use and we do need to create similar classes. So either you give a different name to every class, or you use a different namespace. The namespace is the easiest solution.
- Because the namespace is different, we can use the same class names, and use the original class as a base class. 
- Next to the base class, we also reference the interface.
- Visual Studio can be a great help here: It will tell you which parts of the interface need to be implemented. It cannot do that right now though, because the shared project does not belong to any real project just now. This is why the base class and interface are not recognised.
- We need to implement the interface. In this case this is very simple. The `Value` property *(read-only)* of `DemoTools.IPlatformID` can be implmented with the `Add()` method of `DemoTools.PlatformID`.

Before doing more work, it would be good to reference this shared project from a real project. That way, intellisense will be able to its job.

The first project needing this code will be `DemoTools.NET.Android`. Even though we didn't need it for the Android Application we created before, we can't use that library with Xamarin Forms if we don't implement this.

In the project `DemoTools.NET.Android`, add a reference to this shared project. Also, add a reference to `DemoTools.NET.PCL` because we need the interface code to be known.

### Counter

Let's create another class to the shared library now: `Counter.cs`. This time I want you not to copy/paste the code I provide. Just add it bit by bit.

Start with the class declaration: 

{% highlight c# %}
class Counter : DemoTools.Counter, DemoTools.ICounter
{

}
{% endhighlight %}

At this point Intellisense will kick in and tell you that the interface is not implemented. It will even offer to do that for you. Allowing that will generate this code:

{% highlight c# %}
namespace PCLDemoTools
{
    class Counter : DemoTools.Counter, DemoTools.ICounter
    {
        public int Value => throw new NotImplementedException();

        public void Add(ICounter other)
        {
            throw new NotImplementedException();
        }

        void ICounter.Add(int value)
        {
            throw new NotImplementedException();
        }
    }
}
{% endhighlight %}

Great! There's three methods we need to implement. But something else might be less obvious: we don't need to implement the `Reset()`. This is because the signature of this method is exactly the same as the one in the base object.

Let's implement the rest of the code. We just need to call the base class methods, so that's easy:

{% highlight c# %}
class Counter : DemoTools.Counter, DemoTools.ICounter
{
    public int Value => Get();

    public void Add(ICounter other)
    {
        base.Add(other as Counter);
    }

    void ICounter.Add(int value)
    {
        base.Add(value);
    }
}
{% endhighlight %}

You'd think we'd be done with this class now, but no. Remember the functor in the native library called `MainCounter()`. So far, I did not find a way to get to this object without an extra class. We need another implementation of the interface which behaves differently and accesses the `MainCounter` in the interface implementation:

{% highlight c# %}
class MainCounter: DemoTools.ICounter
{
    public void Reset()
    {
        DemoTools.DemoTools.MainCounter().Reset();
    }

    public void Add(int value)
    {
        DemoTools.DemoTools.MainCounter().Add(value);
    }

    public void Add(ICounter other)
    {
        DemoTools.DemoTools.MainCounter().Add(other.Value);
    }

    public int Value { get => DemoTools.DemoTools.MainCounter().Get(); }
}
{% endhighlight %}

### Manager

The last interface we need to implement is `IManager`. Add a class `Manager.cs` to the shared project. This class will be responsible for creating the actual objects, because we have to hide that from the portable class:

{% highlight c# %}
namespace PCLDemoTools
{
    public class Manager : DemoTools.IManager
    {
        MainCounter mainCounter = new MainCounter();

        public ICounter CreateCounter()
        {
            return new Counter();
        }

        public IPlatformID CreatePlatformID()
        {
            return new PlatformID();
        }

        public ICounter MainCounter()
        {
            return mainCounter;
        }
    }
}
{% endhighlight %}

This concluded the creation of the PCL library. We've also added code to `DemoTools.NET.Android` so it might be a good idea to do the batch build again.