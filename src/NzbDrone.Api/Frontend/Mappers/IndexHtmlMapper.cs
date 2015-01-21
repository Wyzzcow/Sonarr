using System;
using System.IO;
using System.Text.RegularExpressions;
using Nancy;
using NLog;
using NzbDrone.Common.Disk;
using NzbDrone.Common.EnvironmentInfo;
using NzbDrone.Core.Analytics;
using NzbDrone.Core.Configuration;

namespace NzbDrone.Api.Frontend.Mappers
{
    public class IndexHtmlMapper : StaticResourceMapperBase
    {
        private readonly IDiskProvider _diskProvider;
        private readonly IConfigFileProvider _configFileProvider;
        private readonly IAnalyticsService _analyticsService;
        private readonly Func<ICacheBreakerProvider> _cacheBreakProviderFactory;
        private readonly string _indexPath;
        private static readonly Regex ReplaceRegex = new Regex("(?<=(?:href|src|data-main)=\").*?(?=\")", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static String API_KEY;
        private static String URL_BASE;
        private string _generatedContent
            ;

        public IndexHtmlMapper(IAppFolderInfo appFolderInfo,
                               IDiskProvider diskProvider,
                               IConfigFileProvider configFileProvider,
                               IAnalyticsService analyticsService,
                               Func<ICacheBreakerProvider> cacheBreakProviderFactory,
                               Logger logger)
            : base(diskProvider, logger)
        {
            _diskProvider = diskProvider;
            _configFileProvider = configFileProvider;
            _analyticsService = analyticsService;
            _cacheBreakProviderFactory = cacheBreakProviderFactory;
            _indexPath = Path.Combine(appFolderInfo.StartUpFolder, "UI", "index.html");

            API_KEY = configFileProvider.ApiKey;
            URL_BASE = configFileProvider.UrlBase;
        }

        public override string Map(string resourceUrl)
        {
            return _indexPath;
        }

        public override bool CanHandle(string resourceUrl)
        {
            return !resourceUrl.Contains(".");
        }

        public override Response GetResponse(string resourceUrl)
        {
            var response = base.GetResponse(resourceUrl);
            response.Headers["X-UA-Compatible"] = "IE=edge";

            return response;
        }

        protected override Stream GetContentStream(string filePath)
        {
            var text = GetIndexText();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private string GetIndexText()
        {
            if (RuntimeInfoBase.IsProduction && _generatedContent != null)
            {
                return _generatedContent;
            }

            var text = _diskProvider.ReadAllText(_indexPath);

            var cacheBreakProvider = _cacheBreakProviderFactory();

            text = ReplaceRegex.Replace(text, match =>
            {
                var url = cacheBreakProvider.AddCacheBreakerToPath(match.Value);
                return URL_BASE + url;
            });

            text = text.Replace("API_ROOT", URL_BASE + "/api");
            text = text.Replace("API_KEY", API_KEY);
            text = text.Replace("APP_VERSION", BuildInfo.Version.ToString());
            text = text.Replace("APP_BRANCH", _configFileProvider.Branch.ToLower());
            text = text.Replace("APP_ANALYTICS", _analyticsService.IsEnabled.ToString().ToLowerInvariant());
            text = text.Replace("URL_BASE", _configFileProvider.UrlBase);

            _generatedContent = text;

            return _generatedContent;
        }
    }
}