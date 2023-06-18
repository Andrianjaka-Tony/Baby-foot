create table gamer (
  idGamer 
    int 
      primary key
      identity,
  gamerName
    varchar(50)
      not null,
  money
    float
      default 0
      not null
)
go

create table gameMode (
  idGameMode 
    int
      primary key
      identity,
  gameModeName
    varchar(30)
      not null
)
go

create table match (
  idMatch
    int 
      primary key
      identity,
  idGamer1
    int 
      not null
      references gamer(idGamer),
  idGamer2
    int 
      not null
      references gamer(idGamer),
  idGameMode 
    int
      not null
      references gameMode(idGameMode),
  scoreGamer1
    int
      not null
      default 0,
  scoreGamer2
    int
      not null
      default 0
)
go


create sequence idMatch
start with 1
go


create table cashRegister (
  money
    float
      default 0
      not null,
)
go


insert into cashRegister values (0)
go


insert into gamer 
(gamerName, money)
values
('Rakoto', 1000),
('Rabe', 1500),
('Lita', 800)
go


insert into gameMode 
(gameModeName)
values
('LosePay'),
('Paris'),
('Diff')
go