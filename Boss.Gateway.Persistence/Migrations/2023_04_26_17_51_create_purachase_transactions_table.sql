CREATE TABLE purchase_bill_transactions(
    id UUID PRIMARY KEY,
    transaction_number TEXT NOT NULL,
    company_name TEXT NOT NULL,
    quantity integer NOT NULL,
    rate decimal NOT NULL,
    commission_rate decimal NOT NULL,
    sebo_commision decimal NOT NULL,
    eff_rate decimal NOT NULL,
    co_quantity integer NOT NULL,
    co_amount decimal NOT NULL,
    purchase_bill_id UUID NOT NULL REFERENCES purchase_bills(id),
    created_at TIMESTAMP NOT NULL
);