use MarketingSupport;

drop table if exists dbo.markers;

CREATE TABLE dbo.markers
	(
	user_id varchar(36) NOT NULL,
	marker_id varchar(36) NOT NULL,
	contact_id varchar(36) NULL,
	location_id varchar(36) NULL,
	image_id varchar(36) NULL
	CONSTRAINT PK_Markers PRIMARY KEY (user_id,marker_id)
	) 

