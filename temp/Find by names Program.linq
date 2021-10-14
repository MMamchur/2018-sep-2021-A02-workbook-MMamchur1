<Query Kind="Program">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	string partialSongName = "Big";
	var results = SongsByPartialName(partialSongName);
	results.Dump();
}

List<SongList> SongsByPartialName(string partialSongName) 
{
var songCollection = Tracks
	.Where (x => x.Name.Contains(partialSongName))
	.OrderBy (x => x.Album.Title)
	.ThenBy (x => x.Name)
	.Select (x => new SongList
		{
			Album = x.Album.Title,
			Song = x.Name,
			Artist = x.Album.Artist.Name
		});
	return songCollection.ToList();
}

public class SongList 
{
	public string Album{get;set;}
	public string Song{get;set;}
	public string Artist{get;set;}

}

// You can define other methods, fields, classes and namespaces here
