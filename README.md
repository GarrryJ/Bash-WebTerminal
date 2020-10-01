# Bash-WebTerminal
#### Приложение позволяет ввести bash команду в текстовое поле на веб-странице и отправить на сервер для выполнения.
####
#### Приложение тестировалось на таких ОС как Ubuntu 20.04 и macOS Catalina 10.15.6.
#### Тесты запуска проходили только из терминалов этих ОС и терминала внутри Visual Studio Code из-за невозможности установить Visual Studio на Ubuntu и ограниченному пространству на macOS.

В 30 строке файла Startup.cs необходимо вставить свой пароль от SQL сервера вместо 'YOUR_PASSWORD' для подключения, при необходимости изменить 'User ID' и 'Data Source' если приложение будет запускаться на удалённом сервере:
```C#
  services.AddDbContext<CommandsDataBaseContext>(o => o.UseSqlServer($"Password=YOUR_PASSWORD;Data Source=127.0.0.1;User ID=sa;Initial Catalog={nameof(CommandsDataBaseContext)};Integrated Security=False;"));
```

Для запуска на этих ОС необходимо выполнить следующие действия:
1. Установить .NET Core
2. Установить EF Core
3. В терминале перейти в папку с проектом и выполнить следующие команды: 
```bash
dotnet ef database update
dotnet run
```
