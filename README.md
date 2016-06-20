# Scriba

[![GitHub license](https://img.shields.io/badge/license-Apache%202-blue.svg)](https://raw.githubusercontent.com/FuzzyWuzzie/Scriba/master/LICENSE) [![GitHub issues](https://img.shields.io/github/issues/FuzzyWuzzie/Scriba.svg)](https://github.com/FuzzyWuzzie/Scriba/issues)

This is a a small popup tool for entering notes about what you are currently working on (and reviewing them later).

Inspired by [this comment](https://www.reddit.com/r/programming/comments/4otbqr/programmer_interrupted/d4fji2g) from [/u/chrisdoner](https://www.reddit.com/user/chrisdoner).

The Scriba icon is a derivation of work [![CC-BY](http://mirrors.creativecommons.org/presskit/buttons/88x31/png/by.png)](https://thenounproject.com/itsmikerowe/) Mike Rowe.

## Contibuting Guidelines

This source code repository follows Vincent Driessen's
[Git branching model](http://nvie.com/posts/a-successful-git-branching-model/) such that the
`master` branch only ever contains release code. Normal development occurs in the `develop` branch,
while new features and hotfixes get their own branches, prefixed by `feature/` or `branch/`,
respectively.

Additionally, all commits to this source repository must follow conventional changelog notation:

### Commit Message Format

Each commit message consists of at least two parts: a **type** and a **subject**, separated by a colon (`:`):

```
<type>: <subject>

<(optional) body>

<(optional) footer>
```

Optionally, a **body** and **footer** may be included to give more context to the commit, or allow it to
close any issues / tickets.

Any line of the commit message cannot be longer 100 characters! This allows the message to be easier
to read on github as well as in various git tools.

### Type

Must be one of the following:

* **feat**: A new feature
* **fix**: A bug fix
* **docs**: Documentation only changes
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing
  semi-colons, etc)
* **refactor**: A code change that neither fixes a bug or adds a feature
* **perf**: A code change that improves performance
* **test**: Adding missing tests
* **chore**: Changes to the build process or auxiliary tools and libraries such as documentation
  generation
* **debug**: A change that was made for debugging purposes

### Subject

The subject contains succinct description of the change:

* use the imperative, present tense: "change" not "changed" nor "changes"
* don't capitalize first letter
* no dot (.) at the end

### Body

Just as in the **subject**, use the imperative, present tense: "change" not "changed" nor "changes"
The body should include the motivation for the change and contrast this with previous behavior.

### Footer

The footer should contain any information about **Breaking Changes** and is also the place to
reference GitHub issues that this commit **Closes**.

**Breaking Changes** are detected as such if the footer contains a line starting with BREAKING CHANGE:
(with optional newlines) The rest of the commit message is then used for this.