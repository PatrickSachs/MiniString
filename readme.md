# MiniString #

A 6-bit string encoding library for .NET-Standard.

 * [See my website for more details](https://patrick-sachs.de/projekte/ministring/) (german)

## Compile ##

```
> dotnet build
```
## Usage ##

```csharp
byte[] MiniString.Encode(string str)
string MiniString.Decore(byte[] bytes)
```