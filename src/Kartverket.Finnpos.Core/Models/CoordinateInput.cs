namespace Kartverket.Finnpos.Core.Models;

public class CoordinateInput
{
    public CoordinateInput(string firstInput, string secondInput)
    {
        FirstInput = firstInput;
        SecondInput = secondInput;
    }

    public string FirstInput { get; }
    public string SecondInput { get; }

    public bool IsParsable()
    {
        return !string.IsNullOrWhiteSpace(FirstInput) && !string.IsNullOrWhiteSpace(SecondInput);
    }
}
