namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; private set; } = default!;
    public string CardNumber { get; private set; } = default!;
    public string Expiration { get; private set; } = default!;
    public string CVV { get; private set; } = default!;
    public int PaymentMethod { get; private set; } = default!;

    protected Payment()
    {
        
    }

    private Payment(string? cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string? cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cVV);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);
    }


}
