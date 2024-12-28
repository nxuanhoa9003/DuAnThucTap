namespace Web_DonNghiPhep.Services
{
    public interface IMessageService
    {
        void SetMessage(string message, string type = "success");
    }
}
