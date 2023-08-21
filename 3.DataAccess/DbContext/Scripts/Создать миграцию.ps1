Write-Host "Создаем миграцию" -ForegroundColor Green
                Write-Host ""
                $MigrationName = Read-Host "Введите название миграции"
                dotnet ef migrations add $MigrationName -o Migrations -p "D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\DbContext\DbContext.csproj" -v
                pause
