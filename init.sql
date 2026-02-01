-- init.sql for PostgreSQL (all columns nullable)


-- Table: League

CREATE TABLE League (
    LeagueId SERIAL PRIMARY KEY,
    LeagueName TEXT,
    LeagueGame TEXT,
    LeaguePublic BOOLEAN,
    LeagueDescription TEXT
);


-- Table: Tournament

CREATE TABLE Tournament (
    TournamentId SERIAL PRIMARY KEY,
    TournamentLeague INT REFERENCES League(LeagueId),
    TournamentName TEXT,
    TournamentGame TEXT,
    TournamentFormat TEXT,
    TournamentRequireDeck BOOLEAN,
    TournamentRoundNum INT,
    TournamentDescription TEXT,
    TournamentPairing TEXT,
    TournamentCalendar DATE,
    TournamentEntryFee DECIMAL(10,2),
    TournamentMaxParticipants INT
);


-- Table: Player

CREATE TABLE Player (
    PlayerId SERIAL PRIMARY KEY,
    PlayerFirstName TEXT,
    PlayerSurname TEXT,
    PlayerDob DATE,
    PlayerEmail TEXT,
    PlayerMobile BIGINT
);


-- Table: Staff

CREATE TABLE Staff (
    StaffId SERIAL PRIMARY KEY,
    StaffFirstName TEXT,
    StaffSurname TEXT,
    StaffEmail TEXT,
    StaffMobile BIGINT,
    StaffRoleManagement BOOLEAN,
    StaffRoleHead BOOLEAN
);


-- Table: TournamentPlayer

CREATE TABLE TournamentPlayer (
    TpId SERIAL PRIMARY KEY,
    TpTournament INT REFERENCES Tournament(TournamentId),
    TpPlayer INT REFERENCES Player(PlayerId),
    TPPosition NUMERIC
);


-- Table: Pairing

CREATE TABLE Pairing (
    PairingId SERIAL PRIMARY KEY,
    PairingTp1 INT REFERENCES TournamentPlayer(TpId),
    PairingTp2 INT REFERENCES TournamentPlayer(TpId)
);


-- Table: Match

CREATE TABLE Match (
    MatchId SERIAL PRIMARY KEY,
    PairingId INT REFERENCES Pairing(PairingId),
    MatchRoundNum INT,
    Player1Winner BOOLEAN,
    Player2Winner BOOLEAN
);