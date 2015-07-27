
$(function () {
    var container = $("body");

    var logonForm = $(".iLogOnForm", container);
    var registerForm = $(".iRegisterForm", container);
    
    $("#ClientTimeOffset").val(new Date().getTimezoneOffset() / (-60));
    
    container.off("click", ".iLogOn").on("click", ".iLogOn", function () {
        debugger;
        AjaxHelper.LoadContent({
            method: "POST",
            loadUrl: util.link("LogOn", "Account"),
            traditional: true,
            postedData: {
                email: $("#Email", logonForm).val(),
                password: $("#Password", logonForm).val(),
                clientTimeOffset: 
            },
            successFunc: function (dataFromServer) {
                
            }
        });
        return false;
    });

    container.off("click", ".iRegister").on("click", ".iRegister", function () {

        debugger;
        return false;
    });

});

