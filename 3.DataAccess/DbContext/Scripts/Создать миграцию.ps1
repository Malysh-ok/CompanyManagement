Write-Host "������� ��������" -ForegroundColor Green
                Write-Host ""
                $MigrationName = Read-Host "������� �������� ��������"
                dotnet ef migrations add $MigrationName -o Migrations -p "D:\���������\_�������\_����������������\C#\�������� ������� 2023.06\CompanyManagement\3.DataAccess\DbContext\DbContext.csproj" -v
                pause
