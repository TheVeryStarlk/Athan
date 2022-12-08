using System.Globalization;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class LanguageService
{
    public void Update(ApplicationLanguage language)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language switch
        {
            ApplicationLanguage.English => "en",
            ApplicationLanguage.Arabic => "ar",
            ApplicationLanguage.German => "de",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        });
    }

    public ApplicationLanguage Read()
    {
        var name = Thread.CurrentThread.CurrentUICulture.Name;

        if (name.Contains("en"))
        {
            return ApplicationLanguage.English;
        }
        else if (name.Contains("ar"))
        {
            return ApplicationLanguage.Arabic;
        }
        else if (name.Contains("de"))
        {
            return ApplicationLanguage.German;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}