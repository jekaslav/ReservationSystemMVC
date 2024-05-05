// GetAll

$(document).ready(function () {
    $.ajax({
        url: '/student',
        type: 'GET',
        success: function (data) {
            displayStudents(data);
        },
        error: function () {
            alert('Ошибка при получении данных студентов.');
        }
    });

    function displayStudents(students) {
        var table = $('#studentsTable tbody');
        $.each(students, function (index, student) {
            var row = $('<tr>');
            row.append($('<td>').text(student.Name));
            row.append($('<td>').text(student.Email));
            table.append(row);
        });
    }

// GetById

    $(document).ready(function () {
        $('#studentDetailsForm').submit(function (event) {
            event.preventDefault();

            var studentId = $('#studentId').val();

            var url = '/student/' + studentId;

            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $('#studentInfo').html(data);
                },
                error: function () {
                    alert('Произошла ошибка при получении данных студента.');
                }
            });
        });
    });


// Create

    $(document).ready(function () {
        $('#createStudentForm').submit(function (event) {
            event.preventDefault();

            var formData = {
                studentName: $('#studentName').val(),
                studentEmail: $('#studentEmail').val(),
            };

            $.ajax({
                url: '/student/create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (data) {
                    $('#creationStatus').text(data);
                    $('#createStudentForm')[0].reset();
                },
                error: function (xhr) {
                    $('#creationStatus').text(xhr.responseText);
                }
            });
        });
    });

})
