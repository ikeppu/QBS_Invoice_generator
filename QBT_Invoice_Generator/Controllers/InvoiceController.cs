using Microsoft.AspNetCore.Mvc;
using QBT_Invoice_Generator.Services;

namespace QBT_Invoice_Generator.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceGeneratorService _invoiceService;

        public InvoiceController(InvoiceGeneratorService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<ActionResult> GenerateInvoice()
        {
            var pdfBytes = await _invoiceService.GenerateInvoice();

            return File(pdfBytes, "application/pdf", "output.pdf"); ;
        }
    }
}
