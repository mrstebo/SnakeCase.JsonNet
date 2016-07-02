# SnakeCase.JsonNet

[![Join the chat at https://gitter.im/loqu8/SnakeCase.JsonNet](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/loqu8/SnakeCase.JsonNet?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/74nqw67sxsrpuu9i?svg=true)](https://ci.appveyor.com/project/loqu8/SnakeCase.JsonNet)
[![NuGet Version](https://img.shields.io/nuget/v/snakecase.jsonnet_portable.svg)](https://www.nuget.org/packages/snakecase.jsonnet_portable/)
[![MyGet Prerelease](https://img.shields.io/myget/loqu8/vpre/snakecase.jsonnet_portable.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/loqu8/package/nuget/snakecase.jsonnet_portable)

Json Serializer that converts property names to snake_case in the response.


SnakeCase.JsonNet is available via [NuGet](https://www.nuget.org/packages/SnakeCase.JsonNet/):
```PowerShell
Install-Package SnakeCase.JsonNet
```

### Examples

```cs
// We'll try and serialize this!
class TestObject
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

```cs
// Using the serializer directly
private string TestSerializer(TestObject obj)
{
    var serializer = new SnakeCaseJsonSerializer();

    using (var sw = new StringWriter())
    {
        using (var jw = new JsonTextWriter(sw))
        {
            serializer.Serialize(jw, obj);
        }
        return sw.ToString();
    }
}

// Or you can just inject the SnakeCaseContractResolver
private string TestContractResolver(TestObject obj)
{
    var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
    {
        ContractResolver = new SnakeCaseContractResolver()
    });

    return result;
}
```

These functions should serialize this object:
```cs
var obj = new TestObject
{
    Title = "Mr",
    FirstName = "John",
    LastName = "Smith"
};
```
To this JSON object:
```javascript
{
  "title": "Mr",
  "first_name": "John",
  "last_name": "Smith"
}
```
