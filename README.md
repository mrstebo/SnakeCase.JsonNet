# SnakeCase.JsonNet

[![Join the chat at https://gitter.im/mrstebo/SnakeCase.JsonNet](https://badges.gitter.im/mrstebo/SnakeCase.JsonNet.svg)](https://gitter.im/mrstebo/SnakeCase.JsonNet?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![Build status](https://ci.appveyor.com/api/projects/status/j8qoan1wf6owxxj4?svg=true)](https://ci.appveyor.com/project/mrstebo/snakecase-jsonnet)



**SnakeCase.JsonNet**
[![MyGet Prerelease](https://img.shields.io/myget/package-hub/v/SnakeCase.JsonNet.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/package-hub/package/nuget/SnakeCase.JsonNet) [![NuGet Version](https://img.shields.io/nuget/v/SnakeCase.JsonNet.svg)](https://www.nuget.org/packages/SnakeCase.JsonNet/)

**SnakeCase.JsonNet_Portable**
[![MyGet Prerelease](https://img.shields.io/myget/package-hub/v/SnakeCase.JsonNet_Portable.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/package-hub/package/nuget/SnakeCase.JsonNet_Portable) [![NuGet Version](https://img.shields.io/nuget/v/SnakeCase.JsonNet_Portable.svg)](https://www.nuget.org/packages/SnakeCase.JsonNet_Portable/)

Json Serializer that converts property names to snake_case in the response.


SnakeCase.JsonNet is available via [NuGet](https://www.nuget.org/packages/SnakeCase.JsonNet/):
```PowerShell
Install-Package SnakeCase.JsonNet
```

SnakeCase.JsonNet_Portable is available via [NuGet](https://www.nuget.org/packages/SnakeCase.JsonNet_Portable/):
```PowerShell
Install-Package SnakeCase.JsonNet_Portable
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
