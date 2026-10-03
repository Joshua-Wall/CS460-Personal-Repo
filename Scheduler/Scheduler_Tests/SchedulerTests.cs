using System.Net.WebSockets;
using System.Reflection;
using NUnit.Framework;
using Scheduler_Code.Models;

namespace Scheduler_Tests;

// Need 6 uniot tests for the assignmet
public class ScheduleCalculatorTests
{
    [Test]
    // 1 This one tests spaces SHOULD WORK!!
    public void ParseBuyTimes_ValidInput_ReturnsExpectedBusyTimes()
    {
        var result = ScheduleCalculator.ParseBusyTimes("M8 T12 W9");

        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Does.Contain(("M", 8)));
        Assert.That(result, Does.Contain(("T", 12)));
        Assert.That(result, Does.Contain(("W", 9)));
    }
    // 2 Test if the user has put nothing. "" or some whistepace "   "
    [TestCase("")]
    [TestCase("      ")]
    public void ParseBuyTimes_EmptyOrWhitespace_ReturnsNoBusyTimse(string input)
    {
        var result = ScheduleCalculator.ParseBusyTimes(input);

        Assert.That(result, Is.Empty);
    }

    // 3 It doesn't matter if you used lowercase since it's translated with .Upper
    [Test]
    public void ParseBuyTimes_MixedCaseDays_AreAccepted()
    {
        var result = ScheduleCalculator.ParseBusyTimes("m8 t12 sU14 sa9");

        Assert.That(result, Has.Count.EqualTo(4));
        Assert.That(result, Does.Contain(("M", 8)));
        Assert.That(result, Does.Contain(("T", 12)));
        Assert.That(result, Does.Contain(("SU", 14)));
        Assert.That(result, Does.Contain(("SA", 9)));
    }

    // 4 If you put the same time mulitple times it just counts for that one time
    [Test]
    public void ParseBuyTimes_DuplicateEntries_CountOnce()
    {
        var result = ScheduleCalculator.ParseBusyTimes("M8 m8 m8");

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Does.Contain(("M", 8)));
    }

    // 5 If you didn't follow the rules it wont' count.
    [Test]
    public void ParseBusyTimes_InvalidEntries_AreIgnored()
    {
        // Th10 Clash of clans??? unknown day, M18 and M7 are all outside that 8-16
        var result = ScheduleCalculator.ParseBusyTimes("Th10 M18 M7 X9 Mfoo M 8 F16");

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Does.Contain(("F", 16)));
    }


    // 6
    [Test]
    public void CalculateFreeTimes_WeekdaysOnly_ExcludesBusyAndWeekendSlots()
    {
        var free = ScheduleCalculator.CalculateFreeTimes("M8 T12 SU10", includeweekends: false);

        // If we do 5 days a week and we have 9 hours 8,9,10,11,12,13,14,15,16
        // That means minus the 2 weekdays then we should ahve 43
        Assert.That(free, Has.Count.EqualTo(43));
        Assert.That(free, Does.Not.Contain(("M", 8)));
        Assert.That(free, Does.Not.Contain(("T", 12)));
        Assert.That(free, Does.Contain(("M", 9)));
        Assert.That(free.Any(slot => slot.Day == "SU" || slot.Day == "SA"), Is.False);
    }
}
