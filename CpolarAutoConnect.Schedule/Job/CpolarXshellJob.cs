using Quartz;

namespace CpolarAutoConnect.Schedule.Job;

public class CpolarXshellJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.Run(() => { });
    }
}