

AuthServr.API varsayýlan projedir. Tüm rol ve auth kýsmý ile ilgili iþlemlerin yürütüldüðü servisleri barýndýrýr.

Makinenizde entity framework deðilse. Aþaðýdaki iki komut ile yüklemeyi tamamlayýn vs terminal veya cmdtool:
```cmd
dotnet tool list -g
dotnet tool list
```

Aþaðýda ki komuttan kurumulu denetleyebilirsin:
```cmd
dotnet ef
```
EF kurulumu için aþaðýdaki komutlarý çalýþtýrýn:
```cmd
dotnet tool install dotnet-ef -g
```
Migrationlar þuan üretildi ama harici bir migration için aþaðýdaki komutu kullanýn -> migration-name örnektir:
```cmd
dotnet ef migrations add initial
```
Mevcuttaki migrationlarý db ile iþletebilmek için aþaðýdaki komutu çalýþtýrýn custom bir migration üretirsenizde iþlemi yenilemek gerekecektir:
```cmd
dotnet ef database update
```

Uygulama çalýþmaya Hazýr...



Varsayýlan db connector MsSQL'dir. Size uygun connector için deðiþim yapmanýz gerekebilir. DBName *AuthDemo* ve varsayýlan UID ve PWD kayýtlýdýr appsetting ayarlarýný yapýlandýrýn!

Uygulama Microservis mimarisi için çalýþýr ve içinde aþaðýda ki örnekleri bulabilirsiniz:

Daha fazla güvenlik önlemi için *asimetrik algoritma ile JWT .NetCore-Identity*
Rol tabanlý servis eriþimi
ModelMapper örneði
Model güvenliði amacý ile DTOs example
StartUp için *DataSeed*
Log yapýsý için file sistem saatlik loglama yapan *NLog*
Entegrasyon süreçleri için entegrasyona rol yapýsý ile ulaþýlan *Entegrasyon Servisi*
Asimetrik algoritmayý üreten bir *classLibrary* ->Private ve public key ile HMS mantýðý yürülmektedir.
Geliþmiþ yapýlar için *CustomPatinationLib*
CRUD Ýþlemleri için *PRODUCT API*
Referans Olaran Basic Proje *APPUI* microservisidir.

Proje ile ilgili detaylar ilgili microservis projelerinde bulunmakta...

	Ýyi bir referans ve örnek olmasý dileðiyle...		Emin YILMAZOÐLU -> Happy Codingg... :)