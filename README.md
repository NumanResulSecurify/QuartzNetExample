# QuartzNetExample


Quartz.NET, bir .NET platformu için açık kaynaklı bir iş zamanlayıcısıdır. Bu kütüphane, arka planda zamanlanmış görevler çalıştırmak isteyen uygulamalar için idealdir. İşte Quartz.NET'in her ayrıntısını kapsayan bir açıklama:

Genel Bakış
Quartz.NET, Java'daki Quartz Scheduler'ın .NET uyarlamasıdır. Zamanlanmış görevlerin kolayca oluşturulmasına, planlanmasına ve yönetilmesine olanak tanır.

Temel Kavramlar
Job (İş):

İş, belirli bir görevi yerine getiren bir sınıftır.
IJob arayüzünü uygulayan bir sınıf olarak tanımlanır.
```csharp
public class MyJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Hello, Quartz.NET!");
        return Task.CompletedTask;
    }
}
```
Trigger (Tetikleyici):

İşlerin ne zaman çalıştırılacağını belirler.
Bir iş için bir veya daha fazla tetikleyici olabilir.
En yaygın kullanılan tetikleyici türü CronTrigger ve SimpleTrigger'dır.
```csharp
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();
Scheduler (Zamanlayıcı):
```
İşleri ve tetikleyicileri yöneten merkez bileşendir.
IScheduler arayüzünü kullanarak işler ve tetikleyiciler zamanlanır.
```csharp
IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
await scheduler.Start();

IJobDetail job = JobBuilder.Create<MyJob>()
    .WithIdentity("job1", "group1")
    .Build();

await scheduler.ScheduleJob(job, trigger);
```
Kurulum
Quartz.NET, NuGet paketi olarak mevcuttur ve şu şekilde yüklenir:

```csharp
dotnet add package Quartz
```
Quartz.NET ile Zamanlanmış Görev Oluşturma Adımları
Scheduler Oluşturma ve Başlatma:

```csharp
IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
await scheduler.Start();
```
Job Tanımlama:

```csharp
IJobDetail job = JobBuilder.Create<MyJob>()
    .WithIdentity("job1", "group1")
    .Build();
```
Trigger Tanımlama:

```csharp
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();
```
Job'u Zamanlayıcıya Ekleme:

```csharp
await scheduler.ScheduleJob(job, trigger);
```
İş Akışı
Quartz.NET'in iş akışı, şu adımlardan oluşur:

Zamanlayıcı oluşturulur ve başlatılır.
Bir iş tanımlanır.
Tetikleyici oluşturulur.
İş ve tetikleyici zamanlayıcıya eklenir.
Zamanlayıcı, belirtilen tetikleyiciye göre işleri yürütür.
Gelişmiş Özellikler
Kalıcı Depolama:

İşlerin ve tetikleyicilerin bilgileri bir veritabanında saklanabilir.
Böylece, uygulama yeniden başlatıldığında işler kaldığı yerden devam eder.
Quartz.NET Konfigürasyonu:

quartz.config dosyası veya programatik olarak ayarlanabilir.

Örnek konfigürasyon dosyası:

```csharp
quartz.scheduler.instanceName = QuartzScheduler
quartz.threadPool.threadCount = 3
quartz.jobStore.type = Quartz.Simpl.RAMJobStore, Quartz
```
Misfire Instrüksiyonları:

Tetikleyicinin bir "misfire" durumu yaşaması durumunda nasıl davranması gerektiğini belirler.
Misfire, tetikleyicinin planlandığı zamanda tetiklenememesi durumudur.
Listener ve Plugin'ler:

Job ve Trigger olayları için listener'lar eklenebilir.
Quartz.NET, çeşitli hazır plugin'ler sunar (örneğin, tarihçeleme, job management, vb.)
Örnek Senaryo: Veritabanı Tabanlı Quartz.NET
Bir veritabanı kullanarak Quartz.NET'i konfigüre etmek için, öncelikle gerekli veritabanı tablolarını oluşturmalısınız. Quartz.NET, çeşitli veritabanları için SQL betikleri sağlar. Ardından, aşağıdaki gibi bir konfigürasyon yapılabilir:

```csharp
quartz.scheduler.instanceName = QuartzScheduler
quartz.scheduler.instanceId = AUTO

quartz.jobStore.type = Quartz.Impl.AdoJobStore.JobStoreTX, Quartz
quartz.jobStore.useProperties = true
quartz.jobStore.dataSource = default
quartz.jobStore.tablePrefix = QRTZ_
quartz.jobStore.driverDelegateType = Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz

quartz.dataSource.default.provider = SqlServer
quartz.dataSource.default.connectionString = Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
```
Kaynaklar
Quartz.NET Resmi Sitesi
Quartz.NET GitHub Deposu
Quartz.NET Dokümantasyonu
Bu bilgilerle, Quartz.NET kullanarak zamanlanmış görevler oluşturabilir ve yönetebilirsiniz. Detaylı konfigürasyon ve gelişmiş özellikler için dokümantasyonu incelemeniz faydalı olacaktır.
```csharp
IJobDetail job = JobBuilder.Create<MyJob>()
    .WithIdentity("job1", "group1")
    .Build(); şeklinde bir job tanımlıyoruz dedin bu jobın ne iş yapacağını nerede yazıyoruz birde withidentity ne iş yapıyor
```

IJobDetail ile job tanımlarken, job'ın ne iş yapacağını belirten kod, job sınıfının içinde bulunur. İş sınıfınız IJob arayüzünü uygulayan bir sınıf olmalıdır ve bu sınıfın Execute yöntemi içinde iş mantığı tanımlanır.

Job'ın Ne İş Yapacağını Belirleme
İş sınıfı, IJob arayüzünü uygular ve Execute yönteminde iş mantığını içerir. Örneğin:

```csharp
public class MyJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // İş mantığı burada tanımlanır
        Console.WriteLine("Hello, Quartz.NET!");
        return Task.CompletedTask;
    }
}
```
Yukarıdaki örnekte, MyJob sınıfı IJob arayüzünü uygulamakta ve Execute yönteminde bir mesaj yazdırmaktadır. Bu Execute yöntemi, job tetiklendiğinde çalışacaktır.

WithIdentity Ne İşe Yarar?
WithIdentity yöntemi, job veya trigger'a benzersiz bir kimlik atamak için kullanılır. Bu kimlik, job veya trigger'ın benzersiz bir şekilde tanımlanmasını sağlar. WithIdentity iki parametre alır:

Name (İsim): İş veya tetikleyicinin benzersiz adıdır.
Group (Grup): İş veya tetikleyicinin ait olduğu grup adıdır. Aynı isimli işler veya tetikleyiciler farklı gruplarda bulunabilir.
Bu, işleri ve tetikleyicileri daha kolay yönetmeyi sağlar, çünkü her bir işin veya tetikleyicinin benzersiz bir kimliği olur.

Tam Bir Örnek
Aşağıda, Quartz.NET kullanarak bir job'ın nasıl tanımlandığı ve zamanlandığına dair tam bir örnek verilmiştir:

```csharp
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

public class MyJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // İş mantığı burada tanımlanır
        Console.WriteLine("Hello, Quartz.NET!");
        return Task.CompletedTask;
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Scheduler oluşturma ve başlatma
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();

        // Job tanımlama
        IJobDetail job = JobBuilder.Create<MyJob>()
            .WithIdentity("job1", "group1") // Job kimliği
            .Build();

        // Trigger tanımlama
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1") // Trigger kimliği
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();

        // Job'u zamanlayıcıya ekleme
        await scheduler.ScheduleJob(job, trigger);

        // Uygulamanın bitmesini önlemek için bekletme
        await Task.Delay(TimeSpan.FromSeconds(60));

        // Scheduler'ı durdurma
        await scheduler.Shutdown();
    }
}
```
Bu örnekte:

MyJob sınıfı, IJob arayüzünü uygular ve Execute yönteminde "Hello, Quartz.NET!" mesajını yazdırır.
Program sınıfında, zamanlayıcı (scheduler) oluşturulur ve başlatılır.
Job tanımlanır ve WithIdentity yöntemiyle benzersiz bir kimlik atanır.
Trigger tanımlanır ve yine WithIdentity yöntemiyle benzersiz bir kimlik atanır.
Job ve trigger zamanlayıcıya eklenir ve zamanlayıcı başlatılır.
Uygulama 60 saniye boyunca çalışır ve ardından zamanlayıcıyı durdurur.
Bu sayede, Quartz.NET kullanarak zamanlanmış görevler oluşturabilir ve yönetebilirsiniz.

#Cron ifadeleri (Cron Expressions),
 belirli zaman aralıklarında görevlerin çalıştırılması için yaygın olarak kullanılan bir zamanlama mekanizmasıdır. Bu ifadeler, bir dizi zaman birimini kullanarak belirli zamanlarda, günlerde veya tarihlerde görevlerin çalıştırılmasını sağlar. Cron ifadeleri genellikle Unix tabanlı sistemlerde kullanılır, ancak Quartz.NET gibi kütüphanelerle .NET ortamında da kullanılabilir.

Cron İfadesi Sözdizimi
Cron ifadesi genellikle 6 veya 7 alan içerir ve her alan bir zaman birimini temsil eder:

```
* * * * * * *
| | | | | | |
| | | | | | +--- Hafta günü (0-7) (0 veya 7 = Pazar)
| | | | | +----- Ay (1-12)
| | | | +------- Ayın günü (1-31)
| | | +--------- Saat (0-23)
| | +----------- Dakika (0-59)
| +------------- Saniye (0-59)
+--------------- Yıl (opsiyonel) (1970-2099)
```
Alanların Anlamları
Saniye: 0-59 arasında bir değer veya özel karakterler.
Dakika: 0-59 arasında bir değer veya özel karakterler.
Saat: 0-23 arasında bir değer veya özel karakterler.
Ayın günü: 1-31 arasında bir değer veya özel karakterler.
Ay: 1-12 arasında bir değer veya JAN-DEC şeklinde kısaltmalar.
Haftanın günü: 0-7 arasında bir değer (0 veya 7 = Pazar) veya MON-SUN şeklinde kısaltmalar.
Yıl (opsiyonel): 1970-2099 arasında bir değer veya özel karakterler.
Özel Karakterler
*: Herhangi bir değer.
,: Belirtilen değerlerin listesi (örnek: 1,2,3).
-: Belirtilen aralık (örnek: 1-5).
/: Adım değeri (örnek: */5 her 5 dakikada bir).
?: Belirli bir değer yok (ayın günü ve haftanın günü alanlarında kullanılır).
L: Son değer (ayın son günü için L, haftanın son günü için 7L).
W: En yakın hafta içi günü (ayın belirli bir gününe en yakın hafta içi günü).
#: Ayın belirli bir haftasında belirli bir gün (örnek: 2#1 ayın ilk Pazartesi günü).
Örnek Cron İfadeleri
Her dakika:

```
* * * * * *
```
Her gün saat 12:00'de:

```
0 0 12 * * ?
```
Her ayın ilk günü saat 00:00'da:

```
0 0 0 1 * ?
```
Her pazartesi ve çarşamba saat 10:15'te:

```
0 15 10 ? * MON,WED
```
Her 5 dakikada bir:

```
0 */5 * * * ?
```
