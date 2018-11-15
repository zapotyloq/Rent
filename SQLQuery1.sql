-- Здания 100 записей
-- Помещения 300 записей
-- Организации 100 записей
-- Аренда помещений 10000 записей
-- Счета-фактуры 30000 записей


--Создание базы данных
create database RentOfPremisesByOrganizations
go

alter database RentOfPremisesByOrganizations set recovery simple
go

use RentOfPremisesByOrganizations

--Создание таблиц
create table dbo.Buildings (Id int identity(1,1) not null, Name nvarchar(50), Mail nvarchar(50), NumberOfStoreys int, Characteristic nvarchar(200), primary key (Id));
create table dbo.Premises (Id int identity(1,1) not null, Area int, BuildingNumber int, FloorPlan nvarchar(200), Photos nvarchar(200), primary key(Id));
create table dbo.Organizations (Id int identity(1,1) not null, Name nvarchar(50), Mail nvarchar(50), primary key(Id));
create table dbo.Rents (Id int identity(1,1) not null, PremiseId int, OrganizationId int, ArrivalDate date, DateOfDeparture date, primary key(Id));
create table dbo.Invoices (Id int identity(1,1) not null, DateOfPayment date, RentId int, Mounth int, Total float, Bailee nvarchar(50), primary  key(Id));

--Добавление связей между таблицами
alter table dbo.Premises with check add constraint Fk_Premises_Buildings foreign key(BuildingNumber)
references dbo.Buildings (Id) on delete cascade
go
alter table dbo.Rents with check add constraint Fk_Rents_Premises foreign key(PremiseId)
references dbo.Premises (Id) on delete cascade
go
alter table dbo.Rents with check add constraint Fk_Rents_Organizations foreign key(OrganizationId)
references dbo.Organizations (Id) on delete cascade
go
alter table dbo.Invoices with check add constraint Fk_Invoices_Rents foreign key(RentId)
references dbo.Rents (Id) on delete cascade
go
--
set nocount on

declare @Symbol char(52) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
        @i int,
		@Position int,
		@RowCount int,
		@Length int,
		@BuildingName nvarchar(50),
		@BuildingMail nvarchar(50),
		@BuildingCharasteristic nvarchar(200),
		@FloorPlan nvarchar(200),
		@Photos nvarchar(200),
		@OrganizationName nvarchar(50),
		@OrganizationMail nvarchar(50),
		@dIn date,
		@dOut date,
		@dPay date,
		@bailee nvarchar(50),
		@NumberBuildings int,
		@NumberPremises int,
		@NumberOrganizations int,
		@NumberRents int,
		@NumberInvoices int

set @NumberBuildings = 20
set @NumberPremises = 60
set @NumberOrganizations = 20
set @NumberRents = 2000
set @NumberInvoices = 6000

begin tran

--Здания 100
 select @i = 0 from dbo.Buildings with(tablockx) where 1 = 0
 set @RowCount = 1
 while @RowCount <= @NumberBuildings
  begin
   set @BuildingName = ''
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
     set @Position = rand() * 52
     set @BuildingName = @BuildingName + substring(@Symbol, @Position, 1)
     set @i = @i + 1;
    end
   set @BuildingMail = ''
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
     set @Position = rand() * 52
     set @BuildingMail = @BuildingMail + substring(@Symbol, @Position, 1)
     set @i = @i + 1;
    end
   set @BuildingCharasteristic = ''
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
     set @Position = rand() * 52
     set @BuildingCharasteristic = @BuildingCharasteristic + substring(@Symbol, @Position, 1)
     set @i = @i + 1;
    end
   insert into dbo.Buildings (Name, Mail, NumberOfStoreys, Characteristic) select @BuildingName, @BuildingMail, (1 + rand() * 31), @BuildingCharasteristic
   set @RowCount += 1
  end

 --помещения 300
 select @i = 0 from Premises with(tablockx) where 1 = 0
 set @RowCount = 1
 while @RowCount <= @NumberPremises
  begin
   set @FloorPlan = ''
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
	 set @Position = rand() * 52
	 set @FloorPlan = @FloorPlan + substring(@Symbol, @Position, 1)
	 set @i = @i + 1
	end
  set @Photos = ''
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
	 set @Position = rand() * 52
	 set @Photos = @Photos + substring(@Symbol, @Position, 1)
	 set @i = @i + 1
	end
   insert into dbo.Premises (Area, BuildingNumber, FloorPlan, Photos)
   select
   10 + rand() * 490,
   cast((1 + rand() * (@NumberBuildings - 1)) as int),
   @FloorPlan,
   @Photos
   set @RowCount += 1
  end

--организации 100
 select @i = 0 from dbo.Organizations with(tablockx) where 1 = 0
 set @RowCount = 1
 while @RowCount <= @NumberOrganizations
  begin
   set @OrganizationName = ''
   set @Length = 3 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
	 set @Position = rand() * 52
	 set @OrganizationName = @OrganizationName + substring(@Symbol, @Position, 1)
	 set @i = @i + 1
	end
   set @OrganizationMail = ''
   set @Length = 3 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
	 set @Position = rand() * 52
	 set @OrganizationMail = @OrganizationMail + substring(@Symbol, @Position, 1)
	 set @i = @i + 1
	end
   insert into dbo.Organizations( Name, Mail) select @OrganizationName, @OrganizationMail
   set @RowCount += 1
  end

--инфрмация о аренде 10000
 select @i = 0 from dbo.Rents with(tablockx) where 1 = 0
 set @RowCount = 1
 while @RowCount <= @NumberRents
  begin
   set @dIn = dateadd(day,-RAND()*15000,GETDATE())
   set @dOut = dateadd(DAY,rand()*3600,GETDATE())
   insert into dbo.Rents(PremiseId, OrganizationId, ArrivalDate, DateOfDeparture)
   select
   CAST( (1+RAND()*(@NumberPremises-1)) as int),
   CAST( (1+RAND()*(@NumberOrganizations-1)) as int),
   @dIn,
   @dOut
   set @RowCount += 1
  end
 
--счета фактуры 30000
select @i=0 FROM dbo.Invoices WITH (TABLOCKX) WHERE 1=0
 set @RowCount = 1
 while @RowCount <= @NumberInvoices
  begin
   set @dPay = dateadd(day,-RAND()*15000,GETDATE())
   set @bailee = '';
   set @Length = 5 + rand() * 20
   set @i = 1
   while @i <= @Length
    begin
	 set @Position = rand() * 52
	 set @bailee = @bailee + substring(@Symbol, @Position, 1)
	 set @i = @i + 1
	end
   insert into dbo.Invoices(DateOfPayment, RentId, Mounth, Total, Bailee)
   select
   @dPay,
   CAST( (1+RAND()*(@NumberRents-1)) as int),
   1 + rand() * 11,
   (100 + rand() * 99900) as int,
   @bailee
   set @RowCount += 1
  end

commit tran
go

--Представление
create view [dbo].[View_FullInffoRentOfPremises]

as
select Rents.Id, PremiseId, Buildings.Name as BuildingName, Organizations.Name as OrganizationName, ArrivalDate, DateOfDeparture
                    from Rents inner join Organizations
                    on Rents.OrganizationId = Organizations.Id
                    inner join Premises
                    on Rents.PremiseId = Premises.Id
                    inner join Buildings
                    on Premises.BuildingNumber = Buildings.Id
go
--
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Хранимые процедуры
create proc my_proc1
@building_name nvarchar(30),
@d0 date,
@d date
as
begin
select Buildings.Name as BuildingName, Organizations.Name, Organizations.Mail, Premises.Id as PremisesId, Invoices.DateOfPayment, Invoices.Total
from Rents inner join Premises on Rents.PremiseId = Premises.Id
inner join Organizations on Rents.OrganizationId = Organizations.Id
inner join Buildings on Premises.BuildingNumber = Buildings.Id
inner join Invoices on Rents.Id = Invoices.RentId
where Buildings.Name = @building_name and
Invoices.DateOfPayment >= @d0 and Invoices.DateOfPayment <= @d
end;
go

create proc my_proc2
@building_name nvarchar(30),
@d0 date,
@d date
as
begin
select Buildings.Name, Organizations.Id, Organizations.Name, Organizations.Mail, Premises.Id as PremisesId, ArrivalDate, DateOfDeparture
from Rents inner join Premises on Rents.PremiseId = Premises.Id
inner join Organizations on Rents.OrganizationId = Organizations.Id
inner join Buildings on Premises.BuildingNumber = Buildings.Id
where Buildings.Name = @building_name and
ArrivalDate >= @d0 and DateOfDeparture <= @d
end;
go

create proc my_proc3
@d date
as
begin
select Organizations.Id, Organizations.Name, Organizations.Mail, Premises.Id as PremisesId, Buildings.Name, ArrivalDate, DateOfDeparture
from Rents inner join Premises on Rents.PremiseId = Premises.Id
inner join Organizations on Rents.OrganizationId = Organizations.Id
inner join Buildings on Premises.BuildingNumber = Buildings.Id
inner join Invoices on Rents.Id = Invoices.RentId
where DateOfDeparture >= @d and Invoices.DateOfPayment <= @d
end;
go