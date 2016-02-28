# XmlAssertions
Easy assertions for testing XMLs

## Usage 
Run your XML assertions easily, the fluent way:

```csharp
@"<person name = ""Piotr"" />".XmlShould().HaveAttribute("name");
@"<person name = ""Piotr"" />".XmlShould().HaveAttribute("name", "Piotr");
@"<person name = ""Piotr"" />".XmlShould().BeEqualTo(expected);
```
