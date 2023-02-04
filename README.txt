********************************************************************* 
			Author 
********************************************************************* 
#1 Ron Moyal, 207235284  


********************************************************************* 
				Intro   
*********************************************************************
Technologies:
   - Architecture - MVC (Model-View-Controller) ASP.NET
   - Language     - C# HTML, CSS, SQL,Java Script)
   - DataBase     - Microsoft SQL Server management (local) 

We developed a website for managing the Travel Flights booking at the 
Travel Agency (local host) using all the technologies taught during 
the lab sessions.

This Travel Agency web application is a slight version of the real 
Travel Agencies,which is manage flights, tickets, countries, payments
, etc


********************************************************************* 
		          System requirements
********************************************************************* 
A computer without special requirements and with standrad OS

********************************************************************* 
		                 Run
********************************************************************* 
Downloading all the project files and running the home page

********************************************************************* 
		       requirements & constraints
********************************************************************* 

Admin:
(managed automatically in a database)
   - adding/removing flights
   - managing the prices 
   - managing the countries 
   - managing the number of seats in each airplane
   - adding the number of seats for each flight.

Users:
   - can choose a flight according to its date, country
   - can “book” an unoccupied seat
   - Change a number of tickets till the “booking”
   - Make a payment(timet - 2 minute to make a payment , after then cancel order and return back to home page)

Flight options:
   - has a list of flights with their destination and origin country, 
     flight date and time, price 
   - Users can choose flights of a specific date,country,time
   - Direct flights only
   - One-way or two-way flight

Buying a ticket:
   - Users can choose a ticket for a specific flight at a specific 
     date and time to a specific destination from a specific destination
   - It’s impossible to buy a ticket for a flight, which already departed
     a flight list can be ordered according to 
o price increase
o price decrease
o most popular
o country
(Bounus) 

Payment:
   - credit card can stored for payment in database or be entered during
     payment by the user
   - The user can store the payment details in a database of the agency
   If the user has saved his credit information in the past, it will be 
    possible to make an automatic payment
    Payment details encrypted with AES algorithm(Bonus)

Data:
   - All data managed in the Database according to the user permission


********************************************************************* 
		             Code Summary
*********************************************************************
Model - View - Controller

Models:
   - Card
   - Flight
   - Order
   - Find
   - User

Controller (Views):
   - Account (Login, SignUp)
   - Home    (Index)
   - Result (BookSeat,Payment,showAllFlight,showSearchFlight)
Ticket-(Deatails)

