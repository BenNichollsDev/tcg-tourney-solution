--
-- PostgreSQL database dump
--

\restrict kGmFPDls79nv4cExGwtD2DPhcDrVyXqIkDdLsMn3ISvyfhCAyK2ioWYni8MG3Wj

-- Dumped from database version 18.1 (Debian 18.1-1.pgdg13+2)
-- Dumped by pg_dump version 18.1 (Debian 18.1-1.pgdg13+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: leagues; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.leagues (
    league_id integer CONSTRAINT league_league_id_not_null NOT NULL,
    league_name text,
    league_game text,
    league_public boolean,
    league_description text
);


ALTER TABLE public.leagues OWNER TO postgres;

--
-- Name: league_league_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.league_league_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.league_league_id_seq OWNER TO postgres;

--
-- Name: league_league_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.league_league_id_seq OWNED BY public.leagues.league_id;


--
-- Name: matches; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.matches (
    match_id integer CONSTRAINT match_match_id_not_null NOT NULL,
    pairing_id integer,
    match_round_num integer,
    player_1_winner boolean,
    player_2_winner boolean
);


ALTER TABLE public.matches OWNER TO postgres;

--
-- Name: match_match_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.match_match_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.match_match_id_seq OWNER TO postgres;

--
-- Name: match_match_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.match_match_id_seq OWNED BY public.matches.match_id;


--
-- Name: pairings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.pairings (
    pairing_id integer CONSTRAINT pairing_pairing_id_not_null NOT NULL,
    pairing_tp_1 integer,
    pairing_tp_2 integer
);


ALTER TABLE public.pairings OWNER TO postgres;

--
-- Name: pairing_pairing_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.pairing_pairing_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.pairing_pairing_id_seq OWNER TO postgres;

--
-- Name: pairing_pairing_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.pairing_pairing_id_seq OWNED BY public.pairings.pairing_id;


--
-- Name: players; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.players (
    player_id integer CONSTRAINT player_player_id_not_null NOT NULL,
    player_first_name text,
    player_surname text,
    player_dob date,
    player_email text,
    player_mobile bigint
);


ALTER TABLE public.players OWNER TO postgres;

--
-- Name: player_player_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.player_player_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.player_player_id_seq OWNER TO postgres;

--
-- Name: player_player_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.player_player_id_seq OWNED BY public.players.player_id;


--
-- Name: staff; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.staff (
    staff_id integer NOT NULL,
    staff_first_name text,
    staff_surname text,
    staff_email text,
    staff_mobile text,
    staff_role_management boolean,
    staff_role_head boolean,
    staff_password text
);


ALTER TABLE public.staff OWNER TO postgres;

--
-- Name: staff_staff_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.staff_staff_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.staff_staff_id_seq OWNER TO postgres;

--
-- Name: staff_staff_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.staff_staff_id_seq OWNED BY public.staff.staff_id;


--
-- Name: tournament_players; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tournament_players (
    tp_id integer CONSTRAINT tournament_player_tp_id_not_null NOT NULL,
    tp_tournament integer,
    tp_player integer,
    tp_position numeric
);


ALTER TABLE public.tournament_players OWNER TO postgres;

--
-- Name: tournament_player_tp_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tournament_player_tp_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tournament_player_tp_id_seq OWNER TO postgres;

--
-- Name: tournament_player_tp_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tournament_player_tp_id_seq OWNED BY public.tournament_players.tp_id;


--
-- Name: tournaments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tournaments (
    tournament_id integer CONSTRAINT tournament_tournament_id_not_null NOT NULL,
    tournament_league integer,
    tournament_name text,
    tournament_game text,
    tournament_format text,
    tournament_require_deck boolean,
    tournament_round_num integer,
    tournament_description text,
    tournament_pairing text,
    tournament_date date,
    tournament_entry_fee numeric(10,2),
    tournament_max_participants integer,
    tournament_time time(0) without time zone
);


ALTER TABLE public.tournaments OWNER TO postgres;

--
-- Name: tournament_tournament_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tournament_tournament_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tournament_tournament_id_seq OWNER TO postgres;

--
-- Name: tournament_tournament_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tournament_tournament_id_seq OWNED BY public.tournaments.tournament_id;


--
-- Name: leagues league_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.leagues ALTER COLUMN league_id SET DEFAULT nextval('public.league_league_id_seq'::regclass);


--
-- Name: matches match_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matches ALTER COLUMN match_id SET DEFAULT nextval('public.match_match_id_seq'::regclass);


--
-- Name: pairings pairing_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairings ALTER COLUMN pairing_id SET DEFAULT nextval('public.pairing_pairing_id_seq'::regclass);


--
-- Name: players player_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.players ALTER COLUMN player_id SET DEFAULT nextval('public.player_player_id_seq'::regclass);


--
-- Name: staff staff_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff ALTER COLUMN staff_id SET DEFAULT nextval('public.staff_staff_id_seq'::regclass);


--
-- Name: tournament_players tp_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_players ALTER COLUMN tp_id SET DEFAULT nextval('public.tournament_player_tp_id_seq'::regclass);


--
-- Name: tournaments tournament_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournaments ALTER COLUMN tournament_id SET DEFAULT nextval('public.tournament_tournament_id_seq'::regclass);


--
-- Data for Name: leagues; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.leagues (league_id, league_name, league_game, league_public, league_description) FROM stdin;
\.


--
-- Data for Name: matches; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.matches (match_id, pairing_id, match_round_num, player_1_winner, player_2_winner) FROM stdin;
\.


--
-- Data for Name: pairings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.pairings (pairing_id, pairing_tp_1, pairing_tp_2) FROM stdin;
\.


--
-- Data for Name: players; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.players (player_id, player_first_name, player_surname, player_dob, player_email, player_mobile) FROM stdin;
\.


--
-- Data for Name: staff; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.staff (staff_id, staff_first_name, staff_surname, staff_email, staff_mobile, staff_role_management, staff_role_head, staff_password) FROM stdin;
1	Gordon	Freeman	17cities@mail.com	08537285555	f	t	123
2	Eli	Vance	blackmesa@mail.com	07798227364	t	f	456
3	Alyx	Vance	ravenholm@mail.com	07738229100	f	f	789
\.


--
-- Data for Name: tournament_players; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tournament_players (tp_id, tp_tournament, tp_player, tp_position) FROM stdin;
\.


--
-- Data for Name: tournaments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tournaments (tournament_id, tournament_league, tournament_name, tournament_game, tournament_format, tournament_require_deck, tournament_round_num, tournament_description, tournament_pairing, tournament_date, tournament_entry_fee, tournament_max_participants, tournament_time) FROM stdin;
\.


--
-- Name: league_league_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.league_league_id_seq', 1, false);


--
-- Name: match_match_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.match_match_id_seq', 1, false);


--
-- Name: pairing_pairing_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.pairing_pairing_id_seq', 1, false);


--
-- Name: player_player_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.player_player_id_seq', 1, false);


--
-- Name: staff_staff_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.staff_staff_id_seq', 3, true);


--
-- Name: tournament_player_tp_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tournament_player_tp_id_seq', 1, false);


--
-- Name: tournament_tournament_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tournament_tournament_id_seq', 1, false);


--
-- Name: leagues league_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.leagues
    ADD CONSTRAINT league_pkey PRIMARY KEY (league_id);


--
-- Name: matches match_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matches
    ADD CONSTRAINT match_pkey PRIMARY KEY (match_id);


--
-- Name: pairings pairing_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairings
    ADD CONSTRAINT pairing_pkey PRIMARY KEY (pairing_id);


--
-- Name: players player_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.players
    ADD CONSTRAINT player_pkey PRIMARY KEY (player_id);


--
-- Name: staff staff_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_pkey PRIMARY KEY (staff_id);


--
-- Name: tournaments tournament_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournaments
    ADD CONSTRAINT tournament_pkey PRIMARY KEY (tournament_id);


--
-- Name: tournament_players tournament_player_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_players
    ADD CONSTRAINT tournament_player_pkey PRIMARY KEY (tp_id);


--
-- Name: matches match_pairing_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matches
    ADD CONSTRAINT match_pairing_id_fkey FOREIGN KEY (pairing_id) REFERENCES public.pairings(pairing_id);


--
-- Name: pairings pairing_pairing_tp_1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairings
    ADD CONSTRAINT pairing_pairing_tp_1_fkey FOREIGN KEY (pairing_tp_1) REFERENCES public.tournament_players(tp_id);


--
-- Name: pairings pairing_pairing_tp_2_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairings
    ADD CONSTRAINT pairing_pairing_tp_2_fkey FOREIGN KEY (pairing_tp_2) REFERENCES public.tournament_players(tp_id);


--
-- Name: tournament_players tournament_player_tp_player_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_players
    ADD CONSTRAINT tournament_player_tp_player_fkey FOREIGN KEY (tp_player) REFERENCES public.players(player_id);


--
-- Name: tournament_players tournament_player_tp_tournament_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_players
    ADD CONSTRAINT tournament_player_tp_tournament_fkey FOREIGN KEY (tp_tournament) REFERENCES public.tournaments(tournament_id);


--
-- Name: tournaments tournament_tournament_league_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournaments
    ADD CONSTRAINT tournament_tournament_league_fkey FOREIGN KEY (tournament_league) REFERENCES public.leagues(league_id);




    ALTER TABLE public.tournaments ALTER COLUMN tournament_league SET DEFAULT NULL;

--
-- PostgreSQL database dump complete
--

\unrestrict kGmFPDls79nv4cExGwtD2DPhcDrVyXqIkDdLsMn3ISvyfhCAyK2ioWYni8MG3Wj

