using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kartverket.Finnpos.Core.Models;
using Kartverket.Finnpos.Core.Services;
using Xunit;

namespace Kartverket.Finnpos.Core.Tests.Services;

public class CoordinateInputParserTests
{
    [Fact]
    public void GetCoordinatesTest()
    {
        // (1,2) (2,1) Normal/swapped order
        CoordinateInputParser.GetCoordinates(new CoordinateInput("1", "2")).Count.Should().Be(2);

        // (1,2) (2,1) (-1,2) (1,-2) (-2,1) (2,-1) Normal/swapped order with/without inverted X/Y
        CoordinateInputParser.GetCoordinates(new CoordinateInput("1", "1"), true).Count.Should().Be(6);
    }

    [Fact]
    public void IsDecimalDegreesTest()
    {
        CoordinateInputParser.IsDecimalDegrees(new List<double> { 60.1010 }).Should().BeTrue();
    }

    [Fact]
    public void IsDegreesDecimalMinutesTest()
    {
        CoordinateInputParser.IsDegreesMinutes(new List<double> { 60, 10.10 }).Should().BeTrue();
    }

    [Fact]
    public void IsDegreesMinutesSecondsTest()
    {
        CoordinateInputParser.IsDegreesMinutesSeconds(new List<double> { 60, 10, 10 }).Should().BeTrue();
    }

    [Fact]
    public void GetNumberUnitsTest()
    {
        CoordinateInputParser.GetNumberUnits("60 10 15").ElementAt(0).Should().Be(60);
        CoordinateInputParser.GetNumberUnits("60 10 15").ElementAt(1).Should().Be(10);
        CoordinateInputParser.GetNumberUnits("60 10 15").ElementAt(2).Should().Be(15);
    }
}
