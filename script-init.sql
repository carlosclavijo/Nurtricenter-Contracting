--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3
-- Dumped by pg_dump version 15.3

-- Started on 2025-03-22 02:05:47

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 214 (class 1259 OID 217803)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 217808)
-- Name: administrators; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.administrators (
    "administratorId" uuid NOT NULL,
    name character varying(100) NOT NULL,
    phone character varying(9) NOT NULL
);


ALTER TABLE public.administrators OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 217818)
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
-- TOC entry 218 (class 1259 OID 217833)
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
-- TOC entry 216 (class 1259 OID 217813)
-- Name: patients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.patients (
    "patientId" uuid NOT NULL,
    name character varying(100) NOT NULL,
    phone character varying(9) NOT NULL
);


ALTER TABLE public.patients OWNER TO postgres;

--
-- TOC entry 3346 (class 0 OID 217803)
-- Dependencies: 214
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20241218123638_CreateDatabase	8.0.8
\.


--
-- TOC entry 3347 (class 0 OID 217808)
-- Dependencies: 215
-- Data for Name: administrators; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.administrators ("administratorId", name, phone) FROM stdin;
20ffdd4a-ac00-45de-b659-c1a2a812034f	Carlos Clavijo	77777777
77cd6bc2-61fb-4c08-a6b4-16d43898997a	Carlos Clavijo	77777777
764c7368-7103-45f7-9722-01b4479b8b59	Carlos	70926048
\.


--
-- TOC entry 3349 (class 0 OID 217818)
-- Dependencies: 217
-- Data for Name: contracts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.contracts ("contractId", "administratorId", "patientId", "transactionType", "transactionStatus", "creationDate", "startDate", "completedDate", "totalCost") FROM stdin;
a66bc74a-4cd3-482a-8836-c9bb9d871bdc	20ffdd4a-ac00-45de-b659-c1a2a812034f	222f885c-e93b-4720-a5b7-b10eece7a35e	FullMonth	Created	2025-02-24 06:58:31.063973	2025-02-24 06:57:50.242	\N	1000.00
\.


--
-- TOC entry 3350 (class 0 OID 217833)
-- Dependencies: 218
-- Data for Name: deliverydays; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deliverydays ("deliveryDayId", "contractId", date, street, number, longitude, latitude) FROM stdin;
003bdc82-847a-46c7-89fa-b9f3beccd906	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-02-28 06:57:50.242+00	Grove Street	10	0	0
042812fb-f4ea-41a7-a2a5-a437024ed64f	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-03 06:57:50.242+00	Grove Street	10	0	0
14808e5b-01f6-4407-b13b-2ec5b5abca87	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-02 06:57:50.242+00	Grove Street	10	0	0
16c32f95-6424-4670-9cf1-00b57ffad8b3	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-10 06:57:50.242+00	Grove Street	10	0	0
18a18e6f-e0e3-49f2-afbb-598c7ea3e5f4	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-14 06:57:50.242+00	Grove Street	10	0	0
219cd148-4cf4-4621-9eac-22bfde5e416e	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-11 06:57:50.242+00	Grove Street	10	0	0
3d9f4702-5d78-44af-a6d3-4c67e25f4ca4	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-22 06:57:50.242+00	Grove Street	10	0	0
3f0f4134-a9f3-4ebc-90f6-ef052e1ae037	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-25 06:57:50.242+00	Grove Street	10	0	0
423ecbc4-6f85-4289-9977-2511ef183283	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-08 06:57:50.242+00	Grove Street	10	0	0
440a2994-1b91-47f5-9760-f81070b980e5	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-12 06:57:50.242+00	Grove Street	10	0	0
55bee715-c40b-40f2-928d-b87cb03bc160	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-05 06:57:50.242+00	Grove Street	10	0	0
6245fe56-624e-411d-b40f-6d697c9e6b75	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-04 06:57:50.242+00	Grove Street	10	0	0
6ce7f0f6-8aa1-4e23-9d70-a82b3faa44e8	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-02-25 06:57:50.242+00	Grove Street	10	0	0
72e469cb-bc00-48da-81b6-ddc422b81c24	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-17 06:57:50.242+00	Grove Street	10	0	0
8bbfb8c9-375e-4bd7-bfc2-7282ef1b492b	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-21 06:57:50.242+00	Grove Street	10	0	0
96246d7c-1bfb-4688-8760-28fa39e99729	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-16 06:57:50.242+00	Grove Street	10	0	0
98011a5d-9eae-4729-bb25-301ef4d12145	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-13 06:57:50.242+00	Grove Street	10	0	0
98298d3c-0229-4f21-85e1-7af966ae9aad	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-01 06:57:50.242+00	Grove Street	10	0	0
a6b301ca-98fc-4708-95da-9a639ddd9ff3	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-15 06:57:50.242+00	Grove Street	10	0	0
aadbd68b-9e4a-4b05-a6a9-3ec0efe4a6f5	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-07 06:57:50.242+00	Grove Street	10	0	0
ae5dbb1b-5ebb-466b-abba-307b10377cab	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-19 06:57:50.242+00	Grove Street	10	0	0
affe08f5-e31e-4511-b792-a720c5f79d21	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-23 06:57:50.242+00	Grove Street	10	0	0
bc19dcfe-4675-4af3-9bde-1ace28708f43	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-02-27 06:57:50.242+00	Grove Street	10	0	0
cb783f23-16e6-4220-8de6-d3f94a718f31	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-18 06:57:50.242+00	Grove Street	10	0	0
d371a102-351f-4ab2-9e97-6eca152b506f	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-06 06:57:50.242+00	Grove Street	10	0	0
e2fbe99f-b7a8-4d98-8460-204da5f9d6d8	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-02-26 06:57:50.242+00	Grove Street	10	0	0
e3053fe3-0f3a-4576-bf31-aafab1a7589d	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-20 06:57:50.242+00	Grove Street	10	0	0
eed93c7e-5c4d-4bab-9141-23e96b1d583c	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-24 06:57:50.242+00	Grove Street	10	0	0
f3ed5977-943c-49cc-a663-e43688a1032f	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-03-09 06:57:50.242+00	Grove Street	10	0	0
f69b4258-d9af-4df7-a7a5-1b9ae8483bae	a66bc74a-4cd3-482a-8836-c9bb9d871bdc	2025-02-24 06:57:50.242+00	Grove Street	10	0	0
\.


--
-- TOC entry 3348 (class 0 OID 217813)
-- Dependencies: 216
-- Data for Name: patients; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.patients ("patientId", name, phone) FROM stdin;
222f885c-e93b-4720-a5b7-b10eece7a35e	Alberto Fernandez	78649575
db354030-79d0-4b23-a475-32779118c76b	Alberto Fernandez	66666666
\.


--
-- TOC entry 3189 (class 2606 OID 217807)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3191 (class 2606 OID 217812)
-- Name: administrators PK_administrators; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.administrators
    ADD CONSTRAINT "PK_administrators" PRIMARY KEY ("administratorId");


--
-- TOC entry 3197 (class 2606 OID 217822)
-- Name: contracts PK_contracts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "PK_contracts" PRIMARY KEY ("contractId");


--
-- TOC entry 3200 (class 2606 OID 217837)
-- Name: deliverydays PK_deliverydays; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverydays
    ADD CONSTRAINT "PK_deliverydays" PRIMARY KEY ("deliveryDayId");


--
-- TOC entry 3193 (class 2606 OID 217817)
-- Name: patients PK_patients; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.patients
    ADD CONSTRAINT "PK_patients" PRIMARY KEY ("patientId");


--
-- TOC entry 3194 (class 1259 OID 217843)
-- Name: IX_contracts_administratorId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_contracts_administratorId" ON public.contracts USING btree ("administratorId");


--
-- TOC entry 3195 (class 1259 OID 217844)
-- Name: IX_contracts_patientId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_contracts_patientId" ON public.contracts USING btree ("patientId");


--
-- TOC entry 3198 (class 1259 OID 217845)
-- Name: IX_deliverydays_contractId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_deliverydays_contractId" ON public.deliverydays USING btree ("contractId");


--
-- TOC entry 3201 (class 2606 OID 217823)
-- Name: contracts FK_contracts_administrators_administratorId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "FK_contracts_administrators_administratorId" FOREIGN KEY ("administratorId") REFERENCES public.administrators("administratorId") ON DELETE CASCADE;


--
-- TOC entry 3202 (class 2606 OID 217828)
-- Name: contracts FK_contracts_patients_patientId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT "FK_contracts_patients_patientId" FOREIGN KEY ("patientId") REFERENCES public.patients("patientId") ON DELETE CASCADE;


--
-- TOC entry 3203 (class 2606 OID 217838)
-- Name: deliverydays FK_deliverydays_contracts_contractId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverydays
    ADD CONSTRAINT "FK_deliverydays_contracts_contractId" FOREIGN KEY ("contractId") REFERENCES public.contracts("contractId") ON DELETE CASCADE;


-- Completed on 2025-03-22 02:05:48

--
-- PostgreSQL database dump complete
--

