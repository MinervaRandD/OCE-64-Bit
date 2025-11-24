use MarketingSupport;

drop table if exists dbo.users;

CREATE TABLE dbo.users
	(
	user_id varchar(36) NOT NULL,
	last_name varchar(256) NOT NULL,
	first_name varchar(256)
	CONSTRAINT PK_Users PRIMARY KEY (user_id)
	)  ON [PRIMARY]


