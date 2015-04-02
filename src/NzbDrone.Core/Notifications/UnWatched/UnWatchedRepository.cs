using System;
using System.Collections.Generic;
using System.Text;
using NzbDrone.Core.Datastore;
using NzbDrone.Core.Messaging.Events;

namespace NzbDrone.Core.Notifications.UnWatched
{
    public interface IUnWatchedRepository : IBasicRepository<UnWatchedResult>
    {

        void SaveDownload(DownloadMessage message);
    }

    public class UnWatchedRepository : BasicRepository<UnWatchedResult>, IUnWatchedRepository
    {
        private readonly IDatabase _database;

        public UnWatchedRepository(IDatabase database, IEventAggregator eventAggregator)
            : base(database, eventAggregator)
        {
            
        }
        public void SaveDownload(DownloadMessage message)
        {

            var mapper = _database.GetDataMapper();
            var sb = String.Format("insert into Unwatched (EpisodeId, Watched, DownloadedDate, LocalPath) Values ({0},{1},datetime('now','localtime'),{2})", message.EpisodeFile.SeriesId, false, message.EpisodeFile.Path);

            var queryText = sb.ToString();

            var data = mapper.Query<UnWatchedResult>(queryText);
        }
    }
}
