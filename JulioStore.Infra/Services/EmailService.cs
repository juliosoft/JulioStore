using JulioStore.Domain.StoreContext.Services;

namespace JulioStore.Infra.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}