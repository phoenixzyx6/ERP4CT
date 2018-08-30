function logviewerInit(opts) {
    //ztree
    var treeSettings = {
        
        view: { selectedMulti: false},
        data: {
            simpleData: {
                enable: true
            },
            key:{title:'title'}
        },
        async: {
            enable: true,
            url: opts.folderViewUrl,
            autoParam: ["id", "name", "level"]
            // otherParam: { "otherParam": "zTreeAsyncTest" }　
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            },
            onClick: onClick

        }
    };
    function onClick(event, treeId, treeNode, clickFlag) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);

        if (!treeNode.isParent) {
            ajaxRequest(opts.readFileUrl, { id: treeNode.id, pId: treeNode.pId }, false,
            function (response) {
                $('#fileContent textarea').val('');
                $('#fileContent textarea').val(response);
            });
        }
        else {
            zTree.expandNode(treeNode, null, null, null, false);
        }
    }
    $.fn.zTree.init($('#folderViewer'), treeSettings);
    $('#fileContent textarea').css('minHeight', $('#container').height());
    $('#folderViewer textarea').css('minHeight', $('#container').height());
}