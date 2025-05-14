Run This Script On The Sql Server Managment Studio
-- Use the CarShowroom database
USE CarShowroom;

-- Insert the User role
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (1, 'User', 'USER');

-- Insert the Admin role
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (2, 'Admin', 'ADMIN');
