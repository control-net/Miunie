![Miunie](https://i.imgur.com/h2PjgF6.png)

<a href="https://dev.azure.com/spelos/miunie/_apis/build/status/discord-bot-tutorial.Miunie?branchName=master">
  <img src="https://dev.azure.com/spelos/miunie/_apis/build/status/discord-bot-tutorial.Miunie?branchName=master" alt="build status">
</a>
<a href="https://github.com/discord-bot-tutorial/Miunie/graphs/contributors">
  <img src="https://img.shields.io/github/contributors/discord-bot-tutorial/Miunie.svg" alt="license">
</a>
<a href="https://discord.gg/cGhEZuk">
  <img src="https://img.shields.io/discord/377879473158356992.svg" alt="license">
</a>
<a href="https://github.com/discord-bot-tutorial/Miunie/blob/master/LICENSE">
  <img src="https://img.shields.io/badge/License-GPLv3-blue.svg" alt="license">
</a>
<a href="https://discordbots.org/bot/411505318124847114" >
  <img src="https://discordbots.org/api/widget/status/411505318124847114.svg" alt="Community-Bot" />
</a>

[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/0)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/0)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/1)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/1)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/2)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/2)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/3)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/3)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/4)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/4)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/5)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/5)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/6)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/6)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/7)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/7)

💖 **Made possible by these amazing people!** 🏆

## About

**Miunie** is a community Discord bot project.

🎮 Run Miunie on your favorite platform!

![platforms image](img/apps.png)

Currently, Miunie can run on the following platforms:

|                   | Miunie.ConsoleApp | Miunie.WindowsApp | Miunie.AspNet |
|-------------------|:-----------------:|:-----------------:|--------------:|
| Windows 7         | ✅                | ❌               | ✅            |
| MacOS / Linux     | ✅                | ❌               | ✅           |
| Windows 10        | ✅                | ✅               | ✅            |
| IoT Devices       | ✅                | ✅               | ✅            |
| Mobile            | ❌                | ✅               | ✅           |
| Xbox              | ❌                | ✅               | ❔            |
| HoloLens          | ❌                | ✅               | ❔            |
| Surface Hub       | ❌                | ✅               | ❔            |

> ASP.NET implementation of Miunie is still under development.

🤠 **Wanna help?** Checkout the [contributing section](#contributing).

👩‍💻 **Wanna try it out?** See the [getting started guide](#getting-started).

🐛 **Found a bug?** Let us know by creating a [GitHub issue](https://github.com/discord-bot-tutorial/Miunie/issues/new).

💡 **Have an idea/suggestion?** Tell us all about it through a [GitHub issue](https://github.com/discord-bot-tutorial/Miunie/issues/new).

## Getting Started

These instructions are going to walk you through downloading a copy of Miunie and running it.

> 🔧 If you want to know how to setup your environment for development, see [this guide](CONTRIBUTING.md#setting-up-a-development-environment).

> ℹ️ Checkout our [deployment section](#deployment) for notes on how to set the project up for hosting on a dedicated machine.

## Prerequisites

- **IDE / Code Editor**

For picking which IDE to use, take a look at the following table.

* ⭐ - Recommended for a given platform
* ⚠ - Known issues for a given platform
* ✅ - Will work for a given platform

> _If you are a beginner, we suggest sticking to the ⭐ recommended options._

|                   | Miunie.ConsoleApp | Miunie.WindowsApp | Miunie.AspNet |
|-------------------|:-----------------:|:-----------------:|--------------:|
| [Visual Studio **2019** (Community)](https://visualstudio.microsoft.com/downloads/)| ⭐ | ⭐ | ⭐ |
| [Visual Studio 2017 (Community)](https://visualstudio.microsoft.com/downloads/)| ⚠ | ⚠ | ⚠ |
| [Visual Studio Code](https://code.visualstudio.com/)                           | ✅ | ❌ | ⚠  |
| [Rider: The Cross-Platform .NET IDE](https://www.jetbrains.com/rider/)         | ✅ | ❌ | ✅ |

- **Visual Studio Prerequisites**

During the installation process of Visual Studio, you should always include the following options:

![visual studio base dependencies](img/vsdeps-base.png)

If you would like to build the ASP.NET project, include the following option:

![visual studio asp dependencies](img/vsdeps-asp.png)

If you would like to build the Universal Windows Platform App, include the following option:

![visual studio UWP dependencies](img/vsdeps-win.png)

- **Ensuring .NET Core 2.2 (or higher) is installed**

To make sure you have the correct version of .NET Core, open your `command promt`/`Terminal`/`Powershell` and type in the following command:

```
dotnet --list-sdks
```

_Here is an example of this command's output on Windows:_

```
2.1.602 [C:\Program Files\dotnet\sdk]
2.2.202 [C:\Program Files\dotnet\sdk]
3.0.100-preview3-010431 [C:\Program Files\dotnet\sdk]
```

Your output will most likely differ a bit.

If you see a version higher or equal to `2.2`, you can continue to the next step.

If you don't see the right version or if you got a response similar to `Command not found`, you need to download an install the latest version of .NET Core.

You can install it [here](https://dotnet.microsoft.com/).

- **Getting a copy of the project**

> If you want to clone or fork this repository instead, please follow the [contributing guide](CONTRIBUTING.md).

Download the project by pressing the green "Clone or download" button and selecting the "Download ZIP" option.

![GitHub download repo](img/github-download.png)

Unzip the directory and navigate into the `src` folder inside it.

Open the `Miunie.sln` in your IDE.

🎉 Congratulations, you are now set up for building and running Miunie.

If you need more information about how to build and run the project, please contact us on our Discord server:

<a href="https://discord.gg/cGhEZuk">
  <img src="https://img.shields.io/discord/377879473158356992.svg" alt="license">
</a>

## Deployment

- **Deploying to a Raspberry Pi**

There is a two part video tutorial explaining how to deploy a .NET Core Bot onto a Raspberry Pi.
  - [**[PART 1]** Self-Host Discord bot on Raspberry Pi - Setting up the device](https://www.youtube.com/watch?v=JWXbIUETYY8)
  - [**[PART 2]** Self-Host Discord bot on Raspberry Pi - Deploying the bot](https://www.youtube.com/watch?v=O6ffnRcW9DM)

> ℹ️ Know a way to deploy Miunie to a different target? We'd love to see your suggestion in form of a [GitHub issue](https://github.com/discord-bot-tutorial/Miunie/issues/new).

## Known Bugs

There are currently no known bugs! Hooray!

## Built With

- [.Net Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) - Platform Used
- [DsharpPlus](https://github.com/DSharpPlus/DSharpPlus) - Discord API wrapper library
- 💙 Collaborative spirit
- ❤️ Love and Care

## Contributing

There is a number of different tasks you can help with, including non-programming ones. This is a community project that wouldn't be possible without the help of **amazing people like yourself**. <3

Your contributions might include:
- Grammar/Spelling fixes to document files (such as this README)
- Grammar/Spelling fixes to embedded text resources Miunie uses at runtime.
- Improving documentation
- New features you implement
- Feature and Bug fixes
- Suggestions & Ideas in form of a GitHub issue
- Reviewing Pull Requests made by other contributors
- _And more..._

If you are interested, feel free to read a more detailed [CONTRIBUTING.md guide](CONTRIBUTING.md) that will walk you through your first contribution.

## Versioning

There is currently no versioning system used. Feel free to create a new Issue suggesting one.

## Authors

- Petr Sedláček - Initial work - [PetrSpelos](https://github.com/petrspelos)

See also the list of [contributors](https://github.com/discord-bot-tutorial/Miunie/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/discord-bot-tutorial/Miunie/blob/master/LICENSE) file for details

## Acknowledgments

- Thank you to all people who contributed, especially those coming from my Discord-BOT-Tutorial Discord Server.
