namespace AuthService.Application.Helpers;

public class RandomGenerator
{
    public static int GenerateRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
