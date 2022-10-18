using Quartz;

namespace CpolarAutoConnect.Schedule.Job;

public class CpolarVsCodeSshJob: IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"{nameof(CpolarVsCodeSshJob)} is running");
        });
    }
}