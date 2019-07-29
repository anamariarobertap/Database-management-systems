alter function checkCerinta1(@nrCredite int, @data date)
returns int
as
begin
	declare @ret int
	if(@nrCredite <= 1 or @nrCredite > 6 or @data < '01-01-2010') set @ret = -1
	else set @ret = 1
	return @ret
end
go

alter procedure cerinta1
@numeSt nvarchar(50),
@prenumeSt nvarchar(50),
@numeCurs nvarchar(50),
@nrCredite int,
@data date
as
begin

	begin transaction T1
		begin try
			if (dbo.checkCerinta1(@nrCredite, @data) = 1)
				begin
					insert into Student(Nume, Prenume) values (@numeSt, @prenumeSt)
					insert into LogTable(accessedTable, usedProcedure, result, time) values ('Student', 'cerinta1', 'committed', CURRENT_TIMESTAMP)
					insert into Curs (numeCurs, nrCredite) values (@numeCurs, @nrCredite)
					insert into LogTable(accessedTable, usedProcedure, result, time) values ('Curs', 'cerinta1', 'committed', CURRENT_TIMESTAMP)

					declare @idStud int
					set @idStud = (Select idStudent from Student where Nume = @numeSt and Prenume = @prenumeSt)
					declare @idCurs int
					set @idCurs = (Select idCurs from Curs where numeCurs = @numeCurs and nrCredite = @nrCredite) 

					insert into StudentCurs(idStudent, idCurs, ziua) values(@idStud, @idCurs, @data)

					print 'New data was inserted into StudentCurs'
					insert into LogTable(accessedTable, usedProcedure, result, time) values ('StudentCurs', 'cerinta1', 'committed', CURRENT_TIMESTAMP)
				end
			else insert into LogTable(accessedTable, usedProcedure, result, time) values ('StudentCurs', 'cerinta1', 'uncommitted - Invalid data', CURRENT_TIMESTAMP)
		commit transaction T1
	end try
	begin catch
		print 'Data could not be inserted into StudentCurs' 
		rollback transaction T1
		insert into LogTable(accessedTable, usedProcedure, result, time) values ('StudentCurs', 'cerinta1', 'uncommitted', CURRENT_TIMESTAMP)
	end catch
end

exec cerinta1 'A', 'B', 'DB', '-5', '10-10-2019'
exec cerinta1 'Marc', 'Casian', 'DB', '5', '10-10-2019'

select * from Student
select * from Curs
select * from StudentCurs