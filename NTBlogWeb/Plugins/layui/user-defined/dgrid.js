/**
 * Created by k on 2016/10/16.
 */
/**
 扩展一个dgrid组件
 **/

layui.define(['jquery', 'laytpl'], function(exports){
    var obj = {
        render: function(config){
            setTimeout(function (){
                var jquery = layui.jquery, layer = layui.layer;
                // 获取url数据
                jquery.get(config.url, function (data) {
                    // 处理表头
                    var head_tr = '',header_length = data.header.length;
                    for (i=0; i< header_length; i++) {
                        head_tr += '<td>' + data.header[i] + '</td>';
                    }
                    head_tr = '<thead>' + head_tr + '</thead>';

                    // 处理列表数据
                    var body_tr = '';
                    var length = data.columns.length;
                    for (i =0; i < length; i++) {
                        body_tr += '<tr>' +
                            '<td>' + (i + 1) + '</td>' +
                            '<td>' + data.columns[i].name + '</td>' +
                            '<td>' + data.columns[i].city + '</td>' +
                            '<td><button class="layui-btn layui-btn-danger layui-btn-mini btn-del" del-id="' + (i + 1) + '">删除</button><button class="layui-btn layui-btn-warm layui-btn-mini btn-edit" edit-id="' + (i + 1) + '">修改</button>' +
                            '</td>' +
                            '</tr>'
                    }
                    body_tr = '<tbody>' + body_tr + '</tbody>';

                    // 组织表格
                    table = "<table class='layui-table'>" + head_tr + body_tr + "</table>";

                    // 渲染输出
                    layui.laytpl(table).render(data, function(table){
                        jquery('#demo-table').html(table);
                    });

                    jquery('.btn-del').click(function () {
                        layer.msg('确定删除：' + jquery(this).attr('del-id') + '？')
                    });

                    jquery('.btn-edit').click(function () {
                        layer.msg('正在获取：' + jquery(this).attr('edit-id') + '的数据')
                    });
                });
            }, 3000);
        }
    };

    //输出test接口
    exports('dgrid', obj);
});