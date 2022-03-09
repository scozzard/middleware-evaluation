# middleware-evaluation

### Table of Contents

- [General Information](#general-information)
- [Requirements](#requirements)
- [Running Locally](#running-locally)

### General Information

This application is a simple .NET 6 Web API that connects to a static XML API and transforms it to a JSON response based on the following [instructions](https://github.com/MiddlewareNewZealand/evaluation-instructions) and [OpenAPI specification](https://github.com/MiddlewareNewZealand/evaluation-instructions/blob/main/openapi-companies.yaml).

### Requirements

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (including the sdk to run via dotnet cli)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (if you would like an IDE to run/inspect locally)

### Running Locally

**via the dotnet cli**

If you have the .NET 6 SDK installed, go to the root directory and run the command `dotnet build`. If the build execution occurs without errors, run `dotnet run --project src/Evaluation.Api/Evaluation.Api.csproj`.

To visit the swagger homepage, go to https://localhost:7282/swagger/index.html.

**via the IDE**

Simply opening the solution and run the application in Visual Studio 2022 with any profile.
