use libraryManagement

select * from sys.tables
select * from Users
select * from UserCredentials
select * from Books;
Select * from SalesStocks;
Select * from Cart;
select * from Sales;
Select * from SaleDetails;
select * from Feedbacks
select * from Purchases
select * from PurchaseDetails

select * from RentStocks
select * from Rents
select * from RentDetails

update RentStocks set QuantityInStock = 9 where BookId=2;



delete from SalesStocks