function setRectEnabledStatus(index) {
    var vhidChkEnabled = null;
    vhidChkEnabled = document.getElementById("hidChkBox_" + index);

    if (document.getElementById("Rectifications_" + index + "__Selected").checked == true) {
        vhidChkEnabled.value = "false";
    } else {
        vhidChkEnabled.value = "true";
    }
}

function setConMonEnabledStatus(index) {
    var vhidChkEnabled = null;
    vhidChkEnabled = document.getElementById("hidChkBox_" + index);

    if (document.getElementById("ConMonRectifications_" + index + "__Selected").checked == true) {
        vhidChkEnabled.value = "false";
    } else {
        vhidChkEnabled.value = "true";
    }
}

function setAdHocEnabledStatusMobile(index) {
    debugger
    var vhidChkEnabled = null;
    vhidChkEnabled = document.getElementById("hidChkBox_" + index);

    var vhidChkSelected = null;
    vhidChkSelected = document.getElementById("AdHocRectifications_" + index + "__Selected")

    if (document.getElementById("chkbox_" + index).checked == true) {
        vhidChkEnabled.value = "false";
        vhidChkSelected.value = "true";
    } else {
        vhidChkEnabled.value = "true";
        vhidChkSelected.value = "false";
    }
}

function setAdHocEnabledStatus(index) {
    var vhidChkEnabled = null;
    vhidChkEnabled = document.getElementById("hidChkBox_" + index);

    if (document.getElementById("AdHocRectifications_" + index + "__Selected").checked == true) {
        vhidChkEnabled.value = "false";
    } else {
        vhidChkEnabled.value = "true";
    }
}

var page = 0,
    inCallback = false,
    isReachedScrollEnd = false;

var scrollHandler = function () {
    if (isReachedScrollEnd == false &&
        ($(document).scrollTop() <= $(document).height() - $(window).height())) {
        loadProjectData(url);
    }
}

function loadProjectData(loadMoreRowsUrl) {
    if (page > -1 && !inCallback) {
        inCallback = true;
        page++;
        $("div#loading").show();

        $.ajax({
            type: 'GET',
            url: loadMoreRowsUrl,
            data: "pageNum=" + page,
            success: function (data, textstatus) {
                if (data != '') {
                    $("table.infinite-scroll > tbody").append(data);
                    $("table.infinite-scroll > tbody > tr:even").addClass("alt-row-class");
                    $("table.infinite-scroll > tbody > tr:odd").removeClass("alt-row-class");
                }
                else {
                    page = -1;
                }

                inCallback = false;
                $("div#loading").hide();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }
}