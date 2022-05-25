using RazorHtmlEmails.RazorClassLib.Services;
using RazorHtmlEmails.RazorClassLib.Views.Emails.ConfirmAccount;

namespace TestConsoleApp;

public class GetEmailContent
{
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
    
    public GetEmailContent(IRazorViewToStringRenderer razorViewToStringRenderer)
    {
        _razorViewToStringRenderer = razorViewToStringRenderer;

    }

    public async Task<string> GetEmail()
    {
        var confirmAccountModel = new ConfirmAccountEmailViewModel($"{Guid.NewGuid()}");

        return await _razorViewToStringRenderer.RenderViewToStringAsync("~/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);
        // return await _razorViewToStringRenderer.RenderViewToStringAsync("C:/GIT/RazorHtmlEmails/src/RazorHtmlEmails.RazorClassLib/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);

    }
}