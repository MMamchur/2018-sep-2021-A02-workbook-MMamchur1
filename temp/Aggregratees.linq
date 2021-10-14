<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregratees
//.Count(), .Min(), .MAX(), .Average(), Sum()

//.Min(), .MAX(), .Average(), Sum() NEED to execute against a collection of a specifieed single column (expression)
//that is if you need to do one of these aggregates you need to specify what to aggrigate


//query syntax
(from x in Tracks
select x.Milliseconds).Average()

//method syntax
Tracks.Select(x => x.Milliseconds).Average()

//Mixture
from x in Tracks
select new 
{
	name = x.Name,
	AvgLength = x.Average(x => Milliseconds)
}

//list all albums of the 60s showing th title, artist and varius aggigates for albums containing tracks
//each album shows num of tracks, longest/shortest/averge track length and total price 

//query
from x in Albums
	where x.ReleaseYear > 1959 && x.ReleaseYear <1970 && x.Tracks.Count() > 0
	orderby x.Tracks.Count() descending
	select new 
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		NumberOfTracks = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(tr => tr.Milliseconds),
		ShortestTrack = (from tr in x.Tracks
						select tr.Milliseconds).Min(),
		TotalPrice = x.Tracks.Select(tr => tr.UnitPrice).Sum(),
		AverageTrackLength = x.Tracks.Average(tr => tr.Milliseconds)
	}
	
//method syntax
Albums
 .Where(x => x.ReleaseYear > 1959 && x.ReleaseYear < 1970 && x.Tracks.Count() > 0)
 .OrderByDescending(x => x.Tracks.Count())
 .Select(x => new 
 {
 	Title = x.Title,
	Artist = x.Artist.Name,
	NumberOfTracks = x.Tracks.Count(),
 	LongestTrack = x.Tracks.Max(tr => tr.Milliseconds),
 	ShortestTrack = x.Tracks.Select(tr => tr.Milliseconds).Min(),
	TotalPrice = x.Tracks.Select(tr => tr.UnitPrice).Sum(),
	AverageTrackLength = x.Tracks.Average(tr => tr.Milliseconds)
 })