drop table if exists OrderItem;
drop table if exists CartItem;
drop table if exists Orders;
drop table if exists Cart;
drop table if exists OrderStatus;
drop table if exists Book;
drop table if exists Author;
drop table if exists Genre;
drop table if exists Users;
drop table if exists Roles;
drop table if exists Gender;



create table Roles(
	Id int primary key identity(1,1),
	RoleName nvarchar(20)
);

create table Gender(
	Id int primary key identity(1,1),
	GenderName nvarchar(50)
);

create table Users(
	Id int primary key identity(1,1),
	Email nvarchar(200),
	UserPassword nvarchar(100),
	FirstName nvarchar(100),
	LastName nvarchar(100),
	GenderId int references Gender(Id),
	RoleId int references Roles(Id)
);


create table Genre(
	Id int primary key identity(1,1),
	GenreName nvarchar(30),
	GenreDescription nvarchar(max)
);

create table Author(
	Id int primary key identity(1,1),
	FirstName nvarchar(100),
	LastName nvarchar(100),
	BirthDate date,
	GenderId int references Gender(Id)
);


create table Book(
	Id int primary key identity(1,1),
	Title nvarchar(100),
	BookDescription nvarchar(max),
	PublishedYear int,
	BookRank float,
	Price decimal(10, 2),
	GenreId int references Genre(Id),
	AuthorId int references Author(Id)
);

create table Cart(
	Id int primary key identity(1,1),
	UserId int references Users(Id)
);

create table CartItem(
	Id int primary key identity(1,1),
	Amount int,
	IsChecked bit,
	CartId int references Cart(Id),
	BookId int references Book(Id)
);


create table OrderStatus(
	Id int primary key identity(1,1),
	OrderStatusTitle nvarchar
);

create table Orders(
	Id int primary key identity(1,1),
	TotalPrice decimal(10,2),
	OrderDate datetime,
	DeliveryDate date,
	PickUpAddress nvarchar(max),
	OrderStatus int references OrderStatus(Id),
	UserId int references Users(Id)
);

create table OrderItem(
	Id int primary key identity(1,1),
	Amount int,
	OrderId int references Orders(Id),
	BookId int references Book(Id)
);
