DROP TABLE IF EXISTS brokerage_commissions;
CREATE TABLE brokerage_commissions (
    id uuid NOT NULL PRIMARY KEY,
    instrument_type text NOT NULL,
    min_range bigint NOT NULL,
    max_range bigint NOT NULL,
    brokerage_percent numeric NOT NULL
);