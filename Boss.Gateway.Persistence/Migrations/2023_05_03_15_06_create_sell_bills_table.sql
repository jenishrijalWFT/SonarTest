DROP TABLE IF EXISTS sell_bills;
CREATE TABLE sell_bills(
    id UUID PRIMARY KEY,
    client_code VARCHAR(100) NOT NULL,
    client_name VARCHAR(100) NOT NULL,
    bill_number VARCHAR(50) NOT NULL,
    bill_date VARCHAR(50) NOT NULL,
    broker_commission decimal NOT NUll,
    nepse_commission decimal NOT NULL,
    sebo_commission decimal NOT NULL,
    sebo_regulatory_fee decimal NOT NULL,
    clearance_date VARCHAR(50) NOT NULL,
    dp_amount decimal NOT NULL,
    floorsheet_id UUID NOT NULL,
    created_at TIMESTAMP NOT NULL
);