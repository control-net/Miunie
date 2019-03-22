![Miunie](https://i.imgur.com/h2PjgF6.png)

<a href="https://ci.appveyor.com/project/discord-bot-tutorial/miunie">
  <img src="https://ci.appveyor.com/api/projects/status/cpaukw10ih35jl69?svg=true" alt="build status">
</a>
<a href="https://github.com/discord-bot-tutorial/Miunie/graphs/contributors">
  <img src="https://img.shields.io/github/contributors/discord-bot-tutorial/Miunie.svg" alt="license">
</a>
<a href="https://discord.gg/cGhEZuk">
  <img src="https://img.shields.io/discord/377879473158356992.svg" alt="license">
</a>
<a href="https://github.com/discord-bot-tutorial/Miunie/blob/master/LICENSE">
  <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="license">
</a>
<a href="https://discordbots.org/bot/411505318124847114" >
  <img src="https://discordbots.org/api/widget/status/411505318124847114.svg" alt="Community-Bot" />
</a>

[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/0)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/0)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/1)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/1)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/2)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/2)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/3)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/3)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/4)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/4)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/5)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/5)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/6)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/6)[![](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/images/7)](https://sourcerer.io/fame/petrspelos/discord-bot-tutorial/Miunie/links/7)

💖 **Made possible by these amazing people!** 🏆

## About

**Miunie** is a community Discord bot project.

The project's purpose is to connect people interested in 
[free and Open Source software](https://en.wikipedia.org/wiki/Free_and_open-source_software) development, the 
[C# programming language](https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29), and/or 
[Discord's bot API](https://discordapp.com/developers/docs/intro).

> You might be familiar with the [Community-Bot](https://github.com/discord-bot-tutorial/Community-Discord-BOT). This project is a complete rework of Community-Bot that improves the overall architecture.

🤠 **Want to give us a hand? (doesn't have to be by writing code)** Have this cookie 🍪 and read more about how exactly you can help in our [Contributing section](#contributing).

👩‍💻 **Just looking around?** Our [getting started guide](#getting-started) can get you up and running with your local Miunie in no-time.

🐛 **Found a bug?** Let us know by creating a [GitHub issue](https://github.com/discord-bot-tutorial/Miunie/issues/new). If you are hardcore and can squash the bug yourself 😩👌 create the issue first, then create a [Pull Request](https://help.github.com/en/articles/about-pull-requests).

💡 **Have an idea/suggestion?** Tell us all about it by creating a new [GitHub issue](https://github.com/discord-bot-tutorial/Miunie/issues/new).

## Getting Started

These instructions cover how to get a copy of the project running on your local machine for development and testing purposes.

> ℹ️ Checkout our [deployment section](#deployment) for notes on how to set the project up for hosting on a dedicated machine.

## Prerequisites

- **Your favorite IDE/Code Editor**
  - [Visual Studio Community 2017](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=Community&rel=15) - Ensure you have `.NET Core cross-platform development` installed.
  - [Visual Studio Code](https://code.visualstudio.com/) - Ensure you have the extension: [C# For VSCode - Omnisharp](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
  - (recommended for GNU/Linux :penguin:) [Rider: The Cross-Platform .NET IDE](https://www.jetbrains.com/rider/) from JetBrains
  - _Any other IDE/Code Editor you might want to use..._
- **The GIT CLI**
  - [Git Command Line Interface (CLI)](https://git-scm.com/downloads)
  - This is going to be used to both setup and deploy the project.
- **.NET Core**
  - [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download) (or newer)

> :warning: .NET Core does not always come with the VSC 2017 "Cross-platform dev" package.
>
> You can always install it yourself [here](https://dotnet.microsoft.com/download).

## Installation

This is a step by step guide to get Miunie up and running on your machine.

### Getting the source

1. [Fork The Original Repository](https://help.github.com/articles/fork-a-repo/)
2. Navigate to your fork.
3. [Clone](https://help.github.com/articles/cloning-a-repository/) your fork to your local machine.

### Setting up the environment - Visual Studio IDE

- The root directory of the project contains Miunie.sln, this is a solution file and you can open it with Visual Studio (See [Prerequisites](#Prerequisites))
- Once Visual Studio is open, you should try to [build the solution](https://docs.microsoft.com/en-us/visualstudio/ide/building-and-cleaning-projects-and-solutions-in-visual-studio?view=vs-2017).
  - You can do so by using the shortcut `F6` or going to `Build` > `Build Solution`.
- If the build fails due to some kind of error, make sure to fix it before continuing with this guide.

### Setting up the environment - Visual Studio Code

- Navigate to the cloned project's directory. _Should look similar to this:_

![Example](https://i.gyazo.com/57b6aecdb110529c7e61cee7db5b0757.png)
- Open the `src` directory, then right click on the whitespace. From the context menu select `Git Bash Here`. _Should look similar to this:_

![Example](https://i.gyazo.com/57f3233e7ca1a488fbdef8a855a750f9.png)
- The Git Terminal should now be open. Run the following commands in this specific order.
  - `dotnet restore` - Restores the project dependencies & Nuget Packages.
  - `dotnet build` - Ensures your clone of the repo is complete by ensuring the project builds. You should see the build succeeded with no errors.
- You can type `code .` to open Visual Studio Code in that directory and get started making your changes to the project.

### Setting up the environment - Rider IDE

- Open Rider IDE and on the "Welcome to JetBrains Rider" screen select the "Open Solution or Project" option.

![Open Solution or Project](https://i.imgur.com/BcDD0AQ.png)

- Navigate to your cloned Directory and select the `Miunie.sln` file.

- To build the project, navigate to `Build` > `Build Solution` or press <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>B</kbd>

## Running Tests

To run the unit tests already inclduded in the project follow the directions that are relevent to you.

- **Visual Studio IDE**
  - Either use the shortcut `CTRL+R, A` or go to `Test` > `Run` > `All Tests` 
  - Test output Example:
  
![Example Output Of Tests](https://i.gyazo.com/da85fac25967d0f740cfa7c91a2fb182.png)

> ℹ️ You can `Restore` > `Build` > `Test` via the CLI as shown below in Visual Studio Code setup.

- **Visual Studio Code**
  - Open your terminal application and navigate into the project's `src` directory.
  - `dotnet test [Name of any .Tests directory]` - This runs the unit tests for specific modules. You should see a completed output stating that all tests passed.

- **Rider IDE**
  - To run the Project's unit tests, navigate to `Tests` > `Run Unit Tests`.
  - You should see the test results in a "Unit Tests" window:

![Unit Tests window](https://i.imgur.com/xmjEbK7.png)

## Deployment
TODO

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
