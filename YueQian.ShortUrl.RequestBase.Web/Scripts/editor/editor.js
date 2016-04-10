$(function () {
    $(".q_editor").hover(function () {

        $(".editorContainerMask,.editorContainerMask_text").remove();

        var me = $(this);
        var left = me.offset().left;
        var top = me.offset().top;

        var module = me.attr("data-module");
        var description = me.attr("data-description");

        var mask = $("<div class='editorContainerMask' style='width:" + me.width() + "px;height:" + me.height() + "px;left:" + left + "px;top:" + top + "px;'></div><div class='editorContainerMask_text' style='width:" + me.width() + "px;height:" + me.height() + "px;left:" + left + "px;top:" + top + "px;'><b>点击编辑模块</b><p>" + description + "</p></div>");

        mask.click(function () {
            var html = $("<div></div>")
            html.load("/Editor?module=" + module, function () {
                layer.open({
                    title: module,
                    content: html.html(),
                    area: ['800px', 'auto'],
                    offset: '100px',
                    success: function (layero, index) {
                        $(".layui-layer-content").removeAttr("style");
                    },
                    btn: ['提交信息'],
                    yes: function (index, layero) {
                        if ($("#editorForm")[0] != undefined)
                            $("#editorForm").submit();
                        else
                            layer.closeAll();
                    }
                });
            });
        });

        $("body").append(mask);
    });
});