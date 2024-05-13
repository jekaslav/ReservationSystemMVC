// GetAll
$(document).ready(function () {
    $.ajax({
        url: '/requests',
        type: 'GET',
        success: function (data) {
            displayRequests(data);
        },
        error: function () {
            alert('Ошибка при получении заявок.');
        }
    });

    function displayRequests(requests) {
        var table = $('#requestsTable tbody');
        $.each(requests, function (index, request) {
            var row = $('<tr>');
            row.append($('<td>').text(request.StudentId));
            row.append($('<td>').text(request.ClassroomId));
            row.append($('<td>').text(request.StartTime));
            row.append($('<td>').text(request.EndTime));
            row.append($('<td>').text(request.Status));
            table.append(row);
        });
    }
});

// GetById
$(document).ready(function () {
    $('#requestDetailsForm').submit(function (event) {
        event.preventDefault();

        var requestId = $('#requestId').val();

        var url = '/requests/' + requestId;

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $('#requestInfo').html(data);
            },
            error: function () {
                alert('Произошла ошибка при получении данных заявки.');
            }
        });
    });
});



// Create
    
    $(document).ready(function () {
        $('#createRequestForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                StudentId: $('#studentId').val(),
                ClassroomId: $('#classroomId').val(),
                StartTime: $('#startTime').val(),
                EndTime: $('#endTime').val()
            };

            $.ajax({
                url: '/requests/create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#creationStatus').text(data);
                    $('#createRequestForm')[0].reset();
                },
                error: function (xhr) {
                    $('#creationStatus').text(xhr.responseText);
                }
            });
        });
    });


// Update
    
    $(document).ready(function () {
        $('#updateRequestForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                StudentId: $('#studentId').val(),
                ClassroomId: $('#classroomId').val(),
                StartTime: $('#startTime').val(),
                EndTime: $('#endTime').val(),
                Status: $('#status').val()
            };

            var requestId = $('#requestId').val();

            $.ajax({
                url: '/requests/update/' + requestId,
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
        $('#deleteRequestForm').submit(function (event) {
            event.preventDefault();

            var requestId = $('#requestId').val();

            $.ajax({
                url: '/requests/' + requestId,
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


// UpdateStatus    

$(document).ready(function () {
    $('#updateStatusButton').click(function (event) {
        event.preventDefault();

        var chiefId = $('#chiefId').val();
        var reservationRequestId = $('#reservationRequestId').val();
        var newStatus = $('#newStatus').val();

        $.ajax({
            url: '/requests/update/status/' + chiefId + '/' + reservationRequestId + '/' + newStatus,
            type: 'PUT',
            contentType: 'application/json',
            success: function (data) {
                $('#updateStatusResult').html('<p>Статус успешно обновлен.</p>');
            },
            error: function (xhr, status, error) {
                $('#updateStatusResult').html('<p>Произошла ошибка при обновлении статуса: ' + xhr.responseText + '</p>');
            }
        });
    });
});








