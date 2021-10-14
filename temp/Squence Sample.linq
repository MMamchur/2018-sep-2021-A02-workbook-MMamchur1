<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>



//Find all elbums released in the 90's
//Order releses by year and then by album title.
//Display the Year, Title, Artist Name, and Relese Label

//problem 	1)Not all properties of Albums are to be displayed
//			2)The order of the properties are to be displayed in a different sequence
//solution: Use a anonymous dataset

// Use the object initialization syntax to create new instances to be produced for the resulting dataset
Albums
	.Where (albumitem => albumitem.ReleaseYear >= 1990
		&& albumitem.ReleaseYear < 2000)
	.OrderBy (albumitem => albumitem.ReleaseYear)
	.ThenBy (albumitem => albumitem.Artist.Name)
	.Select (albumitem => new
		{
			Year = albumitem.ReleaseYear,
			Title = albumitem.Title,
			Name = albumitem.Artist.Name,
			Label = albumitem.ReleaseLabel
		})

//List all albums released in the 90's ordered by artist then title
//Show the artist Name, Tital, Year and Label

//Order releses by year.

Albums
	.Where (albumitem => albumitem.ReleaseYear >= 1990
		&& albumitem.ReleaseYear < 2000)
	.OrderBy (albumitem => albumitem.Artist.Name)
	.ThenBy (albumitem => albumitem.Title)
	.Select (albumitem => new
		{
			Name = albumitem.Artist.Name,
			Title = albumitem.Title,
			Year = albumitem.ReleaseYear,
			Label = albumitem.ReleaseLabel
		})


