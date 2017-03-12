CREATE PROCEDURE [dbo].[pr_getFollowingFeed]
(
	@userId	INT
)
AS
BEGIN
	SELECT * FROM
	(
		SELECT TOP(200)
			p.ProductId
			, p.CreatedDate
			, u.UserName
			, 0 AS FeedResultType
		FROM
			UserFollows uf WITH(NOLOCK)
			INNER JOIN Users u WITH(NOLOCK)
				ON uf.FollowedUserId=u.UserId
			INNER JOIN Products p WITH(NOLOCK)
				ON u.UserId=p.SellerId
		WHERE
			uf.FollowingUserId=@userId
		ORDER BY p.CreatedDate DESC

		UNION ALL

		SELECT TOP(100)
			ul.ProductId
			, ul.CreatedDate
			, u.UserName
			, 1 AS FeedResultType
		FROM
			UserFollows uf WITH(NOLOCK)
			INNER JOIN Users u WITH(NOLOCK)
				ON uf.FollowedUserId=u.UserId
			INNER JOIN UserLikes ul WITH(NOLOCK)
				ON u.UserId=ul.UserId
		WHERE
			uf.FollowingUserId=@userId
		ORDER BY ul.CreatedDate DESC
		
		UNION ALL
		
		SELECT TOP(100)
			c.ProductId
			, c.CreatedDate
			, u.UserName
			, 2 AS FeedResultType
		FROM
			UserFollows uf WITH(NOLOCK)
			INNER JOIN Users u WITH(NOLOCK)
				ON uf.FollowedUserId=u.UserId
			INNER JOIN Comments c WITH(NOLOCK)
				ON u.UserId=c.UserId
		WHERE
			uf.FollowingUserId=@userId
		ORDER BY c.CreatedDate DESC
	) AS data
	ORDER BY CreatedDate DESC
END
GO

GRANT EXECUTE ON [dbo].[pr_getFollowingFeed] TO LACES_USER
GO