DROP TABLE IF EXISTS tbl_player CASCADE;
CREATE TABLE tbl_player (
    player_name varchar(50) primary key,
    is_active boolean,
    modifydate timestamp
);
CREATE INDEX idx_tbl_player ON tbl_player (
    is_active
);

INSERT INTO tbl_player VALUES ('Naruto', 'true', NOW());
INSERT INTO tbl_player VALUES ('Doraemon', 'true', NOW());
INSERT INTO tbl_player VALUES ('Zoro', 'true', NOW());
INSERT INTO tbl_player VALUES ('Nami', 'true', NOW());

DROP TABLE IF EXISTS tbl_tranaction;
CREATE TABLE tbl_tranaction (
   	id integer generated always as identity primary key,
    coin_code varchar(50),
    coin_name varchar(255),
    coin_unit integer,
    player_name varchar(50) references tbl_player(player_name),
    modifydate timestamp
);
CREATE INDEX idx_tbl_tranaction ON tbl_tranaction (
    player_name
);
