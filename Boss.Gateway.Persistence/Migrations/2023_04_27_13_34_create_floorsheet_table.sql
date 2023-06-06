CREATE TABLE floorsheets (
    id uuid PRIMARY KEY,
    fiscal_year VARCHAR(50) NOT NULL,
    floorsheet_date_ad VARCHAR(50) NOT NULL,
    floorsheet_date_bs VARCHAR(50) NOT NULL,
    import_date_ad VARCHAR(50) NOT NULL,
    import_date_bs VARCHAR(50) NOT NULL,
    amount DECIMAL NOT NULL,
    stock_commission DECIMAL NOT NULL,
    bank_deposit DECIMAL NOT NULL,
    created_at TIMESTAMP NOT NULL
);