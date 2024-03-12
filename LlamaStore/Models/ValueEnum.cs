namespace LlamaStore.Models;

public abstract record ValueEnum<T>
{
    public T Value { get; internal init; }

    internal ValueEnum(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
