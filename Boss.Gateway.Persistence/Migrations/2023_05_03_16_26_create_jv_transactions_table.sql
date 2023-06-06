CREATE TABLE jv_transactions
(
    id uuid PRIMARY KEY,
    description text NOT NULL,
    debit decimal  NULL,
    credit decimal  NULL,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP,
    journal_voucher_id uuid NOT NULL REFERENCES journal_vouchers(id),
    account_head_id VARCHAR(50) NOT NULL,
    branch_id uuid NOT NULL REFERENCES branches(id)
)