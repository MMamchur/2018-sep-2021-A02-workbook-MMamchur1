<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Find any track of music that has the word big in its name
//Show the track name, album name and artist 
//Order the display by the album title then track name

Tracks
	.Where (x => x.Name.Contains("Big"))
	.OrderBy (x => x.Album.Title)
	.ThenBy (x => x.Name)
	.Select (x => new
		{
			Title = x.Album.Title,
			Song = x.Name,
			Artist = x.Album.Artist.Name
		})