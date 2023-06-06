CREATE TABLE companies (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    symbol VARCHAR(100) NOT NULL,
    email VARCHAR(100)  NULL,
    website VARCHAR(100) NULL,
    instrument_type VARCHAR(100) NOT NULL
);