--jucator are id echipa
CREATE TABLE Echipa (
 id_echipa  INT NOT NULL
 Primary Key (id_echipa)
)

Create table Jucator(
id_jucator int not null,
nume varchar(50),
prenume nvarchar(50),
PRIMARY KEY (id_jucator),
id_echipa int,
Constraint FK_Echipa FOREIGN KEY (id_echipa) REFERENCES Echipa(id_echipa));