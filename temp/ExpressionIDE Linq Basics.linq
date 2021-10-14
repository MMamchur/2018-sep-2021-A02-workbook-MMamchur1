<Query Kind="Expression">
  <Connection>
    <ID>d1367d1b-36f6-48f7-bb3a-11399a20edc7</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Our code is using C# grammer/syntax
//
//Comments are done with slashes 
//Hotkeys make line comment ctrl+k, ctrl+k

//Find all albums released in 2000.
from albumitem in Albums
where albumitem.ReleaseYear == 2000
select albumitem

// method syntax
//uses C# method syntax OOP script
Albums
	.Where (albumitem => albumitem.ReleaseYear == 2000)
	.Select (albumitem => albumitem)