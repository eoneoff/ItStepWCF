USE Account
GO

CREATE TRIGGER transactions_insert
ON transactions
AFTER INSERT
AS
BEGIN
	UPDATE accounts
	SET balance = balance+(SELECT transAmount from inserted)
	WHERE id=(SELECT accId FROM inserted);
END