<Query Kind="Program">
  <Connection>
    <ID>1c57f053-9773-4e61-a6b2-8bbd3002e6fc</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Program IDE
	//You can have multiple queries writtin in this IDE enviroment
	//This enviroment works "like" a console application

	//Find all albums released in 2000.
	// method syntax
	int inParam = 2000;
	
	var resultm = Albums
		.Where (albumitem => albumitem.ReleaseYear == inParam)
		.Select (albumitem => albumitem);

	resultm.Dump();
}

// You can define other methods, fields, classes and namespaces here
