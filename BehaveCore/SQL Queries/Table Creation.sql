USE BehaveStorage
GO

-- DROPs
DROP TABLE Occurrences;
DROP TABLE Habits;
DROP TABLE Users;

-- CREATEs with Default values inserted
CREATE TABLE Users
( 
	UserId int PRIMARY KEY IDENTITY,
	Nickname varchar(80),
	OathUserToken varchar(50) -- TODO: actually find out what will be needed and implement for oAuth-style
);
INSERT INTO Users (NickName) VALUES ('Generic User');
INSERT INTO Users (NickName) VALUES ('Mike Randrup');

CREATE TABLE Habits
(
	HabitId int PRIMARY KEY IDENTITY,
	UserId int REFERENCES Users(UserId) DEFAULT 2, -- Initially, this app will be single user for me personally
	Importance real DEFAULT 0, 
	Title varchar (120),
	Details varchar (4096)
);
INSERT INTO Habits (Title) VALUES ('Unspecified Habit');

CREATE TABLE Occurrences
(
	OccurrenceId int PRIMARY KEY IDENTITY,
	EventTime datetime DEFAULT GETDATE(),
	HabitId int REFERENCES Habits(HabitId) DEFAULT 1,
	Notes varchar (4096)
);
INSERT INTO Occurrences (Notes) VALUES ('Example Occurrence');