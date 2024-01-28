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
                    column.Item().Text("Piegādātājs: " + Address.CompanyName + Address.CompanyName);

                    column.Item().Text("Jurid.adrese: " + Address.CompanyName);

                    column.Item().Text("Bankas nosaukums: " + Address.Street);

                    column.Item().Text("Bankas kods: " + Address.Street);

                    column.Item().Text("Tālr./fakss: " + Address.Street);
                });

                row.RelativeItem().Column(column =>
                {
                    column.Spacing(2);
                    column.Item().Text($"Reģ. Nr.: {Address.City}, {Address.State}");
                    column.Item().Text("PVN Nr: " + Address.Email);
                    column.Item().Text("Konts EUR: " + Address.Phone);
                });
            });
        }


    }
}
