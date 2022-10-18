using System.Reflection;
using CpolarAutoConnect.Schedule.Job;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

// log size limit 100M
const long FileSizeLimitBytes = 1L * 1024 * 1024 * 100;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(path: $"{nameof(CpolarAutoConnect.Schedule)}.log",
        fileSizeLimitBytes: FileSizeLimitBytes,
        rollOnFileSizeLimit: true)
    .CreateLogger();

// set quartz log directly, you can use this if not using topshelf
LogContext.SetCurrentLogProvider(new LoggerFactory().AddSerilog(Log.Logger));

StdSchedulerFactory factory = new();
IScheduler scheduler = await factory.GetScheduler();

// and start it off
await scheduler.Start();

// define the job and tie it to our HelloJob class
IJobDetail job = JobBuilder.Create<CpolarVsCodeSshJob>()
    .WithIdentity(nameof(CpolarVsCodeSshJob), "cpolar")
    .Build();

// Trigger the job to run now, and then repeat every 10 seconds
ITrigger everyMinuteTrigger = TriggerBuilder.Create()
    .WithIdentity("everyMinuteTrigger", "cpolar")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(1)
        .RepeatForever())
    .Build();


// Tell quartz to schedule the job using our trigger
await scheduler.ScheduleJob(job, everyMinuteTrigger);



// some sleep to show what's happening
await Task.Delay(TimeSpan.FromSeconds(10));

// and last shut down the scheduler when you are ready to close your program
await scheduler.Shutdown();