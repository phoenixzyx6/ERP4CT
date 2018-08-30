function userIndexInit(opts) {
    $('#sysFuncs').height(gGridHeight + 80);



    //ztree
    var treeSettings = {
        check: {
            enable: false
        },
        view: {
            selectedMulti: false,
            fontCss: getFont    //设置节点字体样式
        },
        data: {
            simpleData: {
                enable: true
            },
            key: { title: 'url1' }
        },
        async: {
            enable: true,
            url: opts.sysFuncTreeUrl,
            autoParam: ["id", "name", "level"]
            // otherParam: { "otherParam": "zTreeAsyncTest" }　
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            },
            onClick: function (event, treeId, treeNode, clickFlag) {
                if (treeNode.url1!=null||treeNode.url1.length != 0) {
                    $("#reportshow").load(treeNode.url1);
                }
                else {
                    return true;
                }

            }
        }
    };
    $.fn.zTree.init($('#sysFuncs'), treeSettings);

    // $("#reportshow").load(treeNode.url);
}
//设置节点字体样式
function getFont(treeId, node) {
    var menuname = node.name; //节点名称
    if (menuname.indexOf("(新)") >= 0) {
        return node.font ? node.font : { 'color': 'red' };
    }
}