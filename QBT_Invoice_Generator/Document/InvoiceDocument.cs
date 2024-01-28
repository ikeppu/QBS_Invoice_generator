using QBT_Invoice_Generator.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data.Common;

namespace QBT_Invoice_Generator.Document
{
    public class InvoiceDocument : IDocument
    {
        public InvoiceModel Model { get; }

        public InvoiceDocument(InvoiceModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(20);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).Bold().FontFamily("Arial");

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().AlignCenter().Text("Rēķins Nr.11-6/2022")
                        .Style(titleStyle)
                        .FontFamily("Arial");
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);
               

                column.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Border(1)
                        .Padding(5)
                        .Component(new AddressComponent("", Model.SellerAddress));
                });

                column.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Border(1)
                        .Padding(5)
                        .Component(new AddressComponent("", Model.CustomerAddress));
                });

                column.Item().Text("Apmaksas termiņš: 14.06.2022").FontFamily("Arial");
                column.Item().Text("Apmaksas veids: Bankas pārskaitījums").FontFamily("Arial");

                column.Item().Element(ComposeTable);

                var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
                var taxAmount = Math.Round(totalPrice * 0.21m, 2);

                column.Item().AlignRight().Row(row => 
                {
                    row.Spacing(3);
                    row.ConstantItem(150).AlignCenter().Text("Summa kopā (EUR)").FontFamily("Arial");
                    row.ConstantItem(75).Border(1).Padding(1).AlignCenter().Text($"{totalPrice}").FontFamily("Arial");
                });
                column.Item().AlignRight().Row(row =>
                {
                    row.Spacing(3);
                    row.ConstantItem(100).AlignCenter().Text("PVN 21%").Bold().FontFamily("Arial");
                    //row.ConstantItem(75).Border(1).Padding(1).AlignCenter().Text($"{Math.Round(totalPrice * 1.21m - totalPrice, 2)}€").FontFamily("Arial");
                    row.ConstantItem(75).Border(1).Padding(1).AlignCenter().Text($"{taxAmount}").FontFamily("Arial");
                });
                column.Item().AlignRight().Row(row =>
                {
                    row.Spacing(3);
                    row.ConstantItem(200).AlignCenter().Text("Kopējā summa apmaksai (EUR)").Bold().FontFamily("Arial");
                    row.ConstantItem(75).Border(1).Padding(1).AlignCenter().Text($"{totalPrice + taxAmount}").FontFamily("Arial");
                });

                if (!string.IsNullOrWhiteSpace(Model.Comments))
                    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Nosaukums").FontFamily("Arial");
                    header.Cell().Element(CellStyle).AlignRight().Text("Mērvienība").FontFamily("Arial");
                    header.Cell().Element(CellStyle).AlignRight().Text("Daudzums").FontFamily("Arial");
                    header.Cell().Element(CellStyle).AlignRight().Text("Cena (EUR)").FontFamily("Arial");
                    header.Cell().Element(CellStyle).AlignRight().Text("Summa EUR").FontFamily("Arial");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in Model.Items)
                {
                    table.Cell().Element(CellStyle).Text(Model.Items.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignCenter().Text("kompl");
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Quantity);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Price);
                    table.Cell().Element(CellStyle).AlignCenter().Text($"{item.Price * item.Quantity}€");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14);
                column.Item().Text(Model.Comments);
            });
        }
    }
}
