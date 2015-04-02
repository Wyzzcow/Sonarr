var _ = require('underscore');
var Marionette = require('marionette');
var UpcomingCollection = require('./UpcomingCollection');
var UpcomingItemView = require('./UpcomingItemView');
require('../Mixins/backbone.signalr.mixin');

module.exports = Marionette.CollectionView.extend({
    itemView : UpcomingItemView,

    initialize : function() {
        this.collection = new UpcomingCollection().bindSignalR({ updateOnly : true });
        this.collection.fetch();

        this._fetchCollection = _.bind(this._fetchCollection, this);
        this.timer = window.setInterval(this._fetchCollection, 60 * 60 * 1000);
    },

    onClose : function() {
        window.clearInterval(this.timer);
    },

    _fetchCollection : function() {
        this.collection.fetch();
    }
});