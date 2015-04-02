using System;
using System.Collections.Generic;
using FluentValidation.Results;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Tv;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Datastore;

namespace NzbDrone.Core.Notifications.UnWatched
{
    public class UnWatchedResult : ModelBase
    {
        public UnWatchedResult() { }
        public Int32 EpisodeId { get; set; }
        public Boolean Watched { get; set; }
        public DateTime DownloadedDate { get; set; }
        public DateTime WatchedDate { get; set; }
        public String LocalPath { get; set; }
    }
    public class UnWatchedSettings : IProviderConfig
    {

        public ValidationResult Validate()
        {
            return new ValidationResult();
        }
    }

    public class UnWatched : NotificationBase<UnWatchedSettings>
    {
        private readonly IUnWatchedService _unwatchedService;

        public UnWatched(IUnWatchedService unwatchedService)
        {
            _unwatchedService = unwatchedService;
        }

        public override string Link
        {
            get { return null; }
        }

        public override void OnGrab(string message)
        {

        }

        public override void OnDownload(DownloadMessage message)
        {
            const string subject = "Sonarr [TV] - Downloaded";
            var body = String.Format("{0} Downloaded and sorted.", message.Message);
            //this._unwatchedService._unwatchedRepository.SaveDownload(message);
            //unwatchedService.SendEmail(Settings, subject, body);
        }

        public override void AfterRename(Series series)
        {
        }
        public override ValidationResult Test()
        {
            var failures = new List<ValidationFailure>();

            return new ValidationResult(failures);
        }
    }
}
