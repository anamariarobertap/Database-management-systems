ALTER PROCEDURE deadLock2
AS
BEGIN
BEGIN TRANSACTION T10
	UPDATE Curs SET nrCredite = 1 WHERE numeCurs = 'MPP'
	insert into LogTable(accessedTable, usedProcedure, result, time) values ('Curs', 'deadLock2', 'committed', CURRENT_TIMESTAMP)
	save transaction T10
	WAITFOR DELAY '00:00:10'
	UPDATE Student SET  Prenume = 'deadlock1' WHERE Nume = 'Rosian'
	insert into LogTable(accessedTable, usedProcedure, result, time) values ('Student', 'deadLock2', 'committed', CURRENT_TIMESTAMP)
Commit TRANSACTION T10
END

------------------------------------------

ALTER PROCEDURE deadLock1
AS
BEGIN
BEGIN TRANSACTION T9
	UPDATE Student SET  Prenume = 'deadlock2' WHERE Nume = 'Rosian'
	insert into LogTable(accessedTable, usedProcedure, result, time) values ('Student', 'deadLock1', 'committed', CURRENT_TIMESTAMP)
	save transaction T9
	WAITFOR DELAY '00:00:10'
	UPDATE Curs SET nrCredite = 2 WHERE numeCurs = 'MPP'
	insert into LogTable(accessedTable, usedProcedure, result, time) values ('Curs', 'deadLock1', 'committed', CURRENT_TIMESTAMP)
Commit TRANSACTION T9
END

exec deadLock1
exec deadLock2