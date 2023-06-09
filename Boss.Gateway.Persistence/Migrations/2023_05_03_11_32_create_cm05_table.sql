CREATE TABLE cm_05(
    id UUID PRIMARY KEY,
    transaction_number VARCHAR(100) NOT NULL,
    date_bs VARCHAR(50) NOT NULL,
    buyer_id integer NOT NULL,
    client_name VARCHAR (100) NOT NULL,
    client_code VARCHAR (100) NOT NULL,
    stock VARCHAR(50) NOT NULL,
    quantity integer NOT NULL,
    rate decimal NOT NULL,
    amount decimal NOT NULL,
    nepse_commission decimal NOT NUll,
    sebon_commission decimal NOT NULL,
    tds decimal NOT NULL,
    adjusted_sell_price decimal NOT NULL,
    adjusted_purchase_price decimal NOT NULL,
    cgt decimal NOT NULL,
    closeout_amount decimal NOT NULL,
    amount_receivable decimal NOT NULL,
    created_at TIMESTAMP NOT NULL,
    cm05_entry_id UUID NOT NULL REFERENCES cm_05_entries(id)
);