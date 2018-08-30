//配比复制
function formulaCopyInit(getUrl) {
    var cp = $("div#CopyFormDiv select:first");
    cp.bind('change', function (select) {
        var op = select.target.value;
        var st = op.substring(0, 2);
        var stidlist = $("#source-id");
        stidlist.empty();

        ajaxRequest(getUrl, { op: st }, false, function (response) {
            var cs = response;
            if (isEmpty(cs)) return;
            var item;
            if (st == "FO") {
                for (var i = 0; i < response.length; i++) {
                    stidlist.append("<option value=\"" + cs[i].ID + "\">" + cs[i].ID + "--" + cs[i].FormulaName + "</option>");
                }
            }
            if (st == "CO") {
                for (var i = 0; i < response.length; i++) {
                    stidlist.append("<option value=\"" + cs[i].ID + "\">" + cs[i].ID + "--" + cs[i].ConStrength + "</option>");
                }
            }
            if (st == "CU") {
                for (var i = 0; i < response.length; i++) {
                    stidlist.append("<option value=\"" + cs[i].ID + "\">" + cs[i].ID + "--" + cs[i].MixpropCode + "</option>");
                }
            }
        });
    });
}

function startCopy(postUrl) {
    var cp = $("div#CopyFormDiv select:first");
    var op = cp.val();
    var sid = $("#source-id").val(); //源ID
    var desid = $("#tid").val();
    var des = "";
    if (desid.length > 0)
        des = desid;
    if (isEmpty(op)) {
        showError("出错啦！", "复制方式是必须的");
        return false;
    }
    ajaxRequest(postUrl, { op: op, sid: sid, did: des }, false, function (response) {
        if (!response.Result) {
            showError("出错啦！", response.Message);
            return false;
        } else {
            $("#source-id").val("");
            cp.val("");
            showMessage("提示", response.Message);
            return true;
        }
    }, null, $("#begincopy"));
}