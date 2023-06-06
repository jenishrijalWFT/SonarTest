
CREATE TABLE cm_05_entries(
    id UUID PRIMARY kEY,
    settlement_id VARCHAR(100) NOT NULL,
    settlement_date_ad VARCHAR(100) NOT NULL,
    settlement_date_bs VARCHAR(100) NOT NULL,
    import_date_ad VARCHAR(100) NOT NULL,
    import_date_bs VARCHAR(100) NULL,
    created_at TIMESTAMP NOT NULL
);