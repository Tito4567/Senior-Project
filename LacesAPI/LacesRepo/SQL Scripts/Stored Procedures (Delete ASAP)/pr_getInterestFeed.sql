CREATE PROCEDURE [dbo].[pr_getInterestFeed]
(
	@userId	INT
)
AS
BEGIN
	SELECT TOP(500) p.ProductId
	FROM
		Products p
		INNER JOIN UserInterestQueue uiq
			ON p.ProductId=uiq.ProductId
	WHERE
		uiq.UserId=@UserId
		AND p.ProductStatusId=1
		AND uiq.Interested=1
	ORDER BY
		p.CreatedDate
END
GO

GRANT EXECUTE ON [dbo].[pr_getInterestFeed] TO LACES_USER
GO