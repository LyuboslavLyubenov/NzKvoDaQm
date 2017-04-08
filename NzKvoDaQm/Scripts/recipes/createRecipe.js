(function() {
    const stepTemplate =
    '<li class="step">' +
        '<textarea class="form-control step-name" placeholder="Какво трябва да се направи?"></textarea>' +
        'Нужно време: <input type="text" class="form-control step-minutes"/> минути' +
    '</li>';

    $('#add-step').click(function () {
        $('#steps-container').append(stepTemplate);
    });

    $('#submit-button').click(function () {
        extractFormData().then(function (data) {
            $.ajax({
                type: "POST",
                url: "/Recipes/Create",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                processData: false,
                success: function () {
                    alert("Успешно качена рецепта!");
                },
                error: function () {
                    alert("Проблем при качването на рецептата!");
                }
            });
        });
    });

    function extractFormData() {
        let deferred = Q.defer();
        let data = {};

        data["Title"] = $('#title').val();
        
        let images = $('#add-photos > input').get(0).files;

        data["Images"] = [];

        for (let j = 0; j < images.length; j++) {
            let reader = new FileReader();
            reader.onload = function(e) {
                data["Images"].push(reader.result);
                if (j === (images.length - 1)) {
                    deferred.resolve(data);
                }
            }
            reader.readAsBinaryString(images[j]);
        }

        data["Ingredients"] = $('#ingredients').val().split(',');

        let stepsElements = $('.step');

        data["StepsTexts"] = [];
        data["StepsMinutes"] = [];

        for (var i = 0; i < stepsElements.length; i++) {
            let stepDescription = $(stepsElements[i]).find('.step-name').val();
            let stepMinutesRequired = $(stepsElements[i]).find('.step-minutes').val();
            data["StepsTexts"].push(stepDescription);
            data["StepsMinutes"].push(stepMinutesRequired);
        }

        data["MinutesRequiredForCooking"] = $("#minutesRequiredToCook").val();
        return deferred.promise;
    }
})();