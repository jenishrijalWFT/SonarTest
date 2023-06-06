DROP TABLE IF EXISTS branches;
CREATE TABLE branches (
    id UUID PRIMARY KEY,
    branch_code VARCHAR(50) NOT NULL,
    account_code VARCHAR(50) NOT NULL,
    account_name VARCHAR(50) NOT NUll,
    phone_number VARCHAR(50) NOT NUll,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP
);