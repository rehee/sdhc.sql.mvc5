var UIIdleTimeout = function() {
    return {
        init: function() {
            var o;
            $("body").append(""), $.idleTimeout("#idle-timeout-dialog", ".modal-content button:last", {
                idleAfter: 5,
                timeout: 3e4,
                pollingInterval: 5,
                keepAliveURL: "/keep-alive",
                serverResponseEquals: "OK",
                onTimeout: function() {
                    window.location = "../../pages/extra-page/lock-screen.html"
                },
                onIdle: function() {
                    $("#idle-timeout-dialog").modal("show"), o = $("#idle-timeout-counter"), $("#idle-timeout-dialog-keepalive").on("click", function() {
                        $("#idle-timeout-dialog").modal("hide")
                    }), $("#idle-timeout-dialog-logout").on("click", function() {
                        $("#idle-timeout-dialog").modal("hide"), $.idleTimeout.options.onTimeout.call(this)
                    })
                },
                onCountdown: function(e) {
                    o.html(e)
                }
            })
        }
    }
}();
jQuery(document).ready(function() {
    UIIdleTimeout.init()
});
