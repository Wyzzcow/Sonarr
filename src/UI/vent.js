var Wreqr = require('./JsLibraries/backbone.wreqr');

var vent = new Wreqr.EventAggregator();

vent.Events = {
    SeriesAdded        : 'series:added',
    SeriesDeleted      : 'series:deleted',
    CommandComplete    : 'command:complete',
    ServerUpdated      : 'server:updated',
    EpisodeFileDeleted : 'episodefile:deleted'
};

vent.Commands = {
    EditSeriesCommand        : 'EditSeriesCommand',
    DeleteSeriesCommand      : 'DeleteSeriesCommand',
    OpenModalCommand         : 'OpenModalCommand',
    CloseModalCommand        : 'CloseModalCommand',
    ShowEpisodeDetails       : 'ShowEpisodeDetails',
    ShowHistoryDetails       : 'ShowHistoryDetails',
    ShowLogDetails           : 'ShowLogDetails',
    SaveSettings             : 'saveSettings',
    ShowLogFile              : 'showLogFile',
    ShowRenamePreview        : 'showRenamePreview',
    ShowFileBrowser          : 'showFileBrowser',
    CloseFileBrowser         : 'closeFileBrowser',
    OpenControlPanelCommand  : 'OpenControlPanelCommand',
    CloseControlPanelCommand : 'CloseControlPanelCommand'
};

vent.Hotkeys = {
    NavbarSearch : 'navbar:search',
    SaveSettings : 'settings:save',
    ShowHotkeys  : 'hotkeys:show'
};

module.exports = vent;