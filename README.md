# Capstone-CIS463

This project contains several legacy applications David has worked on. For the current Mac compatible Cocoa application built with Xamarin, look inside the "FacultySchedules" folder.

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

MUST target Xamarin.Mac .NET 4.5 Framework, NOT Xamarin.Mac Mobile Framework which is the default



# Extra Details

It appears we will be scraping data from each professor's link via this main link:
http://www.cis.gvsu.edu/public/staffListing/

To run Ira's concept, make sure to install the BeautifulSoup v4 plugin dependancy from here first:
https://www.crummy.com/software/BeautifulSoup/
