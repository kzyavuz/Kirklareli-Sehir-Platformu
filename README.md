# kirklareliSehir Platformu.
Projeyi indirip çalıstırmak istediğinizde kirklareliSehir.bak dosyasını mssql servere eklediğiniz taktirde hiçbirsey yapmanıza gerek kalmadan projeyi kullanabilirsiniz. Admin paneline girmke için /adminlogin/login url sine gidin. Kullanıcı adı: a, sifre a'dır.
Üye paneline giris yapmak için kayıt olusturabilir ve ya sifre: b kullanıcı adi : b yi kullanabilirsiniz.

Kendiniz veri tabanını olusturmak işterseniz package manager konsoluna update-database yazarak olusturulan migrationı database yansıtabilirsiniz. 
Olusan db de AspNetRoles tablosuna veri girisi yapmanız gerekiyor. Aksi taktirde çalısmıyacaktır.

![image](https://github.com/user-attachments/assets/37ab0182-b334-4c38-aa5e-0081f806aaae)
Name alanını ve NormalizeName alanını girmeniz yeterlidir. Member kaydı yani kullanıcı kaydı olusturduktan sonra AspNetUserRoles tablosundan rol İd sini admin rolü ile değistirerek admin paneline ulasabilirsniz.

Admin panelinde her alanı doldurursanız bu platformun ne kadar kapsamlı bir çalısma olduğunu görebilirsiniz.
Sehir platformu için yapılmıs olmaktadır. Kodları sizlerle paylasmaktan mutluluk duyarım.
