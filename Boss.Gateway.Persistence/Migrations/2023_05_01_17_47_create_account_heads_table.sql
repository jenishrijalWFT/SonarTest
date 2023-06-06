DROP TABLE IF EXISTS account_heads;
CREATE TABLE account_heads (
    id uuid PRIMARY KEY,
    account_code VARCHAR(50) NOT NULL,
    account_name VARCHAR(200) NOT NULL,
    client_code VARCHAR(200),
    account_type VARCHAR(100) NOT NULL,
    system_account VARCHAR(50) NOT NULL
);