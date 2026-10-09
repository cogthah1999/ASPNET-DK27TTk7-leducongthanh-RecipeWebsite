# Website chia sẻ công thức nấu ăn

## 1. Giới thiệu

Website chia sẻ công thức nấu ăn được xây dựng nhằm hỗ trợ người dùng
tìm kiếm, xem và chia sẻ các công thức nấu ăn. Hệ thống hỗ trợ quản lý
công thức, danh mục, nguyên liệu và bình luận.

Dự án được phát triển bằng ASP.NET Core MVC, sử dụng Entity Framework
Core để truy cập cơ sở dữ liệu SQL Server.

## 2. Mục tiêu

-   Xây dựng website chia sẻ công thức nấu ăn.
-   Cho phép người dùng tìm kiếm và xem công thức.
-   Hỗ trợ đăng ký, đăng nhập và đăng xuất.
-   Cho phép người dùng tạo, chỉnh sửa và xóa công thức của mình theo
    quyền được cấp.
-   Cho phép người dùng bình luận về công thức.
-   Hỗ trợ quản trị viên quản lý dữ liệu của hệ thống.

## 3. Công nghệ sử dụng

-   C#
-   ASP.NET Core MVC
-   Entity Framework Core
-   Microsoft SQL Server
-   Docker
-   HTML, CSS, Bootstrap
-   Git và GitHub

## 4. Chức năng hệ thống

### 4.1. Khách chưa đăng nhập

-   Xem danh sách công thức.
-   Tìm kiếm công thức theo từ khóa.
-   Lọc công thức theo danh mục.
-   Xem chi tiết công thức.
-   Đăng ký tài khoản.
-   Đăng nhập.

### 4.2. Người dùng

-   Đăng nhập và đăng xuất.
-   Xem, tìm kiếm và lọc công thức.
-   Tạo công thức.
-   Chỉnh sửa và xóa công thức của mình theo quyền được cấp.
-   Bình luận về công thức.

### 4.3. Quản trị viên

-   Xem Dashboard.
-   Quản lý công thức.
-   Quản lý danh mục.
-   Quản lý người dùng.
-   Quản lý bình luận.
-   Đăng xuất.

## 5. Kiến trúc hệ thống

Dự án sử dụng mô hình ASP.NET Core MVC kết hợp Entity Framework Core để
làm việc với cơ sở dữ liệu.

``` text
Người dùng
    ↓
View (giao diện)
    ↓
Controller
    ↓
Model / Entity Framework Core
    ↓
SQL Server
```

## 6. Cơ sở dữ liệu

Hệ thống sử dụng các bảng chính:

  Bảng                  Mục đích
  --------------------- ------------------------------------------------
  `Users`               Lưu thông tin tài khoản
  `Categories`          Lưu danh mục công thức
  `Recipes`             Lưu thông tin công thức
  `Ingredients`         Lưu danh sách nguyên liệu
  `RecipeIngredients`   Liên kết công thức với nguyên liệu và số lượng
  `Comments`            Lưu bình luận của người dùng

Quan hệ nhiều-nhiều giữa công thức và nguyên liệu được thể hiện thông
qua bảng `RecipeIngredients`.

## 7. Yêu cầu môi trường

-   .NET SDK tương thích với phiên bản khai báo trong
    `RecipeWebsite.csproj`.
-   Docker Desktop.
-   SQL Server chạy trong Docker và có cổng `1433` được ánh xạ ra máy
    host.
-   Git.
-   Visual Studio hoặc Visual Studio Code.
-   Trình duyệt web hiện đại.

## 8. Cài đặt và chạy dự án

### Bước 1. Clone repository

Mở terminal tại thư mục muốn lưu project và chạy:

``` bash
git clone https://github.com/cogthah1999/ASPNET-DK27Tk7-leducongthanh-RecipeWebsite.git
```

Sau đó mở thư mục repository vừa clone. Mã nguồn ASP.NET nằm trong thư
mục `scr/`.

``` bash
cd ASPNET-DK27Tk7-leducongthanh-RecipeWebsite/scr
```

> Nếu tên repository trên GitHub của bạn khác với đường dẫn trên, hãy
> sao chép URL clone trực tiếp từ trang GitHub và thay vào lệnh
> `git clone`.

### Bước 2. Khởi động SQL Server bằng Docker Desktop

1.  Mở Docker Desktop.
2.  Tìm container SQL Server dùng cho dự án.
3.  Khởi động container và chờ trạng thái chạy ổn định.
4.  Đảm bảo container ánh xạ cổng `1433` ra máy host, ví dụ `1433:1433`.

Nếu container SQL Server chưa được tạo, hãy tạo và cấu hình container
trước khi tiếp tục. README này giả định SQL Server đã chạy trong Docker
Desktop; dự án không yêu cầu phải chạy SQL Server bằng Docker Compose.

### Bước 3. Cấu hình chuỗi kết nối

Trong thư mục `scr/`, sao chép `appsettings.example.json` thành
`appsettings.json` nếu file cấu hình cục bộ chưa có.

Mở `appsettings.json` và thay `YOUR_PASSWORD_HERE` bằng mật khẩu `sa` đã
cấu hình cho SQL Server trong Docker. Không đưa mật khẩu thật vào
GitHub.

Ví dụ cấu hình:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1,1433;Database=RecipeWebsiteDB;User Id=sa;Password=YOUR_PASSWORD_HERE;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Thay `YOUR_PASSWORD_HERE` bằng mật khẩu thực tế trên máy của bạn. Đảm
bảo tên server, cổng, tên database và thông tin đăng nhập khớp với
container SQL Server đang chạy.

### Bước 4. Mở project và khôi phục package

Trong Visual Studio hoặc Visual Studio Code, mở thư mục `scr/` hoặc mở
trực tiếp file `RecipeWebsite.csproj`.

Tại terminal đang ở thư mục `scr/`, chạy:

``` bash
dotnet restore
```

### Bước 5. Cập nhật cơ sở dữ liệu

Đảm bảo SQL Server đang chạy và chuỗi kết nối đã chính xác. Sau đó chạy:

``` bash
dotnet ef database update
```

Lệnh này áp dụng các EF Core Migration có sẵn trong project vào database
`RecipeWebsiteDB`. Nếu máy chưa có công cụ EF Core CLI, hãy cài đặt
`dotnet-ef` tương thích với phiên bản Entity Framework Core của dự án.

### Bước 6. Chạy website

Tại thư mục chứa `RecipeWebsite.csproj`, chạy:

``` bash
dotnet run
```

Mở địa chỉ HTTP hoặc HTTPS được hiển thị trong terminal để truy cập
website.

Nếu không kết nối được cơ sở dữ liệu, hãy kiểm tra: - Container SQL
Server đang chạy. - Cổng `1433` đã được ánh xạ đúng. - Mật khẩu `sa`
trong chuỗi kết nối đúng với cấu hình container. - Database server có
thể được truy cập từ máy host.

Một điều lưu ý có thể sữa localhost thay bằng 127.0.0.1 để DOCKER SQL dễ nhận diện hơn.

## 9. Cấu trúc thư mục

``` text
ASPNET-DK27TTK7-leduccongthanh-RecipeWebsite/
├── docker/
├── progress-report/
├── scr/
│   ├── Controllers/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   ├── ViewModels/
│   ├── Views/
│   ├── wwwroot/
│   ├── Program.cs
│   ├── RecipeWebsite.csproj
│   ├── appsettings.example.json
│   └── README.md (nếu có)
├── setup/
├── thesis/
└── README.md
```

Đây là cấu trúc tổng quát để tham khảo; các file hoặc thư mục phụ có thể
khác tùy phiên bản repository.

## 10. Kiểm thử

Các nhóm chức năng đã được kiểm tra trong quá trình phát triển gồm:

-   Đăng ký và đăng nhập.
-   Hiển thị, tạo, chỉnh sửa và xóa công thức.
-   Tìm kiếm và lọc theo danh mục.
-   Thêm và hiển thị bình luận.
-   Phân quyền giữa người dùng và quản trị viên.
-   Kiểm tra dữ liệu không hợp lệ và ID không tồn tại.
-   Kiểm tra tính toàn vẹn dữ liệu liên quan.

## 11. Hạn chế và hướng phát triển

Các hướng phát triển trong tương lai có thể bao gồm:

-   Bổ sung chức năng đánh giá công thức.
-   Phát triển tính năng yêu thích công thức.
-   Xây dựng chức năng đề xuất công thức.
-   Cải thiện giao diện trên thiết bị di động.
-   Triển khai hệ thống lên môi trường máy chủ thực tế.

Các tính năng nêu trên là hướng phát triển, không được xem là chức năng
đã triển khai nếu chưa có trong phiên bản hiện tại.

## 12. Thông tin đồ án

-   **Đề tài:** Website chia sẻ công thức nấu ăn
-   **Công nghệ chính:** ASP.NET Core MVC, C#, Entity Framework Core và
    SQL Server
-   **Tác giả:** Lê Đức Công Thành
