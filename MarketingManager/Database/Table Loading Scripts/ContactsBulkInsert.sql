use MarketingSupport;

bulk insert dbo.contacts
from 'C:\Minerva Research and Development\Projects\OCERev4\OCERev4\MarketingManager\Database\Table Loading Scripts\TestContacts.csv'
with
	( format = 'csv')