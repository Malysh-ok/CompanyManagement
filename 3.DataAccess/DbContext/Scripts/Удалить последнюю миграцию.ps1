Write-Host "Удаляем последнюю миграцию" -ForegroundColor Green
                Write-Host ""
                dotnet ef migrations remove -p "D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\DbContext\DbContext.csproj" -v
                pause
