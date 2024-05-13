// GetAll

$(document).ready(function () {
    $.ajax({
        url: '/classrooms',
        type: 'GET',
        success: function (data) {
            displayClassrooms(data);
        },
        error: function () {
            alert('Ошибка при получении данных аудиторий.');
        }
    });

    function displayClassrooms(classrooms) {
        var table = $('#classroomsTable tbody');
        $.each(classrooms, function (index, classroom) {
            var row = $('<tr>');
            row.append($('<td>').text(classroom.roomNumber));
            row.append($('<td>').text(classroom.capacity));
            row.append($('<td>').text(classroom.location));
            table.append(row);
        });
    }


// GetById

    $(document).ready(function () {
        $('#classroomDetailsForm').submit(function (event) {
            event.preventDefault();

            var classroomId = $('#classroomId').val();

            var url = '/classrooms/' + classroomId;

            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $('#classroomInfo').html(data);
                },
                error: function (xhr, status, error) {
                    if (xhr.status == 404) {
                        alert('Запись не найдена.');
                    } else {
                        alert('Произошла ошибка при получении данных аудитории.');
                    }
                }
            });
        });
    });



// Create

    $(document).ready(function () {
        $('#createClassroomForm').submit(function (event) {
            event.preventDefault(); 

            var formData = {
                roomNumber: $('#roomNumber').val(),
                capacity: $('#capacity').val(),
                location: $('#location').val()
            };
            
            $.ajax({
                url: '/classroom/create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#creationStatus').text(data); 
                    $('#createClassroomForm')[0].reset();
                },
                error: function (xhr, status, error) {
                    $('#creationStatus').text(xhr.responseText); 
                }
            });
        });
    });


// Update

    $(document).ready(function () {
        $('#updateClassroomForm').submit(function (event) {
            event.preventDefault(); 

            var formData = {
                roomNumber: $('#roomNumber').val(),
                capacity: $('#capacity').val(),
                location: $('#location').val()
            };

            var classroomId = $('#classroomId').val();
            
            $.ajax({
                url: '/classrooms/update/' + classroomId,
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
        $('#deleteClassroomForm').submit(function (event) {
            event.preventDefault();

            var classroomId = $('#classroomId').val();

            $.ajax({
                url: '/classrooms/' + classroomId,
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
})