# Byndyusoft.ModelResult
Result of domain model logic similar to ActionResult

| | | |
| ------- | ------------ | --------- |
| [**Byndyusoft.ModelResult**](https://www.nuget.org/packages/Byndyusoft.ModelResult/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.ModelResult.svg)](https://www.nuget.org/packages/Byndyusoft.ModelResult/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.ModelResult.svg)](https://www.nuget.org/packages/Byndyusoft.ModelResult/) |
| [**Byndyusoft.ModelResult.AspNetCore**](https://www.nuget.org/packages/Byndyusoft.ModelResult.AspNetCore/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.ModelResult.AspNetCore.svg)](https://www.nuget.org/packages/Byndyusoft.ModelResult.AspNetCore/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.ModelResult.AspNetCore.svg)](https://www.nuget.org/packages/Byndyusoft.ModelResult.AspNetCore/) |


## Installing

```shell
dotnet add package Byndyusoft.ModelResult
```

## Usage

ModelResult can be used to return either "ok" or "error" value without explicit casting. Here are some usage examples:

```csharp
public ModelResult<int> GetId(SampleEntity entity)
{
    if (entity.HasId())
        return entity.Id;

    return new ErrorModelResult("Project.SampleEntity.NoId", "Entity does not contain Id");
}

public ModelResult ValidateEntity(SampleEntity entity)
{
    var errorInfoItems = new List<ErrorInfoItem>();

    if (entity.HasId() == false)
        errorInfoItems.Add(new ErrorInfoItem("Id", "Entity does not contain Id"));

    if (errorInfoItems.Any())
        return new ErrorModelResult("Project.SampleEntity.NotValid", "There are validation errors", errorInfoItems.ToArray());

    return new OkModelResult();
}

public ModelResult<EntityInfoDto> GetEntityInfoDto(SampleEntity entity)
{
    ModelResult<int> idResult = GetId(entity);

    if (idResult.IsError())
    {
        _logger.LogError("Error getting id: {@ErrorInfo}", idResult.GetError());
        return idResult.AsSimple();
    }

    var entityInfoDto = new EntityInfoDto {Id = idResult.Result};
    return entityInfoDto;
}
```

# ModelResult.AspNetCore
Converter to ActionResult from ModelResult

## Installing

```shell
dotnet add package Byndyusoft.ModelResult.AspNetCore
```

## Usage

```csharp
[HttpGet("{id:long}")]
public async Task<ActionResult<EntityInfoDto>> GetEntityInfoDto([FromRoute] long id,
    [FromServices] IGetEntityInfoDtoUseCase useCase, CancellationToken cancellationToken)
{
    var result = await useCase.GetAsync(id, cancellationToken);
    return result.ToActionResult();
}
```

If `result` is "ok" result then action method will return message with 200 code and dto content. Otherwise if it is "error" result it is usually will be transformed to 400 code with error info that contains code, message and items. Current version has one exception: if error code is equal to `Byndyusoft.ModelResult.Common.CommonErrorCodes.NotFound` there will be 404 code without any content.

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 6.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Package development lifecycle

- Implement package logic in `src`
- Add or adapt unit-tests in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
