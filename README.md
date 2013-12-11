OneSkyAppSharp
==============
OneSkyAppSharp is a mono compatible implementation of the [OneSky API](https://github.com/onesky/api-documentation-platform) written in C#.

### Repository Structure
* __OneSkyAppSharp__: This is the main library project that contains all implemented OneSky API calls.
* __OneSkyAppTool__: This is a console app project that can be used to automate build processes by downloading localization files using the OneSky API.

### Dependencies
* [__RestSharp__](http://restsharp.org/)

### Usage
Here's a basic usage example that exports a translation and saves it to disk.

```cs
System.IO.File.WriteAllBytes(
	"/Path/File.zip", // Output Filepath
	com.lemonmojo.OneSkyAppSharp.API.Translation.TranslationAPI.Export(
		1234, // Project ID
		"en", // Locale
		"Localizable.strings", // Source Filename
		null, // Export Filename
		new com.lemonmojo.OneSkyAppSharp.APIConfiguration(
			"ABC123", // Public Key
			"123DEF" // Secret Key
		)
	).Data
);
```

### State of the library
Because this library is mainly developed for my own purposes, only the functions I personally use are currently implemented. However, pull requests are very welcome!
