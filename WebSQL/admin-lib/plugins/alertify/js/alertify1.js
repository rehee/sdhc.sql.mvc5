! function() {
    "use strict";

    function t() {
        var t = {
            parent: document.body,
            version: "1.0.10",
            defaultOkLabel: "Ok",
            okLabel: "Ok",
            defaultCancelLabel: "Cancel",
            cancelLabel: "Cancel",
            defaultMaxLogItems: 2,
            maxLogItems: 2,
            promptValue: "",
            promptPlaceholder: "",
            closeLogOnClick: !1,
            closeLogOnClickDefault: !1,
            delay: 5e3,
            defaultDelay: 5e3,
            logContainerClass: "alertify-logs",
            logContainerDefaultClass: "alertify-logs",
            dialogs: {
                buttons: {
                    holder: "<nav>{{buttons}}</nav>",
                    ok: "<button class='ok' tabindex='1'>{{ok}}</button>",
                    cancel: "<button class='cancel' tabindex='2'>{{cancel}}</button>"
                },
                input: "<input type='text'>",
                message: "<p class='msg'>{{message}}</p>",
                log: "<div class='{{class}}'>{{message}}</div>"
            },
            defaultDialogs: {
                buttons: {
                    holder: "<nav>{{buttons}}</nav>",
                    ok: "<button class='ok' tabindex='1'>{{ok}}</button>",
                    cancel: "<button class='cancel' tabindex='2'>{{cancel}}</button>"
                },
                input: "<input type='text'>",
                message: "<p class='msg'>{{message}}</p>",
                log: "<div class='{{class}}'>{{message}}</div>"
            },
            build: function(t) {
                var e = this.dialogs.buttons.ok,
                    n = "<div class='dialog'><div>" + this.dialogs.message.replace("{{message}}", t.message);
                return ("confirm" === t.type || "prompt" === t.type) && (e = this.dialogs.buttons.cancel + this.dialogs.buttons.ok), "prompt" === t.type && (n += this.dialogs.input), n = (n + this.dialogs.buttons.holder + "</div></div>").replace("{{buttons}}", e).replace("{{ok}}", this.okLabel).replace("{{cancel}}", this.cancelLabel)
            },
            setCloseLogOnClick: function(t) {
                this.closeLogOnClick = !!t
            },
            close: function(t, e) {
                this.closeLogOnClick && t.addEventListener("click", function(t) {
                    n(t.srcElement)
                }), e = e && !isNaN(+e) ? +e : this.delay, 0 > e ? n(t) : e > 0 && setTimeout(function() {
                    n(t)
                }, e)
            },
            dialog: function(t, e, n, o) {
                return this.setup({
                    type: e,
                    message: t,
                    onOkay: n,
                    onCancel: o
                })
            },
            log: function(t, e, n) {
                var o = document.querySelectorAll(".alertify-logs > div");
                if (o) {
                    var s = o.length - this.maxLogItems;
                    if (s >= 0)
                        for (var i = 0, a = s + 1; a > i; i++) this.close(o[i], -1)
                }
                this.notify(t, e, n)
            },
            setLogPosition: function(t) {
                this.logContainerClass = "alertify-logs " + t
            },
            setupLogContainer: function() {
                var t = document.querySelector(".alertify-logs"),
                    e = this.logContainerClass;
                return t || (t = document.createElement("div"), t.className = e, this.parent.appendChild(t)), t.className !== e && (t.className = e), t
            },
            notify: function(e, n, o) {
                var s = this.setupLogContainer(),
                    i = document.createElement("div");
                i.className = n || "default", t.logTemplateMethod ? i.innerHTML = t.logTemplateMethod(e) : i.innerHTML = e, "function" == typeof o && i.addEventListener("click", o), s.appendChild(i), setTimeout(function() {
                    i.className += " show"
                }, 10), this.close(i, this.delay)
            },
            setup: function(t) {
                function e(e) {
                    "function" != typeof e && (e = function() {}), s && s.addEventListener("click", function(s) {
                        t.onOkay && "function" == typeof t.onOkay && (a ? t.onOkay(a.value, s) : t.onOkay(s)), e(a ? {
                            buttonClicked: "ok",
                            inputValue: a.value,
                            event: s
                        } : {
                            buttonClicked: "ok",
                            event: s
                        }), n(o)
                    }), i && i.addEventListener("click", function(s) {
                        t.onCancel && "function" == typeof t.onCancel && t.onCancel(s), e({
                            buttonClicked: "cancel",
                            event: s
                        }), n(o)
                    })
                }
                var o = document.createElement("div");
                o.className = "alertify hide", o.innerHTML = this.build(t);
                var s = o.querySelector(".ok"),
                    i = o.querySelector(".cancel"),
                    a = o.querySelector("input"),
                    l = o.querySelector("label");
                a && ("string" == typeof this.promptPlaceholder && (l ? l.textContent = this.promptPlaceholder : a.placeholder = this.promptPlaceholder), "string" == typeof this.promptValue && (a.value = this.promptValue));
                var c;
                return "function" == typeof Promise ? c = new Promise(e) : e(), this.parent.appendChild(o), setTimeout(function() {
                    o.classList.remove("hide"), a && t.type && "prompt" === t.type ? (a.select(), a.focus()) : s && s.focus()
                }, 100), c
            },
            okBtn: function(t) {
                return this.okLabel = t, this
            },
            setDelay: function(t) {
                return t = t || 0, this.delay = isNaN(t) ? this.defaultDelay : parseInt(t, 10), this
            },
            cancelBtn: function(t) {
                return this.cancelLabel = t, this
            },
            setMaxLogItems: function(t) {
                this.maxLogItems = parseInt(t || this.defaultMaxLogItems)
            },
            theme: function(t) {
                switch (t.toLowerCase()) {
                    case "bootstrap":
                        this.dialogs.buttons.ok = "<button class='ok btn btn-primary' tabindex='1'>{{ok}}</button>", this.dialogs.buttons.cancel = "<button class='cancel btn btn-default' tabindex='2'>{{cancel}}</button>", this.dialogs.input = "<input type='text' class='form-control'>";
                        break;
                    case "purecss":
                        this.dialogs.buttons.ok = "<button class='ok pure-button' tabindex='1'>{{ok}}</button>", this.dialogs.buttons.cancel = "<button class='cancel pure-button' tabindex='2'>{{cancel}}</button>";
                        break;
                    case "mdl":
                    case "material-design-light":
                        this.dialogs.buttons.ok = "<button class='ok mdl-button mdl-js-button mdl-js-ripple-effect'  tabindex='1'>{{ok}}</button>", this.dialogs.buttons.cancel = "<button class='cancel mdl-button mdl-js-button mdl-js-ripple-effect' tabindex='2'>{{cancel}}</button>", this.dialogs.input = "<div class='mdl-textfield mdl-js-textfield'><input class='mdl-textfield__input'><label class='md-textfield__label'></label></div>";
                        break;
                    case "angular-material":
                        this.dialogs.buttons.ok = "<button class='ok md-primary md-button' tabindex='1'>{{ok}}</button>", this.dialogs.buttons.cancel = "<button class='cancel md-button' tabindex='2'>{{cancel}}</button>", this.dialogs.input = "<div layout='column'><md-input-container md-no-float><input type='text'></md-input-container></div>";
                        break;
                    case "default":
                    default:
                        this.dialogs.buttons.ok = this.defaultDialogs.buttons.ok, this.dialogs.buttons.cancel = this.defaultDialogs.buttons.cancel, this.dialogs.input = this.defaultDialogs.input
                }
            },
            reset: function() {
                this.parent = document.body, this.theme("default"), this.okBtn(this.defaultOkLabel), this.cancelBtn(this.defaultCancelLabel), this.setMaxLogItems(), this.promptValue = "", this.promptPlaceholder = "", this.delay = this.defaultDelay, this.setCloseLogOnClick(this.closeLogOnClickDefault), this.setLogPosition("bottom left"), this.logTemplateMethod = null
            },
            injectCSS: function() {
                if (!document.querySelector("#alertifyCSS")) {
                    var t = document.getElementsByTagName("head")[0],
                        e = document.createElement("style");
                    e.type = "text/css", e.id = "alertifyCSS", e.innerHTML = "/* style.css */", t.insertBefore(e, t.firstChild)
                }
            },
            removeCSS: function() {
                var t = document.querySelector("#alertifyCSS");
                t && t.parentNode && t.parentNode.removeChild(t)
            }
        };
        return t.injectCSS(), {
            _$$alertify: t,
            parent: function(e) {
                t.parent = e
            },
            reset: function() {
                return t.reset(), this
            },
            alert: function(e, n, o) {
                return t.dialog(e, "alert", n, o) || this
            },
            confirm: function(e, n, o) {
                return t.dialog(e, "confirm", n, o) || this
            },
            prompt: function(e, n, o) {
                return t.dialog(e, "prompt", n, o) || this
            },
            log: function(e, n) {
                return t.log(e, "default", n), this
            },
            theme: function(e) {
                return t.theme(e), this
            },
            success: function(e, n) {
                return t.log(e, "success", n), this
            },
            error: function(e, n) {
                return t.log(e, "error", n), this
            },
            cancelBtn: function(e) {
                return t.cancelBtn(e), this
            },
            okBtn: function(e) {
                return t.okBtn(e), this
            },
            delay: function(e) {
                return t.setDelay(e), this
            },
            placeholder: function(e) {
                return t.promptPlaceholder = e, this
            },
            defaultValue: function(e) {
                return t.promptValue = e, this
            },
            maxLogItems: function(e) {
                return t.setMaxLogItems(e), this
            },
            closeLogOnClick: function(e) {
                return t.setCloseLogOnClick(!!e), this
            },
            logPosition: function(e) {
                return t.setLogPosition(e || ""), this
            },
            setLogTemplate: function(e) {
                return t.logTemplateMethod = e, this
            },
            clearLogs: function() {
                return t.setupLogContainer().innerHTML = "", this
            },
            version: t.version
        }
    }
    var e = 500,
        n = function(t) {
            if (t) {
                var n = function() {
                    t && t.parentNode && t.parentNode.removeChild(t)
                };
                t.classList.remove("show"), t.classList.add("hide"), t.addEventListener("transitionend", n), setTimeout(n, e)
            }
        };
    if ("undefined" != typeof module && module && module.exports) {
        module.exports = function() {
            return new t
        };
        var o = new t;
        for (var s in o) module.exports[s] = o[s]
    } else "function" == typeof define && define.amd ? define(function() {
        return new t
    }) : window.alertify = new t
}();