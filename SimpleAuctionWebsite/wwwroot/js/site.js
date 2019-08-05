
$(function () {
    $('#place-bid-btn').on('click', function (evt) {
        evt.preventDefault();
        var formData = $('form').serialize();
       
        $.post('/Home/PlaceBid', formData, function (data) {

            if (!$('form').valid()) {
                $('#response-text').addClass('error');
            } else {
                $('#response-text').removeClass('error');
            }

            if (data.success) {
                $('#bid-count').show();
                $('#response-text').removeClass('error');
                $('#response-text').text(data.message);
                $('#bid-count').text("Current bid count : " + data.bidCount);
                $("#last-bid").text("Last bid : $" + $('#bid-amount').val());
            } else {
                $('#bid-count').hide();
                $('#response-text').addClass('error');
                $('#response-text').text(data.message);
            }
        });
    });
});

