function changeRole(id, role) {
    $.ajax({
        type: "post",
        url: 'User/UpdateUserRole',
        data: { "Id": id, "Role": role },
        //success: function (response) {
        //    $(".question-create-component-container").empty();
        //    $(".question-create-component-container").append(response);
        //}
    });
    //$.post('UpdateUserRole', { Id: id, Role: 'role' });
}

