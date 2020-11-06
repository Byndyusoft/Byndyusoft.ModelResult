# ModelResult
Result of domain model login similar to ActionResult

## Installing

```shell
dotnet add package Byndyusoft.ModelResult
```

# ModelResult.Text.Json
Converters for serialization via System.Test.Json

## Installing

```shell
dotnet add package Byndyusoft.ModelResult.Test.Json
```

# ModelResult.AspNetCore
Converter to ActionResult from ModelResult

## Installing

```shell
dotnet add package Byndyusoft.ModelResult.AspNetCore
```

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 3.1 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet-core/3.1).

## General folders layout

### src
- source code

### tests

- unit-tests

### example

- example console application

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
