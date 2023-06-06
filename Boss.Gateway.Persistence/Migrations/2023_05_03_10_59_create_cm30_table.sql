DROP TABLE IF EXISTS cm_30;
CREATE TABLE cm_30(
    id UUID PRIMARY kEY,
    settlement_id VARCHAR(50) NOT NULL,
    contract_number VARCHAR(50) NOT NULL,
    seller_client VARCHAR(50) NOT NULL,
    buyer_cm INTEGER NOT NULL,
    buyer_client VARCHAR(50) NOT NULL,
    script VARCHAR(50) NOT NULL,
    trade_quantity DECIMAL NOT NULL,
    rate DECIMAL NOT NULL,
    shortage_quantity DECIMAL NOT NULL,
    closeout_debit_amount DECIMAL NOT NULL,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP,
    cm30_entry_id UUID NOT NULL REFERENCES cm_30_entries(id)
);