using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _5_DetailedErrors.Pages.Errors
{
    public class GeneralErrorModel : PageModel
    {
        public string ErrorMsg { get; set; }
        readonly ILogger<GeneralErrorModel> logger;
        public GeneralErrorModel(ILogger<GeneralErrorModel> logger)
        {
            this.logger = logger;
        }
        public void OnGet()
        {
            ErrorMsg = GetAdditionalMessage();
            LogError();                
        }

        string GetAdditionalMessage()
        {
            if (TempData.ContainsKey("Error"))
                return TempData["Error"]?.ToString();
            else
                return string.Empty;
        }

        void LogError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exception == null)
                return;

            var tempDataMsg = string.Empty;
            if (TempData.ContainsKey("Error"))
                tempDataMsg = TempData.Peek("Error")?.ToString();

            string msg = @$"Fatal error: \nPath: {exception.Path}\nMsg: {exception.Error.Message}\n
                            StackTrace: {exception.Error.StackTrace?.ToString()}\n
                            Msg from temp: {tempDataMsg}";

            logger.LogError(msg);
        }
    }
}
