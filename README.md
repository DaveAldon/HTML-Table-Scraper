# HTML Table Scraper

This is a project that uses a few interesting and elegant tricks to retrieve data from an HTML table and insert everything into a database. The HTML Agility Pack is used to initially grab the data in between the tags and classes, however it does not supply enough functionality in order to accurately retrieve the data from an HTML table that uses rowspans or calendar schedule formats extensively.

# Build Status

**Master Branch** [![Build Status](https://travis-ci.com/DavidAldon/Capstone-CIS463.svg?token=eCnosqg9nhqR9WqTkK12&branch=master)](https://travis-ci.com/DavidAldon/Capstone-CIS463)

# For Development

**External Dependancies Required:**

HTMLAgilityPack-PCL

MySQL.Data

**Requirements for compiling non UI elements:**

C# development enviroment (preferably Xamarin or Visual Studio)

**Requirements for compiling the Cocoa app:**

Mac OS with XCode

Xamarin

MySQL.Data does not add via the Nuget packages UI, so it must be referenced manually after downloading the .dll

You MUST target the Xamarin.Mac .NET 4.5 Framework, NOT the Xamarin.Mac Mobile Framework which is the default
