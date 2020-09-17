function jsOpenHomePage() {
    window.location.href = '/';
}

var emailcheck = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
function checkEmail(sEmail) {
    if (!emailcheck.test(sEmail) && sEmail != '') {
        $("#spValidateEmail").css("display", "");
        return false;
    } else {
        $("#spValidateEmail").css("display", "none");
        return true;
    }
}

// CLEAR FORM SCRIPT
$.fn.clearForm = function () {
    return this.each(function () {
        var type = this.type, tag = this.tagName.toLowerCase();
        if (tag == 'form')
            return $(':input', this).clearForm();
        if (type == 'text' || type == 'password' || tag == 'textarea')
            this.value = '';
        else if (type == 'checkbox' || type == 'radio')
            this.checked = false;
        else if (tag == 'select')
            this.selectedIndex = -1;
    });
};

// START - NotEqualTo FUNCTION - REMOTE CLIENT SERVER
//$.validator.addMethod(
//    'notequalto',
//   function (value, element, params) {
//       console.debug(element);
//       if (!this.optional(element)) {
//           var otherProperty = $('#' + params.otherproperty)
//           return (otherProperty.val() != value);
//       }
//       return true;
//   });

//$.validator.unobtrusive.adapters.add(
//    'notequalto', ['otherproperty', 'otherpropertyname'], function (options) {
//        var params = {
//            otherproperty: options.params.otherproperty,
//            otherpropertyname: options.params.otherpropertyname
//        };
//        options.rules['notequalto'] = params;
//        options.messages['notequalto'] = options.message;
//    });
// END - NotEqualTo FUNCTION - REMOTE CLIENT SERVER

// SHOW PROGRESS BAR FUNCTION
function ShowProgressBar()
{
    $("#divPartial_ProgressBar").show();
}

// HIDE PROGRESS BAR FUNCTION
function HideProgressBar() {  
    $("#divPartial_ProgressBar").hide();
}

// SHOW SLIDE PROGRESS BAR FUNCTION
function ShowSlideProgressBar() {
    $("#divPartial_Slide_ProgressBar").show();
}

// HIDE SLIDE PROGRESS BAR FUNCTION
function HideSlideProgressBar() {
    $("#divPartial_Slide_ProgressBar").hide();
}

// VALIDATE FORM AND THEN SHOW THE PROGRESS BAR
function jsFormValidate(oForm) {
    if ($("#" + oForm).valid()) {
        // sessionStorage.getItem("LoginType");
        ShowProgressBar();
        return true;
    }
}

function jsMakeMenuActive(sMenuName, sSubMenuName) {
    $("#" + sMenuName).addClass("start active");

    if (sSubMenuName != null) {
        $("#" + sSubMenuName).addClass("active");
        $("#" + sMenuName).find('.arrow').addClass("open")
    }
}

function jsOpenPage(sPage)
{
    ShowProgressBar();
    window.location.href = sPage;
}

function fnFormSubmit(sFormName) {
    if (jsFormValidate(sFormName)) {
        ShowProgressBar();
        $("#" + sFormName).submit();
    }
}