
namespace Moravia.Domain.Interfaces
{
    interface IFormatProvider
    {
        IDocument Deserialize(Stream inputStream);

        byte[] Serialize(IDocument document);
    }
}
