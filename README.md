docker exec -it sql_server_local bash


dotnet ef database update


dotnet ef migrations add AddCategoryTableToDb
