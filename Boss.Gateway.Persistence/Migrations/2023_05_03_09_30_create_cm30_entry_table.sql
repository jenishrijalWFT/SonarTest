DROP TABLE IF EXISTS cm_30_entries;
CREATE TABLE cm_30_entries(
    id UUID PRIMARY kEY,
    settlement_id VARCHAR(30) NOT NULL,
    settlement_date_ad VARCHAR(30) NOT NULL,
    settlement_date_bs VARCHAR(30) NOT NULL,
    import_date_ad VARCHAR(30) NOT NULL,
    import_date_bs VARCHAR(30) NULL,
    created_at TIMESTAMP NOT NULL
);