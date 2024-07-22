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

# Quartz ile sıralı job çalıştırma 
Quartz.NET ile sıralı olarak çalışacak işleri tanımlamak için bir iş tamamlandıktan sonra diğer işin çalıştırılmasını sağlayabilirsiniz. Bu senaryoda, bir iş tamamlandığında başka bir işi tetiklemek için IJobListener kullanabilir veya daha basit bir yaklaşımla, bir işin bitişinde diğer işin planlanmasını sağlayabilirsiniz.
# 1. Yöntem 
```csharp
public class Job1 : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Job1 çalıştırılıyor...");
        await Task.Delay(1000); // İşin gerçekleştirilme süresi (örnek olarak 1 saniye)
        Console.WriteLine("Job1 tamamlandı.");

        // Job2'yi tetikle
        IScheduler scheduler = context.Scheduler;
        IJobDetail job2 = JobBuilder.Create<Job2>()
                                    .WithIdentity("job2", "group1")
                                    .Build();

        ITrigger trigger2 = TriggerBuilder.Create()
                                          .WithIdentity("trigger2", "group1")
                                          .StartNow()
                                          .Build();

        await scheduler.ScheduleJob(job2, trigger2);
    }
}

public class Job2 : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Job2 çalıştırılıyor...");
        await Task.Delay(1000); // İşin gerçekleştirilme süresi (örnek olarak 1 saniye)
        Console.WriteLine("Job2 tamamlandı.");
    }
}
```
# 2. Yöntem

Quartz.NET ile işleri sıralı bir şekilde çalıştırmanın başka yöntemleri de vardır. Bunlardan biri, Job Chaining (İş Zincirleme) olarak bilinir. Quartz.NET ile işleri zincirlemek için bir JobListener kullanarak bir iş tamamlandığında başka bir işi çalıştırmak mümkündür.

JobListener Kullanarak İş Zincirleme
JobListener'ı Tanımlayın: İş tamamlandığında tetiklenecek bir iş dinleyicisi oluşturun.
İşleri ve Dinleyiciyi Scheduler'a Ekleyin: İşleri ve dinleyiciyi Scheduler'a ekleyin ve ayarlayın.

Bir iş tamamlandığında başka bir işi çalıştırmak için bir JobListener oluşturun.
```csharp
using Quartz;
using Quartz.Listener;
using System.Threading.Tasks;

public class JobChainingListener : JobListenerSupport
{
    private readonly IScheduler _scheduler;

    public JobChainingListener(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public override string Name => "JobChainingListener";

    public override async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
    {
        JobKey jobKey = context.JobDetail.Key;
        
        if (jobKey.Name == "job1" && jobKey.Group == "group1")
        {
            // Job1 tamamlandığında Job2'yi tetikle
            IJobDetail job2 = JobBuilder.Create<Job2>()
                                        .WithIdentity("job2", "group1")
                                        .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                                              .WithIdentity("trigger2", "group1")
                                              .StartNow()
                                              .Build();

            await _scheduler.ScheduleJob(job2, trigger2);
        }
    }
}
```
# Parametreli Job Çağırımı

Quartz.NET ile parametreli bir iş çalıştırmak mümkündür. Parametreli bir iş çalıştırmak için, işin IJobExecutionContext içindeki JobDataMap kullanılarak parametreleri almasını sağlayabilirsiniz. JobDataMap, işlerinize verileri geçirmenin bir yoludur ve bu harita içinde anahtar-değer çiftleri şeklinde veri saklayabilirsiniz.

1. İş Sınıfını Tanımlayın
Öncelikle, parametreleri alacak bir iş sınıfı oluşturun. Bu sınıf, IJob arayüzünü uygulamalıdır:
```csharp
using Quartz;
using System;
using System.Threading.Tasks;

public class ParametrizedJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // JobDataMap'ten parametreleri al
        JobDataMap dataMap = context.JobDetail.JobDataMap;
        string param1 = dataMap.GetString("param1");
        int param2 = dataMap.GetInt("param2");

        Console.WriteLine($"ParametrizedJob çalıştırılıyor... Param1: {param1}, Param2: {param2}");
        return Task.CompletedTask;
    }
}
```
2. Quartz Scheduler'ı Yapılandırın
Parametreli bir iş oluşturun ve gerekli parametreleri JobDataMap içine ekleyin:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Quartz.NET konfigürasyonunu ekleyin
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();

    // ParametrizedJob için JobDataMap ile birlikte bir job tanımla
    var jobKey = new JobKey("parametrizedJob", "group1");
    q.AddJob<ParametrizedJob>(opts => opts
        .WithIdentity(jobKey)
        .UsingJobData("param1", "Value1")
        .UsingJobData("param2", 42));

    // Job için bir trigger oluştur
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("trigger1", "group1")
        .StartNow());
});

// Quartz.NET işlerini başlatma
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

app.Run();
```
Açıklamalar
ParametrizedJob Sınıfı: Bu sınıf IJob arayüzünü uygular ve Execute metodunda JobDataMap kullanarak parametreleri alır.
Quartz Konfigürasyonu: builder.Services.AddQuartz metodu içinde, ParametrizedJob job'unu tanımlayın ve UsingJobData metodu ile parametreleri ekleyin. JobDataMap içinde parametreler anahtar-değer çiftleri olarak saklanır.
Bu şekilde, Quartz.NET ile parametreli işler oluşturabilir ve çalıştırabilirsiniz. JobDataMap, işlerinize veri geçirmenin esnek bir yolunu sağlar ve işlerinizin farklı durumlarda farklı davranışlar sergilemesini mümkün kılar.


# Cron ifadeleri (Cron Expressions),
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
1. Saniye: 0-59 arasında bir değer veya özel karakterler.
2. Dakika: 0-59 arasında bir değer veya özel karakterler.
3. Saat: 0-23 arasında bir değer veya özel karakterler.
4. Ayın günü: 1-31 arasında bir değer veya özel karakterler.
5. Ay: 1-12 arasında bir değer veya JAN-DEC şeklinde kısaltmalar.
6. Haftanın günü: 0-7 arasında bir değer (0 veya 7 = Pazar) veya MON-SUN şeklinde kısaltmalar.
7. Yıl (opsiyonel): 1970-2099 arasında bir değer veya özel karakterler.
Özel Karakterler
1. *: Herhangi bir değer.
2. ,: Belirtilen değerlerin listesi (örnek: 1,2,3).
3. -: Belirtilen aralık (örnek: 1-5).
4. /: Adım değeri (örnek: */5 her 5 dakikada bir).
5. ?: Belirli bir değer yok (ayın günü ve haftanın günü alanlarında kullanılır).
6. L: Son değer (ayın son günü için L, haftanın son günü için 7L).
7. W: En yakın hafta içi günü (ayın belirli bir gününe en yakın hafta içi günü).
8. #: Ayın belirli bir haftasında belirli bir gün (örnek: 2#1 ayın ilk Pazartesi günü).
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
