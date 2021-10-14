<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Grouping

//when you create a group it builds two (2) components
//	a) Key component (group by)
//	   references this component using a groupname.Key[.attribute]
//	b) data of the field (instances in the group)

//ways to group
// a) by a single column (field, attribute, property)   groupname.Key
// b) by a set of columns (anonymous dataset)			groupname.Key.Attribute
//c) by using an entity									groupname.Key.Attribute

//consept processing
//start with a "pile" of data (original collection)
//specify the grouping attibute(s)
//result of the group will be to "place the data into smaller piles"
//	the piles are dependant om the grouping attribute(s)
// the grouping attribute(s) become the key
//	the individual instances are the data in the smaller piles
// the entire individual instance is placed in the smaller pile
//Manipulate each of the smaller piles using your linq commands 

//Display albums grouped by ReleaseYear
Albums
	.GroupBy(x => x.ReleaseYear)
	
//Display albums grouped by ReleaseYear. Display the releaseYear and number of albums
Albums
	.GroupBy(x => x.ReleaseYear)
	.Select(eachgPile => new 
	{
		Year = eachgPile.Key,
		NumberOfAlbums = eachgPile.Count()
	})
// 	
//query syntax
from x in Albums
group x by x.ReleaseYear into eachgPile
select new 
{
	Year = eachgPile.Key,
	NumberOfAlbums = eachgPile.Count()
}

//grouping using multipe columns
//you will create a new anonymous class for use within your query
//	this class will be your Key
//This temporary class DOES NOT need to have a physical class code

//Display albums grouped by ReleaseLabel ReleaseYear. Display the releaseYear 
//and number of albums.  List only the years with 5 or more albums
//releases

//method
Albums
	.GroupBy(x => new {x.ReleaseLabel, x.ReleaseYear})
	.Where(eachgPile => eachgPile.Key.ReleaseLabel != null &&
					eachgPile.Count() > 2)
	//.OrderBy(eachgPile => eachgPile.Key.ReleaseLabel)
	.Select(eachgPile => new 
	{
		Label = eachgPile.Key.ReleaseLabel,
		//NumberOfAlbums = eachgPile.Count()
		Year = eachgPile.Key.ReleaseYear,
		AlbumItems = eachgPile
							.Select(eachgPileRecord => new
							{
								//YearOnAlbum = eachgPileRecord.ReleaseYear,
								TitleOnAlbum = eachgPileRecord.Title
							})
	})
	.OrderBy(y => y.Label)
	
//timing of the OrderBy is dependent on the statement loaction
//If the OrderBy is donr AFTER the .Select, the OrderBy uses the data collection form the .Select
//If the OrderBy is donr AFTER the .Select, the OrderBy uses the small piles of grouped data and thus is based on said collection