/* Check if database already exists and delete it if it does exist*/
use master
go
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'GreenThumbGardens') 
BEGIN

DROP DATABASE GreenThumbGardens;
END;
GO

CREATE DATABASE GreenThumbGardens;
GO

USE GreenThumbGardens;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO


/**********************************************************************************/
/******************************* Schemas ******************************************/
/**********************************************************************************/


create schema Admin;
go
create schema Donations;
go
create schema Expert;
go
create schema Gardens;
go


/**********************************************************************************/
/******************************* Tables *******************************************/
/**********************************************************************************/


create table Admin.ActivityLog(
	ActivityLogID int identity(1000,1) primary Key not null,
	UserID int not null,
	Date smalldatetime not null,
	LogEntry varchar(250) not null,
	UserAction varchar(100) null
);
go

create table Admin.GroupRequest(
	RequestID int identity(1000,1) primary key not null,
	UserID int not null,
	RequestStatus char(1) not null,
	RequestDate smalldatetime not null,
	RequestedBy int not null,
	ApprovedDate smalldatetime null,
	ApprovedBy int null
);
go

create table Admin.Messages(
	MessageID int identity(1000,1) primary key not null,
	MessageContent varchar(250) not null,
	MessageDate smalldatetime not null,
	Subject varchar(100) null,
	MessageSender int not null,
	Active bit not null default 1	
);
go

create table Admin.MessageLineItems(
	MessageID int not null,
	SenderID int not null, 
	DateSent smalldatetime not null,
	ReadBy int null,
	DateRead smalldatetime null, 
	MessageContent varchar(250) not null,
	Active bit not null default 1	

	CONSTRAINT [PK_MessageLineItems] PRIMARY KEY ( MessageID, DateSent ASC )
);
go

create table Admin.Regions(
	RegionID int not null primary key,
	SoilType varchar(20) null,
	AverageTempSummer decimal null,
	AverageTempFall decimal null,
	AverageTempWinter decimal null,
	AverageTempSpring decimal null,
	AverageRainfall decimal null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null	  
);
go

create table Admin.Roles(
	RoleID varchar(30) primary key not null,
	Description varchar(100) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null

);
go

Create table Admin.UserRoles(
	UserID int not null,
	RoleID varchar(30) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	Active bit default 1 not null

	CONSTRAINT [PK_UserRoles] PRIMARY KEY ( UserID, RoleID ASC )
);

create table Admin.Users(
	UserID int identity(1000,1) primary key not null,
	FirstName varchar(50) not null,
	LastName varchar(100) not null,
	Zip char(9) null,
	EmailAddress varchar(100) null,
	UserName varchar(20) not null,
	PassWord varchar(150) default 'NEWUSER' not null,
	Active bit not null default 1,
	RegionID int null
	
	CONSTRAINT ck_UserName UNIQUE(UserName) 
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentDonated(
	EquipmentDonatedID int identity(1000,1) not null primary key, 
	EquipmentName varchar(50) not null,
	EquipmentQuantity int not null,
	DateDonated smalldatetime not null,
	UserID int not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentNeeded(
	EquipmentNeededID int identity(1000,1) not null primary key,
	EquipmentName varchar(50) not null,
	EquipmentQuantity int not null,
	DateDonated smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	ReceivingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentPendingTrans(
	EquipmentDonatedID int not null, 
	EquipmentNeededID int not null,
	Date smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1
);

create table Donations.LandDonated(
	TransactionNum int identity(1000,1) not null primary key,
	UserID int not null,
	Size int not null,
	Address varchar(100) not null,
	City varchar(30) null,
	state char(2) null,
	zip char(9) null,
	Notes varchar(255) null,
	DateDonated smalldatetime not null,
	Active bit not null default 1 
);

--updated by Chris Schwebach 2-19-2016
create table Donations.LandNeeded(
	LandIdentifier int identity(1000,1) not null primary key,
	UserID int not null,
	DateNeeded smalldatetime not null,
	DateRequested smalldatetime null,
	Notes varchar(255) null,
	Zip varchar(9) null,
	City varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.LandPendingTrans(
	LandDonated int not null,
	LandNeeded int not null,
	DateCompleted smalldatetime null,
	Notes varchar(255) null,
	ExpirationDate smalldatetime null,
	TransDate smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_LandPendingTrans] PRIMARY KEY ( LandDonated, LandNeeded ASC )
);

create table Donations.MoneyDonated(
	DonationID int identity(1000,1) primary key not null,
	UserID int not null,
	Location varchar(50) not null,
	Amount decimal not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.MoneyNeeded(
	NeedID int identity(1000,1) primary key not null,
	UserID int not null,
	Location varchar(50) not null,
	Amount decimal not null,
	DateCreated smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.MoneyPendingTrans(
	NeedID int not null,
	DonationID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_MoneyPendingTrans] PRIMARY KEY ( NeedID, DonationID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SeedsDonated(
	DonationID int identity(1000,1) primary key not null,
	UserID int not null,
	Quantity int not null,
	SeedType varchar(50) not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SeedsNeeded(
	SeedsNeededID int identity(1000,1) primary key not null,
	UserID int not null,
	NeededAmount int not null,
	SeedType varchar(50) not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	Active bit not null default 1
);


create table Donations.SeedsPendingTrans(
	SeedsDonatedID int not null,
	SeedsNeededID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SeedsPendingTrans] PRIMARY KEY ( SeedsDonatedID, SeedsNeededID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilDonated(
	SoilDonatedID int identity(1000,1) primary key not null,
	SoilType varchar(50) not null,
	UserID int not null,
	SoilName varchar(75) not null,
	Quantity int not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilNeeded(
	SoilNeededID int identity(1000,1) primary key not null,
	SoilType varchar(50) not null,
	UserID int not null,
	SoilName varchar(75) not null,
	Quantity int not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilPendingTrans(
	SoilNeededID int not null,
	SoilDonatedID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SoilPendingTrans] PRIMARY KEY ( SoilNeededID, SoilDonatedID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyDonated(
	SupplyDonatedID int identity(1000,1) not null primary key,
	userID int not null,
	SupplyName varchar(50) not null,
	SupplyAmount decimal not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyNeeded(
	SupplyNeededID int identity(1000,1) not null primary key,
	userID int not null,
	SupplyName varchar(50) not null,
	SupplyAmount decimal not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyPendingTrans(
	SupplyNeededID int not null,
	SupplyDonatedID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SupplyPendingTrans] PRIMARY KEY ( SupplyNeededID, SupplyDonatedID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimeNeeded(
	TimeNeededID int identity(1000,1) not null primary key,
	UserID int not null, 
	DateNeeded smalldatetime not null,
	GardenAffiliation varchar(50) null,
	Location char(5) null,
	Date smalldatetime not null,
	CityGardenLocated varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimePledge(
	TimePledgeID int identity(1000,1) not null primary key,
	UserID int not null,
	StartTime smalldatetime null,
	FinishTime smalldatetime null,
	DatePledge smalldatetime null,
	Affiliation varchar(75) null,
	Location char(5) null,
	Date smalldatetime not null,
	CityPledging varchar(30) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimePledgeTrans(
	TimePledgeID int not null,
	TimeNeededID int not null,
	DateMatched smalldatetime not null,
	City varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
	
	CONSTRAINT [PK_TimePledgeTrans] PRIMARY KEY ( TimePledgeID, TimeNeededID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.VolunteerHours(
	UserID int not null,
	Date smalldatetime not null,
	HoursVolunteered int not null,
	City varchar(30) not null

	CONSTRAINT [PK_VolunteerHours] PRIMARY KEY ( UserID, Date ASC )
);

create table Expert.BecomeAnExpert(
	RequestNo int identity(1000,1) not null primary key,
	Username int not null,
	WhyShouldIBeAnExpert varchar(200) not null,
	ApprovedBy int null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Active bit not null default 1
);

create table Expert.BlogEntry(
	BlogID int identity(1000,1) not null primary key,
	BlogData varchar(max) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	active bit not null default 1
);

create table Expert.Content(
	ContentID int identity(1000,1) not null primary key,
	UserID int not null,
	RegionID int null,
	Title varchar(50) not null,
	Category varchar(50) null,
	Content varchar(max) not null,
	Date smalldatetime not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Expertise(
	GardenTypeID Varchar(20) not null,
	RegionID int null,
	Content varchar(max) not null,
	CreatedDate smalldatetime not null,
	ModifiedDate smalldatetime null,
	CreatedBy int not null,
	ModifiedBy int null,
	ExpertID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_Expertise] PRIMARY KEY ( GardenTypeID, ExpertID ASC )
);

create table Expert.GardenNotifications(
	GardenID int not null,
	NotificationID int not null,
	TriggerDate smalldatetime null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null

	CONSTRAINT [PK_GardenNotifications] PRIMARY KEY ( GardenID, NotificationID ASC )
);

create table Expert.GardenPlants(
	GardenID int not null,
	PlantID int not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Quantity int not null

	CONSTRAINT [PK_GardenPlants] PRIMARY KEY ( GardenID, PlantID ASC )
);

create table Expert.GardenTypes(
	GardentypeID int identity(1000,1) not null primary key,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Notifications(
	NotificationID int primary key identity(1000,1) not null,
	Type varchar(50) not null,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Active bit not null default 1
);

create table Expert.Plants(
	PlantID int identity(1000,1) not null primary key,
	Name varchar(100) not null,
	Type varchar(100) not null,
	Category varchar(30) not null,
	Description Varchar(255) not null,
	Season Varchar(10) null,
	CreatedBy int not null, 
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	active bit not null default 1
);

create table Expert.PlantCategory(
	CategoryName varchar(30) not null primary key,
	CreatedBy int not null,
	Date smalldatetime not null,
	Active bit not null default 1
);

create table Expert.PlantNutrients(
	PlantID int not null,
	NutrientID int not null

	CONSTRAINT [PK_PlantNutrients] PRIMARY KEY ( PlantID, NutrientID ASC )
);

create table Expert.Nutrients(
	NutrientID int identity(1000,1) not null primary key,
	Name varchar(100) not null,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Question(
	QuestionID int not null identity(1000,1) primary key,
	Title varchar(50) not null,
	Category varchar(50) null,
	Content varchar(max) not null,
	RegionID int null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.QuestionResponse(
	QuestionID int not null,
	Date smalldatetime not null,
	Response varchar(250) not null,
	UserID int not null

	CONSTRAINT [PK_QuestionResponse] PRIMARY KEY ( QuestionID, Date ASC )
);

create table Expert.Recipes(
	RecipeID int identity(1000,1) not null primary key,
	Title varchar(50) not null,
	Category varchar(30) not null,
	Directions varchar(max) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.RecipeCategory(
	CategoryName varchar(30) not null primary key,
	CreatedBy int not null,
	Date smalldatetime not null,
	Active bit not null default 1
);

create table Expert.Templates(
	TemplateID int identity(1000,1) not null primary key,
	UserID int not null,
	Description varchar(max) not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

--created by Dat Tran 2-25-16
CREATE TABLE Gardens.Announcements(
	AnnouncementID int not null primary key identity(1000,1),
	UserID	int not null,
	Date smalldatetime not null,
	OrganizationID int not null,
	Announcement VARCHAR(250) not null
);

create table Gardens.Gardens(
	GardenID int identity(1000,1) not null primary key,
	GroupID int not null,
	UserID int not null,
	GardenDescription varchar(max) not null,
	GardenRegion varchar(25) null
);

Create table Gardens.GardenGuides(
	GardenGuideID int identity(1000,1) not null primary key,
	UserID int not null,
	Content varchar(max) not null
);

create table Gardens.GroupLeaders(
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_GroupLeaders] PRIMARY KEY ( UserID, GroupID ASC )
);

--added by nick king 9-4-16
create table Gardens.GroupLeaderRequests(
	RequestID int identity(1000,1) primary key not null,
	UserID int not null,
	GroupID int not null,
	RequestActive bit not null,
	RequestDate smalldatetime not null,
	ModifiedDate smalldatetime null,
	ModifiedBy int null	
);

--modified by trent 2-23-16
create table Gardens.GroupMembers(
	GroupID int not null,
	UserID int not null,
	CreatedDate smalldatetime not null,
	CreatedBy int not null,
	Leader bit default 0

	CONSTRAINT [PK_GroupMembers] PRIMARY KEY ( GroupID, UserID ASC )
);

create table Gardens.GardenPlans(
	GardenPlanID int identity(1000,1) not null primary key,
	UserID int not null,
	Description varchar(max) not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

--Modified by Kristine 2-25-16 (add orgID field)
create table Gardens.Groups(
	GroupID int identity(1000,1) not null primary key, 
	GroupName varchar(100) not null,
	GroupLeaderID int not null,
	Active bit not null default 1,
	OrganizationID int null
);

--Modified by Chris Sheehan, removed Organization Contact varchar(100) added organizationLeader int should come from userID table 2-25-16
create table Gardens.Organizations(
	OrganizationID int identity(1000,1) primary key not null,
	OrganizationName varchar(100) not null, 
	OrganizationLeader int not null,
	ContactPhone char(10) null,
	Active bit not null default 1
);

create table Gardens.PostLineItems(
	PostID int not null,
	PostLineID int not null,
	UserID int not null,
	GroupID int not null,
	DateSent smalldatetime not null,
	CommentContent varchar(255) not null
);

create table Gardens.PostThreads(
	PostID int identity(1000,1) not null primary key,
	PostType varChar(50),
	GroupComments bit not null default 1,
	NoComments int null,
	ViewByAll bit not null default 1,
	UserID int not null,
	GroupID int not null,
	PostDateTime smalldatetime not null,
	Content varchar(max) not null,
	PostTitle varchar(100) not null,
	Active bit not null default 1
);

create table Gardens.Tasks(
	TaskID int identity(1000,1) not null primary key,
	Description varchar(100) not null,
	Active bit not null default 1
);

create table Gardens.WorkLogs(
	WorkLogID int identity(1000,1) not null primary key, 
	UserID int not null,
	TaskID int not null,
	TimeBegun smalldatetime not null,
	TimeFinished smalldatetime null
);


/**********************************************************************************/
/******************************* Foreign Keys *************************************/
/**********************************************************************************/


ALTER TABLE Admin.ActivityLog WITH NOCHECK ADD  CONSTRAINT [FK_ActivityLog_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.ActivityLog CHECK CONSTRAINT FK_ActivityLog_UserID;
GO

ALTER TABLE Admin.GroupRequest WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.GroupRequest CHECK CONSTRAINT FK_GroupRequest_UserID;
GO

ALTER TABLE Admin.GroupRequest WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_RequestedBy] FOREIGN KEY(RequestedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.GroupRequest CHECK CONSTRAINT FK_GroupRequest_RequestedBy;
GO

ALTER TABLE Admin.GroupRequest WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_approvedBy] FOREIGN KEY(approvedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.GroupRequest CHECK CONSTRAINT FK_GroupRequest_approvedBy;
GO

ALTER TABLE Admin.Messages WITH NOCHECK ADD  CONSTRAINT [FK_Messages_MessageSender] FOREIGN KEY(MessageSender)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.Messages CHECK CONSTRAINT [FK_Messages_MessageSender];
GO

ALTER TABLE Admin.MessageLineItems WITH NOCHECK ADD  CONSTRAINT [FK_MessageLineItems_SenderID] FOREIGN KEY(SenderID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.MessageLineItems CHECK CONSTRAINT [FK_MessageLineItems_SenderID];
GO

ALTER TABLE Admin.MessageLineItems WITH NOCHECK ADD  CONSTRAINT [FK_MessageLineItems_ReadBy] FOREIGN KEY(ReadBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.MessageLineItems CHECK CONSTRAINT [FK_MessageLineItems_ReadBy];
GO

ALTER TABLE Admin.Regions WITH NOCHECK ADD  CONSTRAINT [FK_Regions_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.Regions CHECK CONSTRAINT [FK_Regions_CreatedBy];
GO

ALTER TABLE Admin.Regions WITH NOCHECK ADD  CONSTRAINT [FK_Regions_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.Regions CHECK CONSTRAINT [FK_Regions_ModifiedBy];
GO

ALTER TABLE Admin.UserRoles WITH NOCHECK ADD  CONSTRAINT [FK_UserRoles_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.UserRoles CHECK CONSTRAINT [FK_UserRoles_UserID];
GO

ALTER TABLE Admin.Users WITH NOCHECK ADD  CONSTRAINT [FK_Users_RegionID] FOREIGN KEY(RegionID)
REFERENCES Admin.Regions(RegionID);
GO
ALTER TABLE Admin.Users CHECK CONSTRAINT [FK_Users_RegionID];
GO

ALTER TABLE Donations.EquipmentDonated WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentDonated CHECK CONSTRAINT [FK_EquipmentDonated_UserID];
GO

ALTER TABLE Donations.EquipmentNeeded WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentNeeded CHECK CONSTRAINT [FK_EquipmentNeeded_UserID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_EquipmentDonatedID] FOREIGN KEY(EquipmentDonatedID)
REFERENCES Donations.EquipmentDonated(EquipmentDonatedID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_EquipmentDonatedID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_EquipmentNeededID] FOREIGN KEY(EquipmentNeededID)
REFERENCES Donations.EquipmentNeeded(EquipmentNeededID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_EquipmentNeededID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_UserID];
GO

ALTER TABLE Donations.LandDonated WITH NOCHECK ADD  CONSTRAINT [FK_LandDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandDonated CHECK CONSTRAINT [FK_LandDonated_UserID];
GO

ALTER TABLE Donations.LandNeeded WITH NOCHECK ADD  CONSTRAINT [FK_LandNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandNeeded CHECK CONSTRAINT [FK_LandNeeded_UserID];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_LandDonated] FOREIGN KEY(LandDonated)
REFERENCES Donations.LandDonated(TransactionNum);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_LandDonated];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_LandNeeded] FOREIGN KEY(LandNeeded)
REFERENCES Donations.LandNeeded(LandIdentifier);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_LandNeeded];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_UserID];
GO

ALTER TABLE Donations.MoneyDonated WITH NOCHECK ADD  CONSTRAINT [FK_MoneyDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyDonated CHECK CONSTRAINT [FK_MoneyDonated_UserID];
GO

ALTER TABLE Donations.MoneyNeeded WITH NOCHECK ADD  CONSTRAINT [FK_MoneyNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyNeeded CHECK CONSTRAINT [FK_MoneyNeeded_UserID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_NeedID] FOREIGN KEY(NeedID)
REFERENCES Donations.MoneyNeeded(NeedID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_NeedID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_DonationID] FOREIGN KEY(DonationID)
REFERENCES Donations.MoneyNeeded(NeedID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_DonationID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_UserID];
GO

ALTER TABLE Donations.SeedsDonated WITH NOCHECK ADD  CONSTRAINT [FK_SeedsDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsDonated CHECK CONSTRAINT [FK_SeedsDonated_UserID];
GO

ALTER TABLE Donations.SeedsNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SeedsNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsNeeded CHECK CONSTRAINT [FK_SeedsNeeded_UserID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_SeedsDonatedID] FOREIGN KEY(SeedsDonatedID)
REFERENCES Donations.SeedsDonated(DonationID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_SeedsDonatedID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_SeedsNeededID] FOREIGN KEY(SeedsNeededID)
REFERENCES Donations.SeedsNeeded(SeedsNeededID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_SeedsNeededID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_UserID];
GO

ALTER TABLE Donations.SoilDonated WITH NOCHECK ADD  CONSTRAINT [FK_SoilDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilDonated CHECK CONSTRAINT [FK_SoilDonated_UserID];
GO

ALTER TABLE Donations.SoilNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SoilNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilNeeded CHECK CONSTRAINT [FK_SoilNeeded_UserID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_UserID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_SoilDonatedID] FOREIGN KEY(SoilDonatedID)
REFERENCES Donations.SoilDonated(SoilDonatedID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_SoilDonatedID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_SoilNeededID] FOREIGN KEY(SoilNeededID)
REFERENCES Donations.SoilNeeded(SoilNeededID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_SoilNeededID];
GO

ALTER TABLE Donations.SupplyDonated WITH NOCHECK ADD  CONSTRAINT [FK_SupplyDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyDonated CHECK CONSTRAINT [FK_SupplyDonated_UserID];
GO

ALTER TABLE Donations.SupplyNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SupplyNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyNeeded CHECK CONSTRAINT [FK_SupplyNeeded_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_SupplyNeededID] FOREIGN KEY(SupplyNeededID)
REFERENCES Donations.SupplyNeeded(SupplyNeededID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_SupplyDonatedID] FOREIGN KEY(SupplyDonatedID)
REFERENCES Donations.SupplyDonated(SupplyDonatedID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.TimeNeeded WITH NOCHECK ADD  CONSTRAINT [FK_TimeNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.TimeNeeded CHECK CONSTRAINT [FK_TimeNeeded_UserID];
GO

ALTER TABLE Donations.TimePledge WITH NOCHECK ADD  CONSTRAINT [FK_TimePledge_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.TimePledge CHECK CONSTRAINT [FK_TimePledge_UserID];
GO

ALTER TABLE Donations.TimePledgeTrans WITH NOCHECK ADD  CONSTRAINT [FK_TimePledgeTrans_TimePledgeID] FOREIGN KEY(TimePledgeID)
REFERENCES Donations.TimePledge(TimePledgeID);
GO
ALTER TABLE Donations.TimePledgeTrans CHECK CONSTRAINT [FK_TimePledgeTrans_TimePledgeID];
GO

ALTER TABLE Donations.TimePledgeTrans WITH NOCHECK ADD  CONSTRAINT [FK_TimePledgeTrans_TimeNeededID] FOREIGN KEY(TimeNeededID)
REFERENCES Donations.TimeNeeded(TimeNeededID);
GO
ALTER TABLE Donations.TimePledgeTrans CHECK CONSTRAINT [FK_TimePledgeTrans_TimeNeededID];
GO

ALTER TABLE Donations.VolunteerHours WITH NOCHECK ADD  CONSTRAINT [FK_VolunteerHours_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.VolunteerHours CHECK CONSTRAINT [FK_VolunteerHours_UserID];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_ApprovedBy] FOREIGN KEY(ApprovedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_ApprovedBy];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_CreatedBy];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_ModifiedBy];
GO

ALTER TABLE Expert.BlogEntry WITH NOCHECK ADD  CONSTRAINT [FK_BlogEntry_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BlogEntry CHECK CONSTRAINT [FK_BlogEntry_CreatedBy];
GO

ALTER TABLE Expert.BlogEntry WITH NOCHECK ADD  CONSTRAINT [FK_BlogEntry_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BlogEntry CHECK CONSTRAINT [FK_BlogEntry_ModifiedBy];
GO

ALTER TABLE Expert.Content WITH NOCHECK ADD  CONSTRAINT [FK_Content_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Content CHECK CONSTRAINT [FK_Content_CreatedBy];
GO

ALTER TABLE Expert.Content WITH NOCHECK ADD  CONSTRAINT [FK_Content_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Content CHECK CONSTRAINT [FK_Content_ModifiedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_CreatedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_ModifiedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_ExpertID] FOREIGN KEY(ExpertID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_ExpertID];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_CreatedBy];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_ModifiedBy];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_GardenID] FOREIGN KEY(GardenID)
REFERENCES Gardens.Gardens(GardenID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_GardenID];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_NotificationID] FOREIGN KEY(NotificationID)
REFERENCES Expert.Notifications(NotificationID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_NotificationID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_GardenID] FOREIGN KEY(GardenID)
REFERENCES Gardens.Gardens(GardenID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_GardenID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_PlantID] FOREIGN KEY(PlantID)
REFERENCES Expert.Plants(PlantID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_PlantID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_CreatedBy];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_ModifiedBy];
GO

ALTER TABLE Expert.GardenTypes WITH NOCHECK ADD  CONSTRAINT [FK_GardenTypes_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenTypes CHECK CONSTRAINT [FK_GardenTypes_CreatedBy];
GO

ALTER TABLE Expert.GardenTypes WITH NOCHECK ADD  CONSTRAINT [FK_GardenTypes_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenTypes CHECK CONSTRAINT [FK_GardenTypes_ModifiedBy];
GO

ALTER TABLE Expert.Notifications WITH NOCHECK ADD  CONSTRAINT [FK_Notifications_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Notifications CHECK CONSTRAINT [FK_Notifications_CreatedBy];
GO

ALTER TABLE Expert.Notifications WITH NOCHECK ADD  CONSTRAINT [FK_Notifications_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Notifications CHECK CONSTRAINT [FK_Notifications_ModifiedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_CreatedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_ModifiedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_Category] FOREIGN KEY(Category)
REFERENCES Expert.PlantCategory(CategoryName);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_Category];
GO

ALTER TABLE Expert.PlantCategory WITH NOCHECK ADD  CONSTRAINT [FK_PlantCategory_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.PlantCategory CHECK CONSTRAINT [FK_PlantCategory_CreatedBy];
GO

ALTER TABLE Expert.PlantNutrients WITH NOCHECK ADD  CONSTRAINT [FK_PlantNutrients_PlantID] FOREIGN KEY(PlantID)
REFERENCES Expert.Plants(PlantID);
GO
ALTER TABLE Expert.PlantNutrients CHECK CONSTRAINT [FK_PlantNutrients_PlantID];
GO

ALTER TABLE Expert.PlantNutrients WITH NOCHECK ADD  CONSTRAINT [FK_PlantNutrients_NutrientID] FOREIGN KEY(NutrientID)
REFERENCES Expert.Nutrients(NutrientID);
GO
ALTER TABLE Expert.PlantNutrients CHECK CONSTRAINT [FK_PlantNutrients_NutrientID];
GO

ALTER TABLE Expert.Nutrients WITH NOCHECK ADD  CONSTRAINT [FK_Nutrients_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Nutrients CHECK CONSTRAINT [FK_Nutrients_CreatedBy];
GO

ALTER TABLE Expert.Nutrients WITH NOCHECK ADD  CONSTRAINT [FK_Nutrients_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Nutrients CHECK CONSTRAINT [FK_Nutrients_ModifiedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_CreatedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_ModifiedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_RegionID] FOREIGN KEY(RegionID)
REFERENCES Admin.Regions(RegionID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_RegionID];
GO

ALTER TABLE Expert.QuestionResponse WITH NOCHECK ADD  CONSTRAINT [FK_QuestionResponse_QuestionID] FOREIGN KEY(QuestionID)
REFERENCES Expert.Question(QuestionID);
GO
ALTER TABLE Expert.QuestionResponse CHECK CONSTRAINT [FK_QuestionResponse_QuestionID];
GO

ALTER TABLE Expert.QuestionResponse WITH NOCHECK ADD  CONSTRAINT [FK_QuestionResponse_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.QuestionResponse CHECK CONSTRAINT [FK_QuestionResponse_UserID];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_CreatedBy];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_ModifiedBy];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_Category] FOREIGN KEY(Category)
REFERENCES Expert.RecipeCategory(CategoryName);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_Category];
GO

ALTER TABLE Expert.RecipeCategory WITH NOCHECK ADD  CONSTRAINT [FK_RecipeCategory_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.RecipeCategory CHECK CONSTRAINT [FK_RecipeCategory_CreatedBy];
GO

ALTER TABLE Expert.Templates WITH NOCHECK ADD  CONSTRAINT [FK_Templates_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Templates CHECK CONSTRAINT [FK_Templates_UserID];
GO

ALTER TABLE Gardens.Announcements WITH NOCHECK ADD  CONSTRAINT [FK_Announcements_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Announcements CHECK CONSTRAINT [FK_Announcements_UserID];
GO

ALTER TABLE Gardens.Announcements WITH NOCHECK ADD  CONSTRAINT [FK_Announcements_OrganizationID] FOREIGN KEY(OrganizationID)
REFERENCES Gardens.Organizations(OrganizationID);
GO
ALTER TABLE Gardens.Announcements CHECK CONSTRAINT [FK_Announcements_OrganizationID];
GO

ALTER TABLE Gardens.Gardens WITH NOCHECK ADD  CONSTRAINT [FK_Gardens_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.Gardens CHECK CONSTRAINT [FK_Gardens_GroupID];
GO

ALTER TABLE Gardens.Gardens WITH NOCHECK ADD  CONSTRAINT [FK_Gardens_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Gardens CHECK CONSTRAINT [FK_Gardens_UserID];
GO

ALTER TABLE Gardens.GardenGuides WITH NOCHECK ADD  CONSTRAINT [FK_GardenGuides_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GardenGuides CHECK CONSTRAINT [FK_GardenGuides_UserID];
GO

ALTER TABLE Gardens.GroupLeaders WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaders_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaders CHECK CONSTRAINT [FK_GroupLeaders_UserID];
GO

ALTER TABLE Gardens.GroupLeaders WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaders_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupLeaders CHECK CONSTRAINT [FK_GroupLeaders_GroupID];
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_UserID;
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_GroupID;
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_ModifiedBy;
GO


ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_GroupID];
GO

ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_CreatedBy];
GO

ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_UserID];
GO

ALTER TABLE Gardens.GardenPlans WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GardenPlans CHECK CONSTRAINT [FK_GardenPlans_UserID];
GO

ALTER TABLE Gardens.Groups WITH NOCHECK ADD  CONSTRAINT [FK_Groups_GroupLeaderID] FOREIGN KEY(GroupLeaderID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Groups CHECK CONSTRAINT [FK_Groups_GroupLeaderID];
GO

Alter Table Gardens.Organizations with nocheck add constraint FK_Organizations_OrganizationLeader FOREIGN KEY(OrganizationLeader)
references Admin.Users(UserID);
go
Alter Table Gardens.Organizations CHECK CONSTRAINT FK_Organizations_OrganizationLeader;
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_GroupLeaderID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_GroupLeaderID];
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_GroupID];
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_PostID] FOREIGN KEY(PostID)
REFERENCES Gardens.PostThreads(PostID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_PostID];
GO

ALTER TABLE Gardens.PostThreads WITH NOCHECK ADD  CONSTRAINT [FK_PostThreads_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.PostThreads CHECK CONSTRAINT [FK_PostThreads_UserID];
GO

ALTER TABLE Gardens.PostThreads WITH NOCHECK ADD  CONSTRAINT [FK_PostThreads_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.PostThreads CHECK CONSTRAINT [FK_PostThreads_GroupID];
GO

ALTER TABLE Gardens.WorkLogs WITH NOCHECK ADD  CONSTRAINT [FK_WorkLogs_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.WorkLogs CHECK CONSTRAINT [FK_WorkLogs_UserID];
GO

ALTER TABLE Gardens.WorkLogs WITH NOCHECK ADD  CONSTRAINT [FK_WorkLogs_TaskID] FOREIGN KEY(TaskID)
REFERENCES Gardens.Tasks(TaskID);
GO
ALTER TABLE Gardens.WorkLogs CHECK CONSTRAINT [FK_WorkLogs_TaskID];
GO


/**********************************************************************************/
/******************************* Indexes ******************************************/
/**********************************************************************************/


CREATE NONCLUSTERED INDEX IX_ActivityLog_UserID ON Admin.ActivityLog (UserID);
GO

CREATE NONCLUSTERED INDEX IX_Messages_MessageSender ON Admin.Messages (MessageSender);
GO

CREATE NONCLUSTERED INDEX IX_MessageLineItems_SenderID ON Admin.MessageLineItems (SenderID);
GO

CREATE NONCLUSTERED INDEX IX_Users_LastName ON Admin.Users (LastName);
GO

CREATE NONCLUSTERED INDEX IX_Users_RegionID ON Admin.Users (RegionID);
GO


/****************************************************************************************/
/*******************************Stored Procedures ***************************************/
/****************************************************************************************/


------------------------------------------
-----------Admin.ActivityLog--------------
------------------------------------------

create procedure Admin.spInsertActivityLog(	
	@UserID int,
    @date smalldatetime,
	@LogEntry varchar(250),
	@UserAction varchar(100)
	)
as 
begin
INSERT INTO [Admin].[ActivityLog]
    ([UserID]
    ,[Date]
    ,[LogEntry]
    ,[UserAction])
VALUES(
    @UserID,
    @date,
	@LogEntry,
	@UserAction
	);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Admin.GroupRequest-------------
------------------------------------------

create procedure Admin.spInsertGroupRequest(
	@UserID int, 
	@RequestStatus char(1),
	@RequestDate smalldatetime,
	@RequestedBy int,
	@ApprovedDate smalldatetime,
	@ApprovedBy int)
as
begin
insert into Admin.GroupRequest(
	UserID, 
	RequestStatus,
	RequestDate,
	RequestedBy,
	ApprovedDate,
	ApprovedBy)
values(
	@UserID,
	@RequestStatus,
	@RequestDate,
	@RequestedBy,
	@ApprovedDate,
	@ApprovedBy);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Admin.MessageLineItems---------
------------------------------------------

create procedure Admin.spInsertMessageLineItems(
	@MessageID int,
	@SenderID int,
	@DateSent smalldatetime,
	@ReadBy int,
	@DateRead smalldatetime,
	@MessageContent varchar(250)
	)
as
begin
insert into Admin.MessageLineItems(
	MessageID,
	SenderID,
	DateSent,
	ReadBy,
	DateRead,
	MessageContent)
values(
	@MessageID,
	@SenderID,
	@DateSent,
	@ReadBy,
	@DateRead,
	@MessageContent);
	return @@ROWCOUNT;
end;
go

--created by Ibrahim 2-25-16
CREATE PROCEDURE Admin.spUpdateMessageLineItems (
@MessageID      int,
@SenderID       int,
@DateSent       smalldatetime,
@ReadBy         int,
@DateRead       smalldatetime,
@MessageContent  varchar(250),
@originalSenderID       int,
@originalDateSent       smalldatetime,
@originalReadBy         int,
@originalDateRead       smalldatetime,
@originalMessageContent  varchar(250))
AS
BEGIN
         UPDATE Admin.MessageLineItems
		     SET   		   
				   SenderID  = @SenderID,
				   DateSent  = @DateSent,
				   ReadBy    = @ReadBy,
				   DateRead  = @DateRead,
				   MessageContent =  @MessageContent			 				 
			WHERE
				   MessageID = @MessageID AND
		        	SenderID = @originalSenderID AND
				   DateSent  = @originalDateSent AND
				   ReadBy    = @originalReadBy AND
				   DateRead  = @originalDateRead AND
			  MessageContent = @originalMessageContent;

	return @@ROWCOUNT;  
END;
go

--created by Ibrahim 2-25-16
CREATE PROCEDURE Admin.spUpdateMessageLineItemsRemove (
@MessageID int, @DateSent smalldatetime) 
AS
BEGIN
      UPDATE Admin.MessageLineItems 
	    SET Active = 0
		WHERE MessageID = @MessageID
		and DateSent = @DateSent;

	return @@ROWCOUNT;
END;
go

------------------------------------------
-----------Admin.Messages-----------------
------------------------------------------

create procedure Admin.spInsertMessage(
	@MessageContent varchar(250),
	@MessageDate smalldatetime,
	@Subject varchar(100),
	@MessageSender int)
as 
begin
insert into Admin.Messages(
	MessageContent,
	MessageDate,
	Subject,
	MessageSender)
values(
	@MessageContent,
	@MessageDate,
	@Subject,
	@MessageSender);
	return @@ROWCOUNT;
end;
go

--created by ibrahim 2-19-16
CREATE PROCEDURE Admin.spSelectMessage (
@MessageID   int)
AS
BEGIN
	SELECT MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
	FROM Admin.Messages
	WHERE MessageID = @MessageID
END;
go

CREATE PROCEDURE Admin.spDisplayMessages 
AS
BEGIN
	SELECT MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
	FROM Admin.Messages
	WHERE Active = 1 ;
END;
go

--created by ibrahim 2-19-16
CREATE PROCEDURE Admin.spUpdateMessage (
@MessageID      int,
@MessageContent varchar(250),
@MessageDate    smalldatetime,
@Subject        varchar(100),
@MessageSender  varchar(20),
@Active bit,
@originalMessageContent varchar(250),
@originalMessageDate    smalldatetime,
@originalSubject        varchar(100),
@originalMessageSender  varchar(20),
@originalActive bit)
AS
BEGIN
        UPDATE Admin.Messages
		     SET   
				   MessageContent = @MessageContent,
				   MessageDate    = @MessageDate,
				   Subject        = @Subject,
				   MessageSender  =	@MessageSender,
				   Active         = @Active				 				 
			WHERE 
			            MessageID = @MessageID AND
			       MessageContent = @originalMessageContent AND
				     MessageDate  = @originalMessageDate AND
				         Subject  = @originalSubject AND
				    MessageSender =	@originalMessageSender AND
				          Active  = @originalActive;	

	return @@ROWCOUNT;   
END;
go

--created by ibrahim 2-19-16
CREATE PROCEDURE Admin.spUpdateMessageRemove (
@MessageID int)
AS
BEGIN
      UPDATE Admin.Messages 
	    SET Active = 0
		WHERE MessageID = @MessageID;

	return @@ROWCOUNT;
END;
go


--chris - no stored procs for region yet

------------------------------------------
-----------Admin.Roles--------------------
------------------------------------------
 
 create procedure Admin.spInsertRoles(
	@Description varchar(100),
	@CreatedBy int,
	@CreatedDate smalldatetime)
as 
begin
insert into Admin.Roles(
	Description, 
	CreatedBy,
	CreatedDate)
values(
	@Description,
	@CreatedBy,
	@CreatedDate);
	return @@ROWCOUNT;
end;
go

CREATE PROCEDURE Admin.spSelectRoles (
    @userID INT
)
AS
BEGIN
    SELECT Admin.UserRoles.RoleID, Admin.Roles.Description, Admin.UserRoles.Active 
    FROM [Admin].[Roles], [Admin].[UserRoles]
    WHERE UserRoles.UserID = @userID
    AND Roles.roleID = UserRoles.roleID;
END;
GO

------------------------------------------
-----------Admin.UserRoles----------------
------------------------------------------

create procedure Admin.spInsertUserRoles(
	@UserID int,
	@RoleID int,
	@CreatedBy int,
	@CreatedDate smalldatetime)
as begin
insert into Admin.UserRoles(
	UserID, 
	RoleID,
	CreatedBy,
	CreatedDate)
values(
	@UserID,
	@RoleID,
	@CreatedBy,
	@CreatedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Admin.Users--------------------
------------------------------------------

create procedure Admin.spInsertUsers(
	@FirstName varchar(50),
	@LastName varchar(100),
	@Zip char(9) ,
	@EmailAddress varchar(100),
	@UserName varchar(20),
	@Password varchar(150),
	@RegionID int)
as
begin
insert into Admin.Users(
	FirstName,
	LastName,
	Zip ,
	EmailAddress,
	UserName,
	Password,
	RegionID)
values(
	@FirstName,
	@LastName,
	@Zip ,
	@EmailAddress,
	@UserName,
	@Password,
	@RegionID);
	return @@ROWCOUNT;
end;
go


CREATE PROCEDURE Admin.spUpdateUser (
@UserID int,
@FirstName varchar(50),
@LastName varchar(100),
@Zip char(9),
@EmailAddress varchar(100),
@UserName varchar(20),
@PassWord varchar(150),
@Active bit,
@RegionID int,
@originalFirstName varchar(50),
@originalLastName varchar(100),
@originalZip char(9),
@originalEmailAddress varchar(100),
@originalUserName varchar(20),
@originalPassWord varchar(150),
@originalActive bit,
@originalRegionID int)
AS
BEGIN
        UPDATE Admin.Users
		     SET     FirstName = @FirstName,
					 LastName  = @LastName, 
					 Zip       = @Zip,
				  EmailAddress = @EmailAddress, 
					 userName  = @userName,
					 PassWord  = @PassWord, 
					 Active    = @Active,
					 RegionID  = @RegionID					 
			WHERE       userID = @userID AND
			         FirstName = @originalFirstName AND
					 LastName  = @originalLastName AND 
					 Zip       = @originalZip AND
				  EmailAddress = @originalEmailAddress AND 
					 userName  = @originaluserName AND
					 PassWord  = @originalPassWord AND 
					 Active    = @originalActive AND
					 RegionID  = @originalRegionID;

	return @@ROWCOUNT;     
END;
go

--created by Rhett 2-25-16
CREATE PROCEDURE Admin.spSelectUsers (
	@Active	bit
)
AS
BEGIN
	SELECT UserID, FirstName, LastName, Zip, EmailAddress, UserName,
		PassWord, Active, RegionID
	FROM Admin.Users
	WHERE Active = @Active;
END;
go

--created by Rhett 2-25-16
CREATE PROCEDURE Admin.spSelectUser (
	@UserID int
)
AS
BEGIN
	SELECT UserID, FirstName, LastName, Zip, EmailAddress, UserName,
		PassWord, Active, RegionID
	FROM Admin.Users
	WHERE UserID = @UserID;
END;
go

--created by Ryan Taylor 3-4-16, updated by Ryan 3-4-16
CREATE PROCEDURE Admin.spSelectUserByUserName (
    @username VARCHAR(20)
)
AS
BEGIN
	SELECT UserID, UserName, FirstName, LastName, Zip, EmailAddress, RegionID, Active
    FROM [Admin].[Users]
    WHERE username = @username
END
GO

--created by Ryan Taylor 3-4-16
CREATE PROCEDURE Admin.spSelectUserWithUsernameAndPassword (
    @username VARCHAR(20),
    @password VARCHAR(150)
)
AS
BEGIN
    SELECT COUNT(UserName)
    FROM [Admin].[Users]
    WHERE username = @username
    AND password = @password
    AND active = 1;
END;
GO

--Created by Chris Schwebach 2-25-16
CREATE PROCEDURE Admin.spUpdateUserPersonalInfo (
	@UserID int,
	@FirstName varchar(50),
	@LastName varchar(100),
	@Zip char(9),
	@EmailAddress varchar(100),
	@regionID int)
AS
BEGIN
     UPDATE Admin.Users
		     SET     FirstName = @FirstName,
					 LastName  = @LastName, 
					 Zip       = @Zip,
					 EmailAddress =  @EmailAddress, 
					 regionID = @regionID				 
			WHERE userID = @userID;
	return @@ROWCOUNT;    
END;
go

Create procedure Admin.spSelectUserPersonalInfo (
	@UserID int
)
as
begin
	SELECT FirstName, LastName, Zip, EmailAddress, RegionID
	FROM Admin.Users
	WHERE UserID = @UserID;
end;
go

--created by ibrahim 2-19-16
CREATE PROCEDURE Admin.spUpdateUserRemove (
@userID int)
AS
BEGIN
   UPDATE Admin.Users 
	    SET Active = 0 
		WHERE UserID = @UserID;

	return @@ROWCOUNT; 
END;
go
 --created by Ryan Taylor 3-4-16 
CREATE PROCEDURE Admin.spUpdatePassword (
    @username VARCHAR(20),
    @oldPassword VARCHAR(150),
    @newPassword VARCHAR(150)
)
AS
BEGIN
    UPDATE Admin.Users
    SET password = @newPassword
    WHERE username = @username
    AND password = @oldPassword
    AND active = 1
    RETURN @@rowcount;
END;
GO

------------------------------------------
-----------Donations.EquipmentDonated-----
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentDonated(
	@EquipmentName varchar(50),
	@EquipmentQuntity int,
	@DateDonated smalldatetime,
	@UserID int,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as
begin
insert into Donations.EquipmentDonated(
	EquipmentName,
	EquipmentQuantity,
	DateDonated,
	UserID,
	ShippingNotes,
	StateLocated)
values(
	@EquipmentName,
	@EquipmentQuntity,--
	@DateDonated,
	@UserID,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.EquipmentNeeded------
------------------------------------------
	
--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentNeeded(
	@EquipmentName varchar(50),
	@EquipmentQuantity int,
	@dateDonated smalldatetime,
	@UserID int,
	@GroupID int,
	@ReceivingNotes varchar(255),
	@StateLocated char(2))
as 
begin
insert into Donations.EquipmentNeeded(
	EquipmentName,
	EquipmentQuantity,
	DateDonated,
	UserID,
	GroupID,
	ReceivingNotes,
	StateLocated)
values(
	@EquipmentName,
	@EquipmentQuantity,
	@dateDonated,
	@UserID,
	@GroupID,
	@ReceivingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.EquipmentPendingTrans
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentPendingTrans(
	@EquipmentDonatedID int,
	@EquipmentNeededID int,
	@Date smalldatetime,
	@UserID int,
	@GroupID int)
as 
begin
insert into Donations.EquipmentPendingTrans(
	EquipmentDonatedID,
	EquipmentNeededID,
	Date,
	UserID,
	GroupID)
values(
	@EquipmentDonatedID,
	@EquipmentNeededID,
	@Date,
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandDonated----------
------------------------------------------

create procedure Donations.spInsertLandDonated(
	@UserID int,
	@Size int,
	@Address varchar(100),
	@City varchar (30),
	@State char(2),
	@Zip char(9),
	@Notes varchar(255),
	@DateDonated smalldatetime)
as 
begin
insert into Donations.LandDonated(
	UserID,
	Size,
	Address,
	City,
	state,
	zip,
	Notes,
	DateDonated)
values(
	@UserID,
	@Size,
	@Address,
	@City,
	@State,
	@Zip,
	@Notes,
	@DateDonated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandNeeded-----------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertLandNeeded(
	@UserID int,
	@DateNeeded smalldatetime,
	@DateRequested smalldatetime,
	@Notes varchar(255),
	@Zip varchar(9),
	@City varchar(30),
	@GroupID int)
as
begin
insert into Donations.LandNeeded(
	UserID,
	DateNeeded,
	DateRequested,
	Notes,
	Zip,
	City,
	GroupID)
values(
	@UserID,
	@DateNeeded,
	@DateRequested,
	@Notes,
	@Zip,
	@City,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandPendingTrans-----
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertLandPendingTrans(
	@LandDonated int,
	@LandNeeded int,
	@DateCompleted smalldatetime,
	@Notes varchar(255),
	@ExpirationDate smalldatetime,
	@TransDate smalldatetime,
	@UserID int,
	@GroupID int)
as
begin
insert into Donations.LandPendingTrans(
	LandDonated,
	LandNeeded,
	DateCompleted,
	Notes,
	ExpirationDate,
	TransDate,
	UserID,
	GroupID)
values(
	@LandDonated,
	@LandNeeded,
	@DateCompleted,
	@Notes,
	@ExpirationDate,
	@TransDate,
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.MoneyDonated---------
------------------------------------------

create procedure Donations.spInsertMoneyDonated(
	@UserID int,
	@Location varchar(50),
	@Amount decimal,
	@DateCreated smalldatetime)
as
begin
insert into Donations.MoneyDonated(
	UserID,
	Location,
	Amount,
	DateCreated)
values(
	@UserID,
	@Location,
	@Amount,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.MoneyNeeded----------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertMoneyNeeded(
	@UserId int,
	@Location varchar(50),
	@Amount decimal,
	@DateCreated smalldatetime,
	@GroupID int)
as
begin
insert into Donations.MoneyNeeded(
	UserID,
	Location,
	Amount,
	DateCreated,
	GroupID)
values(
	@UserId,
	@Location,
	@Amount,
	@DateCreated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.MoneyPendingTrans----
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertMonetPendingTrans(
	@NeedID int,
	@DonationID int,
	@UserID int,
	@Date smalldatetime,
	@GroupID int)
as
begin
insert into Donations.MoneyPendingTrans(
	NeedID,
	DonationID,
	UserID,
	Date,
	GroupID)
values(
	@NeedID,
	@DonationID,
	@UserID,
	@Date,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SeedsDonated---------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertSeedsDonated(
	@UserID int,
	@Quantity int,
	@SeedType varchar(50),
	@Date smalldatetime,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as 
begin
insert into Donations.SeedsDonated(
	UserID,
	Quantity,
	SeedType,
	Date,
	ShippingNotes,
	StateLocated)
values(
	@UserID,
	@Quantity,
	@SeedType,
	@Date,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go


------------------------------------------
-----------Expert.BecomeAnExpert----------
------------------------------------------

create procedure Expert.spInsertExpertBecomeAnExpert(
	@Username int,
	@WhyShouldIBeAnExpert varchar(200),
	@ApprovedBy int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.BecomeAnExpert(
	UserName,
	WhyShouldIBeAnExpert,
	ApprovedBy,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@UserName,
	@WhyShouldIBeAnExpert,
	@ApprovedBy,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.BlogEntry---------------
------------------------------------------

create procedure Expert.spInsertBlogEntry(
	@BlogData varchar(max),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.BlogEntry(
	BlogData,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@BlogData,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Content-----------------
------------------------------------------

create procedure Expert.spInsertContent(
	@UserID int,
	@RegionID int,
	@Title varchar(50),
	@Category varchar(50),
	@Content varchar(max),
	@Date smalldatetime ,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Content(
	UserID,
	RegionID,
	Title,
	Category,
	Content,
	Date,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@UserID,
	@RegionID,
	@Title,
	@Category,
	@Content,
	@Date,
	@Createdby,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Expertise---------------
------------------------------------------

create procedure Expert.spInsertExpertise(
	@GardenTypeId varchar(20),
	@RegionID int,
	@Content varchar(max),
	@CreatedDate smalldatetime,
	@ModifiedDate smalldatetime,
	@CreatedBy int,
	@ModifiedBy int,
	@ExpertID int)
as
begin
insert into Expert.Expertise(
	GardenTypeID,
	RegionID,
	Content,
	CreatedDate,
	ModifiedDate,
	CreatedBy,
	ModifiedBy,
	ExpertID)
values(
	@GardenTypeId,
	@RegionID,
	@Content,
	@CreatedDate,
	@ModifiedDate,
	@CreatedBy,
	@ModifiedBy,
	@ExpertID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.GardenNotifications-----
------------------------------------------

create procedure Expert.spInsertGardenNotifictions(
	@GardenID int,
	@NotificationID int,
	@TriggerDate smalldatetime,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.GardenNotifications(
	GardenID,
	NotificationID,
	TriggerDate,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@GardenID,
	@NotificationID,
	@TriggerDate,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.GardenPlants------------
------------------------------------------

create procedure Expert.spInsertGardenPlants(
	@GardenID int,
	@PlantID int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime,
	@Quantity int)
as
begin
insert into Expert.GardenPlants(
	GardenID,
	PlantID,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate,
	Quantity)
values(
	@GardenID,
	@PlantID,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate,
	@Quantity);
	return @@ROWCOUNT;
end;
go	

------------------------------------------
-----------Expert.GardenTypes-------------
------------------------------------------	

create procedure Expert.spInsertGardenTypes(
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.GardenTypes(
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Notifications-----------
------------------------------------------

create procedure Expert.spInsertNotofications(
	@Type varchar(50),
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDte smalldatetime)
as
begin
insert into Expert.Notifications(
	Type,
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Type,
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDte);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Nutrients---------------
------------------------------------------

create procedure Expert.spInsertNutrients(
	@Name varchar(100),
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Nutrients(
	Name,
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Name,
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.PlantCtegory------------
------------------------------------------

create procedure Expert.spInsertPlantCategory(
	@CategoryName varchar(30),
	@CreatedBy int,
	@Date smalldatetime)
as 
begin
insert into Expert.PlantCategory(
	CategoryName,
	CreatedBy,
	Date)
values(
	@CategoryName,
	@CreatedBy,
	@Date);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.PlantNutrients----------
------------------------------------------

create procedure Expert.spInsertPlantNutrients(
	@PlantID int,
	@NutrientID int)
as 
begin
insert into Expert.PlantNutrients(
	PlantID,
	NutrientID)
values(
	@PlantID,
	@NutrientID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Plants------------------
------------------------------------------

create procedure Expert.spInsertPlants(
	@Name varchar(100),
	@Type varchar(100),
	@Category varchar(30),
	@Description varchar(255),
	@Season varchar(10),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.Plants(
	Name,
	Type,
	Category,
	Description,
	Season,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Name,
	@Type,
	@Category,
	@Description,
	@Season,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

--created by Rhett 2-25-16
CREATE PROCEDURE Expert.spUpdatePlant (
	@PlantID int,
	@Name varchar(100),
	@Type varchar(100),
	@Category varchar(30),
	@Description Varchar(255),
	@Season Varchar(10),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime,
	@Active bit,
	@OriginalName varchar(100),
	@OriginalType varchar(100),
	@OriginalCategory varchar(30),
	@OriginalDescription Varchar(255),
	@OriginalSeason Varchar(10),
	@OriginalCreatedBy int,
	@OriginalCreatedDate smalldatetime,
	@OriginalModifiedBy int,
	@OriginalModifiedDate smalldatetime,
	@OriginalActive bit
	)
AS
BEGIN
     UPDATE Expert.Plants
			 SET    Name = @Name,
					Type = @Type, 
					Category = @Category, 
					Description = @Description,
					Season = @Season, 
					CreatedBy = @CreatedBy,
					CreatedDate = @CreatedDate,	
					ModifiedBy = @ModifiedBy,	
					ModifiedDate = @ModifiedDate,	
					Active = @Active
			WHERE @PlantID = PlantID
			and @OriginalName = Name
			and @OriginalType = Type
			and @OriginalCategory = Category
			and @OriginalDescription = Description
			and @OriginalSeason = Season
			and @OriginalCreatedBy = CreatedBy
			and @OriginalCreatedDate = CreatedDate
			and @OriginalModifiedBy = ModifiedBy
			and @OriginalModifiedDate = ModifiedDate
			and @OriginalActive = Active;
	return @@ROWCOUNT;  
END;
go

CREATE PROCEDURE Expert.spSelectPlants (
	@Active	bit
)
AS
BEGIN
	SELECT PlantID, Name, Type, Category, Description, Season, 
		CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
	FROM Expert.Plants
	WHERE Active = @Active;
END;
go

CREATE PROCEDURE Expert.spSelectPlant (
	@PlantID int
)
AS
BEGIN
	SELECT PlantID, Name, Type, Category, Description, Season, 
		CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
	FROM Expert.Plants
	WHERE PlantID = @PlantID;
END;
go

--Added by Ibrahim 2-25-16
CREATE PROCEDURE Expert.spUpdatePlantsRemove (
@PlantID int)
AS
BEGIN
      UPDATE Expert.Plants 
	    SET Active = 0
		WHERE PlantID = @PlantID;

	return @@ROWCOUNT;  
END;
go

------------------------------------------
-----------Expert.Question----------------
------------------------------------------

create procedure Expert.spInsertQuestion(
	@Title varchar(50),
	@Category varchar(50),
	@Content varchar(max),
	@RegionID int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Question(
	Title,
	Category,
	Content,
	RegionID,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Title,
	@Category,
	@Content,
	@RegionID,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.QuestionResponse--------
------------------------------------------

create procedure Expert.spInsertQuestionResponse(
	@QuestionID int,
	@Date smalldatetime,
	@Response varchar(250),
	@UserID int)
as
begin
insert into Expert.QuestionResponse(
	QuestionID,
	Date,
	Response,
	UserID)
values(
	@QuestionID,
	@Date,
	@Response,
	@UserID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.RecipeCategory----------
------------------------------------------

create procedure Expert.spInsertRecipeCategory(
	@CategoryName varchar(30),
	@CreatedBy int,
	@Date smalldatetime)
as
begin
insert into Expert.RecipeCategory(
	CategoryName,
	CreatedBy,
	Date)
values(
	@CategoryName,
	@CreatedBy,
	@Date);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Recipes-----------------
------------------------------------------

create procedure Expert.spInsertRecipes(
	@Title varchar(50),
	@Category varchar(30),
	@Directions varchar(max),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.Recipes(
	Title,
	Category,
	Directions,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Title,
	@Category,
	@Directions,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Templates---------------
------------------------------------------

create procedure Expert.spInsertTemplates(
	@UserID int,
	@Description varchar(max),
	@DateCreated smalldatetime)
as
begin
insert into Expert.Templates(
	UserID,
	Description,
	DateCreated)
values(
	@UserID,
	@Description,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.Announcements----------
------------------------------------------

create procedure Gardens.spInsertAnnouncements(
	@UserID int,
	@Date smalldatetime,
	@OrganizationID int,
	@Announcement VARCHAR(250))
as
begin
insert into Gardens.Announcements(
	UserID,
	Date,
	OrganizationID,
	Announcement)
values(
	@UserID,
	@Date,
	@OrganizationID,
	@Announcement);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.GardenGuides-----------
------------------------------------------

create procedure Gardens.spInsertGardenGuides(
	@UserID int,
	@Content varchar(max))
as
begin
insert into Gardens.GardenGuides(
	UserID,
	Content)
values(
	@UserID,
	@Content);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.GardenPlants-----------
------------------------------------------

create procedure Gardens.spInsertGardenPlans(
	@UserID int,
	@Description varchar(max),
	@DateCreated smalldatetime)
as 
begin
insert into Gardens.GardenPlans(
	UserID,
	Description,
	DateCreated)
values(
	@UserID,
	@Description,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.Gardens----------------
------------------------------------------

create procedure Gardens.spInsertGardens(
	@GroupID int,
	@UserID int,
	@GardenDescription varchar(max),
	@GardenRegion varchar(25))
as 
begin
insert into Gardens.Gardens(
	GroupID,
	UserID,
	GardenDescription,
	GardenRegion)
values(
	@GroupID,
	@UserID,
	@GardenDescription,
	@GardenRegion);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.GroupLeaders-----------
------------------------------------------

create procedure Gardens.spInsertGroupLeaders(
	@UserID int,
	@GroupID int)
as
begin
insert into Gardens.GroupLeaders(
	UserID,
	GroupID)
values(
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-------Gardens.GroupLeaderRequests--------
------------------------------------------

--added by nick king 3-4-16
create procedure Gardens.spInsertGroupLeaderRequest(
	@UserID int,
	@GroupID int,
	@RequestDate smalldatetime,
	@ModifiedDate smalldatetime,
	@ModifiedBy int,
	@RequestActive bit)
as
begin
insert into Gardens.GroupLeaderRequests(
	UserID, 
	GroupID,
	RequestDate,
	ModifiedDate,
	ModifiedBy,
	RequestActive)
values(
	@UserID,
	@GroupID,
	@RequestDate,
	@ModifiedDate,
	@ModifiedBy,
	@RequestActive);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.GroupMembers-----------
------------------------------------------

--updated due to a table update by Trent - updated by chris sheehan 2-25-16
create procedure Gardens.spInsertGroupMembers(
	@GroupID int,
	@UserID int,
	@CreatedDate smalldatetime,
	@CreatedBy int,
	@Leader bit)
as
begin
insert into Gardens.GroupMembers(
	GroupID,
	UserID,
	CreatedDate,
	CreatedBy,
	Leader)
values(
	@GroupID,
	@UserID,
	@CreatedDate,
	@CreatedBy,
	@Leader);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.Groups-----------------
------------------------------------------

create procedure Gardens.spInsertGroups(
	@GroupName varchar(100),
	@GroupLeaderID int,
	@OrganizationID int)
as 
begin
insert into Gardens.Groups(
	GroupName,
	GroupLeaderID,
	OrganizationID)
values(
	@GroupName,
	@GroupLeaderID,
	@OrganizationID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.Organizations----------
------------------------------------------

--Modified by Chris Sheehan, removed OrganizationContact varchar(100) added OrganizationLeader int should come from userID table 2-25-16
create procedure Gardens.spInsertOrganizations(
	@OrganizationName varchar(100),
	@OrganizationLeader varchar(100),
	@ContactPhone char(10))
as 
begin
insert into Gardens.Organizations(
	OrganizationName,
	OrganizationLeader,
	ContactPhone)
values(
	@OrganizationName,
	@OrganizationLeader,
	@ContactPhone);
	return @@ROWCOUNT;
end;
go

--created by Kris Johnson 3-4-16
create procedure Gardens.spSelectOrganization(
	@OrganizationID int)
as 
begin
select GroupID,GroupName,GroupLeaderID
from Gardens.Groups 
where  OrganizationID = @OrganizationID;
end;
go

------------------------------------------
-----------Gardens.PostLineItems----------
------------------------------------------

create procedure Gardens.spInsertPostLineItems(
	@PostLineID int,
	@UserID int,
	@GroupID int,
	@DateSent smalldatetime,
	@CommentContent varchar(255))
as
begin
insert into Gardens.PostLineItems(
	PostLineID,
	UserID,
	GroupID,
	DateSent,
	CommentContent)
values(
	@PostLineID,
	@UserID,
	@GroupID,
	@DateSent,
	@CommentContent);
	return @@ROWCOUNT;
end;

go

------------------------------------------
-----------Gardens.PostThreads------------
------------------------------------------

Create procedure Gardens.spInsertPostThreads(
	@PostType varchar(50),
	@GroupComments bit,
	@NoComments int,
	@ViewByAll bit,
	@UserID int,
	@GroupID int,
	@PostDateTime smalldatetime,
	@Content varchar(max),
	@PostTitle varchar(100))
as 
begin
insert into Gardens.PostThreads(
	PostType,
	GroupComments,
	NoComments,
	ViewByAll,
	UserID,
	GroupID,
	PostDateTime,
	Content,
	PostTitle)
values(
	@PostType,
	@GroupComments,
	@NoComments,
	@ViewByAll,
	@UserID,
	@GroupID,
	@PostDateTime,
	@Content,
	@PostTitle);
	return @@ROWCOUNT;
	end;
	
go

--create procedure Gardens.sp

------------------------------------------
-----------Gardens.Tasks------------------
------------------------------------------

--created by Nasr 3-4-16
CREATE PROCEDURE Gardens.spUpdateTasks 
	(@TaskID INT,
	@Description VARCHAR(100),
	@Active BIT,
	@OriginalTaskID INT,
	@OriginalDescription VARCHAR(100),
	@OriginalActive BIT)
 AS
 BEGIN 
	UPDATE Gardens.Tasks
	SET   
		Description = @Description,
		Active = @Active
		WHERE TaskID = @TaskID
		and Description = @OriginalDescription
		and Active = @OriginalActive;
	
	RETURN @@ROWCOUNT;
END;
GO

--created by Nasr 3-4-16
CREATE PROCEDURE Gardens.spInsertTasks 
	(@TaskID INT,
	@Description VARCHAR(100))	
AS
BEGIN
INSERT INTO Gardens.Tasks
    (TaskID,
    Description)
	
VALUES
   (@TaskID,
    @Description);	
END;
GO


/**********************************************************************************/
/******************************* Test Data ****************************************/
/**********************************************************************************/

exec Admin.spInsertUsers 'Jeff', 'Jeff', '11111', 'E@E.com', 'jeff', 'xxxx', null