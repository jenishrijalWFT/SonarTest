CREATE TABLE journal_vouchers (
    id uuid PRIMARY KEY,
    client_name VARCHAR(100) NOT NULL,
    client_code VARCHAR (100) NOT NULL,
    voucher_date_ad VARCHAR(50) NOT NULL,
    voucher_date_bs VARCHAR(50) NOT NULL,
    voucher_no VARCHAR(50) NOT NULL,
    reference_no VARCHAR(50) NOT NULL,
    amount decimal NOT NULL,
    approved_status VARCHAR(50) NOT NULL,
    created_at TIMESTAMP NOT NULL,
    type_id uuid NOT NULL REFERENCES voucher_types(id)
);