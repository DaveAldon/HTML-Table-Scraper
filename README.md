# HTML Table Scraper

**Why?**
If you have one or a thousand HTML tables that you need to scan through and have their values inserted into a MySQL database, **AND** if you do not have a CSV file or any control over the source data, then this application is the perfect starting point for you!

This is a project that uses a few interesting and elegant tricks to retrieve data from an HTML table and insert everything into a database. The HTML Agility Pack is used to initially grab the data in between the tags and classes, however it does not supply enough functionality in order to accurately retrieve the data from an HTML table that uses rowspans or calendar schedule formats extensively. This application fills in where the HTML Agility Pack leaves off, and essentially makes those pesky rowspans a thing of the past. Additionally, there is a lot of query functionality to ask the database various questions about the retrieved data, however that portion is entirely optional.

The application is currently setup to scan through a University's publicly available faculty page and grab all of their various availabilties, making it easy to query the data.

# Out-of-the-Box Requirements
**(v1.0)**
* Mac OS 10.5+
* A running MySQL server

**(v1.1+)**
* Mac OS 10.5+
* A running MySQL server
* If you want it to work correctly, please change the Globals.cs settings to accomodate your database setup. This means you will need to re-compile at some point, so please see below for additional details

# For Development

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/3ccf2ef44f4c41bd958a78a875b41736)](https://www.codacy.com/app/crawford_2/HTML-Table-Scraper?utm_source=github.com&utm_medium=referral&utm_content=DaveAldon/HTML-Table-Scraper&utm_campaign=badger)
**Master Branch** - [![Build Status](https://www.bitrise.io/app/8eb52e35de8c2067.svg?token=3xm3z_hNZxt_UvNnetlqRQ)](https://www.bitrise.io/app/8eb52e35de8c2067)

**External Dependancies Required:**

HTMLAgilityPack-PCL

MySQL.Data

**Requirements for compiling non UI elements:**

C# development enviroment (preferably Xamarin or Visual Studio)

**Requirements for compiling the Cocoa app:**

Mac OS with XCode

Xamarin

**Special Requirements**

MySQL.Data does not add via the Nuget packages UI, so it must be referenced manually after downloading the .dll

You MUST target the Xamarin.Mac .NET 4.5 Framework, NOT the Xamarin.Mac Mobile Framework which is the default
