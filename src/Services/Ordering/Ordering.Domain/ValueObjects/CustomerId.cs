namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => this.Value = value;

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot Be empty.");
        }

        return new CustomerId(value);
    }

}
