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
INSERT INTO Users (NickName) VALUES ('(unspecified user)');
INSERT INTO Users (NickName) VALUES ('Mike Randrup');

CREATE TABLE Habits
(
	HabitId int PRIMARY KEY IDENTITY,
	UserId int REFERENCES Users(UserId) DEFAULT 2, -- Initially, this app will be single user for me personally

	IsDeleted bit default 0,

	Interval real DEFAULT 1, -- 1 = Once per day
	Importance real DEFAULT 0, -- apparently nothing much matters
	DoingThisIsGood bit default 1, -- usually things are good
	Title varchar (120) DEFAULT NULL,
	Details varchar (4096) DEFAULT NULL,

	-- Someday I want a natural language version of this
	-- that can have tenses applied to the verbs and what-not
	-- and an engine can generate a natural language summary
	-- of how well things are going.
	Timing varchar (250) DEFAULT NULL, -- every day
	Verb varchar (250) DEFAULT NULL, -- wash
	What varchar (250) DEFAULT NULL, -- the car
);
INSERT INTO Habits (Title) VALUES ('(unspecified habit)');
INSERT INTO Habits (Title) VALUES ('AM Workout');
INSERT INTO Habits (Title) VALUES ('AM Yoga');
INSERT INTO Habits (Title) VALUES ('Veggie Pattie Breakfast only');
INSERT INTO Habits (Title) VALUES ('AM Fruit & Nut snack only');
INSERT INTO Habits (Title) VALUES ('Container Meal & Veggie Lunch only');
INSERT INTO Habits (Title) VALUES ('Take Daily Vitamins');
INSERT INTO Habits (Title) VALUES ('PM Fruit & Nut snack only');
INSERT INTO Habits (Title) VALUES ('PM Workout');
INSERT INTO Habits (Title) VALUES ('PM Yoga');
INSERT INTO Habits (Title) VALUES ('Not a Drop of Alcohol');

CREATE TABLE Occurrences
(
	OccurrenceId int PRIMARY KEY IDENTITY,
	UserId int REFERENCES Users(UserId) DEFAULT 2, -- Initially, this app will be single user for me personally
	EventTime datetime DEFAULT GETDATE(),
	HabitId int REFERENCES Habits(HabitId) DEFAULT 1,
	Notes varchar (4096) DEFAULT NULL
);