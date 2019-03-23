# Miunie Configuration

Configuration is a way of storing your connection information, such as the bot token, for Miunie to use.

The `Miunie.Configuration` C# project is where the reading of configuration is implemented. You don't need to understand it, however, to use it.

# How does configuration work?

The configuration itself is stored in an [XML](https://en.wikipedia.org/wiki/XML) file.

When you compile Miunie, the project uses the configuration from `Miunie.ConsoleApp/App.config`.

The project comes with a configuration template: `Miunie.ConsoleApp/App.config.template`. To use the template:

- Create a copy of the `App.config.template` file.
- Rename it to `App.config`
- Open the file for editing
- Find the line with `key="DiscordToken"`
- Replace the `Your-Token-Here` placeholder with your bot's token.

> ℹ️ Our repository is setup to ignore `App.config` to prevent you from accidentally committing your token into the repository.

## Where is the configuration taken from?

* Any project that has an `App.config` in it, will have a config generated after its build.

> ℹ️ If there is an `App.config` inside `Miunie.ConsoleApp` the resulting build will contain `Miunie.ConsoleApp.dll.config`.

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
