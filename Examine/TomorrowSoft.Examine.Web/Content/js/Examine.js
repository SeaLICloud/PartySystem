//操作成功则刷新页面，失败提示失败原因
function callbackHandle(data) {
    if (data.IsSuccess) {
        $.pjax.reload("#main_content", { timeout: 10000 });
        $.sticky(data.Message, { autoclose: 5000, position: "top-right", type: "st-success" });
    } else {
        $.sticky(data.Message, { autoclose: 5000, position: "top-right", type: "st-error" });
    }
}

$(function () {
    $(document).pjax('a[data-pjax]', '#main_content', { fragment: '#main_content', timeout: 10000 });
});

//防止按钮双击触发两次提交
function DisableButton() {
    var button = $(this);
    button.attr("disabled", "true");
    button.html("正在" + button.html() + "...");
}

//pjax方式的提交
$(document).on('submit', 'form[data-pjax]', function (event) {
    event.preventDefault();
    $.pjax.submit(event, '#main_content', { "timeout": 10000 })
});

//使用pjax加载页面时，统一加上样式
$(document).on('pjax:complete', function() {
    $("form input[type='text']").addClass("form-control");
    $("form select").addClass("form-control");
    $("form textarea").addClass("form-control");
});

//打开页面，统一加上样式
$(function() {
    $("form input[type='text']").addClass("form-control");
    $("form select").addClass("form-control");
    $("form textarea").addClass("form-control");
});

//brand添加链接
$(function () {
    var path = location.pathname;
    var re = /\/(\w+)\//g;
    var result = path.match(re)[0];
    $('#brand').attr('href', location.origin + result + 'Home/Index/');
})