<Query Kind="Statements">
  <Connection>
    <ID>1c57f053-9773-4e61-a6b2-8bbd3002e6fc</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Statement IDE can hold multiple queries
// Queries must be written in the C# language for a statement

//Find all albums released in 2000.
//query syntax
var resultq = from albumitem in Albums
where albumitem.ReleaseYear == 2000
select albumitem;
resultq.Dump();

// method syntax
//uses C# method syntax OOP script
var resultm = Albums
	.Where (albumitem => albumitem.ReleaseYear == 2000)
	.Select (albumitem => albumitem);

resultm.Dump();