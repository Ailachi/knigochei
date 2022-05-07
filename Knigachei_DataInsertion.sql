insert into Roles(RoleName)
values
('Admin'),
('Customer');

insert into Gender(GenderName)
values
('Female'),
('Male'),
('Unknown');



insert into Genre(GenreName, GenreDescription)
values
('Detective', 'Detective fiction is a subgenre of crime fiction and mystery fiction in which an investigator or a detective—either professional, amateur or retired—investigates a crime, often murder.'),
('Science Fiction', 'Science Fiction genre lean heavily on themes of technology and future science. '),
('Fantasy Fiction', 'Fantasy Fiction Descr'),
('Philosophical Novel', 'Philosophical Novel Descr');



insert into Author(FirstName, LastName, BirthDate, GenderId)
values
('Jo', 'Nesbø', '19600329', 2),
('Joanne', 'Rowling', '19650731', 1),
('Albert', 'Camus', '19131107', 2);

select * from Author


insert into Book(Title, PublishedYear,Price, BookRank, BookDescription, GenreId, AuthorId)
values
('Harry Potter and the Philosopher''s Stone', 2001, 5000,8.5, 'The Book is about a Harry Potter', 3, 2),
('The Bat', 1997, 10.0, 10000, 'The Book is about detective Harry Hole. First Book', 1, 1),
('The Fall', 1956, 9.5, 15000,'The Fall consists of a series of dramatic monologues by the self-proclaimed judge-penitent Jean-Baptiste Clamence, as he reflects upon his life to a stranger.', 4, 3);



select * from Users

insert into Users(Email, UserPassword, FirstName, LastName, GenderId, RoleId)
values 
('anuar@mail.ru', 'anuar123', 'Anuar	Bora',	2, 1),
('elvira@mail.ru', 'elvira123', 'Elvira','Nugmanova', 1, 2);

