# RxTracker

A .Net Core web application that allows a user to track their prescription cost and other details (e.g. prescribing doctor, pharmacy). The intent is to allow the user to manage medications they are currently on, where they are getting them and cost. The application is most useful to those users with multiple prescriptions being filled and multiple pharmacies and using multiple means of payment (insurance, discounts, manufacturer coupons, etc...)

## Getting Started

The easiest way to get a local copy of the project running is to clone the repository and run locally using Visual Studio. The project requires .Net Core 2.2 and your Visual Studio instance must have the following workloads selected:

	a.	ASP.NET and web development
	b.	.NET Core cross-platform development

As well as the individual components relating to SQL Server. 

If you are using an instance of SQL Server other than what is available from the Visual Studio download, you will need to update the connection string in appsettings.json.