CREATE DATABASE LibraryDB

USE LibraryDB

CREATE TABLE Books (
    BookId INT PRIMARY KEY,
    Title NVARCHAR(255),
    Author NVARCHAR(255),
    Genre NVARCHAR(255),
    Quantity INT
)

insert into Books values(1, 'Harry Potter and the Secrets of chamber','J.K.Rowling','Fiction',10)
insert into Books values(2, 'To Kill a Mockingbird', 'Harper Lee', 'Fiction', 8)
insert into Books values(3, 'Pride and Prejudice', 'Jane Austen', 'Romance', 12)

select * from Books