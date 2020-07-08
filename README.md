# 01-Webscraper


provide details of how to run the code, and any notes you wish to add, such as libraries chosen for HTML parsing, JSON mapping, and unit testing

Running the Application
  The application can be run by building the project and navigating to the Project Folder/bin/{BuildMode}/ and running 01-Webscraper.exe. 
  Output will be found in the Project Folder/bin/{BuildMode}/Output folder in a file named Output.txt
  
Libraries Used
  For this challenge I have made use of the HTMLAgilityPack library for retrieving and parsing the page data with a further library named ScrapySharp which works with            the HTMLAgilityPack library to provide additional functionality in the form of CSS Selectors that enabled me to access the data on the site easily. 
  
  For the JSON I have used the Newtonsoft.Json library and for the unit testing I have used Microsoft's own libraries under the Microsoft.VisualStudio.TestTools.UnitTesting namespace.
