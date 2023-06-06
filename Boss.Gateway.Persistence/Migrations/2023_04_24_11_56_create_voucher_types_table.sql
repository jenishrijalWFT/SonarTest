DROP TABLE IF EXISTS voucher_types;
CREATE TABLE voucher_types (
    id UUID PRIMARY KEY,
    type VARCHAR(50) NOT NULL,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP
);