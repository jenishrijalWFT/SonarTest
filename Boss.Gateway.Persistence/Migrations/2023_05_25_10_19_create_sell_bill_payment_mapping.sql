CREATE TABLE sell_bill_payment_mapping
(
    id uuid PRIMARY KEY NOT NULL,
    sell_bill_id uuid REFERENCES sell_bills(id),
    sell_bill_payment uuid REFERENCES sell_bill_payment(id),
    is_fully_paid boolean NOT NULL
)