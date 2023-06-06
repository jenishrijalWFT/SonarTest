CREATE TABLE sell_bill_transactions(
    id UUID PRIMARY KEY,
    transaction_number VARCHAR(50) NOT NULL,
    company_name VARCHAR(100) ,
    quantity integer NOT NULL,
    rate decimal NOT NULL,
    commission_rate decimal NOT NULL,
    cgt decimal NOT NULL,
    sebo_commission decimal NOT NULL,
    wacc_purchase_price decimal Not NULL,
    effective_rate decimal NOT NULL,
    co_quantity integer NOT NULL,
    co_amount decimal NOT NULL,
    is_billed boolean NOT NULL,
    created_at TIMESTAMP NOT NULL,
    sell_bill_id UUID NOT NULL REFERENCES sell_bills(id)
);