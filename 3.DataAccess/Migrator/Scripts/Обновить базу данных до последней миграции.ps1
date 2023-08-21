Write-Host "Обновляем базу данных до последней миграции" -ForegroundColor Green
                Write-Host ""
                
                $dbContextProjPath = ""
                $arr = "..\DataAccessManagement\DataAccessManagement.csproj
..\..\4.Infrastructure\BaseComponents\BaseComponents.csproj
..\..\4.Infrastructure\BaseExtensions\BaseExtensions.csproj
..\DbConfigureManagement\DbConfigureManagement.csproj
..\Entities\Entities.csproj
..\..\4.Infrastructure\Phrases\Phrases.csproj".Replace("`r", "").Split("`n")
                ForEach ($item in $arr){
                    if ($item.Contains("DataAccessManagement.csproj")) {
                        $dbContextProjPath = $item
                    }
                }
                $dbContextProjPath = [System.IO.Path]::Combine("D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\Migrator\", $dbContextProjPath)
                $dbContextProjPath = [System.IO.Path]::GetFullPath($dbContextProjPath)            
            
                Read-Host "Выполните сборку (а лучше - пересборку) проекта 'Migrator' и нажмите 'Enter'"
                dotnet ef database update -p "D:\Документы\_ПРОЕКТЫ\_Программирование\C#\Тестовые задания 2023.06\CompanyManagement\3.DataAccess\Migrator\Migrator.csproj" -v --no-build  -- --migrationsAssembly Migrator
                pause
