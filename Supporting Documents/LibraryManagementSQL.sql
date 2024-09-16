use libraryManagement

select * from sys.tables
select * from Users
select * from UserCredentials
select * from Books;
select * from Authors;
select * from Publishers
Select * from SalesStocks;
Select * from Cart;
select * from RentCart;
select * from superRentCart;
select * from Sales;	
Select * from SaleDetails;
select * from Feedbacks
select * from Purchases
select * from PurchaseDetails

select * from RentStocks
select * from Rents
select * from RentDetails

select * from fines
select * from FineDetails



update Books set RatingCount = 0 where id =1 ;



delete from Feedbacks ;













use dbRequestTracker14May24
select * from Employees

drop database LibraryManagement