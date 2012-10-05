/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="backbone.js" />
/// <reference path="underscore.js" />
/// <reference path="handlebars-1.0.js" />

$(function () {

    var PurchaseView = Backbone.View.extend({
        events: {
            "click input[type='reset']": "closeView",
            "submit form": "purchase"
        },
        initialize: function () {
            _.bind(this.closeView, this);
        },
        purchase: function () {

            var form = $("form", this.$el),
                that = this;

            form.empty();

            var lblMessage = $("<span />")
                .addClass("loader")
                .text("Gennemfører køb... vent venligst");

            form.append(lblMessage);
            form.append($("<a />").attr("href", "#").addClass("close").text("Luk vindue").click(function () {
                that.closeView.apply(that)
            }));

            $.post("/Shop/AjaxPurchase/" + this.model.get("albumId"), function (data) {

                lblMessage.removeClass("loader");

                switch (data.Type) {
                    case "Success":
                        lblMessage.addClass("success");
                        break;
                    case "Error":
                        lblMessage.addClass("error");
                        break;
                }

                lblMessage.html(data.Message);

            });

            return false;
        },
        closeView: function () {
            var that = this;

            console.log(that);

            this.$el.fadeOut(1000, function () {
                that.$el.empty();
            });
        },
        render: function () {

            var template = Handlebars.compile('<img src="{{largePicture}}"/><div class="content"><h1>Er du sikker på at du vil købe"{{albumName}}"af{{artistName}}</h1><p>Er du sikker på at du vil købe albummet?Det koster jo virtuelle penge?</p><dl><dt>Pris:</dt><dd>{{price}}Grunker</dd></dl><form><input type="submit"value="Ja, køb albummet"/><input type="reset"value="Fortryd"/></form></div>'),
                context = this.model.toJSON();

            $(this.el).html(template(context));

            return this;
        }
    });

    var Album = Backbone.Model.extend({
        defaults: {
            albumName: "",
            artistName: "",
            albumId: "",
            price: "",
            largePicture: ""
        }
    });

    $(".buy-album").click(function () {

        var $this = $(this),
            album = new Album({
                albumName: $this.data("albumname"),
                artistName: $this.data("artistname"),
                albumId: $this.data("albumid"),
                price: $this.data("price"),
                largePicture: $this.data("largepicture")
            });

        var purchaseView = new PurchaseView({ el: $("#buy-module"), model: album });

        purchaseView.$el.removeClass("hide").hide();
        purchaseView.$el.fadeIn(500);

        $('body').append(purchaseView.render());

        return false;
    });

});