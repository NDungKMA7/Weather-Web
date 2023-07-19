Dự án theo dõi thời tiết
Dự án này sử dụng công nghệ ASP.NET Core, SignalR và Entity Framework để theo dõi thời tiết.

Mô tả:

Đây là 1 dự án IOT. Bằng việc kết hợp các cảm biến kèm theo đó là ESP32 để đo các thông số của môi trường sau đó
thông qua giao thức MQTT kết hợp với websocket để gửi dữ liệu lên cho server. Sau đó server xử lý lưu dữ liệu 
vào cơ sở dữ liệu rồi gửi lại data cho client thông qua websocket. 

Yêu cầu
.NET Core 6
Visual Studio 2019
SQL Server

Cài đặt
Tạo một dự án ASP.NET Core mới bằng Visual Studio.
Thêm thư viện SignalR vào dự án.
Thêm thư viện Entity Framework vào dự án.
Tạo một bảng thời tiết trong SQL Server.
Tạo một lớp thời tiết để đại diện cho dữ liệu thời tiết.

Sử dụng
Khởi động ứng dụng ASP.NET Core.
Truy cập trang web tại local của Backend.
Trang web sẽ hiển thị dữ liệu thời tiết cho vị trí hiện tại.

Tài liệu
ASP.NET Core documentation: https://docs.microsoft.com/en-us/aspnet/core/
SignalR documentation: https://docs.microsoft.com/en-us/aspnet/core/signalr/
Entity Framework documentation: https://docs.microsoft.com/en-us/ef/core/
Cảm ơn
