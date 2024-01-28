using System.Text;

namespace QBT_Invoice_Generator.Services
{
    public class InvoiceGeneratorService
    {      
        public void GenerateInvoice()
        {
            string filePath = Environment.CurrentDirectory + "/Html/Test.html";
   
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }
         
            var html = File.ReadAllText(filePath, Encoding.UTF8);

            html = html.Replace("{{Gredding}}", "<span style='color: red'>CusTom Hello World</span>");

        }
    }
}
