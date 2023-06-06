CREATE TABLE sell_floorsheets (
    id uuid PRIMARY KEY,
    contract_no VARCHAR(100) NOT NULL,
    symbol VARCHAR(50) NOT NULL,
    buyer integer NOT NULL,
    seller integer NOT NULL,
    client_name VARCHAR(100) NOT NULL,
    client_code VARCHAR(100) NOT NULL,
    quantity integer NOT NULL,
    rate decimal NOT NULL,
    stock_commission decimal NOT NULL,
    created_at TIMESTAMP NOT NULL,
    floorsheet_id uuid NOT NULL REFERENCES floorsheets(id)
);