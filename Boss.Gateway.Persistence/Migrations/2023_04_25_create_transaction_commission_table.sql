DROP TABLE IF EXISTS transaction_commissions;
CREATE TABLE transaction_commissions(
    id uuid PRIMARY KEY NOT NULL,
    nepse_commission_percentage numeric NOT NULL,
    sebon_commission_percentage numeric NOT NULL,
    sebon_regulatory_percentage numeric NOT NULL,
    broker_commission_percentage numeric NOT NULL,
    dp_charge numeric NOT NULL,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP
);