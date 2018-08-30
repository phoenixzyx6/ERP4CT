var setting = {
    async: {
        enable: true,
        url: "../../ABC.mvc/GetZtreeData_Project"
    },
    data: {
        key: {
            children: "children",
            name: "name",
            title: "title",
            url: "url",
            dbId: "dbId",
            termId: "termId",
            lonlat: "lonlat",
            otherinfo: "otherinfo"
        }
    },
    view: {
        dblClickExpand: false,
        showLine: true,
        fontCss: getFont
    },
    callback: {
        beforeExpand: beforeExpand,
        onExpand: zTreeOnExpand,
        onClick: zTreeOnClick,
        onCheck: zTreeOnCheck,
        onAsyncSuccess: zTreeOnAsyncSuccess
    },
    check: {
        enable: true
    } 
};
function initzTree() {
    this.setting.async.url = "../../ABC.mvc/GetZtreeData_Project";
    $.fn.zTree.init($("#treeDemo_Project"), setting);
    this.setting.async.url = "../../ABC.mvc/GetZtreeData_Car";
    $.fn.zTree.init($("#treeDemo_Car"), setting);
   
}
var curExpandNode = null;
var ispostBack = false;
function beforeExpand(treeId, treeNode) {

    var pNode = curExpandNode ? curExpandNode.getParentNode() : null;
    
    var treeNodeP = treeNode.parentTId ? treeNode.getParentNode() : null;
    var zTree = $.fn.zTree.getZTreeObj(treeId);
    for (var i = 0, l = !treeNodeP ? 0 : treeNodeP.children.length; i < l; i++) {
        if (treeNode !== treeNodeP.children[i]) {
            zTree.expandNode(treeNodeP.children[i], false);
        }
    }
    while (pNode) {
        if (pNode === treeNode) {
            break;
        }
        pNode = pNode.getParentNode();
    }
    if (!pNode) {
        singlePath(treeId, treeNode);
    }

}
function setZtreeCheckNodes(treeId) {
    
    var zTree = $.fn.zTree.getZTreeObj(treeId);
   
    var cknodeids = new Array();
    $.each(zTree.getCheckedNodes(true), function (index, node) {
        cknodeids[index] = node.dbId;
    });

    var cks = cknodeids.join();
   
    setCookie("ck_CheckedNodes_" + treeId + cacheKeystr, cks);
}
function singlePath(treeId, newNode) {
    if (newNode === curExpandNode) return;
    if (curExpandNode && curExpandNode.open == true) {
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        if (newNode.parentTId === curExpandNode.parentTId) {
            zTree.expandNode(curExpandNode, false);
        } else {
            var newParents = [];
            while (newNode) {
                newNode = newNode.getParentNode();
                if (newNode === curExpandNode) {
                    newParents = null;
                    break;
                } else if (newNode) {
                    newParents.push(newNode);
                }
            }
            if (newParents != null) {
                var oldNode = curExpandNode;
                var oldParents = [];
                while (oldNode) {
                    oldNode = oldNode.getParentNode();
                    if (oldNode) {
                        oldParents.push(oldNode);
                    }
                }
                if (newParents.length > 0) {
                    for (var i = Math.min(newParents.length, oldParents.length) - 1; i >= 0; i--) {
                        if (newParents[i] !== oldParents[i]) {
                            zTree.expandNode(oldParents[i], false);
                            break;
                        }
                    }
                } else {
                    zTree.expandNode(oldParents[oldParents.length - 1], false);
                }
            }
        }
    }
    curExpandNode = newNode;
}
function carcheckedfilter(node) {
    return (node.checked && node.level == 1 && node.dbId.indexOf("c") > -1);
}
function carcheckedfilter2(node) {
    return (node.checked && node.level == 2 && node.dbId.indexOf("c") > -1);
}
function zTreeOnExpand(event, treeId, treeNode) {
    curExpandNode = treeNode;
}

function zTreeOnClick(e, treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj(treeId);
     
    if (treeNode.level == 2 && treeId == 'treeDemo_Car' || treeNode.level == 1 && treeId == 'treeDemo_Project') {
        if (treeNode.checked == false) {
            zTree.checkNode(treeNode, null, true, null);
        }
        var nodes = [treeNode];
        showOverlaysInMap(nodes, true);
       
    }
    zTree.expandNode(treeNode, null, null, null, true);
}
function zTreeOnCheck(event, treeId, treeNode) {

    
    if (treeNode.level == 0 && isEmpty(treeNode.children)) {
         return;
     }
     if (treeId == 'treeDemo_Car' && treeNode.level == 1 && isEmpty(treeNode.children))
         return;


    var nodes = new Array();
    if (treeNode.checked) {
        if (treeNode.level == 0 && treeId == 'treeDemo_Project' || treeNode.level == 1 && treeId == 'treeDemo_Car') {
            showOverlaysInMap(treeNode.children, false);
        
        } else {
            
            var nodes = [treeNode];
            if (ispostBack) {
              
                showOverlaysInMap(nodes, true);
                var aid = treeNode.tId + "_a";
                $('#' + aid).click();
            }
            else {
                
                showOverlaysInMap(nodes, false);
            }
        }
    } else {

        if (treeNode.level == 1 && treeId == 'treeDemo_Car' || treeNode.level == 0 && treeId == 'treeDemo_Project') {
            hideOverlaysInMapByNodes(treeNode.children );
        } else {
            hideOverlaysInMapByNodeId(treeNode.dbId); 
        }
    }
     
    setZtreeCheckNodes(treeId);
}
function showOverlaysInMap(nodes, isCenter) {
    var mk4cars = []; var mk4prjs = [];
    
    $.each(nodes, function (i, node) {
       
        var zid = node.dbId;
        
        var dbId = zid.substring(3);
       
        if (zid.indexOf('c') == 0) {
            var d = doCarData(dbId);

            if (d != null)
                mk4cars.push(d);
        } else if (zid.indexOf('p') == 0) {
            
            var d = doPrjData(node);
            if (d != null)
                mk4prjs.push(d);
        }
    });
  
    if (mk4cars.length > 0)
        _abcMap.addPoints(mk4cars, isCenter);
    if (mk4prjs.length > 0)
        _abcMap.addPoints(mk4prjs, isCenter);
}
function hideOverlaysInMapByNodeId(nodeId) {
    if (nodeId.indexOf('c') == 0) {
        _abcMap.removeMark("car" + nodeId.substring(3));
    } else if (nodeId.indexOf('p') == 0) {
        _abcMap.removeMark("prj" + nodeId.substring(3));
        _abcMap.removeMark("pjrange" + nodeId.substring(3));
    }
}
function hideOverlaysInMapByNodes(nodes) {
    $.each(nodes, function (i, node) {
        hideOverlaysInMapByNodeId(node.dbId);
    });
}

function doCarData(id) {
    var data = $("#carinfojqgrd").jqGrid('getRowData', id);
    var idPreffix = "car";
    if (!isEmpty(data["Latitude"]) && !isEmpty(data["Longtidue"])) {//如果不为空
        var content = "<table class='tb_cr_img'><tr><td><img src='" + getCarIcon(data) + "'/></td></tr><tr><td><span> " + data.CustomNo + " </span></td></tr></table>";
        return { lng: data["Longtidue"], lat: data["Latitude"], iurl: "", rtclickEv: "bindCarContextMenu", clickEv: "getCrAddrEv", tipTxt: "", content: content, id: idPreffix + id };
    }
    return null;
}
function doPrjData(node) {
   
    var idPreffix = "prj"; var id = node.dbId.substring(3);
    var lnglat = node.lonlat.split(",");
    var lng = lnglat[0]; var lat = lnglat[1];
    
    if (!isEmpty(lng) && !isEmpty(lat)) {//如果不为空
        var content = "<table class='tb_pj_img'><tr><td><img src='" + iconArr.yardIcon + "' /></td></tr><tr><td><span>" + node.name + "</span></td></tr></table>";
        return { lng: lng, lat: lat, iurl: iconArr.yardIcon, offsetx: 55, rtclickEv: "bindPrjContextMenu", clickEv: "pjMarkClickEv", tipTxt: "", content: content, id: idPreffix + id };
    }
    return null;
}

function getFont(treeId, node) {
    return node.font ? { 'color': 'red'} : {};
}
function zTreeOnAsyncSuccess(event, treeId, treeNode, msg) {
    var zcook = $.cookie("ck_CheckedNodes_" + treeId + cacheKeystr);
     
    if (zcook != null) {
        var _zTree = $.fn.zTree.getZTreeObj(treeId);
        var _cknodes = zcook.split(",");
        var _node = null;
        
        for (var i = 0; i < _cknodes.length; i++) {
            _node = _zTree.getNodeByParam("dbId", _cknodes[i], null);
            
            if (!isEmpty(_node) && (_node.level == 1  && treeId == 'treeDemo_Project' || _node.level == 2 && treeId == 'treeDemo_Car')) {
                _zTree.checkNode(_node, null, true, zTreeOnCheck);
            }
        }
    } 
    ispostBack = true;
} 
function refreshzTree() {
    ispostBack = false;
    var zTree_Project = $.fn.zTree.getZTreeObj("treeDemo_Project");
    if (zTree_Project != null) {
        zTree_Project = null;
        $("#treeDemo_Project").html("");
         
    }

    var zTree_Car = $.fn.zTree.getZTreeObj("treeDemo_Car");
    if (zTree_Car != null) {
        zTree_Car = null;
        $("#treeDemo_Car").html("");

    }
    initzTree();
}


function searchNode(treeId) {
    var condition = '';
    var level = 2;
    var _zTree = $.fn.zTree.getZTreeObj(treeId);
    if (treeId == 'treeDemo_Car') {
        condition = $("input[name='CarSearch']").val();
       
    }
    else {
        condition = $("input[name='ProjectSearch']").val();
        level = 1;
    }

    var nodes = $.fn.zTree.getZTreeObj(treeId).getNodesByParam('level', level);
 
    $.each(nodes, function (index, node) {
        if (node.name.indexOf(condition) != -1) {
            $('#' + node.tId).css("display", "block");
        }
        else {

            $('#' + node.tId).css("display", "none");

        }
    });
}