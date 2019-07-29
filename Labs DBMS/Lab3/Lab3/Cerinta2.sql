create function checkCerintaStud(@nume nvarchar(50), @prenume nvarchar(50))
returns int
as
begin
	declare @ret int
	if (PATINDEX('%[0-9]%', @nume) > 0 or PATINDEX('%[0-9]%',@prenume) > 0)  set @ret = -1
	else set @ret = 1
	return @ret
end
go

create function checkCerintaCurs(@nrCredite int)
returns int
as
begin
	declare @ret int
	if (@nrCredite <= 1 or @nrCredite > 6)  set @ret = -1
	else set @ret = 1
	return @ret
end
go

alter procedure cerinta2
@numeSt nvarchar(50),
@prenumeSt nvarchar(50),
@numeCurs nvarchar(50),
@nrCredite int,
@data date
as
begin
	begin transaction T2
		declare @canAdd int;
		set @canAdd = -1;
		begin try
			if (dbo.checkCerintaStud(@numeSt, @prenumeSt) = 1)
				begin
					set @canAdd = 1;
					insert into Student(Nume, Prenume) values (@numeSt, @prenumeSt)
					insert into LogTable(accessedTable, usedProcedure, result, time) values ('Student', 'cerinta2', 'committed', CURRENT_TIMESTAMP)
					print 'New data was inserted into Student'
					save transaction T2
				end
			if (dbo.checkCerintaCurs(@nrCredite) = 1)
				begin
					set @canAdd = 2;
					insert into Curs(numeCurs, nrCredite) values (@numeCurs, @nrCredite)
					insert into LogTable(accessedTable, usedProcedure, result, time) values ('Curs', 'cerinta2', 'committed', CURRENT_TIMESTAMP)
					print 'New data was inserted into Curs'
				end

			if (@canAdd = 2)
				begin
					declare @idStud int
					set @idStud = (Select idStudent from Student where Nume = @numeSt and Prenume = @prenumeSt)
					declare @idCurs int
					set @idCurs = (Select idCurs from Curs where numeCurs = @numeCurs and nrCredite = @nrCredite) 

					insert into StudentCurs(idStudent, idCurs, ziua) values(@idStud, @idCurs, @data)
				end
		commit transaction T2
	end try
	begin catch
		print 'Data could not be inserted into tables' 
		--rollback transaction T2
		insert into LogTable(accessedTable, usedProcedure, result, time) values ('StudentCurs', 'cerinta2', 'uncommitted', CURRENT_TIMESTAMP)
	end catch
end

exec cerinta2 'TestDoi', 'TestDoi', 'DB', '-5', '10-10-2019'
exec cerinta2 'X', 'Y', 'W', '5', '10-10-2019'

select * from Student
select * from Curs
select * from StudentCurs