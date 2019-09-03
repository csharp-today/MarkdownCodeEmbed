# MarkdownCodeEmbed

MarkdownCodeEmbed is a dotnet tool designed to embed source files into Markdown files.

[![Build Status](https://dev.azure.com/mariuszbojkowski/Open%20Source%20projects/_apis/build/status/csharp-today.MarkdownCodeEmbed?branchName=master)](https://dev.azure.com/mariuszbojkowski/Open%20Source%20projects/_build/latest?definitionId=7&branchName=master) [![NuGet Version](https://img.shields.io/nuget/v/MarkdownCodeEmbed)](https://www.nuget.org/packages/MarkdownCodeEmbed/) [![NuGet Downloads](https://img.shields.io/nuget/dt/MarkdownCodeEmbed)](https://www.nuget.org/packages/MarkdownCodeEmbed/)

## Tool installation

The easiest way to install the tool is to use the `dotnet` tool.

```
dotnet tool install -g MarkdownCodeEmbed
```

## Syntax - Embed code inside Markdown file

To embed code from a source file add following line in the Markdown file:

```
[embed-code]: # (Path\To\Source\File.cs)
```

**Example**

The following source MarkdownFile from LucidCode library: [source file](https://raw.githubusercontent.com/csharp-today/LucidCode/master/Docs/RawDocumentationSource/Docs/Extensions/In.md)

Is transformed to: [file with embeded code](https://raw.githubusercontent.com/csharp-today/LucidCode/master/Docs/Extensions/In.md)

## Supported file types

| Markdown code block type | File types |
|--------------------------|------------|
| csharp | cs |
| css | css |
| html | htm, html |
| javascript | js |
| xml | csproj, xml |
| (default) | (all other file types) |

## Execute the tool

To embed a code inside Markdown files execute the tool with following parameters

```
MarkdownCodeEmbed --code DIRECTORY_WITH_SOURCE_FILES --input DIRECTORY_WITH_INPUT_MARKDOWN_FILES --output DIRECTORY_FOR_TRANSFORMED_MARKDOWN_FILES
```

## Build system integration

The tool can be integrated with build system and executed as post-build event.

Commands for the event:

```
dotnet tool install -g MarkdownCodeEmbed || dotnet tool update -g MarkdownCodeEmbed
MarkdownCodeEmbed --input MARKDOWN_INPUT_DIR --code SOURCE_CODE_DIR --output MARKDOWN_OUTPUT_DIR
```

**Example**: Examples project of the LucidCode library: [Examples.csproj](https://github.com/csharp-today/LucidCode/blob/master/Examples/Examples.csproj)
