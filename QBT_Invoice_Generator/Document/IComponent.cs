using QuestPDF.Infrastructure;

namespace QBT_Invoice_Generator.Document
{
    public interface IComponent
    {
        void Compose(IContainer container);
    }
}
