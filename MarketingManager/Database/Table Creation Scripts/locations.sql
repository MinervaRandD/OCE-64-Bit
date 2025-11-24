use MarketingSupport;

drop table if exists dbo.locations;

CREATE TABLE dbo.locations
	(
	user_id varchar(36) NOT NULL,
	location_id varchar(36) NOT NULL,
	lat float(53) NOT NULL,
	lng float(53) NOT NULL,
	location_name varchar(256),
	comments text
	CONSTRAINT PK_Locations PRIMARY KEY (user_id,location_id)
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
