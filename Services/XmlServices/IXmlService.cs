namespace T1Balance.Services.XmlServices
{
    public interface IXmlService
    {
        string Token { get; set; }
        bool IsRemember { get; set; }
        string LastLogin { get; set; }
    }
}
