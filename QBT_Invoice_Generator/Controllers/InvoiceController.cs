using Microsoft.AspNetCore.Mvc;
using QBT_Invoice_Generator.Document;
using QBT_Invoice_Generator.Services;
using System.IO;
using QuestPDF.Previewer;
using QuestPDF.Fluent;

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
            var filePath = "invoice.pdf";
            _invoiceService.GenerateInvoice();
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model);
            var result = document.GeneratePdf();
            return File(result, "application/pdf", "invoice.pdf");
        }
    }
}
