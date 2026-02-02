-- init.sql for PostgreSQL (all columns nullable)

-- Table: league
CREATE TABLE league (
    league_id SERIAL PRIMARY KEY,
    league_name TEXT,
    league_game TEXT,
    league_public BOOLEAN,
    league_description TEXT
);

-- Table: tournament
CREATE TABLE tournament (
    tournament_id SERIAL PRIMARY KEY,
    tournament_league INT REFERENCES league(league_id),
    tournament_name TEXT,
    tournament_game TEXT,
    tournament_format TEXT,
    tournament_require_deck BOOLEAN,
    tournament_round_num INT,
    tournament_description TEXT,
    tournament_pairing TEXT,
    tournament_calendar DATE,
    tournament_entry_fee DECIMAL(10,2),
    tournament_max_participants INT
);

-- Table: player
CREATE TABLE player (
    player_id SERIAL PRIMARY KEY,
    player_first_name TEXT,
    player_surname TEXT,
    player_dob DATE,
    player_email TEXT,
    player_mobile BIGINT
);

-- Table: staff
CREATE TABLE staff (
    staff_id SERIAL PRIMARY KEY,
    staff_first_name TEXT,
    staff_surname TEXT,
    staff_email TEXT,
    staff_mobile BIGINT,
    staff_role_management BOOLEAN,
    staff_role_head BOOLEAN
);

-- Table: tournament_player
CREATE TABLE tournament_player (
    tp_id SERIAL PRIMARY KEY,
    tp_tournament INT REFERENCES tournament(tournament_id),
    tp_player INT REFERENCES player(player_id),
    tp_position NUMERIC
);

-- Table: pairing
CREATE TABLE pairing (
    pairing_id SERIAL PRIMARY KEY,
    pairing_tp_1 INT REFERENCES tournament_player(tp_id),
    pairing_tp_2 INT REFERENCES tournament_player(tp_id)
);

-- Table: match
CREATE TABLE match (
    match_id SERIAL PRIMARY KEY,
    pairing_id INT REFERENCES pairing(pairing_id),
    match_round_num INT,
    player_1_winner BOOLEAN,
    player_2_winner BOOLEAN
);
