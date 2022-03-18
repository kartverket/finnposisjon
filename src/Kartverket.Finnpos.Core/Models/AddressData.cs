namespace Kartverket.Finnpos.Core.Models;

public class AddressData
{
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string Place { get; set; }
    public string Municipality { get; set; }
    public Coordinates Coordinates { get; set; }
    public double DistanceFromPosition { get; set; }
}
