namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    public string Value { get; }

    public const int DefaultLength = 5;

    private OrderName(string value) => Value = value;

    public static OrderName Of (string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new OrderName(value);
    }
}
