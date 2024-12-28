using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Web_DonNghiPhep.Services
{
    public class MessageService : IMessageService
    {
        private readonly ITempDataDictionary _tempData;
        public MessageService(ITempDataDictionaryFactory tempDataFactory, IHttpContextAccessor httpContextAccessor)
        {
            // Lấy TempData từ HttpContext
            var context = httpContextAccessor.HttpContext;
            _tempData = tempDataFactory.GetTempData(context);
        }


        public void SetMessage(string message, string type = "success")
        {
            _tempData[type] = message;
        }
    }
}
