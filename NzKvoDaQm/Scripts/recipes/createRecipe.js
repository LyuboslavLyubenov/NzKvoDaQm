(function() {

    const ingredientTemplate =
        '<li class="ingredient ordered-list-item">' +
            '<input type="text" class="ingredient-name form-control" placeholder="Име на съставката" />' +
            '<input type="text" class="ingredient-quantity form-control" oninput="this.value=this.value.replace(/[^0-9]/g,\'\');"/>' +
            '<select class="ingredient-measurement-type form-control">' +
                '<option selected>Мерна единица</option>' +
                '<option value="0">Литра</option>' +
                '<option value="1">Милилитра</option>' +
                '<option value="2">Лъжици</option>' +
                '<option value="3">Чаени лъжици</option>' +
                '<option value="4">Грама</option>' +
                '<option value="5">Килограма</option>' +
            '</select>' +
        '</li>';

    const stepTemplate =
    '<li class="step ordered-list-item">' +
        '<textarea class="form-control step-name" placeholder="Какво трябва да се направи?"></textarea>' +
        'Нужно време: <input type="text" class="form-control step-minutes" oninput="this.value=this.value.replace(/[^0-9]/g,\'\');"/> минути' +
    '</li>';

    $('#add-ingredients').click(function() {
        $('#ingredients-container').append(ingredientTemplate);
    });

    $('#add-step').click(function () {
        $('#steps-container').append(stepTemplate);
    });

    $('#submit-button').click(function () {
        extractFormData().then(function(data) {

            $.ajax({
                beforeSend: function(xhr) {
                    let token = $('input[name="__RequestVerificationToken"]').val();
                    xhr.setRequestHeader('__RequestVerificationToken', token);
                },
                type: 'POST',
                url: '/Recipes/Create',
                contentType: 'application/x-www-form-urlencoded',
                data: data,
                success: function() {
                    alert('Успешно качена рецепта!');
                },
                error: function(xhr, status, errorThrown) {
                    $.notify({
                            message: JSON.parse(xhr.responseText).error
                        },
                        {
                            type: 'danger',
                            placement: {
                                from: 'top',
                                align: 'center'
                            },
                            animate: {
                                enter: 'animated fadeInDown',
                                exit: 'animated fadeOutUp'
                            }
                        });
                }
        });
        });
    });

    function extractIngredientsData() {
        let result = {};

        result['IngredientsNames'] = [];
        result['IngredientsMeasurementTypes'] = [];
        result['IngredientsQuantities'] = [];

        $('.ingredient').each(function (index, element) {
            let ingredientName = $(element).find('.ingredient-name').first().val();
            let ingredientMeasurementType = $(element).find('.ingredient-measurement-type').first().val();
            let ingredientQuantity = $(element).find('.ingredient-quantity').first().val();
            result['IngredientsNames'].push(ingredientName);
            result['IngredientsMeasurementTypes'].push(ingredientMeasurementType);
            result['IngredientsQuantities'].push(ingredientQuantity);
        });

        return result;
    }

    function extractStepsData() {
        let result = {};

        result['StepsTexts'] = [];
        result['StepsMinutes'] = [];

        $('.step').each(function(index, element) {
            let stepDescription = $(element).find('.step-name').first().val();
            let stepMinutesRequired = parseInt($(element).find('.step-minutes').first().val());
            result['StepsTexts'].push(stepDescription);
            result['StepsMinutes'].push(stepMinutesRequired);
        });

        return result;
    }

    function extractFormData() {
        let deferred = Q.defer();
        let data = {};

        data['Title'] = $('#title').val();
        
        let images = $('#add-photos > input').get(0).files;

        data['Images'] = [];

        if (images.length === 0) {
            setTimeout(
                function() {
                    deferred.resolve(data);
                },
                10);
        } else {
            for (let j = 0; j < images.length; j++) {
                let reader = new FileReader();
                reader.onload = function (e) {
                    data['Images'].push(reader.result);
                    if (j === (images.length - 1)) {
                        deferred.resolve(data);
                    }
                }
                reader.readAsDataURL(images[j]);
            }
        }

        let ingredientsData = extractIngredientsData();

        data['IngredientsNames'] = ingredientsData['IngredientsNames'];
        data['IngredientsMeasurementTypes'] = ingredientsData['IngredientsMeasurementTypes'];
        data['IngredientsQuantities'] = ingredientsData['IngredientsQuantities'];

        let stepsData = extractStepsData();

        data['StepsTexts'] = stepsData['StepsTexts'];
        data['StepsMinutes'] = stepsData['StepsMinutes'];

        data['MinutesRequiredForCooking'] = $(' #minutesRequiredToCook').val();

        return deferred.promise;
    }
})();