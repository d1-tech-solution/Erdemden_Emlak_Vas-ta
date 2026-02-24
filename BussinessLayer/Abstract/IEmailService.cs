namespace BussinessLayer.Abstract;

public interface IEmailService
{
    Task SendOfferNotificationEmailAsync(string toEmail, string customerName, string vehicleInfo, decimal minPrice, decimal maxPrice);
    Task SendNewQuoteNotificationToAdminAsync(string customerName, string vehicleInfo, string? customerPhone, string? customerEmail);
    Task SendQuoteResponseNotificationToAdminAsync(string customerName, string vehicleInfo, bool accepted);
}
