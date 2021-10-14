<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Using the ternary operator

// condition(s) ? true value : false value

// both true value and false value MUST resolve to a SINGLE piece of data (a single value)

//compare to a conditional statement

//if(condition(s))
// [{]
// true path (complex logic)
// [}]
// [else
// [{]
//  false path (complex logic)
// [}]]

//List all albums by release label. Any album with no label should be indicated as unknown. List title and label

Albums
	.OrderBy(x => x.ReleaseLabel)
	.Select(x => new 
		{
			title = x.Title,
			label = x.ReleaseLabel == null ?
					"Unknown" : x.ReleaseLabel		
		})

//List all albums showing the title, Artist Name and decade of release (Oldies, 70s, 80s, 90s, or modern).
//Order by artist.

Albums
	.OrderBy(x => x.Artist.Name)
	.Select(x => new
		{
			artist = x.Artist.Name,
			title = x.Title,
			year = x.ReleaseYear,
			decade = x.ReleaseYear < 1970 ? "Oldies" :
			(x.ReleaseYear < 1980 ? "70s" : 
			(x.ReleaseYear < 1990 ? "80s" : 
			(x.ReleaseYear < 2000 ? "90s" : "Modern")))
		})

// if condition
//		Oldies
// else
//		if condsition
//			70s
//		 else
//			 if condition
//				80s
//			 else
//				 if condition
//					90s
//				 else
//					Modern

