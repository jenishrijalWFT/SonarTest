CREATE TABLE buy_floorsheets (
    id uuid PRIMARY KEY,
    contract_no VARCHAR(80) NOT NULL,
    symbol VARCHAR(50) NOT NULL,
    buyer integer NOT NULL,
    seller integer NOT NULL,
    client_name VARCHAR(80) NOT NULL,
    client_code VARCHAR(80) NOT NULL,
    quantity integer NOT NULL,
    rate decimal NOT NULL,
    stock_commission decimal NOT NULL,
    bank_deposit decimal NOT NULL,
    floorsheet_id uuid NOT NULL REFERENCES floorsheets(id),
    created_at TIMESTAMP NOT NULL
    
);