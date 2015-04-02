using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using NzbDrone.Core.Messaging.Events;
using NzbDrone.Core.MediaFiles.Events;
using FluentValidation.Results;
using NLog;

namespace NzbDrone.Core.Notifications.UnWatched
{
    public interface IUnWatchedService
    {
        IUnWatchedRepository _unwatchedRepository { get; set; }
    }

    public class UnWatchedService : IUnWatchedService,
                                    IHandle<EpisodeDownloadedEvent>
    {
        public IUnWatchedRepository _unwatchedRepository {get; set;}
        public UnWatchedService(IUnWatchedRepository unwatchedRepository)
        {
            _unwatchedRepository = unwatchedRepository;
        }
        public void Handle(EpisodeDownloadedEvent downloaded)
        {
            foreach (var episode in downloaded.EpisodeFile.Episodes.Value)
            {
                var unwatched = new UnWatchedResult
                    {
                        DownloadedDate = DateTime.UtcNow,
                        Watched = false,
                        EpisodeId = episode.Id,
                        LocalPath = Path.Combine(downloaded.Episode.Series.Path, downloaded.EpisodeFile.RelativePath)
                    };


                _unwatchedRepository.Insert(unwatched);
            }
        }
          
    }
}
