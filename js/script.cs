function startTimer(duration, display, callback) {
	let timer = duration;
	const interval = setInterval(function () {
		display.text(timer);
		if (--timer < 0) {
			clearInterval(interval);
			callback();
		}
	}, 1000);
}

function onRightDrop($item) {
	$item.fadeOut(function() { $item.remove(); });
}

$(function () {
	$("#message_1").fadeIn();
	$("#message_1 button").one('click', function () {
		$("#message_1").remove();
		$('#timer').fadeIn();
		startTimer(30, $('#time'), function () {
			$('#timer').remove();
			$("#message_2").fadeIn();
			$("#message_2 button").one('click', function () {
				$("#message_2").remove();
				$(".droppable").addClass('active');
				$("#variants").addClass('active');
				$(".droppable").each(function( index, element )  {
					const $dropable = $(element)
					const $place = $dropable.find('.item__placeholder');
					const accept = $place.data('accept').split(',').map(id => '#' + id).join(',');
					$place.droppable({
						accept,
						drop: function( event, ui ) {
							$dropable.removeClass('active');
							onRightDrop(ui.draggable)
						}
					});
				});
				$(".draggable").draggable({ snap: ".item__placeholder" });
			});
		});
	})

});
