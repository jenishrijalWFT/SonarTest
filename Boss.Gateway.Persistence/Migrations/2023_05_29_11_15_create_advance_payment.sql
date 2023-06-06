CREATE TABLE advance_payments 
(
    id UUID PRIMARY KEY,
    date_ad VARCHAR(15),
    date_bs VARCHAR(15),
    branch VARCHAR(15),
    client_name VARCHAR(50),
    amount DECIMAL,
    adjusted_amount DECIMAL,
    cheque_no VARCHAR(50),
    bank VARCHAR(50),
    created_by VARCHAR(50),
    status VARCHAR(20),
    remarks TEXT
);