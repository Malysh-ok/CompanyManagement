Write-Host "Создаем первую миграцию" -ForegroundColor Green
                Write-Host ""
                dotnet ef migrations add Initial -o Migrations -p "D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\DbContext\DbContext.csproj" -v
                pause
