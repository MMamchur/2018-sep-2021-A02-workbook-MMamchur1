<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//OrderBy, OrderByDecending, ThenBy, ThenByDecending
//Order the output results of the query

//syntax
//Order field [asending, decending] [, field2 [asending, decending]]
//asending is the defualt option

//method syntax
//Each sort request is a different method
//.OrderBy(plholder => plholder) or .OrderByDecending(plholder => plholder)
//.ThenBy(plholder = > plholder) or ThenByDesnding(plholder => plholder)

//Order releses by year and then by album tital.
Albums
	.Where (albumitem => albumitem.ReleaseYear >= 1990
		&& albumitem.ReleaseYear < 2000)
	.OrderBy (albumitem => albumitem.ReleaseYear)
	.ThenBy (albumitem => albumitem.Artist.Name)
	.Select (albumitem => albumitem)