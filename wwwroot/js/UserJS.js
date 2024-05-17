$(document).ready(function () {

    ReadAllUsers();

});

function ReadAllUsers() {
    $.ajax({
        url:'/User/ReadUsers',
        type:'get',
        dataType:'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var obj = '';
                obj += '<tr>';
                obj += '<td class="text-center" colspan="8">' + 'Users not available in the Database' + '</ td>';
                obj += '</ tr>';

                $('#table').html(obj);
            }
            else
            {
                var obj = '';
                $.each(response, function (index, item) {
                    console.log("Processing user:", item.id, item.Email, item.Gender, item.PhoneNum,
                        item.DataCreated, item.DataUpdated);

                    obj += '<tr>';
                    obj += '<td>' + item.id + '</ td>';
                    obj += '<td>' + item.name + '</ td>';
                    obj += '<td>' + item.email + '</ td>';
                    obj += '<td>' + item.gender + '</ td>';
                    obj += '<td>' + item.phoneNum + '</ td>';
                    obj += '<td>' + item.dataCreated + '</ td>';
                    obj += '<td>' + item.dataUpdated + '</ td>';
                    obj += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id +')">Delete</a> </ td>';
                    obj += '</tr>';
                });
                $('#table').html(obj);
            }
        },
        error: function () {
            alert('Unable to read the Data');
        }
    });
}

$('#adduser').click(function () {
    $('#UserModal').modal('show');
    $('#modalTitle').text('Create User');
});

function Insert() {
    var formData = new Object();
    formData.Name = $('#Name').val();
    formData.Email = $('#Email').val();
    formData.Password = $('#Password').val();
    formData.Gender = $('.gender:checked').val();
    formData.Address = $('#Address').val();
    formData.PhoneNum = $('#PhoneNum').val();

    $.ajax({
        url:'/User/CreateUser',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save data!');
            } else {
                HideModel();
                alert(response);
                ReadAllUsers();

            }
        },
        error: function () {
            alert('Unable to save data!');
        }
    });
}

function Edit(Id) {
    $.ajax({
        url: 'User/EditUser?id=' + Id,
        type:'GET',
        contentType:'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to Read the data!');
            }
            else {
                $('#UserModal').modal('show');
                $('#modalTitle').text('Update User');
                $('#submit').css('display', 'none');
                $('#Update').css('display', 'block');
                
                $('#Id').val(response.id);
                $('#Name').val(response.name);
                $('#Email').val(response.email);
                $('input[name="flexRadio"][value=' + response.gender + ']').prop('checked', true);
                $('#Password').val(response.password);
                $('#Address').val(response.address);
                $('#PhoneNum').val(response.phoneNum);               

            }

        },
        error: function () {
            alert('Unable to Read data!');
        }
    });
}

function Update() {
    var formData = new Object();
    formData.Id = $('#Id').val();
    formData.Name = $('#Name').val();
    formData.Email = $('#Email').val();
    formData.Password = $('#Password').val();
    formData.Gender = $('.gender:checked').val();
    formData.Address = $('#Address').val();
    formData.PhoneNum = $('#PhoneNum').val();

    $.ajax({
        url: '/User/UpdateUser',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save data!');
            } else {
                HideModel();
                ReadAllUsers();
                alert(response);               

            }
        },
        error: function () {
            alert('Unable to save data!');
        }
    });
}

function Delete(Id) {
    if (confirm('Are you sure to delete this data?')) {

        $.ajax({
            url: 'User/DeleteUser?id=' + Id,
            type: 'POST',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to Delete the data!');
                }
                else {
                    alert(response);
                    ReadAllUsers();
                }

            },
            error: function () {
                alert('Unable to Delete the data!');
            }
        });
    }
}

function HideModel() {
    ClearData();
    $('#UserModal').modal('hide');
    $('#submit').css('display', 'block');
    $('#Update').css('display', 'none');
}

function ClearData() {
    $('#Name').val('');
    $('#Email').val('');
    $('#Password').val('');
    $('input[name="flexRadio"][value="Others"]').prop('checked', true);
    $('#Address').val('');
    $('#PhoneNum').val('');

}