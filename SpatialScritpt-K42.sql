create database Test
use Test
SELECT * FROM sys.spatial_reference_systems;

CREATE TABLE Store
(
	StoreID int IDENTITY PRIMARY KEY,
	StoreName nvarchar(200),
	StoreAddress nvarchar(200),
	StorePhone nvarchar(25),
	StoreLocation geometry
);

DECLARE @g geometry;
SET @g = geometry::STGeomFromText('LINESTRING (100 100, 
20 180, 180 180)', 0);
SELECT @g;

--Chèn 1 cửa hàng
INSERT INTO Store(StoreName, StoreAddress, StorePhone, StoreLocation)
VALUES(N'KFC Trần Hưng Đạo', N'222 Trần Hưng Đạo P.PNL, Q1', '383838383',
geometry::STGeomFromText('POINT (3 4)',4168));

SELECT StoreLocation FROM Store;

------------LAB 06
--Cuối trang 03, lab 06: Yêu cầu tính diện tích
DECLARE @g1 geometry ='POLYGON((0 3, 3 3, 4 0, 1 1, 0 3))';
select @g1;

DECLARE @g2 geometry ='POLYGON((-1 -1, 0 3, 3 3, 4 0, -1 -1))';
select @g2;
DECLARE @g3 geometry ='POLYGON((0 0, 1 2, 3 2, 3 1, 0 0))';
--select @g3;
DECLARE @g4 geometry;
select @g4 = @g2.STDifference(@g3)
select @g4.STArea();

--Lab06 - Case Study 2
CREATE TABLE ManhDat(
	Id int identity (1,1) not null primary key,
	HoTen nvarchar(50) not null,
	CMND nvarchar(15),
	ViTri geometry
)

INSERT INTO ManhDat(HoTen, CMND, ViTri)
VALUES(N'Nguyễn Văn A', '029034034',
geometry::STGeomFromText('MULTIPOLYGON(((1 6, 7 6, 7 3, 5 2, 1 3, 1 6)),((-1 1, 2 1, 2 0, -1 1)))', 0)
)

SELECT ViTri.STArea() FROM ManhDat WHERE Id = 1