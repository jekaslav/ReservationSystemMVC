// GetAll

$(document).ready(function () {
    $.ajax({
        url: '/chiefs',
        type: 'GET',
        success: function (data) {
            displayChiefs(data);
        },
        error: function () {
            alert('Ошибка при получении данных.');
        }
    });

    function displayChiefs(chiefs) {
        var table = $('#chiefsTable tbody');
        $.each(chiefs, function (index, chief) {
            var row = $('<tr>');
            row.append($('<td>').text(chief.Name));
            row.append($('<td>').text(chief.Email));
            table.append(row);
        });
    }

// GetById

    $(document).ready(function () {
        $('#chiefDetailsForm').submit(function (event) {
            event.preventDefault();

            var chiefId = $('#chiefId').val();

            var url = '/chiefs/' + chiefId;

            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $('#chiefInfo').html(data);
                },
                error: function () {
                    alert('Произошла ошибка при получении данных ответственного.');
                }
            });
        });
    });


// Create

    $(document).ready(function () {
        $('#createChiefForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                Name: $('#Name').val(),
                Email: $('#Email').val()
            };

            $.ajax({
                url: '/chiefs/create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#creationStatus').text(data);
                    $('#createChiefForm')[0].reset();
                },
                error: function (xhr) {
                    $('#creationStatus').text(xhr.responseText);
                }
            });
        });
    });


// Update

    $(document).ready(function () {
        $('#updateChiefForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                Name: $('#Name').val(),
                Email: $('#Email').val()
            };

            var chiefId = $('#chiefId').val();

            $.ajax({
                url: '/chiefs/update/' + chiefId,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#updateStatus').text(data);
                },
                error: function (xhr, status, error) {
                    $('#updateStatus').text(xhr.responseText);
                }
            });
        });
    });


// Delete

    $(document).ready(function () {
        $('#deleteChiefForm').submit(function (event) {
            event.preventDefault();

            var chiefId = $('#chiefId').val();

            $.ajax({
                url: '/chiefs/' + chiefId,
                type: 'DELETE',
                success: function (data) {
                    $('#deleteStatus').text(data);
                },
                error: function (xhr, status, error) {
                    $('#deleteStatus').text(xhr.responseText);
                }
            });
        });
    });


// TakeControl
    
    $(document).ready(function () {
        $('#takeControlForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                classroomId: $('#classroomId').val(),
                chiefId: $('#chiefId').val()
            };

            $.ajax({
                url: '/chief/takecontrol',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#takeControlStatus').text(data);
                    $('#takeControlForm')[0].reset();
                },
                error: function (xhr) {
                    $('#takeControlStatus').text(xhr.responseText);
                }
            });
        });

        
// ReleaseControl
        
        $('#releaseControlForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                classroomId: $('#classroomId').val(),
                chiefId: $('#chiefId').val()
            };

            $.ajax({
                url: '/chief/releasecontrol',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#releaseControlStatus').text(data);
                    $('#releaseControlForm')[0].reset();
                },
                error: function (xhr) {
                    $('#releaseControlStatus').text(xhr.responseText);
                }
            });
        });
    });

})
