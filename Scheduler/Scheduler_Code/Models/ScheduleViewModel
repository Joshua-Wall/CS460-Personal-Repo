namespace Scheduler_Code.Models;

public class ScheduleViewModel
{
    public string BusyTimes { get; set; } = "";
    public bool IncludeWeekends { get; set; }

    public List<string> Days { get; set; } = new();
    public List<int> Hours { get; } = Enumerable.Range(8, 9).ToList();
    public HashSet<(string Day, int Hour)> FreeTimes { get; set; } = new();
    public bool HasResults { get; set; }
}