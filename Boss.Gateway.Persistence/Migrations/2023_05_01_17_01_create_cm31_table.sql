CREATE TABLE cm_31(
    id uuid PRIMARY KEY,
    settlement_id VARCHAR(100) NOT NULL,
    contract_number VARCHAR(100) NOT NULL,
    buyer_cm integer NOT NULL,
    buyer_client VARCHAR(100) NOT NULL,
    seller_cm integer NOT NULL,
    seller_client VARCHAR(100) NOT NULL,
    isin VARCHAR(100) NOT NULL,
    script_name VARCHAR(100) NOT NULL,
    trade_quantity decimal NOT NULL,
    rate decimal NOT NULL,
    shortage_quantity decimal NOT NULL,
    close_out_credit_amount decimal NOT NULL,
    created_at TIMESTAMP NOT NULL,
    cm031_entry_id uuid NOT NULL REFERENCES cm_31_entries(id)
);