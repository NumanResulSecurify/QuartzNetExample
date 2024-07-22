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
