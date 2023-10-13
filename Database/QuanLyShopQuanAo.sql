--------------------------------------------------------------------------------
--                          SHOP MANAGEMENT DATABASE                          --
--------------------------------------------------------------------------------
CREATE DATABASE QL_ShopQuanAo
GO
	
USE QL_ShopQuanAo
GO

--------------------------------------------------------------------------------
--                                   TABLE                                    --
--------------------------------------------------------------------------------
CREATE TABLE NHACUNGCAP
(
	MaNCC int not null,
	TenNCC nvarchar(30),
	DiaChi nvarchar(30),
	SDT int,
	Primary key(MANCC)
)
CREATE TABLE LoaiQA
(
	ID_LQA INT NOT NULL IDENTITY(1,1),
	Ten_LQA NVARCHAR(100) NOT NULL,
	Is_Alive BIT NOT NULL DEFAULT 1
)
GO

CREATE TABLE HinhQA
(
	ID_HQA INT NOT NULL IDENTITY(1,1),
	HinhQA IMAGE NOT NULL,
	HinhQAP NVARCHAR(MAX) NOT NULL,
	ID_QA INT NOT NULL
)
GO

CREATE TABLE QuanAo
(
	ID_QA INT NOT NULL,
	Ten_QA NVARCHAR(100) NOT NULL,
	Size CHAR(5) NOT NULL,
	ID_LQA INT NOT NULL,
	GiaBan FLOAT NOT NULL,
	SoLuong INT NOT NULL,
	GhiChu NVARCHAR(MAX) NULL,
	Is_Alive BIT NOT NULL
)
GO

CREATE TABLE KieuTaiKhoan
(
	ID_KTK INT NOT NULL IDENTITY(1,1),
	Ten_KTK NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE QuanTriVien
(
	ID_QTV INT NOT NULL, 
	TenDangNhap NVARCHAR(50) NOT NULL,
	MatKhau NVARCHAR(50) NOT NULL,
	ID_KTK INT NOT NULL
)
GO

CREATE TABLE KhachHang
(
	ID_KH INT NOT NULL IDENTITY(1,1),
	HoTen NVARCHAR(50) NOT NULL,
	SDT NVARCHAR(50) NOT NULL,
	DiaChi NVARCHAR(MAX) NULL
)
GO

CREATE TABLE BanHang
(
	ID_BH INT NOT NULL IDENTITY(1,1),
	ID_KH INT NOT NULL,
	ID_GD NVARCHAR(50) DEFAULT NULL, -- NULL: Khách hàng chưa thanh toán tiền OR 'yyyyddmmhhMMss': thời gian hóa đơn được thanh toán
	NgayBanHang DATETIME DEFAULT GETDATE(),
	Discount FLOAT DEFAULT 0
)
GO

CREATE TABLE ChiTietBanHang
(
	ID_CTBH INT NOT NULL IDENTITY(1,1),
	ID_QA INT NOT NULL,
	ID_BH INT NOT NULL,
	SoLuongSanPham INT NOT NULL
	-- GiaBan FLOAT NOT NULL
)
GO

CREATE TABLE HoaDon
(
	ID_QA int not null IDENTITY(1,1),
	HoTen nvarchar(30),
	SDT int,
	DiaChi nvarchar(30),
	TongTien int,
	TongTienSauGiamGia int,
	Primary key (ID_QA)
)

--------------------------------------------------------------------------------
--                                PRIMARY KEY                                 --
--------------------------------------------------------------------------------
ALTER TABLE LoaiQA WITH NOCHECK ADD CONSTRAINT PK_LoaiQA PRIMARY KEY CLUSTERED 
(
	ID_LQA ASC 
)
GO

ALTER TABLE HinhQA WITH NOCHECK ADD CONSTRAINT PK_HinhQA PRIMARY KEY CLUSTERED 
(
	ID_HQA ASC 
)
GO

ALTER TABLE QuanAo WITH NOCHECK ADD CONSTRAINT PK_QuanAo PRIMARY KEY CLUSTERED 
(
	ID_QA ASC 
)
GO

ALTER TABLE KieuTaiKhoan WITH NOCHECK ADD CONSTRAINT PK_KieuTaiKhoan PRIMARY KEY CLUSTERED 
(
	ID_KTK ASC 
)
GO

ALTER TABLE QuanTriVien WITH NOCHECK ADD CONSTRAINT PK_QuanTriVien PRIMARY KEY CLUSTERED 
(
	ID_QTV ASC 
)
GO

ALTER TABLE KhachHang WITH NOCHECK ADD CONSTRAINT PK_KhachHang PRIMARY KEY CLUSTERED 
(
	ID_KH ASC 
)
GO

ALTER TABLE ChiTietBanHang WITH NOCHECK ADD CONSTRAINT PK_ChiTietBanHang PRIMARY KEY CLUSTERED 
(
	ID_CTBH ASC 
)
GO

ALTER TABLE BanHang WITH NOCHECK ADD CONSTRAINT PK_BanHang PRIMARY KEY CLUSTERED 
(
	ID_BH ASC 
)
GO

--------------------------------------------------------------------------------
--                                FOREIGN KEY                                 --
--------------------------------------------------------------------------------
ALTER TABLE HinhQA
ADD CONSTRAINT FK_QuanAo_HinhQuanAo
FOREIGN KEY (ID_QA) REFERENCES QuanAo(ID_QA)
GO

ALTER TABLE QuanAo
ADD CONSTRAINT FK_LoaiQA_QuanAo
FOREIGN KEY (ID_LQA) REFERENCES LoaiQA(ID_LQA)
GO

ALTER TABLE QuanTriVien
ADD CONSTRAINT FK_KieuTaiKhoan_QuanTriVien
FOREIGN KEY (ID_KTK) REFERENCES KieuTaiKhoan(ID_KTK)
GO

ALTER TABLE ChiTietBanHang
ADD CONSTRAINT FK_QuanAo_ChiTietBanHang
FOREIGN KEY (ID_QA) REFERENCES QuanAo(ID_QA)
GO

ALTER TABLE ChiTietBanHang
ADD CONSTRAINT FK_BanHang_ChiTietBanHang
FOREIGN KEY (ID_BH) REFERENCES BanHang(ID_BH)
GO

ALTER TABLE BanHang
ADD CONSTRAINT FK_KhachHang_BanHang
FOREIGN KEY (ID_KH) REFERENCES KhachHang(ID_KH)
GO

ALTER TABLE HoaDon
ADD CONSTRAINT FK_HD_QA
FOREIGN KEY (ID_QA) REFERENCES QuanAo(ID_QA)
GO

--------------------------------------------------------------------------------
--                                Du lieu mau                                 --
--------------------------------------------------------------------------------

INSERT LoaiQA(Ten_LQA)
VALUES 
	(N'Quần jean'),
	(N'Áo thun'),
	(N'Váy'),
	(N'Áo len')
GO
SELECT * FROM LoaiQA

INSERT QuanAo(ID_QA,Ten_QA, ID_LQA, Size, SoLuong, GiaBan, GhiChu,Is_Alive)
VALUES
	(1,N'Quần jean ngắn', 1, 'M', 100, 100000, N'Quần jean hấp dẫn cho ngày hè năng động',1),
	(2,N'Quần jean dài', 1, 'M', 100, 120000, N'Quần jean hấp dẫn cho ngày đông buốt giá',1),
	(3,N'Áo thun hiệu con bò cười', 2, 'M', 50, 50000, N'Áo thun con bò',1),
	(4,N'Áo thun Doremon', 2, 'M', 100, 50000, N'Áo thun doremon',1),
	(5,N'Váy Doremon', 3, 'M', 70, 50000, N'Váy doremon',1),
	(6,N'Váy dài', 3, 'M', 70, 150000, N'Váy dài siêu cấp',1),
	(7,N'Áo len mỏng', 4, 'M', 70, 200000, N'Áo len mát mẻ',1),
	(8,N'Áo len dày', 4, 'M', 70, 300000, N'Áo len ấm áp',1),
	(9,N'Áo len cổ dài', 4, 'M', 0, 300000, N'Áo len ấm áp',0)
SELECT * FROM QuanAo

INSERT KieuTaiKhoan(Ten_KTK)
VALUES 
	(N'Quản trị viên'),
	(N'Nhân viên')

SELECT * FROM KieuTaiKhoan

INSERT QuanTriVien(ID_QTV,TenDangNhap, MatKhau,ID_KTK)
VALUES
	(1,N'truongan', N'123456',1),
	(2,N'vietthanh', N'123456',1),
	(3,N'truonganNV', N'123456789',2),
	(4,N'vietthanhNV', N'123456789',2)

SELECT * FROM QuanTriVien

INSERT KhachHang(HoTen, SDT, DiaChi)
VALUES
	(N'Nguyễn Trọng Hiếu', '0374408253', N'Đà Lạt'),
	(N'Nguyễn Thị Hà', '0374408254', N'Đà Lạt'),
	(N'Trần Hữu Khải Quân', '0374408255', N'Đà Lạt')

SELECT * FROM KhachHang

INSERT BanHang(ID_KH, ID_GD, Discount, NgayBanHang)
VALUES
	(1, '202001121054', 30, GETDATE()),
	(2, '202002121055', 50, GETDATE()),
	(3, '202003122054', 10, GETDATE())
SELECT * FROM BanHang
SELECT * FROM QuanAo

INSERT ChiTietBanHang(ID_BH, ID_QA, SoLuongSanPham)
VALUES
	(1, 1, 2),
	(1, 2, 2),
	(1, 3, 1),
	(2, 4, 1),
	(2, 5, 2),
	(1, 6, 3),
	(3, 7, 5),
	(3, 8, 1)
SELECT * FROM ChiTietBanHang
GO

INSERT QuanAo(Ten_QA, ID_LQA, Size, SoLuong, GiaBan, GhiChu)
VALUES
	(N'Quần jean ngắn MOI MOI', 1, 'M', 100, 100000, N'Quần jean hấp dẫn cho ngày hè năng động');
	GO

INSERT NHACUNGCAP VALUES('1','Nhà Cung Cấp 01','123 TP.HCM',01234567890)
INSERT NHACUNGCAP VALUES('2','Nhà Cung Cấp 02','123 TP.HCM',01234567891)
INSERT NHACUNGCAP VALUES('3','Nhà Cung Cấp 03','123 TP.HCM',01234567892)
--------------------------------------------------------------------------------
--                             Procedure bo sung                              --
--------------------------------------------------------------------------------
-- PROC Thêm (frmAdmin Tài khoản - Mật Khẩu)
CREATE PROC [dbo].[SP_ThemTK]
@ID int,
@TaiKhoan nvarchar(50),
@MatKhau nvarchar(50),
@KTK int
AS
BEGIN
	INSERT INTO QuanTriVien VALUES( @ID,@TaiKhoan, @MatKhau,@KTK)
END
GO

-- PROC Xoá (frmAdmin Tài khoản - Mật Khẩu)
CREATE proc [dbo].[SP_XoaTaiKhoan]
@IDQTV int
as
begin
    delete QuanTriVien where ID_QTV = @IDQTV
end
GO
EXEC [dbo].[SP_XoaTaiKhoan] '5'
SELECT * FROM QuanTriVien
GO

-- PROC Thêm (frmAdmin Quần Áo)
SELECT * FROM LoaiQA
SELECT * FROM QuanAo
GO
CREATE PROC [dbo].[SP_ThemQA]
@IDQA int,
@TENQA nvarchar(50),
@Size char(5),
@IDLQA int,
@GiaBan int,
@SoLuong int,
@GhiChu nvarchar(100),
@TrangThai bit
AS
BEGIN
	INSERT INTO QuanAo VALUES(@IDQA,@TENQA,@Size,@IDLQA,@GiaBan,@SoLuong,@GhiChu,@TrangThai)
END
GO

EXEC [dbo].[SP_ThemQA] 10,'Quan Jean', 'XXL' ,1,120000,40,'Quan Jean rat tuyet voi',1
GO
-- PROC Xoá (frmAdmin Quần Áo)
SELECT * FROM QuanAo
GO

CREATE proc [dbo].[SP_XoaQuanAo]
@IDQA int
as
begin
    delete QuanAo where ID_QA = @IDQA
end
GO

EXEC [dbo].[SP_XoaQuanAo] '13'
GO
-- PROC Sửa (frmAdmin Quần Áo)
SELECT * FROM QuanAo
GO

create proc [dbo].[SP_SuaQuanAo]
@IDQA int,
@TENQA nvarchar(50),
@Size char(5),
@IDLQA int,
@GiaBan int,
@SoLuong int,
@GhiChu nvarchar(100),
@TrangThai bit
as
begin
    update QuanAo set
    Ten_QA = @TENQA,
    Size = @Size,
	ID_LQA= @IDLQA,
	GiaBan=@GiaBan,
	SoLuong=@SoLuong,
	GhiChu=@GhiChu,
	Is_Alive=@TrangThai
    where ID_QA = @IDQA
end
GO
EXEC [dbo].[SP_SuaQuanAo]
GO

-- Thêm (frmQuanLy - Hoá Đơn)
SELECT * FROM HOADON
GO
INSERT INTO HoaDon VALUES (1,N'Nguyễn Trọng Hiếu',0123123122,N'123 Cộng Hoà',240000,120000)
GO
create PROC [dbo].[SP_ThemHD]
@HoTenKH nvarchar(50),
@SDT int,
@DiaChi nvarchar(50),
@TongTien int,
@TongTienSauGG int
AS
BEGIN
	INSERT INTO HoaDon VALUES(@HoTenKH,@SDT,@DiaChi,@TongTien,@TongTienSauGG)
END
GO

--
CREATE PROC sp_select_cacsanphamkhongbanduoc
AS
	BEGIN
	SELECT ID_QA, Ten_QA
	FROM QuanAo
	WHERE ID_QA NOT IN (
		SELECT DISTINCT ID_QA FROM ChiTietBanHang
	)
END
GO

EXEC sp_select_cacsanphamkhongbanduoc
GO


--Top 5 khách hàng mua hàng nhiều nhất
CREATE PROC sp_select_top5KhachHangcosolanmuahangnhieunhat
AS
	BEGIN
		SELECT ID_KH,HoTen
		FROM KhachHang
		WHERE ID_KH IN (
			SELECT TOP 5 ID_KH
			FROM BanHang
			GROUP BY ID_KH
			ORDER BY COUNT(DISTINCT ID_BH) DESC
		)
	END
GO

EXEC sp_select_top5KhachHangcosolanmuahangnhieunhat
GO

-- Danh sách loại sản phẩm bán chạy nhất
CREATE PROCEDURE sp_select_Master_LoaiQA
AS
BEGIN
	SELECT LoaiQA.ID_LQA, COUNT(QuanAo.ID_LQA)  AS SoLuongSanPham, Ten_LQA
	FROM QuanAo JOIN LoaiQA ON QuanAo.ID_LQA = LoaiQA.ID_LQA
	WHERE QuanAo.ID_LQA = LoaiQA.ID_LQA
	GROUP BY LoaiQA.ID_LQA,Ten_LQA
END
GO

EXEC sp_select_Master_LoaiQA
GO

-- Top 5 sản phẩm bán chạy nhất
CREATE proc sp_select_sanphambanchaynhat
as
	begin
		Select Top 5 Ten_QA, SUM(SoLuongSanPham) as SoLuongSanPham
		from QuanAo, ChiTietBanHang
		where QuanAo.ID_QA = ChiTietBanHang.ID_QA
		group by Ten_QA
		order by SoLuongSanPham desc
	end
go

EXEC sp_select_sanphambanchaynhat
GO

