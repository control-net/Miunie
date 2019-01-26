# Miunie Contributing

## Important resources

* [Setting up Miunie](https://github.com/discord-bot-tutorial/Miunie/blob/master/README.md#getting-started) - _Explains how to get a development version ready._
* [First time contributing](https://egghead.io/courses/how-to-contribute-to-an-open-source-project-on-github) - _Free course walking you through your first contribution._
* [Discord Server](https://discord.gg/cGhEZuk) - _Contributors get a cool role._
* [DSharpPlus Docs](https://dsharpplus.github.io/) - _Documentation for the library we're using._

## How to submit changes

If you're new to GitHub, we highly suggest you check out the **First time contributing** link in the [Important resources](#important-resources) section. It explains everything from committing your changes to creating a pull request.

With Miunie, a pull request must pass all Unit Tests and get a code review by a staff member. Once you create it, however, don't worry, we will check it as soon as possible.

**NOTE:** While we do ask that you add Unit Tests it is not a requirement. If you're not sure how to do them yourself then simply say so on the pull request.

:blush: We all make mistakes, so don't worry about it. After all, it's a learning experience. If you have any questions, feel free to ask on our [Discord Server](https://discord.gg/cGhEZuk) or in the appropriate comment section. Nobody is here to mock anyone and we appreciate **any contribution** no matter how small.

---

## Unit Tests

Unit test are required on everything except `Miunie.Discord`. Ideally you would follow TDD (Test Driven Development). However like metioned above, if you're not sure how to add Unit Tests. Just make sure you add a note in your PR that states you're not sure. This will allow others to either help you out in adding them or they can add the tests for you.

---

## First time contributing

Here are some tips to help you understand our style before you contribute your first changes.

* Write your commit message in the present tense - _"Add file" not "Added file"_
* Provide detailed descriptions of what you did.

---

## Found a bug

To report a :bug: bug, create a new [Issue](https://github.com/discord-bot-tutorial/Miunie/issues) and try to describe it as much as possible. Here are some tips:

* Write a guide to reproducing the problem if possible
* Add some information about your system, such as OS, CPU, GPU, etc.
* You can also follow a cool template, like [this one](https://gist.github.com/auremoser/72803ba969d0e61ff070).

And if you can fix the bug as well, that would be super stellar! :revolving_hearts:

---

## Style Guide

To make sure we all understand each other's code, we have a unified standard to follow.

### **You promise to follow the clean code bible**

### 80 Character Line Limit

* This is already included in the `settings.json` for VSCode.
* Below is an example `.vimrc` to follow this 80 character rule.

```bash
" 80 characters reminder
set cc=80
highlight ColorColumn ctermbg=0
```

### No body should be immited (Example Below)

This means, if you can add a body to an `if`, `else`, `foreach` or any other statement that can have a body `{ }` you should do it.

```cs
//Wrong way
if(foo is null) return;
//Correct way
if(foo is null) { return; }
```

### Commenting rules

* All comments should follow the following template.
  
#### Notes

Below is am example of how to correctly. Note how you should include your name when quoting.

```cs
// NOTE (Peter) : Something happens here
```

#### TODO

Below is an example for a `TODO` comment. Notw how it follows the same idea as the `Notes` template. However remember to keep `TODO` comments as short and to the point as possible.

```cs
// TODO (Peter): Your Todo Here
```

### Overall Style Standard

The standard is derived from [JetBrain's Resharper](https://www.jetbrains.com/resharper/) default settings. ReSharper is a Visual Studio extension that helps you with styling, debugging and IntelliSense. If you follow its advice, you will conform to our style.

#### Or do it manually

Feel free to check out [MSDN Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). We, however, recommend you look through the project to better understand the style we use. Don't worry, nobody will reject your contribution because of a line-break.
