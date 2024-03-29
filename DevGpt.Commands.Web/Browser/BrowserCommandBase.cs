﻿using DevGpt.Models.Browser;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Web.Browser;

public class BrowserCommandBase
{
    private readonly IBrowser _browser;

    protected BrowserCommandBase(IBrowser browser)
    {
        _browser = browser;
    }

    protected async Task<DevGptContextMessage> GetHtmlContextMessage()
    {
        return new DevGptContextMessage("browser_html", "html of page:" + await _browser.GetPageHtml());
    }
}