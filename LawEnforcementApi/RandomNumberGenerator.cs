namespace LawEnforcementApi;

public static class RandomNumberGenerator
{
    private static Random Generator = new Random();

    public static int GetRandomFromRange(int lowerBound, int upperBound)
    {
        return Generator.Next(lowerBound, upperBound);
    }
}
