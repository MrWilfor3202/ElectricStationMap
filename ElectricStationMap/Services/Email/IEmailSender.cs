namespace ElectricStationMap.Services.Email
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string title, string message);
    }
}
