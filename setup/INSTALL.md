# Hướng dẫn cài đặt và chạy hệ thống

## 1. Yêu cầu môi trường

- .NET SDK
- Docker Desktop
- SQL Server
- Git

## 2. Khởi động SQL Server

Chạy Docker Compose:

docker compose up -d

## 3. Cập nhật cơ sở dữ liệu

dotnet ef database update

## 4. Chạy ứng dụng

dotnet run

## 5. Truy cập hệ thống

Mở trình duyệt và truy cập địa chỉ localhost được hiển thị trong terminal.