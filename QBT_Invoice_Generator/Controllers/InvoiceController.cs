using Microsoft.AspNetCore.Mvc;
using QBT_Invoice_Generator.Document;
using QBT_Invoice_Generator.Services;
using System.IO;
using QuestPDF.Previewer;
using QuestPDF.Fluent;
using System.Text;
using QBT_Invoice_Generator.Models;

namespace QBT_Invoice_Generator.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {

        [HttpGet]
        [Route("generate")]
        public  ActionResult GenerateInvoice([FromQuery] string password)
        {
            Console.WriteLine($"--> Password {password}");

            if(password == "query")
            {
                var model = InvoiceDocumentDataSource.GetInvoiceDetails();

                var document = new InvoiceDocument(model);

                var result = document.GeneratePdf();

                return File(result, "application/pdf", "invoice.pdf");
            }

            return Ok("Hello world");
        }
    }
}
