# Miunie Contributing

## Important resources

* [Setting up Miunie](https://github.com/discord-bot-tutorial/Miunie#getting-started) - _Explains how to get a development version ready._
* [First time contributing](https://egghead.io/courses/how-to-contribute-to-an-open-source-project-on-github) - _Free course walking you through your first contribution._
* [Discord Server](https://discord.gg/cGhEZuk) - _Always someone ready to help on our Discord Server._
* [DSharpPlus Docs](https://dsharpplus.github.io/) - _Documentation for the library we're using._

## How to submit changes

![Forking Miunie](https://i.imgur.com/mz93Muk.png)

> Forking is a way of collecting all of the code and other good stuff we have in this repository and making a new repository off of it that is just yours to play with.
>
> You are fully in charge of your fork and don't need to worry about making any change you have in mind.

* To fork Miunie, you will need a GitHub account.

* Once you are logged in, just click the "Fork" button near the top of the page:

![Fork button](https://i.imgur.com/oNViGmX.png)

Once you have forked the repository, you can view it on your profile page under "Repositories".

If you're not sure whether you're looking at a fork or the original, look near the top of the page where the name of the repository is.

You can be sure you're looking at your fork if there is your name in front of ` / Miunie` and if it has a little "Forked from..." message under it.

![Fork message](https://i.imgur.com/gjJHKfQ.png)

After your fork is ready, you are free to [clone it to your local machine](https://help.github.com/en/articles/cloning-a-repository).

![Keeping forks up to date](https://i.imgur.com/9XsurGf.png)

What happens when the original repository gets updated with some sweet features you'd like in your own fork? (or if you want to avoid merge conflicts)

You can update your own fork. For that, you will need to have it [cloned to your local machine](https://help.github.com/en/articles/cloning-a-repository).

Using the command line, after you navigate to your cloned repository, you can first check if you have a remote reference to the upstream (original repo).

```
git remote -v
```

This will output a list of remote locations your repository is connected to.

If you only see entries starting with `origin`, you do not have an upstream link and will need to create it.

- Setting up an upstream remote

To setup an upstream remote run the following command in your cloned repo:

```
git remote add origin https://github.com/discord-bot-tutorial/Miunie.git
```

Verify the origin was added by running the `git remote -v` command again.

- Updating your fork

Once you have the upstream remote setup, you can fetch and merge it with:

```
git fetch upstream
```

and

```
git merge upstream/master
```

This will update your local clone with the upstream changes. Now you just need to push these changes to your fork with

```
git push
```

and you're all set!

![Merging changes](https://i.imgur.com/lDKN1Gl.png)

Once your fork is head of the upstream (original repository), you have an option to create a pull request (ask for your changes to be merged into the main repository).

To create a pull request open your forked repository on GitHub. You should see a Pull request button right above the list of files.

![Pull request button](https://i.imgur.com/eXb69te.png)

Once you select to create the Pull request, GitHub will show you what changes you've made, if everything looks alright, click on "Create pull request".

After that, you will see a form with a pull request template. Try to fill the description as best as you can, describe what changes you've made.

Once you confirm your pull request, you just need to wait for the automated tests to finish and to get your changes reviewed by a staff member.

With Miunie, a pull request must pass all Unit Tests and get a code review by a staff member. Once you create it, however, don't worry, we will check it as soon as possible.

**NOTE:** While we do ask that you add Unit Tests it is not a requirement. If you're not sure how to do them yourself then simply say so in the pull request.

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

Below is am example of how to correctly comment a note. Note how you should include your name when quoting.

```cs
// NOTE (Peter) : Something happens here
```

#### TODO

Below is an example for a `TODO` comment. Note how it follows the same idea as the `Notes` template. Please remember to keep `TODO` comments as short and to the point as possible.

```cs
// TODO (Peter): Your Todo Here
```

### Overall Style Standard

The standard is derived from [JetBrain's Resharper](https://www.jetbrains.com/resharper/) default settings. ReSharper is a Visual Studio extension that helps you with styling, debugging and IntelliSense. If you follow its advice, you will conform to our style.

#### Or do it manually

Feel free to check out [MSDN Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). We, however, recommend you look through the project to better understand the style we use. Don't worry, nobody will reject your contribution because of a line-break.
