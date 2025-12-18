namespace Reload.Services;

public class GenericCalculator<T>
{
    public T Echo(T value)
    {
        return value;
    }
}