--ALTER TABLE Employee DROP CONSTRAINT [FK_Department];
--ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_Employee];
--ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_TrainingProgram];
--ALTER TABLE EmployeeComputer DROP CONSTRAINT [FK_EmployeeComputer_Employee];
--ALTER TABLE EmployeeComputer DROP CONSTRAINT [FK_EmployeeComputer_Computer];
--ALTER TABLE ProductOrder DROP CONSTRAINT [FK_Product];
--ALTER TABLE ProductOrder DROP CONSTRAINT [FK_Orders];
--ALTER TABLE Orders DROP CONSTRAINT [FK_Customer_Order];
--ALTER TABLE Orders DROP CONSTRAINT [FK_PaymentType];
--ALTER TABLE Product DROP CONSTRAINT [FK_ProductType];
--ALTER TABLE Product DROP CONSTRAINT [FK_Seller];
--ALTER TABLE PaymentType DROP CONSTRAINT [FK_Customer];

--delete from ProductOrder;
--delete from Orders;
--delete from PaymentType;
--delete from Customer;
--delete from Product;
--delete from ProductType;
--delete from EmployeeComputer;
--delete from Computer;
--delete from EmployeeTraining;
--delete from TrainingProgram;
--delete from Employee;
--delete from Department;

--drop table if exists Department;
--drop table if exists TrainingProgram;
--drop table if exists EmployeeTraining;
--drop table if exists Computer;
--drop table if exists EmployeeComputer;
--drop table if exists ProductType;
--drop table if exists Product;
--drop table if exists Customer;
--drop table if exists PaymentType;
--drop table if exists Orders;
--drop table if exists ProductOrder;
--drop table if exists Employee;
	
CREATE TABLE Department (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
DeptName	varchar(80) NOT NULL,
ExpenseBudget integer
);
					
Insert into Department 
(DeptName, ExpenseBudget)
select 'Human Resources',
400000;
		
Insert into Department
(DeptName, ExpenseBudget)
select 'Information Technology',
342000;

Insert into Department
(DeptName, ExpenseBudget)
select 'Marketing',
538000;

Insert into Department
(DeptName, ExpenseBudget)
select 'Accounting',
619000;

CREATE TABLE Computer (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
PurchaseDate	DATE NOT NULL,
Manufacturer	varchar(80) not null,
Make varchar(80) not null,
DecommissionDate	DATE,
Condition	varchar(80)
);
					
Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2018/07/01', 'Apple', 'MacBook Pro', null, 'new';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2018/08/02', 'Apple', 'MacBook Pro', null, 'new';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2018/09/03', 'Apple', 'MacBook Pro', null, 'new';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2016/11/23', 'Apple', 'MacBook Pro', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2016/11/23', 'Apple', 'MacBook Pro', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2016/11/23', 'Apple', 'MacBook Pro', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2016/11/23', 'HP', 'Spectre Laptop', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'HP', 'Spectre Laptop', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'HP', 'Spectre Laptop', null, 'good';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'Dell', 'Inspiron', null, 'fair';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'Dell', 'Inspiron', null, 'fair';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'Dell', 'Inspiron', null, 'fair';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'Dell', 'Inspiron', '2018/04/30', 'decommissioned';

Insert into Computer
(PurchaseDate, Manufacturer, Make, DecommissionDate, Condition)
select '2017/04/15', 'HP', 'Spectre Laptop', '2018/04/23', 'decommissioned';
	
CREATE TABLE Employee (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
FirstName	varchar(80) NOT NULL,
LastName	varchar(80) NOT NULL,
StartDate DATE NOT NULL,
EndDate DATE,
IsSupervisor	bit,
DepartmentId integer NOT NULL,
IsActive	bit
Constraint FK_Department FOREIGN KEY(DepartmentId) REFERENCES Department(Id)
);
					
Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'John',
'Doe',
'2010/01/20',
null,
1,
d.Id,
1
from Department d
where d.DeptName = 'Marketing'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Jane',
'Smith',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Human Resources'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select'Brittany',
'Jefferson',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Information Technology'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Frank',
'Willson',
'2010/01/20',
null,
1,
d.Id,
1
from Department d
where d.DeptName = 'Accounting'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Jack',
'Johnson',
'2010/01/20',
null,
0,
d.Id,
0
from Department d
where d.DeptName = 'Marketing'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'John',
'Bob',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Accounting'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Iron',
'Man',
'2010/01/20',
null,
1,
d.Id,
1
from Department d
where d.DeptName = 'Human Resources'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Joe',
'Black',
'2010/01/20',
null,
0,
d.Id,
0
from Department d
where d.DeptName = 'Marketing'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Ben',
'Button',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Accounting'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Lady',
'Gaga',
'2010/01/20',
null,
1,
d.Id,
1
from Department d
where d.DeptName = 'Marketing'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Mister',
'Smith',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Accounting'
;

Insert into Employee
(FirstName, LastName, StartDate, EndDate, IsSupervisor, DepartmentId, IsActive)
select 'Ryan',
'Reynolds',
'2010/01/20',
null,
0,
d.Id,
1
from Department d
where d.DeptName = 'Marketing'
;
					
CREATE TABLE TrainingProgram (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
ProgName	varchar(80) NOT NULL,
StartDate	DATE NOT NULL,
EndDate	DATE not null,
ProgDesc varchar(80) NOT NULL,
MaxAttendees integer not null
);

Insert into TrainingProgram
(ProgName, StartDate, EndDate, ProgDesc, MaxAttendees)
select 'New Hire', '2018/09/19', '2019/12/18', 'Onboarding', 20
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, ProgDesc, MaxAttendees)
select 'Sales', '2018/10/11', '2019/01/18', 'Learning how to sell', 8
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, ProgDesc, MaxAttendees)
select 'Six Sigma', '2018/11/11', '2019/02/10', 'Learn what Six Sigma is', 15
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, ProgDesc, MaxAttendees)
select 'Diversity', '2018/12/11', '2019/04/12', 'Learning to love everyone', 50
;
					
CREATE TABLE EmployeeTraining (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
EmployeeId	integer NOT NULL,
TrainingProgramId	integer NOT NULL,
Constraint FK_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
Constraint FK_TrainingProgram FOREIGN KEY(TrainingProgramId) REFERENCES TrainingProgram(Id)
);

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Mister' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Lady' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Ben' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Ryan' and tp.ProgName = 'New Hire'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Joe' and tp.ProgName = 'New Hire'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Iron' and tp.ProgName = 'Sales'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'John' and tp.ProgName = 'Six Sigma'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Jack' and tp.ProgName = 'Six Sigma'
;
					

					
CREATE TABLE EmployeeComputer (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
EmployeeId	integer NOT NULL,
ComputerId	integer NOT NULL,
DateAssigned		DATE not null,
DateTurnedIn		DATE,
Constraint FK_EmployeeComputer_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
Constraint FK_EmployeeComputer_Computer FOREIGN KEY(ComputerId) REFERENCES Computer(Id)
);
					
Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 1, 1, c.PurchaseDate, null
from Computer c where c.Id = 1;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 2, 2, c.PurchaseDate, null
from Computer c where c.Id = 2;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 3, 3, c.PurchaseDate, null
from Computer c where c.Id = 3;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 4, 4, c.PurchaseDate, null
from Computer c where c.Id = 4;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 5, 5, c.PurchaseDate, null
from Computer c where c.Id = 5;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 6, 6, c.PurchaseDate, null
from Computer c where c.Id = 6;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 7, 7, c.PurchaseDate, null
from Computer c where c.Id = 7;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 8, 8, c.PurchaseDate, null
from Computer c where c.Id = 8;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 9, 9, c.PurchaseDate, null
from Computer c where c.Id = 9;

Create table Customer (
Id		integer not null primary key IDENTITY,
FirstName		varchar(80) not null,
LastName		varchar(80) not null,
AccountCreated		DATE not null,
LastLogin		DATE not null
);

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'April', 'Watson', '2018/01/11', '2018/09/27';

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Larry', 'King', '2018/01/11', '2018/09/27';

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Kenya', 'Stevens', '2018/01/11', '2018/09/27';

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Kenneth', 'Burnett', '2018/01/11', '2018/09/27';
					
Create table ProductType (
Id	integer not null primary key IDENTITY,
Name	varchar(80) not null
);

Insert into ProductType
(Name)
select 'Toys';

Insert into ProductType 
(Name)
select 'Home & Garden';

Insert into ProductType
(Name) 
select 'Health & Beauty';

Insert into ProductType
(Name) 
select 'Food';

CREATE TABLE Product (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
ProductTypeId	integer NOT NULL,
SellerId	integer not null,	
Price	integer NOT NULL,
Title		varchar(80) not null,
ProdDesc		varchar(80) not null,
Quantity		integer not null,
Constraint FK_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
Constraint FK_Seller Foreign Key(SellerId) References Customer(Id)
);
					
Insert into Product
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, c.Id, 5, 'Teddy Bear', 'fluffy & cuddly', 582
from ProductType pt, Customer c where pt.Name = 'Toys' and c.Id = 1;

Insert into Product
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, c.Id, 3, 'Dove Beauty Bar', 'gentle cleanser for skin', 1980      
from ProductType pt,Customer c  where pt.Name = 'Health & Beauty' AND c.Id = 2;

Insert into Product 
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity)
select pt.Id, c.Id, 8, 'Pantene Gold Series Shampoo', 'hair nourishment', 750
from ProductType pt, Customer c where pt.Name = 'Health & Beauty' AND c.Id = 2;
					
Create table PaymentType (
Id		integer not null primary key IDENTITY,
AccountNo		integer not null,
AccType		varchar(80) not null,
Nickname		varchar(80) not null,
IsActive bit not null,
CustomerId	integer not null,
Constraint FK_Customer foreign key(CustomerId) references Customer(Id)
);

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 10000, 'CC', 'Larry Visa', 1, c.Id
from Customer c where c.FirstName = 'Larry' and c.LastName = 'King';

insert into PaymentType
(AccountNo, AccType, Nickname, IsActive, CustomerId) 
select 20000, 'CC', 'Kenya Visa', 1, c.Id
from Customer c where c.FirstName = 'Kenya' and c.LastName = 'Stevens';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 30000, 'CC', 'Kenya MasterCard', 1, c.Id
from Customer c where c.FirstName = 'Kenya' and c.LastName = 'Stevens';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 40000, 'CC', 'Ken Discover', 1, c.Id
from Customer c where c.FirstName = 'Kenneth' and c.LastName = 'Burnett';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 50000, 'CC', 'April Visa', 1, c.Id
from Customer c where c.FirstName = 'April' and c.LastName = 'Watson';

insert into PaymentType
(AccountNo, AccType, Nickname, IsActive, CustomerId) 
select 60000, 'CC', 'April MasterCard', 1, c.Id
from Customer c where c.FirstName = 'April' and c.LastName = 'Watson';

Create table Orders (
Id		integer not null primary key IDENTITY,
OrderDate		DATE not null,
CustomerId 		integer not null,
PaymentTypeId	integer ,
Constraint FK_Customer_Order foreign key(CustomerId) references Customer(Id),
Constraint FK_PaymentType foreign key(PaymentTypeId) references PaymentType(Id)
);

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '2018/01/11', c.Id, pt.Id
from Customer c, PaymentType pt where pt.Nickname = 'Kenya Visa' and c.Id = pt.CustomerId;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '2018/01/11', c.Id, pt.Id
from Customer c, PaymentType pt where pt.Nickname = 'April Visa' and c.Id = pt.CustomerId;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '2018/01/11', 4, null;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '2018/01/11', 4, null;


Create table ProductOrder (
Id	integer not null primary key IDENTITY,
ProductId	integer not null,
OrderId		integer not null,
Constraint FK_Product foreign key(ProductId) references Product(Id),
Constraint FK_Orders foreign key(OrderId) references Orders(Id)
);

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, o.Id
from Product p, Orders o
where p.Title = 'Dove Beauty Bar' AND o.Id = 1;

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, o.id
from Product p, Orders o
where p.Title = 'Pantene Gold Series Shampoo' AND o.Id = 2;

