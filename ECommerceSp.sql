USE ECommerce

sp_helptext sp_GetOffers

CREATE PROC sp_GetOffers  
@Id INT  
AS  
BEGIN  
IF(@Id IS NULL)  
BEGIN  
 SELECT * FROM Offers   
END  
ELSE  
BEGIN  
SELECT * FROM Offers Where OfferId = @Id  
END  
END

sp_helptext sp_GetProductCategories

CREATE   PROC sp_GetProductCategories  
@Id INT  
AS  
BEGIN  
IF(@Id IS NULL)  
BEGIN  
 SELECT * FROM ProductCategories   
END  
ELSE  
BEGIN  
SELECT * FROM ProductCategories Where CategoryId = @Id  
END  
END

sp_helptext sp_GetSuggestedProducts

CREATE OR ALTER  PROCEDURE sp_GetSuggestedProducts 
    @Category NVARCHAR(50),    
    @Subcategory NVARCHAR(50),    
    @Count INT    
AS    
BEGIN     
    SELECT TOP (@Count)   
        p.ProductId,  
        p.Title,  
        p.Description,  
        p.Price,  
        p.Quantity,  
        p.ImageName,  
        pc.CategoryId AS CategoryId,  
        pc.Category AS Category,  
        pc.SubCategory AS SubCategory,  
        o.OfferId AS OfferId,  
  o.Title AS OfferTitle,  
        o.Discount AS Discount  
    FROM Products p  
    INNER JOIN ProductCategories pc   
        ON p.CategoryId = pc.CategoryId  
    LEFT JOIN Offers o   
        ON p.OfferId = o.OfferId  
    WHERE pc.Category = @Category AND pc.SubCategory = @Subcategory  
    ORDER BY NEWID();    
END

CREATE OR ALTER PROC sp_GetProductById
@Id INT
AS
BEGIN
	SELECT p.*,pc.CategoryId AS CategoryId,  
        pc.Category AS Category,  
        pc.SubCategory AS SubCategory,  
        o.OfferId AS OfferId,  
  o.Title AS OfferTitle,  
        o.Discount AS Discount  
    FROM Products p  
    INNER JOIN ProductCategories pc   
        ON p.CategoryId = pc.CategoryId  
    LEFT JOIN Offers o   
        ON p.OfferId = o.OfferId WHERE p.ProductId = @Id;
END

GO
CREATE OR ALTER PROC sp_InsertUser 
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@Email NVARCHAR(100),
@Address NVARCHAR(100),
@Mobile NVARCHAR(15),
@Password NVARCHAR(50),
@CreatedAt NVARCHAR(MAX)=null,
@ModifiedAt NVARCHAR(MAX)=null
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Users WHERE Email=@Email)
	BEGIN
	INSERT INTO Users(FirstName,LastName,Email,Address,Mobile,Password,CreatedAt,ModifiedAt) 
	VALUES(@FirstName,@LastName,@Email,@Address,@Mobile,@Password,GETDATE(),GETDATE())
	SELECT 1
	END
END


GO
CREATE OR ALTER PROC sp_UserLogin
@Email NVARCHAR(100),
@Password NVARCHAR(50)
AS
BEGIN
	SELECT * FROM Users WHERE Email = @Email AND Password = @Password
END

GO
CREATE OR ALTER PROC sp_InsertReview
@UserId INT,
@ProductId INT,
@Review NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO Reviews(UserId,ProductId,Review,CreatedAt) VALUES(@UserId,@ProductId,@Review,GETDATE())
END

GO
CREATE OR ALTER PROC sp_GetReviews
@ProductId INT
AS
BEGIN
	SELECT R.*,U.*,P.* FROM Reviews R INNER JOIN Users U ON R.UserId = U.UserId INNER JOIN Products P ON R.ProductId = P.ProductId WHERE R.ProductId = @ProductId
END

GO
CREATE PROC sp_GetUserById
@UserId INT
AS
BEGIN
	SELECT * FROM Users WHERE UserId = @UserId
END

GO
CREATE OR ALTER PROC sp_InsertCartItem
@UserId INT,
@ProductId INT
AS
BEGIN
	IF(SELECT COUNT(*) FROM Carts WHERE UserId = @UserId AND Ordered = 'false') = 0
	BEGIN
		INSERT INTO Carts(UserId,Ordered,OrderedOn) VALUES(@UserId,'false',GETDATE())
	END
	DECLARE @cartId INT;
	SET @cartId = (SELECT CartId FROM Carts WHERE UserId = @UserId AND Ordered = 'false');
	INSERT INTO CartItems(CartId,ProductId) VALUES(@cartId,@ProductId)
END

--GO
--CREATE OR ALTER PROC sp_GetActiveCart 
--@UserId INT
--AS
--BEGIN
--		IF(SELECT COUNT(*) FROM Carts WHERE UserId = @UserId AND Ordered = 'false') = 0
--		BEGIN
--			SELECT 0
--		END
--		ELSE
--		BEGIN
--		DECLARE @cartId INT;
--		SET @cartId = (SELECT CartId FROM Carts WHERE UserId = @UserId AND Ordered = 'false');
--		SELECT CI.*,P.* FROM CartItems CI INNER JOIN Products P ON CI.ProductId = P.ProductId WHERE CartId = @cartId
--		END
--END

GO
CREATE OR ALTER PROCEDURE sp_GetActiveCart 
@UserId INT
AS
BEGIN
    DECLARE @CartId INT;
    
    -- Check if there is an active cart for the user
    SELECT @CartId=CartId
    FROM Carts
    WHERE UserId = @UserId AND Ordered = 'false';
	Select @CartId
    -- If no active cart found, return an empty cart
    IF @CartId IS NULL
    BEGIN
        SELECT 
            CartId,
            UserId,
            Ordered,
            OrderedOn
        FROM (SELECT
                  NULL AS CartId,
                  NULL AS UserId,
                  NULL AS Ordered,
                  NULL AS OrderedOn) AS EmptyCart;
        RETURN;
    END

    -- Get the user's active cart and its cart items
    SELECT
        CartId,
        UserId,
        Ordered,
        OrderedOn
    FROM (SELECT
              @CartId AS CartId,
              @UserId AS UserId,
              0 AS Ordered,
              '' AS OrderedOn) AS ActiveCart;

    -- Get the cart items
   SELECT
        CI.CartItemId,
		P.ProductId,
		P.Title,
		P.Description,
		C.CategoryId,
		C.Category,
		C.SubCategory,
		O.OfferId,
		O.Title AS OfferTitle,
		O.Discount,
		P.Price,
		P.Quantity,
		P.ImageName
    FROM CartItems AS CI
    INNER JOIN Products AS P ON CI.ProductId = P.ProductId 
	INNER JOIN ProductCategories C ON P.CategoryId = C.CategoryId
	INNER JOIN Offers O ON P.OfferId = O.OfferId
    WHERE CI.CartId = @CartId;
END


GO
CREATE OR ALTER PROC sp_GetAllPreviousCArtOfUser 8
@UserId INT
AS
BEGIN
SELECT CartId FROM Carts WHERE UserId=@UserId AND Ordered='true'
	--SELECT C.CartId,U.UserId,C.Ordered,C.OrderedOn,
	--U.FirstName,U.LastName,U.Email,U.Address,U.Mobile,U.CreatedAt,U.ModifiedAt,
	--CI.CartItemId,P.ProductId,P.Title,P.Description,P.CategoryId,P.OfferId,P.Price,P.Quantity,P.ImageName FROM Carts C INNER JOIN Users U ON C.UserId = U.UserId
	--INNER JOIN CartItems CI ON CI.CartId = C.CartId 
	--INNER JOIN Products P ON CI.ProductId = P.ProductId  WHERE C.UserId = @UserId AND Ordered = 'true';
END


--GO
--CREATE PROC sp_GetCart 
--@CartId INT
--AS
--BEGIN
--	IF EXISTS (SELECT * FROM CartItems WHERE CartId = (SELECT CartId FROM Carts WHERE CartId = @CartId))
--	BEGIN 
--		SELECT * FROM CartItems WHERE CartId = @CartId
--	END
--	SELECT * FROM Carts WHERE CartId = @CartId
--END


--UPDATE Carts SET UserId = 1  WHERE CartId = 6

--CREATE OR ALTER PROCEDURE sp_GetCartWithUserAndProduct 
--    @CartId INT
--AS
--BEGIN
--    -- Declare variables to store the data
--    DECLARE @UserId INT;
--    DECLARE @Ordered BIT;
--    DECLARE @OrderedOn NVARCHAR(255);

--    -- Retrieve the cart information
--    SELECT
--        @UserId = C.UserId,
--        @Ordered = C.Ordered,
--        @OrderedOn = C.OrderedOn
--    FROM Carts AS C
--    WHERE C.CartId = @CartId;

--    -- Retrieve user and product data
--    SELECT
--        U.UserId,U.FirstName,U.LastName,U.Email,U.Address,U.Mobile,U.CreatedAt,U.ModifiedAt,CI.CartItemId,-- Select all columns from the Users table
--        P.ProductId,P.Title,P.Description,PC.CategoryId,PC.Category,PC.SubCategory,O.OfferId,O.Title AS OfferTitle, O.Discount,P.Price,P.Quantity,P.ImageName -- Select all columns from the Products table
--    FROM Users AS U
--    JOIN Carts AS C ON U.UserId = C.UserId
--    JOIN CartItems AS CI ON C.CartId = CI.CartId
--    JOIN Products AS P ON CI.ProductId = P.ProductId
--	JOIN ProductCategories PC ON P.CategoryId = PC.CategoryId
--	JOIN Offers O ON O.OfferId = P.OfferId
--    WHERE C.CartId = @CartId;

--END

Go
CREATE OR ALTER PROC sp_GetCartItems 
@CartId INT 
AS
BEGIN 
	SELECT CI.CartItemId,C.CartId,P.ProductId,P.Title,P.Description,PC.CategoryId,PC.Category,PC.SubCategory,O.OfferId,O.Title AS OfferTitle, O.Discount,P.Price,P.Quantity,P.ImageName,C.Ordered,C.OrderedOn FROM 
	CartItems CI INNER JOIN Products P ON CI.ProductId = P.ProductId 
	INNER JOIN ProductCategories PC ON P.CategoryId = PC.CategoryId
	INNER JOIN Offers O ON O.OfferId = P.OfferId
	INNER JOIN Carts C ON C.CartId = CI.CartId
	WHERE C.CartId = @CartId
END

UPDATE CartItems SET CartId = 5 WHERE ProductId = 11 


GO
CREATE PROC sp_SelectPaymentMethods
As
BEGIN
	SELECT * FROM PaymentMethods
END


GO
CREATE OR ALTER PROC sp_InsPayment 
@PaymentMethodId INT,
@UserId INT,
@TotalAmt INT,
@ShippingCharges INT,
@AmountReduced INT,
@AmountPaid INT
AS
BEGIN
	INSERT INTO Payments(PaymentMethodId,UserId,TotalAmount,ShippingCharges,AmountReduced,AmountPaid,CreatedAt)
	VALUES(@PaymentMethodId,@UserId,@TotalAmt,@ShippingCharges,@AmountReduced,@AmountPaid,GETDATE())
	SELECT TOP 1 Id FROM Payments ORDER BY Id DESC
END

GO
CREATE OR ALTER PROC sp_InsOrder 
@UserId INT,
@CartId INT,
@PaymentId INT
AS
BEGIN
	INSERT INTO Orders(UserId,CartId,PaymentId,CreatedAt)
	VALUES(@UserId,@CartId,@PaymentId,GETDATE())
	UPDATE Carts SET Ordered='true',OrderedOn=GETDATE() WHERE CartId = @CartId;
	SELECT TOP 1 Id FROM Orders ORDER BY Id DESC
END

