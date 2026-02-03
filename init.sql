--
-- PostgreSQL database dump
--

\restrict xo61kOzkdO20vqdXbB5mjflz0CH81aDp0bhmbQdr17L4TqJ3ulCjXUgOsYOm1v5

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
-- Name: league; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.league (
    league_id integer NOT NULL,
    league_name text,
    league_game text,
    league_public boolean,
    league_description text
);


ALTER TABLE public.league OWNER TO postgres;

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

ALTER SEQUENCE public.league_league_id_seq OWNED BY public.league.league_id;


--
-- Name: match; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.match (
    match_id integer NOT NULL,
    pairing_id integer,
    match_round_num integer,
    player_1_winner boolean,
    player_2_winner boolean
);


ALTER TABLE public.match OWNER TO postgres;

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

ALTER SEQUENCE public.match_match_id_seq OWNED BY public.match.match_id;


--
-- Name: pairing; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.pairing (
    pairing_id integer NOT NULL,
    pairing_tp_1 integer,
    pairing_tp_2 integer
);


ALTER TABLE public.pairing OWNER TO postgres;

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

ALTER SEQUENCE public.pairing_pairing_id_seq OWNED BY public.pairing.pairing_id;


--
-- Name: player; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.player (
    player_id integer NOT NULL,
    player_first_name text,
    player_surname text,
    player_dob date,
    player_email text,
    player_mobile bigint
);


ALTER TABLE public.player OWNER TO postgres;

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

ALTER SEQUENCE public.player_player_id_seq OWNED BY public.player.player_id;


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
-- Name: tournament; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tournament (
    tournament_id integer NOT NULL,
    tournament_league integer,
    tournament_name text,
    tournament_game text,
    tournament_format text,
    tournament_require_deck boolean,
    tournament_round_num integer,
    tournament_description text,
    tournament_pairing text,
    tournament_calendar date,
    tournament_entry_fee numeric(10,2),
    tournament_max_participants integer
);


ALTER TABLE public.tournament OWNER TO postgres;

--
-- Name: tournament_player; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tournament_player (
    tp_id integer NOT NULL,
    tp_tournament integer,
    tp_player integer,
    tp_position numeric
);


ALTER TABLE public.tournament_player OWNER TO postgres;

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

ALTER SEQUENCE public.tournament_player_tp_id_seq OWNED BY public.tournament_player.tp_id;


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

ALTER SEQUENCE public.tournament_tournament_id_seq OWNED BY public.tournament.tournament_id;


--
-- Name: league league_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.league ALTER COLUMN league_id SET DEFAULT nextval('public.league_league_id_seq'::regclass);


--
-- Name: match match_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.match ALTER COLUMN match_id SET DEFAULT nextval('public.match_match_id_seq'::regclass);


--
-- Name: pairing pairing_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairing ALTER COLUMN pairing_id SET DEFAULT nextval('public.pairing_pairing_id_seq'::regclass);


--
-- Name: player player_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.player ALTER COLUMN player_id SET DEFAULT nextval('public.player_player_id_seq'::regclass);


--
-- Name: staff staff_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff ALTER COLUMN staff_id SET DEFAULT nextval('public.staff_staff_id_seq'::regclass);


--
-- Name: tournament tournament_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament ALTER COLUMN tournament_id SET DEFAULT nextval('public.tournament_tournament_id_seq'::regclass);


--
-- Name: tournament_player tp_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_player ALTER COLUMN tp_id SET DEFAULT nextval('public.tournament_player_tp_id_seq'::regclass);


--
-- Data for Name: league; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.league (league_id, league_name, league_game, league_public, league_description) FROM stdin;
\.


--
-- Data for Name: match; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.match (match_id, pairing_id, match_round_num, player_1_winner, player_2_winner) FROM stdin;
\.


--
-- Data for Name: pairing; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.pairing (pairing_id, pairing_tp_1, pairing_tp_2) FROM stdin;
\.


--
-- Data for Name: player; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.player (player_id, player_first_name, player_surname, player_dob, player_email, player_mobile) FROM stdin;
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
-- Data for Name: tournament; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tournament (tournament_id, tournament_league, tournament_name, tournament_game, tournament_format, tournament_require_deck, tournament_round_num, tournament_description, tournament_pairing, tournament_calendar, tournament_entry_fee, tournament_max_participants) FROM stdin;
\.


--
-- Data for Name: tournament_player; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tournament_player (tp_id, tp_tournament, tp_player, tp_position) FROM stdin;
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
-- Name: league league_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.league
    ADD CONSTRAINT league_pkey PRIMARY KEY (league_id);


--
-- Name: match match_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.match
    ADD CONSTRAINT match_pkey PRIMARY KEY (match_id);


--
-- Name: pairing pairing_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairing
    ADD CONSTRAINT pairing_pkey PRIMARY KEY (pairing_id);


--
-- Name: player player_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.player
    ADD CONSTRAINT player_pkey PRIMARY KEY (player_id);


--
-- Name: staff staff_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_pkey PRIMARY KEY (staff_id);


--
-- Name: tournament tournament_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament
    ADD CONSTRAINT tournament_pkey PRIMARY KEY (tournament_id);


--
-- Name: tournament_player tournament_player_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_player
    ADD CONSTRAINT tournament_player_pkey PRIMARY KEY (tp_id);


--
-- Name: match match_pairing_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.match
    ADD CONSTRAINT match_pairing_id_fkey FOREIGN KEY (pairing_id) REFERENCES public.pairing(pairing_id);


--
-- Name: pairing pairing_pairing_tp_1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairing
    ADD CONSTRAINT pairing_pairing_tp_1_fkey FOREIGN KEY (pairing_tp_1) REFERENCES public.tournament_player(tp_id);


--
-- Name: pairing pairing_pairing_tp_2_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pairing
    ADD CONSTRAINT pairing_pairing_tp_2_fkey FOREIGN KEY (pairing_tp_2) REFERENCES public.tournament_player(tp_id);


--
-- Name: tournament_player tournament_player_tp_player_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_player
    ADD CONSTRAINT tournament_player_tp_player_fkey FOREIGN KEY (tp_player) REFERENCES public.player(player_id);


--
-- Name: tournament_player tournament_player_tp_tournament_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament_player
    ADD CONSTRAINT tournament_player_tp_tournament_fkey FOREIGN KEY (tp_tournament) REFERENCES public.tournament(tournament_id);


--
-- Name: tournament tournament_tournament_league_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournament
    ADD CONSTRAINT tournament_tournament_league_fkey FOREIGN KEY (tournament_league) REFERENCES public.league(league_id);


--
-- PostgreSQL database dump complete
--

\unrestrict xo61kOzkdO20vqdXbB5mjflz0CH81aDp0bhmbQdr17L4TqJ3ulCjXUgOsYOm1v5

