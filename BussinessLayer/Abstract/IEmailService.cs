namespace BussinessLayer.Abstract;

public interface IEmailService
{
    Task SendOfferNotificationEmailAsync(string toEmail, string customerName, string vehicleInfo, decimal minPrice, decimal maxPrice);
    Task SendQuoteReceivedConfirmationEmailAsync(string toEmail, string customerName, string vehicleInfo);
    Task SendNewQuoteNotificationToAdminAsync(string customerName, string vehicleInfo, string? customerPhone, string? customerEmail);
    Task SendQuoteResponseNotificationToAdminAsync(string customerName, string vehicleInfo, bool accepted);
    Task SendPasswordResetCodeEmailAsync(string toEmail, string customerName, string resetCode);
}
