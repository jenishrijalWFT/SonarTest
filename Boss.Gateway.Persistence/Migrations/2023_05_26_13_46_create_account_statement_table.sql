CREATE TABLE
account_statements(
    id serial PRIMARY KEY NOT NULL,
    account_head_id character varying NULL,
    description text,
    jv_transactions_id uuid NULL,
    reference_number text,
    voucher_number text,
    journal_voucher_id uuid NULL,
    debit numeric NULL,
    credit numeric NULL,
    balance numeric NULL
  );