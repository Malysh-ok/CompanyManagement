Write-Host "Обновляем базу данных до последней миграции" -ForegroundColor Green
                Write-Host ""

                dotnet ef database update -p "D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\DbContext\DbContext.csproj" -v 
                pause
