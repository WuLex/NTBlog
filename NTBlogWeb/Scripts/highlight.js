/** 
layui 搜索结果关键字高亮模块

依赖LayUI框架

Author:Van
Email:zheng_jinfan@126.com

举一个栗子：

layui.config({
    base: '/Scripts/' //你的模块目录
});
layui.use(['highlight'], function () {
    var highlight = layui.highlight;

    highlight.handler('layui')
}); //加载入口

*/

;!function (window, document) {
    "use strict";
    var Highlight = {
        handler: function (s) {
            highlight(s);
        }
    };

    function encode(s) {
        return s.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">").replace(/([\\\.\*\[\]\(\)\$\^])/g, "\\$1");
    }
    function decode(s) {
        return s.replace(/\\([\\\.\*\[\]\(\)\$\^])/g, "$1").replace(/>/g, ">").replace(/</g, "<").replace(/&/g, "&");
    }
    function highlight(s) {
        if (s.length == 0) {
            //alert('搜索关键词未填写！');
            return false;
        }
        s = encode(s);
        var obj = document.getElementsByTagName("body")[0];
        var t = obj.innerHTML.replace(/<span\s+class=.?highlight.?>([^<>]*)<\/span>/gi, "$1");
        obj.innerHTML = t;
        var cnt = loopSearch(s, obj);
        t = obj.innerHTML
        var r = /{searchHL}(({(?!\/searchHL})|[^{])*){\/searchHL}/g
        t = t.replace(r, "<span class='highlight'>$1</span>");
        obj.innerHTML = t;
        //alert("搜索到关键词" + cnt + "处")
    }
    function loopSearch(s, obj) {
        var cnt = 0;
        if (obj.nodeType == 3) {
            cnt = replace(s, obj);
            return cnt;
        }
        for (var i = 0, c; c = obj.childNodes[i]; i++) {
            if (!c.className || c.className != "highlight")
                cnt += loopSearch(s, c);
        }
        return cnt;
    }
    function replace(s, dest) {
        var r = new RegExp(s, "g");
        var tm = null;
        var t = dest.nodeValue;
        var cnt = 0;
        if (tm = t.match(r)) {
            cnt = tm.length;
            t = t.replace(r, "{searchHL}" + decode(s) + "{/searchHL}")
            dest.nodeValue = t;
        }
        return cnt;
    }


    layui.define(function (exports) {
        exports('highlight', Highlight);
    });
}(window, document);

