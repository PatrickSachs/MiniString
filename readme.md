# MiniString #

A 6-bit string encoding library for .NET-Standard.

 * [See my website for more details](https://patrick-sachs.de/projekte/ministring/) (german)
 
Supports serializing A-Z, a-z, 0-9 and _ with just 6 bits each. All other characters are serialized with 24 bits. This makes MiniString ideal for saving machine generated strings such as IDs.

## Compile ##

```
$ dotnet build
```
## Usage ##

```csharp
byte[] MiniString.Encode(string text)
string MiniString.Decode(byte[] bytes)

```

```csharp
MiniString.Encode("Hello_World");
// [82, 10, 195, 243, 31, 206, 54, 140, 2]
// 11 characters have been serialized to 8 bytes!
```
