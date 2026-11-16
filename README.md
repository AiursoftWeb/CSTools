# Aiursoft CSTools

<a name="cstools"></a>

[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/AiursoftWeb/cstools/blob/master/LICENSE)
[![Pipeline stat](https://badge-proxy.aiursoft.com/aiursoft/CSTools/badges/master/pipeline.svg)](https://gitlab.aiursoft.com/aiursoft/CSTools/-/pipelines)
[![Test Coverage](https://badge-proxy.aiursoft.com/aiursoft/CSTools/badges/master/coverage.svg)](https://gitlab.aiursoft.com/aiursoft/CSTools/-/pipelines)
[![S360 compliance](https://s360.aiursoft.com/badge.svg?project=aiursoft%2Fcstools)](https://s360.aiursoft.com/SupplyChain?tab=projects&project=aiursoft%2Fcstools&scope=baseline&ecosystem=all)
[![NuGet version (Aiursoft.CSTools)](https://img.shields.io/nuget/v/Aiursoft.CSTools.svg)](https://www.nuget.org/packages/Aiursoft.CSTools/)
[![Man hours](https://manhours.aiursoft.com/r/github.com/aiursoftweb/cstools.svg)](https://manhours.aiursoft.com/r/github.com/aiursoftweb/cstools.html)

CSTools are Aiursoft's common CSharp extends for all apps. It contains many useful tools for CSharp developers.

## Overview

### What is CSTools?

Common C# helpers provide string conversion, reflection, operating-system utilities and small services for Aiursoft applications.

## Installation

Add the package to your .NET project:

<a name="how-to-install"></a>

To install `Aiursoft.CSTools` to your project from [nuget.org](https://www.nuget.org/packages/Aiursoft.CSTools/):

```bash
dotnet add package Aiursoft.CSTools
```

Requires .NET 10 or later.

## Usage

Start with this complete console `Program.cs` after installing the package:

```csharp
using Aiursoft.CSTools.Tools;

var encoded = "Hello from CSTools".StringToBase64();
Console.WriteLine(encoded.Base64ToString());
```

## Configuration

No configuration file or external service is required for the string-conversion example. Helpers are configured through their method arguments. Services such as `Counter` can be constructed directly or registered with dependency injection; inspect their intended lifetime before sharing an instance.

## Development

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

From the repository root, restore, build and test:

```bash
dotnet restore Aiursoft.CSTools.sln
dotnet build Aiursoft.CSTools.sln --no-restore
dotnet test Aiursoft.CSTools.sln --no-restore
```

Run `./lint.sh` for the existing static checks.

## How to contribute

<a name="how-to-contribute"></a>

There are many ways to contribute to the project: logging bugs, submitting pull requests, reporting issues, and creating suggestions.

Even if you have push rights on the repository, you should create a personal fork and create feature branches there when you need them. This keeps the main repository clean and your workflow cruft out of sight.

We're also interested in your feedback on the future of this project. You can submit a suggestion or feature request through the issue tracker. To make this process more effective, we're asking that these include more information to help define them more clearly.

## License

Licensed under the [MIT License](https://github.com/AiursoftWeb/cstools/blob/master/LICENSE).
