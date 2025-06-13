--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3
-- Dumped by pg_dump version 15.3

-- Started on 2025-06-13 17:33:33

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 6 (class 2615 OID 283345)
-- Name: outbox; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA outbox;


ALTER SCHEMA outbox OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 283351)
-- Name: outboxMessage; Type: TABLE; Schema: outbox; Owner: postgres
--

CREATE TABLE outbox."outboxMessage" (
    "outboxId" uuid NOT NULL,
    content text,
    type text NOT NULL,
    created timestamp with time zone NOT NULL,
    processed boolean NOT NULL,
    "processedOn" timestamp with time zone,
    "correlationId" text,
    "traceId" text,
    "spanId" text
);


ALTER TABLE outbox."outboxMessage" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 283340)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 283346)
-- Name: administrators; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.administrators (
    "administratorId" uuid NOT NULL,
    name character varying(100) NOT NULL,
    phone character varying(8) NOT NULL
);


ALTER TABLE public.administrators OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 283363)
-- Name: contracts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.contracts (
    "contractId" uuid NOT NULL,
    "administratorId" uuid NOT NULL,
    "patientId" uuid NOT NULL,
    "transactionType" character varying(20) NOT NULL,
    "transactionStatus" character varying(20) NOT NULL,
    "creationDate" timestamp without time zone NOT NULL,
    "startDate" timestamp without time zone NOT NULL,
    "completedDate" timestamp without time zone,
    "totalCost" numeric(18,2) NOT NULL
);


ALTER TABLE public.contracts OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 283378)
-- Name: deliverydays; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.deliverydays (
    "deliveryDayId" uuid NOT NULL,
    "contractId" uuid NOT NULL,
    date timestamp with time zone NOT NULL,
    street character varying(100) NOT NULL,
    number integer NOT NULL,
    longitude double precision NOT NULL,
    latitude double precision NOT NULL
);


ALTER TABLE public.deliverydays OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 283358)
-- Name: patients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.patients (
    "patientId" uuid NOT NULL,
    name character varying(100) NOT NULL,
    phone character varying(8) NOT NULL
);


ALTER TABLE public.patients OWNER TO postgres;

--
-- TOC entry 3355 (class 0 OID 283351)
-- Dependencies: 217
-- Data for Name: outboxMessage; Type: TABLE DATA; Schema: outbox; Owner: postgres
--

COPY outbox."outboxMessage" ("outboxId", content, type, created, processed, "processedOn", "correlationId", "traceId", "spanId") FROM stdin;
\.


--
-- TOC entry 3353 (class 0 OID 283340)
-- Dependencies: 215
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250606004001_CreateDatabase	9.0.5
\.


--
-- TOC entry 3354 (class 0 OID 283346)
-- Dependencies: 216
-- Data for Name: administrators; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.administrators ("administratorId", name, phone) FROM stdin;
\.


--
-- TOC entry 3357 (class 0 OID 283363)
-- Dependencies: 219
-- Data for Name: contracts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.contracts ("contractId", "administratorId", "patientId", "transactionType", "transactionStatus", "creationDate", "startDate", "completedDate", "totalCost") FROM stdin;
\.


--
-- TOC entry 3358 (class 0 OID 283378)
-- Dependencies: 220
-- Data for Name: deliverydays; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deliverydays ("deliveryDayId", "contractId", date, street, number, longitude, latitude) FROM stdin;
\.


--
-- TOC entry 3356 (class 0 OID 283358)
-- Dependencies: 218
-- Data for Name: patients; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.patients ("patientId", name, phone) FROM stdin;
\.


--
-- TOC entry 3198 (class 2606 OID 283357)
-- Name: outboxMessage PK_outboxMessage; Type: CONSTRAINT; Schema: outbox; Owner: postgres
--

ALTER TABLE ONLY outbox."outboxMessage"
    ADD CONSTRAINT "PK_outboxMessage" PRIMARY KEY ("outboxId");


--
-- TOC entry 3194 (class 2606 OID 283344)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3196 (class 2606 OID 283350)
-- Name: administrators PK_administrators; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.administrators
    ADD CONSTRAINT "PK_administrators" PRIMARY KEY ("administratorId");


--
-- TOC entry 3204 (class 2606 OID 283367)
-- Name: contracts PK_contracts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "PK_contracts" PRIMARY KEY ("contractId");


--
-- TOC entry 3207 (class 2606 OID 283382)
-- Name: deliverydays PK_deliverydays; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverydays
    ADD CONSTRAINT "PK_deliverydays" PRIMARY KEY ("deliveryDayId");


--
-- TOC entry 3200 (class 2606 OID 283362)
-- Name: patients PK_patients; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.patients
    ADD CONSTRAINT "PK_patients" PRIMARY KEY ("patientId");


--
-- TOC entry 3201 (class 1259 OID 283388)
-- Name: IX_contracts_administratorId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_contracts_administratorId" ON public.contracts USING btree ("administratorId");


--
-- TOC entry 3202 (class 1259 OID 283389)
-- Name: IX_contracts_patientId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_contracts_patientId" ON public.contracts USING btree ("patientId");


--
-- TOC entry 3205 (class 1259 OID 283390)
-- Name: IX_deliverydays_contractId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_deliverydays_contractId" ON public.deliverydays USING btree ("contractId");


--
-- TOC entry 3208 (class 2606 OID 283368)
-- Name: contracts FK_contracts_administrators_administratorId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "FK_contracts_administrators_administratorId" FOREIGN KEY ("administratorId") REFERENCES public.administrators("administratorId") ON DELETE CASCADE;


--
-- TOC entry 3209 (class 2606 OID 283373)
-- Name: contracts FK_contracts_patients_patientId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "FK_contracts_patients_patientId" FOREIGN KEY ("patientId") REFERENCES public.patients("patientId") ON DELETE CASCADE;


--
-- TOC entry 3210 (class 2606 OID 283383)
-- Name: deliverydays FK_deliverydays_contracts_contractId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverydays
    ADD CONSTRAINT "FK_deliverydays_contracts_contractId" FOREIGN KEY ("contractId") REFERENCES public.contracts("contractId") ON DELETE CASCADE;


-- Completed on 2025-06-13 17:33:34

--
-- PostgreSQL database dump complete
--

