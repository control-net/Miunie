# Miunie Configuration

This project provides needed configuration to systems like `Miunie.Discord`.

## When testing locally

You might need to change the configuration to contain a testing bot account's token while developing.

## Where is the configuration taken from?

* Any project that has an `App.config` in it, will have a config be generated after a build for it.

* Note that `App.config` is not included in the repository (because it contains keys). You will have to create your config file yourself.

> For example:
> 
> If there is an `App.config` inside `Miunie.Core` the resulting build will contain `Miunie.Core.dll.config`.

## What should be inside the configuration file?

In the `Miunie.ConsoleApp` project, a `App.config.template` is included.

It includes the following template:

```xml
<?xml version="1.0" encoding="utf-8" ?>  
<configuration>  
    <startup>   
        <supportedRuntime version="v4.0" sku="netcoreapp2.1" />  
    </startup>  
  <appSettings>  
    <add key="DiscordToken" value="Your-Token-Here"/>
  </appSettings>  
</configuration>

```

(?) While the supported runtime says `netcoreapp2.1` even though we're using `netcoreapp2.2`, there doesn't seem to be an issue with it for now.

## What about unit test configuration?

In unit tests, the running application is `testhost.dll`. This means the configuration system will use `testhost.dll.config`, which is included for example in `Miunie.Configuration.XUnit.Test` project to test the ability to retreive a token.

## What configuration file is the main application looking for?

Since Miunie runs through a Console Application, the `Miunie.ConsoleApp` is the one with the config.

`Miunie.ConsoleApp.dll.config` will be used when running normally through a console.
