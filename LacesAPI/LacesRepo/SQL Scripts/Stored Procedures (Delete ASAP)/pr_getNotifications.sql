CREATE PROCEDURE [dbo].[pr_getNotifications]
(
	@userId	INT
)
AS
BEGIN
	SELECT * FROM
	(
		SELECT
			u2.UserName
			, ul.ProductId
			, ul.CreatedDate
			, 0 AS NotificationTypeId
		FROM
			Users u
			INNER JOIN Products p
				ON p.SellerId=u.UserId
			INNER JOIN UserLikes ul
				ON ul.ProductId=p.ProductId
			INNER JOIN Users u2
				ON u2.UserId=ul.UserId
		WHERE
			u.UserId=@userId
			AND ul.CreatedDate >= u.LastAlertCheck
		
		UNION ALL

		SELECT
			u2.UserName
			, c.ProductId
			, c.CreatedDate
			, 1 AS NotificationTypeId
		FROM
			Users u
			INNER JOIN Products p
				ON p.SellerId=u.UserId
			INNER JOIN Comments c
				ON c.ProductId=p.ProductId
			INNER JOIN Users u2
				ON u2.UserId=c.UserId
		WHERE
			u.UserId=@userId
			AND c.CreatedDate >= u.LastAlertCheck
		
		UNION ALL

		SELECT
			u2.UserName
			, 0 AS ProductId
			, uf.CreatedDate
			, 2 AS NotificationTypeId
		FROM
			Users u
			INNER JOIN UserFollows uf
				ON uf.FollowedUserId=u.UserId
			INNER JOIN Users u2
				ON u2.UserId=uf.FollowingUserId
		WHERE
			u.UserId=@userId
			AND uf.CreatedDate >= u.LastAlertCheck
		
		UNION ALL

		SELECT
			u2.UserName
			, p.ProductId
			, uiq.UpdatedDate AS CreatedDate
			, 3 AS NotificationTypeId
		FROM
			Users u
			INNER JOIN Products p
				ON p.SellerId=u.UserId
			INNER JOIN UserInterestQueue uiq
				ON uiq.ProductId=p.ProductId
			INNER JOIN Users u2
				ON u2.UserId=uiq.UserId
		WHERE
			u.UserId=@userId
			AND uiq.UpdatedDate >= u.LastAlertCheck
			AND uiq.Interested = 1
	) AS alert
	ORDER BY alert.CreatedDate
END