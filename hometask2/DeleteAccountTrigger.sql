USE Account
GO

CREATE TRIGGER accounts_delete
ON accounts
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM transactions
	WHERE accid = (SELECT id FROM deleted)
	DELETE FROM accounts
	WHERE id = (SELECT id FROM deleted)
END