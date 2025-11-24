use MarketingSupport;

drop table if exists dbo.contacts;

CREATE TABLE dbo.contacts
	(
	user_id varchar(36) NOT NULL,
	contact_id varchar(36) NOT NULL,
	point_of_contact varchar(1024) NULL,
	company_name varchar(1024) NOT NULL,
	company_address_1 varchar(1024) NULL,
	company_address_2 varchar(1024) NULL,
	company_city varchar(1024) NULL,
	company_state_or_region varchar(1024) NULL,
	company_country varchar(1024) NULL,
	company_postal_code varchar(1024) NULL,
	contact_phone varchar(128) NULL,
	contact__email varchar(1024) NULL,
	contact_web_site varchar(1024) NULL,
	comments text NULL,
	marker_id varchar(36) NULL,
	location_id varchar(36) NULL,
	status varchar(128) NULL
	Constraint PK_Contacts primary key (user_id, contact_id)
	)  ;
