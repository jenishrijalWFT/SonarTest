CREATE TABLE sell_bill_payment
(
    id UUID NOT NULL PRIMARY KEY,
    client_name TEXT,
    ledger_amount DECIMAL NOT NULL,
    bill_number TEXT,
    bill_amount DECIMAL NOT NULL,
    paid_amount DECIMAL NOT NULL,
    pending_amount DECIMAL NOT NULL,
    close_out_amount DECIMAL NOT NULL,
    amount_to_pay DECIMAL NOT NULL,
    cheque_number TEXT,
    cheque_date TEXT,
    paid_branch TEXT,
    date_in_ad TEXT,
    date_in_bs TEXT,
    created_at TIMESTAMP NOT NULL,
    created_by TEXT,
    status INTEGER NOT NULL,
    payment_mode TEXT,
    is_billed BOOLEAN NOT NULL,
    payment_type_id UUID,
    CONSTRAINT fk_sell_bill_payment_sell_bill_payment_type FOREIGN KEY (payment_type_id)
        REFERENCES sell_bill_payment_type (id)
);
