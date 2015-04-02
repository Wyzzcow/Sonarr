using FluentMigrator;
using NzbDrone.Core.Datastore.Migration.Framework;

namespace NzbDrone.Core.Datastore.Migration
{
    [Migration(1001)]
    public class create_unwatched : NzbDroneMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Create.TableForModel("UnWatched")
                .WithColumn("EpisodeId").AsInt32()
                .WithColumn("Watched").AsBoolean()
                .WithColumn("DownloadedDate").AsDateTime().Nullable()
                .WithColumn("WatchedDate").AsDateTime().Nullable()
                .WithColumn("LocalPath").AsString();
        }
    }
}
