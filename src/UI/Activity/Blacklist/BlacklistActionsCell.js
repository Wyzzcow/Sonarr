var NzbDroneCell = require('../../Cells/NzbDroneCell');

module.exports = NzbDroneCell.extend({
    className : 'blacklist-controls-cell',

    events : {
        'click' : '_delete'
    },

    render : function() {
        this.$el.empty();
        this.$el.html('<i class="icon-sonarr-delete"></i>');

        return this;
    },

    _delete : function() {
        this.model.destroy();
    }
});
