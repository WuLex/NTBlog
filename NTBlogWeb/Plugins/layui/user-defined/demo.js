/**
  项目JS主入口
  以依赖Layui的layer模块为例
**/
layui.define('layer', function (exports) {
    var layer = layui.layer;

    var demo = {
        sayHello: function (name) {
            name =name || '匿名者' ;
            layer.msg('欢迎您哦，<span style="color:red;">' + name + '</span>');
        }
    };

    exports('demo', demo);
});