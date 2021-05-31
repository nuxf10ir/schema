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

$(function () {
	const $timer = $('#timer');
	const $message1 = $("#message_1");
	const $message2 = $("#message_2");
	const $message3 = $("#message_3");
	const $droppables = $(".droppable");
	const $draggables = $(".draggable");
	const $variants = $("#variants");

	$message1.fadeIn();

	$('button', $message3).on('click', function () {
		$message3.fadeOut(function () {
			$variants.removeClass('active');
			$draggables.removeAttr('style');
			$message1.fadeIn();
		});
	});

	$('button', $message2).on('click', function () {
			$message2.fadeOut(function () {
				$draggables.draggable({ snap: ".item__placeholder" });
				$variants.addClass('active');
			});
		}
	);

	$('button', $message1).on('click', function () {
		$message1.fadeOut(function () {
			$timer.fadeIn();
			startTimer(30, $('#time'), function () {
				$timer.fadeOut();
				$message2.fadeIn();
				$droppables.addClass('active');
			});
		});
	})

	$droppables
		.each(function( index, element )  {
			const $dropable = $(element)
			const $place = $dropable.find('.item__placeholder');
			const accept = $place
				.data('accept')
				.split(',')
				.map(id => '#' + id)
				.join(',');
			$place.droppable({
				accept,
				drop: function( event, ui ) {
					$dropable.removeClass('active');
					ui.draggable.fadeOut().draggable("destroy");
					if (!$variants.find('.ui-draggable').length) {
						$message3.fadeIn();
					}
				}
			});
		});

});
