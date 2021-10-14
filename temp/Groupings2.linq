<Query Kind="Expression">
  <Connection>
    <ID>c6a98514-1fcc-419a-9ec3-0a1e2b8b56cb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>MATT-CUSTOM-PC\MSSQLSERVER01</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>





Customers
	.GroupBy(x => x.SupportRep)
	.Select(gTemp => new
		{
			Employee = gTemp.Key == null ? "Unassigned" : 
						gTemp.Key.LastName + ", " +
						gTemp.Key.FirstName + " (" +
						gTemp.Key.Phone + ")",
			NumOfCust = gTemp.Count(),
			CustomerList = gTemp
						.Select(cus => new 
								{
									CustName = cus.LastName +
									", " + cus.FirstName,
									City = cus.City,
									State = cus.State

								})
	
		})