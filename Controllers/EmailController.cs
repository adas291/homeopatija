using homeopatija.Repos;
using Microsoft.AspNetCore.Mvc;

namespace homeopatija.Controllers;



public class EmailController : ControllerBase
{
  private readonly EmailRepo _emailRepo;

  public EmailController(EmailRepo emailRepo)
  {
    _emailRepo = emailRepo;
  }

  [Route("/email")]
  public async Task<IActionResult> Index() 
  {
    await _emailRepo.SendEmail("to@example.com", "wtff", "new body world");
    return Ok("sent or not idk");
  }

}