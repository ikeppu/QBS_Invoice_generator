using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace QBT_Invoice_Generator.Services
{
    public class InvoiceGeneratorService
    {      
        //public byte[] GenerateInvoice()
        //{
        //    string filePath = Directory.GetCurrentDirectory() + "/Html/Test.html";
        //    // Check if the file exists
        //    if (!File.Exists(filePath))
        //    {
        //        throw new FileNotFoundException($"The file '{filePath}' does not exist.");
        //    }

        //    // Read the HTML file content into a byte array
        //    byte[] fileBytes;
        //    using (FileStream fileStream = File.OpenRead(filePath))
        //    {
        //        fileBytes = new byte[fileStream.Length];
        //        fileStream.Read(fileBytes, 0, (int)fileStream.Length);
        //    }

        //    return fileBytes;
        //}        
        
        public async Task<byte[]> GenerateInvoice()
        {
            string filePath = Directory.GetCurrentDirectory() + "/Html/Test.html";
   
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }
         
            var html = File.ReadAllText(filePath, Encoding.UTF8);

            html = html.Replace("{{Gredding}}", "<span style='color: red'>CusTom Hello World</span>");

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(html);

            return pdfBytes;
        }
    }
}
