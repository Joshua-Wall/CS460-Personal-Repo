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


    // 3

    // 4

    // 5 

    // 6
}
