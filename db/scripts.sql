use paisa2u;
select * from users;
   for update;
update users set vendortype = 'V';
commit;
select * from vendorproducts;
delete from vendorproducts where productid=5;
insert into vendorproducts values (5,1);
select v.*,u.*,p.* from vendor v
inner join users u on u.regid = v.regid
inner join vendorproducts vp
ON       vp.vendorid =
         (
         SELECT  productid 
         FROM    vendorproducts
         WHERE   v.vendorid = vendorproducts.vendorid limit 1
         )
inner join products p
on vp.productid = p.productid;
 on vp.vendorid = v.vendorid;
group by vp.productid,vp.vendorid
having vp.productid ;
select * from vendor;
delete from vendor where vendorid=3;
insert into vendor(regid, endate,enuser) values (2,'2023-07-08','shazia');
rollback;
select * from products;
select * from subscription;
select * from subscriptionperc;
select * from city;
insert into city (countryid, cityname) values(1,'Karachi');
select * from country;
describe addresses;
alter table vendorproducts;
ALTER TABLE vendorproducts
ADD FOREIGN KEY (Productid) REFERENCES Products(Productid) ON DELETE CASCADE;vendorproducts
describe vendorproducts;
insert into category(catdesc, endate,enuser) values ('Shoes','2023-06-23','mars' );
insert into country(countryname) values("Pakistan");
SET SQL_SAFE_UPDATES = 0;
delete from products where productid>1;
describe users;
insert into users(firstname,	middlename,	lastname,	email,	username,	pwd,	referredby,	regtype,	vendortype,	phonenumber,	endate,	enuser,	substype,	regstatus,	autorenewal,	qrpicture,	PasswordSalt,	PasswordHash)
values           ('shazia',	'mars',	'mars',	'mars',	'mars',	'mars',	'mars',	'1',	'1',	'1',	'2023-06-23',	'mars',	'1',	true	,true	,'mars',	'mars',	'mars');
alter table siteuser add column jwttoken varchar(2000);
ALTER TABLE registration RENAME users;

drop column pwd;
create table country(countryid int NOT NULL AUTO_INCREMENT, countryname varchar(50) NOT NULL, PRIMARY KEY (countryid));

create table city(cityid int NOT NULL AUTO_INCREMENT, countryid int, cityname varchar(50) NOT NULL, PRIMARY KEY (cityid), FOREIGN KEY (countryid) REFERENCES country(countryid));

create table subscription(subsid int NOT NULL AUTO_INCREMENT, 
	subtype varchar(50) NOT NULL, 
    subfee int,
    appowner float,
    vendor float,
    subvendor float,
    customer float,
    PRIMARY KEY (subsid),
    endate datetime,
    enuser varchar(50));

create table registration(
regId	int NOT NULL AUTO_INCREMENT,
firstname varchar(50) NOT NULL,
middlename varchar(50),
lastname varchar(50) NOT NULL,
email varchar(50) NOT NULL,
username varchar(50) NOT NULL,
pwd varchar(50) NOT NULL,
referredby varchar(50), 
regtype varchar(1) NOT NULL,
vendortype varchar(1),
phonenumber varchar(50) NOT NULL,
endate datetime NOT NULL,
enuser varchar(50) NOT NULL,
substype varchar(1) NOT NULL,
regstatus boolean NOT NULL,
autorenewal boolean NOT NULL,
qrpicture varchar(100),
PRIMARY KEY (regid));
drop table subscriptionperc;
create table subscriptionperc(
recId	int NOT NULL AUTO_INCREMENT,
regId	int NOT NULL,
appowner float,
vendor float,
subvendor float,
customer float,
endate datetime NOT NULL,
enuser varchar(50) NOT NULL,
PRIMARY KEY (recId),
FOREIGN KEY (regId) REFERENCES registration(regId)
);

create table addresses(
addid int NOT NULL AUTO_INCREMENT,
regId	int NOT NULL,
countryid int,
cityid int,
streetaddress varchar(200) NOT NULL,
postal varchar(10) ,
district varchar(100) NOT NULL,
townstehsil varchar(100) NOT NULL,
area varchar(100) NOT NULL,
longitude varchar(100),
latitude varchar(100),
PRIMARY KEY (addid),
FOREIGN KEY (regId) REFERENCES registration(regId),
FOREIGN KEY (countryid) REFERENCES country(countryid),
FOREIGN KEY (cityid) REFERENCES city(cityid)
);

create table vendor(
vendorid int NOT NULL AUTO_INCREMENT,
regId	int NOT NULL,
PRIMARY KEY (vendorid),
endate datetime NOT NULL,
enuser varchar(50) NOT NULL,
FOREIGN KEY (regId) REFERENCES registration(regId)
);
create table category(
catid int NOT NULL AUTO_INCREMENT,
catdesc varchar(100),
PRIMARY KEY (catid),
endate datetime NOT NULL,
enuser varchar(50) NOT NULL
);

insert into siteuser (username, email,pwd,adminrole) values('a','a@a.com','aa','A');
create table siteuser(
userid int NOT NULL AUTO_INCREMENT,
username varchar(50),
email varchar(50),
pwd varchar(50),
adminrole varchar(1),
PRIMARY KEY (userid)
);

alter table siteuser
add column PasswordSalt varchar(500) , 
add column PasswordHash varchar(500);
alter table users
add column PasswordSalt varchar(500) , 
add column PasswordHash varchar(500);

create table products(
productid int NOT NULL AUTO_INCREMENT,
catid int NOT NULL,
productname varchar(100) NOT NULL,
productcode varchar(50) NOT NULL,
picture varchar(200) NOT NULL,
discountpercentage float,
discountamount float,
discountcode varchar(50) NOT NULL,
dicountcondition varchar(100) NOT NULL,
PRIMARY KEY (productid),
FOREIGN KEY (catid) REFERENCES category(catid)
);

create table vendorproducts(
productid int NOT NULL,
vendorid int NOT NULL,
PRIMARY KEY (productid, vendorid),
FOREIGN KEY (productid) REFERENCES products(productid),
FOREIGN KEY (vendorid) REFERENCES vendor(vendorid)
);
describe transactions;
create table transactions(
tranid int NOT NULL AUTO_INCREMENT,
regId	int NOT NULL,
amount float,
endate datetime NOT NULL,
enuser varchar(50) NOT NULL,
PRIMARY KEY (tranid),
FOREIGN KEY (regId) REFERENCES users(regId)
);





