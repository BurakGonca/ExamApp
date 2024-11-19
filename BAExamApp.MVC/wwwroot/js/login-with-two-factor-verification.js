$(document).ready(function () {
	$('#verificationForm').hide();

	$('#loginForm').submit(function (e) {
		e.preventDefault();

		var email = $('#loginForm #Email').val().trim();
		var password = $('#loginForm #Password').val().trim();
		$('#loginError').text('');
		if (!email || !password) {
			$('#loginError').text('Lütfen tüm alanları doldurun.');
			return false;
		}

		$.ajax({
			url: '/Login/Verify',
			type: 'POST',
			data: $(this).serialize(),
			success: function (response) {
				if (response.success) {
					$('#verificationForm').show();
					$('#loginForm :input').prop('disabled', true);
				}
			},
			error: function (xhr, status, error) {
				console.log(
					'İstek başarısız! Durum Kodu: ' + xhr.status + ', Hata: ' + error
				);
			},
		});
	});

	$('#verificationForm').submit(function (e) {
		e.preventDefault();

		var verificationCode = $('#verificationCodeInput').val().trim();

		if (!verificationCode) {
			$('#verificationCodeError').text('Lütfen kod girişinizi yapın.');
			return false;
		}

		var formData = new FormData($('#verificationForm')[0]);
		formData.append('Email', $('#loginForm #Email').val());
		formData.append('Password', $('#loginForm #Password').val());
		formData.append('VerificationCode', $('#loginForm #VerificationCode').val());

		$.ajax({
			url: '/Login/Index',
			type: 'POST',
			data: formData,
			processData: false,
			contentType: false,
			success: function (response) {
				if (response.success) {
					location.reload();
				} else {
					$('#verificationCodeError').text('Hatalı / Geçersiz kod girişi.');
				}
			},
		});
	});
});