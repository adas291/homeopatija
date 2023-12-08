using System.Net;
using System.Net.Mail;

namespace homeopatija.Repos;



public class EmailRepo
{

  private readonly IConfiguration _config;
  private readonly ILogger _logger;
  public EmailRepo(IConfiguration config, ILogger<EmailRepo> logger)
  {
    _config = config;
    _logger = logger;
  }

  public async Task<bool> SendEmail(string address, string subject, string body)
  {
    using var client = new SmtpClient(_config["Email:Host"], 2525)
    {
      Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
      EnableSsl = true
    };
    
    try
    {
      await client.SendMailAsync("from@example.com", address, subject, body);
      _logger.LogInformation("Sent");
      return true;
    }
    catch (Exception) { }

    return false;
  }
}