var $ = require('jquery');
var Backbone = require('backbone');
var Marionette = require('marionette');
require('bootstrap');
var region = Marionette.Region.extend({
    el : '#modal-region',

    constructor : function() {
        Backbone.Marionette.Region.prototype.constructor.apply(this, arguments);
        this.on('show', this.showModal, this);
    },

    getEl : function(selector) {
        var $el = $(selector);
        $el.on('hidden', this.close);
        return $el;
    },

    showModal : function() {
        this.$el.addClass('modal fade');

        //need tab index so close on escape works
        //https://github.com/twitter/bootstrap/issues/4663
        this.$el.attr('tabindex', '-1');
        this.$el.modal({
            show     : true,
            keyboard : true,
            backdrop : true
        });

        this.$el.on('hide.bs.modal', $.proxy(this._closing, this));

        this.currentView.$el.addClass('modal-dialog');
    },

    closeModal : function() {
        $(this.el).modal('hide');
        this.reset();
    },

    _closing : function() {
        if (this.$el) {
            this.$el.off('hide.bs.modal');
        }

        this.reset();
    }
});

module.exports = region;