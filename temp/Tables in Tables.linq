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
	//Nested Queries
	//Sometimes refered to as subqueries
	
	var resultsq = from emp in Employees
					where emp.Title.Contains("Sales Support")
					orderby emp.LastName, emp.FirstName 
					
					select new 
					{
						FullName = emp.LastName + ", " + emp.FirstName,
						Title =emp.Title,
						NumberOfCustomers = emp.SupportRepCustomers.Count(),
						CustomerList = from cus in emp.SupportRepCustomers
						orderby cus.State, cus.City
						select new
						{
							State = cus.State == null ? " " :
							 			cus.State,
							City = cus.City,
							Name = cus.LastName + ", " + cus.FirstName,
							Phone = cus.Phone
						}
					};
	resultsq.Dump();




var resultsm = Employees
   .Where (emp => emp.Title.Contains ("Sales Support"))
   .OrderBy (emp => emp.LastName)
   .ThenBy (emp => emp.FirstName)
   .Select (
      emp =>
         new 
         {
            FullName = ((emp.LastName + ", ") + emp.FirstName),
            Title = emp.Title,
            NumberOfCustomers = emp.SupportRepCustomers.Count (),
			CustomerList = emp.SupportRepCustomers
							.OrderBy(cus => cus.State)
							.ThenBy (cus => cus.City)
							.Select (cus => new 
							{
								State = (cus.State == null) ? " " : cus.State,
		                        City = cus.City,
		                        Name = ((cus.LastName + ", ") + cus.FirstName),
		                        Phone = cus.Phone
							}
					)
         }
   );

resultsm.Dump();


var albumlist = Albums
				.Where(x => x.Tracks.Count >= 25)
				.Select(x => new AlbumsTracksItem
					{
						Title = x.Title,
						Artisit = x.Artist.Name,
						AlbumSongs = x.Tracks
								.Select(trk => new Song 
								{
									SongName = trk.Name,
									LengthInSeconds = trk.Milliseconds / 1000
								})
								
					});



}

public class Song {
	public string SongName{get;set;}
	
	public int LengthInSeconds{get;set;}
}

public class AlbumsTracksItem 
{
	public string Title{get;set;}
	public string Artisit{get;set;}
	public IEnumerable<Song> AlbumSongs{get;set;}
}


public class 	EmployeItem 
{
	public string FullName{get;set;}
	public string Title{get;set;}
	public int NumberOfCustomers{get;set;}

}


// HW: Create a list of Items showing its title and artist.
//Show Albumsa with 25 or more tracks only.
//Show the songs on the album listening the name and song length in seconds.
//There are 1000 milliseconds in a second



