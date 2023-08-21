Для установки dotnet-ef (.NET Core CLI) набрать в терминале:
    dotnet tool install --global dotnet-ef
Для обновления:
    ...можно с версией:
    dotnet tool update --global dotnet-ef --version X.Y.Z

В проекте должны быть установлены NuGet-пакеты:
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore


Работа с миграциями (все делаем в терминале):
1) Задаем текущую директорию:
	cd <Путь_к_контексту_БД>
2) Создаем миграцию (в качестве примера указано имя Initial - для первой миграции):
	dotnet ef migrations add Initial -o Migrations --project DbContexts.csproj
3) Применяем миграцию (обновляем БД):
	dotnet ef database update
3) Удаляем последнюю миграцию (если нужно):
	dotnet ef migrations remove
	
UPD (01.06.2023)
Абзац выше устарел. Теперь создаются скрипты (см. <Имя_проекта>.csproj).