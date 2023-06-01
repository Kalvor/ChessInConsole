namespace Networking.Services.Interfaces
{
    public interface IRequestParser
    {
        bool TryParse<TParsedObject>(byte[] bytes, out TParsedObject data);
    }
}
