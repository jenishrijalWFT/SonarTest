CREATE TABLE advance_against_sell
(
    id UUID PRIMARY KEY,
    transaction_no VARCHAR(30),
    advance_amount DECIMAL,
    advance_payment_id UUID REFERENCES advance_payments(id)
);
