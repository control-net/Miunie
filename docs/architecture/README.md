# Architecture

This part of the documentation deals with the architecture philosophy of Miunie.

The goal of this architecture is to make development as easy as possible. In the following sections, we will go into details about what exactly is meant by that.

This documentation is suitable for developers of any skillset and should equip you with all the important concepts needed to begin development on Miunie yourself. The principles in the following sections provide plenty of detailed examples and point to relevant documentation sections.

If you feel a section doesn't explain something in enough detail, please [create an issue](https://github.com/discord-bot-tutorial/Miunie/issues/new) about it.

## Decoupling from 3rd party libraries

Being a Discord bot, a lot of Miunie's feature heavily depend on a 3rd party library handling interaction with Discord's API (so called Wrapper). Many bot developers decide to couple with their wrapper. In order to better understand why Miunie wants to stay decoupled, let's first explain coupling and dependency.

### Coupling

When we're talking about coupling, we usually refer to an implementation strategy.

Consider for a second that we have two classes. `WoodenBox` and `House`.

Let's also consider that the `House` class references `WoodenBox`. Perhaps by creating a new instance of it somewhere in its source code. (ie. `new WoodenBox();`)

If we one day decide to substitute `WoodenBox` for perhaps a `SteelBox`, we cannot do that without editing `House` and changing old code. This means that our `House` is coupled to `WoodenBox`. It can only work with a `WoodenBox`. Even if the difference is just the type it instantiates.

### Dependency

Unlike coupling, dependency is something we cannot get rid of.

To continue our `House` and `WoodenBox` example...

In order for `House` to be able to instantiate `WoodenBox`, it has to have a reference to it. In case of C# this reference is a `using` directive.

The most frequent reference to dependency in Miunie's architecture is the dependency of modules. Our project considers individual C# projects, each with their own `.csproj`, to be a single module. These modules are compiled into separate `.dll` files.

When any class from `Module A` references any class from `Module B`, we say `Module A` **depends** on `Module B`.

### The implications of dependencies

Let's consider for a second what it means for a class to depend on another.

```cs
class House
{
    public void Foo()
    {
        var b = new WoodenBox();
        b.Bar();
    }
}
```

In this example, `House` depends on `WoodenBox`.

When any part of `WoodenBox` changes, `House` needs to recompile. In a way, changes to `WoodenBox` can break `House`. It may, however, be in our interest to avoid this attribute. If `House` is a more important and central component, we would like to protect it from changes to other components.

In C# and many other Object Oriented languages, we do this with **Dependency Inversion**. Where we make `WoodenBox` depend on `House` instead and therefore, protect `House` from changes to `WoodenBox`.

### Dependency Inversion

Let's take the example from the previous section and invert the current dependency.

The general strategy to invert a dependency is to use an `interface`.

```cs
class House
{
    private readonly IBox _box;

    public House(IBox box)
    {
        _box = box;
    }

    public void Foo()
    {
        _box.Bar();
    }
}
```

Here we first introduced an interface `IBox` which defined all methods `House` requires. In our case that would only be the `Bar` method used inside `House`'s `Foo`.

This interface would be in the same component (ie. C# project) as `House`.

Now `WoodenBox` has to first implement the `IBox` interface. This will force `WoodenBox` to implement the needed methods.

The most important attribute here, however, is the fact that by trying to implement `IBox`, `WoodenBox` has to reference it. Which means it has to add the needed `using` directives. And since `IBox` is inside the same module as `House`, the dependency now points the other way.

Now changes to `House` have potential to break `WoodenBox`. But there is nothing `WoodenBox` can do to break `House`. A change that break `WoodenBox` for example, is a change to the interface itself.

If we require a new method to be implemented as part of `IBox`, `WoodenBox` breaks until we implement it.

## Why we decouple from 3rd party libraries

From the definitions above, you should now understand why we would want to **decouple** from a particular wrapper. It's because it gives us the option to switch a wrapper library by only writing new code and not changing the old one.

We also want to **invert our dependency** on the wrapper. We want changes to the wrapper to not effect our code. We achieve this by having a single module (C# project) that is tightly **coupled** to a particular wrapper, however, is also dependent on our Core.

This in reality means that changes to the wrapper can really break only our coupled module.

Should the cost of repairing this module be too high, we can outright swap the library.


_This document is still work in progress and more will be written in the near future._
