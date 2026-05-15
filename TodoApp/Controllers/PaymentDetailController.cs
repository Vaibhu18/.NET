using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PaymentDetailController : ControllerBase
{
    private readonly AppDbContext db;

    public PaymentDetailController(AppDbContext context)
    {
        db = context;
    }

    // GET: api/PaymentDetail
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetAllPayments()
    {
        var payments = await db.PaymentDetails.ToListAsync();

        return Ok(payments);
    }

    // POST : api/PaymentDetail
    [HttpPost]
    public async Task<ActionResult<IEnumerable<PaymentDetail>>> CreatePaymentDetails(PaymentDetail payment)
    {
        await db.PaymentDetails.AddAsync(payment);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllPayments), new
        {
            id = payment.PaymentId
        }, payment);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentBtId(int id)
    {
        var payment = await db.PaymentDetails.FirstOrDefaultAsync(p => p.PaymentId == id);
        return Ok(payment);
    }
}