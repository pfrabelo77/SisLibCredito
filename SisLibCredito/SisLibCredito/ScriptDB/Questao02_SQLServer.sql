CREATE DATABASE DBFIN;
GO

USE DBFIN; 
GO

CREATE TABLE tbCliente (
    IdCliente  INT NOT NULL PRIMARY KEY,
    Nome varchar(200) NULL,
    UF char(2) NULL,
    Celular varchar(20) NULL
);

GO

CREATE TABLE tbFinanciamento (
    IdFinanciamento int NOT NULL PRIMARY KEY,
    IdCliente int FOREIGN KEY REFERENCES tbCliente(IdCliente),
	TipoFinanciamento int null,
	ValorTotal DECIMAL(19,4) null,
	DataVencimento DateTime null
);

GO

CREATE TABLE tbParcela (
    IdParcela int NOT NULL PRIMARY KEY,
    IdFinanciamento int FOREIGN KEY REFERENCES tbFinanciamento(IdFinanciamento),
	NumParcela int null,
	ValorParcela DECIMAL(19,4) null,
	DataVencimento DateTime null,
	DataPagamento DateTime null
);

GO

INSERT INTO tbCliente (IdCliente, Nome, UF, Celular) VALUES (1, 'Paulo Rabelo', 'SP', '11 955663322');
INSERT INTO tbFinanciamento (IdFinanciamento,IdCliente, TipoFinanciamento, ValorTotal, DataVencimento) VALUES (11, 1, 1, 11000, '2021-10-20');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (111, 11, 1, 1000, '2020-06-10',null);
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (112, 11, 2, 1000, '2020-07-10','2020-07-10');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (113, 11, 3, 1000, '2020-07-28',null);
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (114, 11, 4, 1000, '2020-07-01',null);

INSERT INTO tbCliente (IdCliente, Nome, UF, Celular) VALUES (2, 'Sergio Luis', 'SP', '11 988223344');
INSERT INTO tbFinanciamento (IdFinanciamento,IdCliente, TipoFinanciamento, ValorTotal, DataVencimento) VALUES (21, 2, 1, 15000, '2021-10-20');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (211, 21, 1, 500, '2020-05-10',null);
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (212, 21, 2, 500, '2020-06-10',null);
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (213, 21, 3, 500, '2020-06-10','2020-07-10');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (214, 21, 4, 500, '2020-07-10','2020-07-30');

INSERT INTO tbCliente (IdCliente, Nome, UF, Celular) VALUES (3, 'Hugo Almeida', 'SP', '11 933334444');
INSERT INTO tbFinanciamento (IdFinanciamento,IdCliente, TipoFinanciamento, ValorTotal, DataVencimento) VALUES (31, 3, 1, 20000, '2021-01-20');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (311, 31, 1, 1500, '2020-07-31','2020-07-31');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (312, 31, 2, 1500, '2020-08-30','2020-07-31');
INSERT INTO tbParcela (IdParcela,IdFinanciamento, NumParcela, ValorParcela, DataVencimento, DataPagamento) VALUES (313, 31, 3, 1500, '2020-09-30',null);


--Listar todos os clientes do estado de SP que tenham mais de 60% das parcelas pagas.
	Select 
		c.IdCliente, c.Nome, tp.NumTotalParcelas, Count(*) ParcelasPagas, Count(*)*100/tp.NumTotalParcelas Porcentagem
	from 
		tbCliente c 
		inner join tbFinanciamento f on c.IdCliente = f.IdCliente
		inner join tbParcela p on f.IdFinanciamento = p.IdFinanciamento
		inner join (Select Count(*) NumTotalParcelas, IdCliente from tbFinanciamento f inner join tbParcela p on f.IdFinanciamento = p.IdFinanciamento Group by f.IdCliente) tp on c.IdCliente = tp.IdCliente	
	Where 
		c.UF = 'SP' and p.DataPagamento is not null
	Group by 
		c.IdCliente, c.Nome, tp.NumTotalParcelas
	Having
		Count(*)*100/tp.NumTotalParcelas > 60


--Listar os primeiros 4 clientes que tenham alguma parcela com mais de 05 dias atrasadas (Data Vencimento maior que data atual E data pagamento nula)
	Select 
		Top 4 c.IdCliente, c.Nome
	from 
		tbCliente c 
		inner join tbFinanciamento f on c.IdCliente = f.IdCliente
		inner join tbParcela p on f.IdFinanciamento = p.IdFinanciamento
	Where 
		p.DataVencimento + 5 < Getdate() and p.DataPagamento is null
	Group by 
		c.IdCliente, c.Nome


-- Listar todos os clientes que já atrasaram em algum momento duas ou mais parcelas em mais de 10 dias, e que o valor do financiamento seja maior que R$ 10.000,00.
	Select 
		c.IdCliente , 
		c.Nome 
	from 
		tbCliente c 
		inner join tbFinanciamento f on c.IdCliente = f.IdCliente
		inner join tbParcela p on f.IdFinanciamento = p.IdFinanciamento
	Where 
		(DATEDIFF(day,  p.DataVencimento, p.DataPagamento) > 10 or (p.DataVencimento + 10 < Getdate() and p.DataPagamento is null) )and f.ValorTotal > 10000
	Group by 
		c.IdCliente, c.Nome
	Having 
		Count(*) >= 2




