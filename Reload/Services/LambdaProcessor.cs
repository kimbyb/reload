namespace Reload.Services;

public class LambdaProcessor
{
    public int Process(int value, Func<int, int> operation)
    {
        return operation(value);
    }
}