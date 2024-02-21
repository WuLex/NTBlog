-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- BlogDB.dbo.Accounts definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Accounts;

CREATE TABLE BlogDB.dbo.Accounts (
	Id int IDENTITY(1,1) NOT NULL,
	UserName nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Password nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_Accounts PRIMARY KEY (Id)
);


-- BlogDB.dbo.Archives definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Archives;

CREATE TABLE BlogDB.dbo.Archives (
	Id int IDENTITY(1,1) NOT NULL,
	ArchiveDate nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Count int NOT NULL,
	CONSTRAINT PK_Archives PRIMARY KEY (Id)
);


-- BlogDB.dbo.Categorys definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Categorys;

CREATE TABLE BlogDB.dbo.Categorys (
	Id int IDENTITY(1,1) NOT NULL,
	CategoryName nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Sort int NOT NULL,
	IsTop bit NOT NULL,
	CreateTime datetime2 NOT NULL,
	CONSTRAINT PK_Categorys PRIMARY KEY (Id)
);


-- BlogDB.dbo.LoginLogs definition

-- Drop table

-- DROP TABLE BlogDB.dbo.LoginLogs;

CREATE TABLE BlogDB.dbo.LoginLogs (
	Id int IDENTITY(1,1) NOT NULL,
	Ip nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CreateTime datetime2 NOT NULL,
	UserAgent nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_LoginLogs PRIMARY KEY (Id)
);


-- BlogDB.dbo.Logs definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Logs;

CREATE TABLE BlogDB.dbo.Logs (
	Id int IDENTITY(1,1) NOT NULL,
	[Date] datetime2 NOT NULL,
	Thread nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	[Level] nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Logger nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Message nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	[Exception] nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_Logs PRIMARY KEY (Id)
);


-- BlogDB.dbo.Tags definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Tags;

CREATE TABLE BlogDB.dbo.Tags (
	Id int IDENTITY(1,1) NOT NULL,
	TagName nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CreateTime datetime2 NOT NULL,
	CONSTRAINT PK_Tags PRIMARY KEY (Id)
);


-- BlogDB.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE BlogDB.dbo.[__EFMigrationsHistory];

CREATE TABLE BlogDB.dbo.[__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE Chinese_PRC_CI_AS NOT NULL,
	ProductVersion nvarchar(32) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);


-- BlogDB.dbo.Articles definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Articles;

CREATE TABLE BlogDB.dbo.Articles (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Content nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Author nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CreateTime datetime2 NOT NULL,
	IsTop bit NOT NULL,
	Sort int NOT NULL,
	State int NOT NULL,
	Hits int NOT NULL,
	IsDel bit NOT NULL,
	Tags nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	MetaTitle nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	MetaKeywords nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	MetaDescription nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CategoryId int NOT NULL,
	CONSTRAINT PK_Articles PRIMARY KEY (Id),
	CONSTRAINT FK_Articles_Categorys_CategoryId FOREIGN KEY (CategoryId) REFERENCES BlogDB.dbo.Categorys(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Articles_CategoryId ON dbo.Articles (  CategoryId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- BlogDB.dbo.Comments definition

-- Drop table

-- DROP TABLE BlogDB.dbo.Comments;

CREATE TABLE BlogDB.dbo.Comments (
	Id int IDENTITY(1,1) NOT NULL,
	Nickname nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	Email nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	CommentText nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	ArticleId int NOT NULL,
	CreateTime datetime2 NOT NULL,
	CommentIp nvarchar COLLATE Chinese_PRC_CI_AS NULL,
	State int NOT NULL,
	CONSTRAINT PK_Comments PRIMARY KEY (Id),
	CONSTRAINT FK_Comments_Articles_ArticleId FOREIGN KEY (ArticleId) REFERENCES BlogDB.dbo.Articles(Id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Comments_ArticleId ON dbo.Comments (  ArticleId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
