DROP TABLE IF EXISTS sectors;
CREATE TABLE sectors (
    id UUID PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    code VARCHAR(50) NOT NULL,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP
);