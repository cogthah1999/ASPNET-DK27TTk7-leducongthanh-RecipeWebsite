# Website chia sẻ công thức nấu ăn

## 1. Giới thiệu

Website chia sẻ công thức nấu ăn được xây dựng nhằm hỗ trợ người dùng
tìm kiếm, xem và chia sẻ các công thức nấu ăn.

Hệ thống được phát triển bằng ASP.NET Core MVC và sử dụng SQL Server
để lưu trữ dữ liệu.

## 2. Mục tiêu

- Xây dựng website chia sẻ công thức nấu ăn.
- Cho phép người dùng tìm kiếm và xem công thức.
- Cho phép người dùng đăng ký và đăng nhập.
- Cho phép người dùng tạo, chỉnh sửa và xóa công thức của mình.
- Cho phép người dùng bình luận về công thức.
- Cho phép quản trị viên quản lý dữ liệu của hệ thống.

## 3. Công nghệ sử dụng

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Docker
- HTML/CSS
- Bootstrap
- Git/GitHub

## 4. Chức năng hệ thống

### 4.1. Khách chưa đăng nhập

- Xem danh sách công thức.
- Tìm kiếm công thức.
- Lọc công thức theo danh mục.
- Xem chi tiết công thức.
- Đăng ký tài khoản.
- Đăng nhập.

### 4.2. Người dùng

- Đăng nhập và đăng xuất.
- Xem công thức.
- Tìm kiếm và lọc công thức.
- Tạo công thức.
- Chỉnh sửa công thức của mình.
- Xóa công thức của mình.
- Bình luận về công thức.

### 4.3. Quản trị viên

- Xem Dashboard.
- Quản lý công thức.
- Quản lý danh mục.
- Quản lý người dùng.
- Quản lý bình luận.
- Đăng xuất.

## 5. Kiến trúc hệ thống

Project sử dụng mô hình ASP.NET Core MVC:

User
↓
Controller
↓
Model / Entity Framework Core
↓
SQL Server

## 6. Cơ sở dữ liệu

Hệ thống sử dụng các bảng:

- Users
- Categories
- Recipes
- Ingredients
- RecipeIngredients
- Comments

## 7. Yêu cầu môi trường

- .NET SDK
- Docker Desktop
- SQL Server
- Visual Studio Code hoặc Visual Studio
- Git

## 8. Cài đặt

### Bước 1: Clone project

```bash
git clone <repository-url>
cd RecipeWebsite