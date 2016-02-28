# XmlAssertions
Easy assertions for testing XMLs

## Usage 
Run your XML assertions easily, the fluent way:

```csharp
@"<person name = ""Piotr"" />".XmlShould().HaveAttribute("name");
@"<person name = ""Piotr"" />".XmlShould().HaveAttribute("name", "Piotr");
@"<person name = ""Piotr"" />".XmlShould().BeEqualTo(expected);
```

## Exceptions 
Thanks to informative exception messages, you'll be able to easily find the causes of your problems:
> Node //people[0]/person[1]/documents[2]/document[1]; Attributes collection does not match expected state, redundant attributes found: [valid-from]lacking attributes: [number]

> Node //people[0]/person[1]/documents[2]/document[1]; Expected attribute [number] with value [10], but found [55]

> Node //people[0]/person[1]/documents[2]/document[0]/authority[0]/name[0]/#text[0]; Expected text [Urząd gminy w Terespolu], but found [Urząd gminy w Kalinowie]

## API 
Current assertions is right now in `IXmlAssertable` interface:
```csharp
    public interface IXmlAssertable
    {
        void BeEqualTo(string expected);
        void BeEqualTo(XmlNode expected);
        void HaveAttribute(string attributeName);
        void HaveAttribute(string attributeName, string attributeValue);
        void HaveName(string expectedName);
        void BeEqualShallowTo(XmlNode expected);
    }
```
