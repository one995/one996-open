layui.form.verify({
    username: function (value, item) {
        //value：表单的值、item：表单的DOM对象
        if (!new RegExp("^[a-zA-Z0-9_\u4e00-\u9fa5\\s·]+$").test(value)) {
            return '用户名不能有特殊字符';
        }
        if (/(^\_)|(\__)|(\_+$)/.test(value)) {
            return '用户名首尾不能出现下划线\'_\'';
        }
        if (/^\d+\d+\d$/.test(value)) {
            return '用户名不能全为数字';
        }
        if (value.length<3) {
            return '用户名长度不能小于3';
        }
        if (value.length >8) {
            return '用户名长度不能大于8';
        }
        //如果不想自动弹出默认提示框，可以直接返回 true，这时你可以通过其他任意方式提示（v2.5.7 新增）
        if (value === '中国') {
            alert('用户名不能为敏感词');
            return true;
        }
    },
        //我们既支持上述函数式的方式，也支持下述数组的形式
    //数组的两个值分别代表：[正则匹配、匹配不符时的提示文字]
    password: [
        /^[\S]{8,12}$/
        , '密码必须8到12位，且不能出现空格'
    ],
    title: function (value) {
        if (value.length < 5) {
            return '标题至少得5个字符啊';
        }
    }, fname: function (value) {
        if (value.length < 4) {
            return '请输入至少4位的用户名';
        }
    }, contact: function (value) {
        if (value.length < 4) {
            return '内容请输入至少4个字符';
        }
    }
    , phone: [/^1[3|4|5|7|8]\d{9}$/, '手机必须11位，只能是数字！']
    , 
    confirmPass: function (value) {
        if ($('input[name=password]').val() !== value)
            return '两次密码输入不一致！';
    }
});

