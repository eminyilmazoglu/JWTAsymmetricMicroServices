

AuthServr.API varsay�lan projedir. T�m rol ve auth k�sm� ile ilgili i�lemlerin y�r�t�ld��� servisleri bar�nd�r�r.

Makinenizde entity framework de�ilse. A�a��daki iki komut ile y�klemeyi tamamlay�n vs terminal veya cmdtool:
```cmd
dotnet tool list -g
dotnet tool list
```

A�a��da ki komuttan kurumulu denetleyebilirsin:
```cmd
dotnet ef
```
EF kurulumu i�in a�a��daki komutlar� �al��t�r�n:
```cmd
dotnet tool install dotnet-ef -g
```
Migrationlar �uan �retildi ama harici bir migration i�in a�a��daki komutu kullan�n -> migration-name �rnektir:
```cmd
dotnet ef migrations add initial
```
Mevcuttaki migrationlar� db ile i�letebilmek i�in a�a��daki komutu �al��t�r�n custom bir migration �retirsenizde i�lemi yenilemek gerekecektir:
```cmd
dotnet ef database update
```

Uygulama �al��maya Haz�r...



Varsay�lan db connector MsSQL'dir. Size uygun connector i�in de�i�im yapman�z gerekebilir. DBName *AuthDemo* ve varsay�lan UID ve PWD kay�tl�d�r appsetting ayarlar�n� yap�land�r�n!

Uygulama Microservis mimarisi i�in �al���r ve i�inde a�a��da ki �rnekleri bulabilirsiniz:

Daha fazla g�venlik �nlemi i�in *asimetrik algoritma ile JWT .NetCore-Identity*
Rol tabanl� servis eri�imi
ModelMapper �rne�i
Model g�venli�i amac� ile DTOs example
StartUp i�in *DataSeed*
Log yap�s� i�in file sistem saatlik loglama yapan *NLog*
Entegrasyon s�re�leri i�in entegrasyona rol yap�s� ile ula��lan *Entegrasyon Servisi*
Asimetrik algoritmay� �reten bir *classLibrary* ->Private ve public key ile HMS mant��� y�r�lmektedir.
Geli�mi� yap�lar i�in *CustomPatinationLib*
CRUD ��lemleri i�in *PRODUCT API*
Referans Olaran Basic Proje *APPUI* microservisidir.

Proje ile ilgili detaylar ilgili microservis projelerinde bulunmakta...

	�yi bir referans ve �rnek olmas� dile�iyle...		Emin YILMAZO�LU -> Happy Codingg... :)