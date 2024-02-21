/**
 * Created by k on 2016/10/15.
 */

layui.config({
    base: '/Plugins/layui/user-defined/' //假设这是test.js所在的目录
});

layui.all(function () {
    var $ = layui.jquery, form = layui.form(), layer = layui.layer, upload = layui.upload;

    // side 折叠
    $('.layui-side .layui-nav-title').click(function () {
        var stopItem = $(this).attr('nav-item-num');
        var navItemIcon = $(this).find('.layui-icon');
        if($(this).hasClass('item-hide')) {
            $(this).nextAll('.layui-nav-item').slice(0, stopItem).show();
            $(this).addClass('item-show').removeClass('item-hide');
            navItemIcon.css('transform','rotate(0deg)');
        } else if($(this).hasClass('item-show')) {
            $(this).nextAll('.layui-nav-item').slice(0, stopItem).hide();
            $(this).addClass('item-hide').removeClass('item-show');
            navItemIcon.css('transform','rotate(-180deg)');
        }
    });
    // 表单
    form.on('checkbox(write)', function(data){
        layer.msg('你选择了写作');
    });
    form.on('checkbox(read)', function(data){
        layer.msg('你选择了阅读');
    });
    form.on('radio(man)', function(data){
        layer.msg('你选择了男');
    });
    form.on('radio(woman)', function(data){
        layer.msg('你选择了女');
    });


    // 上传
    upload({
        url: '上传接口url'
        ,success: function(res){
            console.log(res); //上传成功返回值，必须为json格式
        }
    });

    // code
    layui.code(); //引用code方法

    // 编辑器
    layui.layedit.build('demo-textarea', {
        height: 180
    });

});