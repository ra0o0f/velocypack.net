### VelocyPack - A fast and compact format for serialization and storage
.NET implementation(ported from java implementation) for [ArangoDB VelocyPack](https://github.com/arangodb/velocypack)

Supported runtimes are .NET 4.5, .NET Core 1.0, Mono/Xamarin, UAP 10.0, WPA 8.1, WIN 8 ...(Except Silverlight)

For Serialize/Deserialize objects:

```c#
class Person
{
    public string Fullname { get; set; }

    public int Age { get; set; }

    public List<string> Hobbies { get; set; }

    public Dictionary<string, Person> FamilyMembers { get; set; }
}

byte[] vpack = VPack.Serialize(new Person
{
    Fullname = "Jack Nicholson",
    Age = 79,
    Hobbies = new List<string> { "movies", "acting"},
    FamilyMembers = new Dictionary<string, Person>
    {
        ["child_1"] = new Person { Fullname = "Lorraine Nicholson" },
        ["child_2"] = new Person { Fullname = "Ray Nicholson" }
    }
});

Person person = VPack.Deserialize<Person>(vpack);
```

Or use `VPAckBuilder` directly to have more control on serialization:

```c#
var s = new VPackBuilder()
                .Add(SliceType.Object)
                    .Add("Fullname", "Jack Nicholson")
                    .Add("Age", 79)
                    .Add("Hobbies", SliceType.Array)
                        .Add("movies")
                        .Add("acting")
                    .Close()
                    .Add("FamilyMembers", SliceType.Object)
                        .Add("child_1", SliceType.Object)
                            .Add("Fullname", "Lorraine Nicholson")
                        .Close()
                        .Add("child_2", SliceType.Object)
                            .Add("Fullname", "Ray Nicholson")
                        .Close()
                    .Close()
               .Close()
               .Slice();
```

