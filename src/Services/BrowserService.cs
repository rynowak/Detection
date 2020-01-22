// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Type { get; }

        public BrowserService(IUserAgentService userAgentService, IPlatformService platformService, IEngineService engineService)
        {
            var agent  = userAgentService.UserAgent;
            var os     = platformService.OperatingSystem;
            var cpu    = platformService.Processor;
            var engine = engineService.Type;
            Type = ParseBrowser(agent);
        }

        private static Browser ParseBrowser(UserAgent agent)
        {
            // fail and return fast
            if (agent.IsNullOrEmpty())
                return Browser.Unknown;
            // Google chrome
            if (agent.Contains(Browser.Chrome))
                return Browser.Chrome;
            // Microsoft Internet Explorer
            if (IsInternetExplorer(agent))
                return Browser.InternetExplorer;
            // Apple Safari
            if (agent.Contains(Browser.Safari))
                return Browser.Safari;
            // Firefox
            if (agent.Contains(Browser.Firefox))
                return Browser.Firefox;
            // Microsoft Edge
            if (agent.Contains(Browser.Edge))
                return Browser.Edge;
            // Opera
            if (agent.Contains(Browser.Opera))
                return Browser.Opera;

            return Browser.Others;
        }

        private static bool IsInternetExplorer(UserAgent agent)
            => agent.Contains("MSIE");
    }
}
