namespace Bill
{
    public interface IBillCalculator
    {
        IEnumerable<int> CalculateSplitAmount(int totalAmount, decimal tipRate, int numberOfPeople);
    }
}