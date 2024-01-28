using QBT_Invoice_Generator.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data.Common;
using System.Text;

namespace QBT_Invoice_Generator.Document
{
    public class AddressComponent : QuestPDF.Infrastructure.IComponent
    {
        private string Title { get; }
        private Address Address { get; }

        public AddressComponent(string title, Address address)
        {
            Title = title;
            Address = address;
        }

        public void Compose(IContainer container)
        {
            container.Row(row => 
            {
                row.RelativeItem().Column(column => 
                {
                    column.Spacing(2);
                    column.Item().Text("Piegādātājs: " + Address.CompanyName + Address.CompanyName).FontFamily("Arial");

                    column.Item().Text("Jurid.adrese: " + Address.CompanyName).FontFamily("Arial");

                    column.Item().Text("Bankas nosaukums: " + Address.Street).FontFamily("Arial");

                    column.Item().Text("Bankas kods: " + Address.Street).FontFamily("Arial");

                    column.Item().Text("Tālr./fakss: " + Address.Street).FontFamily("Arial");
                });

                row.RelativeItem().Column(column =>
                {
                    column.Spacing(2);
                    column.Item().Text($"Reģ. Nr.: {Address.City}, {Address.State}").FontFamily("Arial");
                    column.Item().Text("PVN Nr: " + Address.Email).FontFamily("Arial");
                    column.Item().Text("Konts EUR: " + Address.Phone).FontFamily("Arial");
                });
            });
        }


    }
}
