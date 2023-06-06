CREATE TABLE cm_31_entries(
    id uuid PRIMARY KEY,
    settlement_id VARCHAR(100) NOT NULL,
    settlement_date_ad VARCHAR(50) NOT NULL,
    settlement_date_bs VARCHAR(50) NOT NULL,
    imported_at_ad VARCHAR(50) NOT NULL,
    imported_at_bs VARCHAR(50) NOT NULL,
    created_at TIMESTAMP NOT NULL
);