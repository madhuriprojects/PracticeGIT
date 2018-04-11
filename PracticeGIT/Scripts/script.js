//I need to pass date to backend and receive not available time slots
function hideNotAvailableTimes(date) {
    //I need to receive array of booked hours in this format from the backend 
    var notAvailableTimes = [9, 10, 11, 12, 4];

    // $.get('url', {
    //     q: date
    //   }).done(function(data) {
    //     var notAvailable = data;
    $('.time').show();
    for (var i = 0; i < notAvailableTimes.length; i++) {
        $('#' + notAvailableTimes[i]).hide();
    }

    // });


}


//creates datepicker
$("#datepicker").datepicker({
    beforeShowDay: function (date) {
        $('.time').hide();
        $('#picked').hide();
        return [date.getDay() == 6 || date.getDay() == 0, ""];
    },
    onSelect: function (date) {
        //removing not available times from showing
        hideNotAvailableTimes(date);
    }
});
//hiding time picker and summary before choosing the date
$('.time').hide();
$('#picked').hide();


//showing the picked date and time
$('.time').on('click', function () {
    $('#picked').show();
    var selectedDate = $('#datepicker').val();
    var selectedTime = this.innerText;
    $('#date-picked').text(selectedDate);
    $('#time-picked').text(selectedTime);
});

$('#submit').on('click', function () {
    var selectedDate = $('#date-picked').text();
    var selectedTime = $('#time-picked').text();
    //call function to update database if not using form html
    // $.post('url',
    // {
    //   date: selectedDate,
    //   time: selectedTime,
    //   user: email   
    // },
    // function(data,status){
    //     alert("Data: " + data + "\nStatus: " + status);
    // });

    // selectedTime = this.innerText;
    // console.log(datePicked);
    //update UI
    $("#datepicker").datepicker("refresh");
    $(datepicker).val('');
})