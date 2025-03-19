using System;
using System.Timers; // 明确使用 System.Timers 命名空间

public class TickEventArgs : EventArgs
{
    public DateTime CurrentTime { get; set; }
}

public class AlarmEventArgs : EventArgs
{
    public DateTime AlarmTime { get; set; }
}

public class AlarmClock
{
    private readonly System.Timers.Timer _timer; 
    public DateTime CurrentTime { get; private set; }
    public DateTime AlarmTime { get; set; }

    public event EventHandler<TickEventArgs> Tick;
    public event EventHandler<AlarmEventArgs> Alarm;

    public AlarmClock(DateTime initialTime)
    {
        CurrentTime = initialTime;
        _timer = new System.Timers.Timer(1000); 
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true; 
        _timer.Enabled = true;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        CurrentTime = CurrentTime.AddSeconds(1);
        Tick?.Invoke(this, new TickEventArgs { CurrentTime = CurrentTime });

        if (CurrentTime >= AlarmTime)
        {
            Alarm?.Invoke(this, new AlarmEventArgs { AlarmTime = AlarmTime });
        }
    }
}

class Program
{
    static void Main()
    {
        var alarmClock = new AlarmClock(new DateTime(2024, 1, 1, 8, 0, 0));
        alarmClock.AlarmTime = alarmClock.CurrentTime.AddSeconds(5);

        alarmClock.Tick += (sender, e) =>
        {
            Console.WriteLine($"Tick: {e.CurrentTime:HH:mm:ss}");
        };

        alarmClock.Alarm += (sender, e) =>
        {
            Console.WriteLine($"\nALARM! Time reached: {e.AlarmTime:HH:mm:ss}");
        };

        Console.WriteLine("闹钟运行中，按任意键退出...");
        Console.ReadKey();
    }
}