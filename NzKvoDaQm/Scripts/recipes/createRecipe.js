(function() {
    const imageTemplate = '<img class="recipe-image"/>';

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

    $('#add-images').change(function() {

        $('#loading').show();

        $('#images-container').empty();

        loadImages().done(function(data) {
            populateImagesContainer(data);
            $('#loading').hide();
        });
    });

    function loadImages() {
        let deferred = Q.defer();
        let imagesData = [];
        let images = $('#add-images').get(0).files;

        for (let j = 0; j < images.length; j++) {
            let reader = new FileReader();
            reader.onload = function () {
                imagesData.push(reader.result);
                if (j === (images.length - 1)) {
                    deferred.resolve(imagesData);
                }
            }
            reader.readAsDataURL(images[j]);
        }

        return deferred.promise;
    }

    function populateImagesContainer(images) {
        $.each(images, function(index, imageData) {
            let image = $(imageTemplate);
            image.attr('src', imageData);
            $('#images-container').prepend(image);
        });
    }

    $('#add-ingredients').click(function() {
        $('#ingredients-container').append(ingredientTemplate);
    });

    $('#add-step').click(function () {
        $('#steps-container').append(stepTemplate);
    });

    $('#submit-button').click(function () {
        let data = extractFormData();

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
        let data = {};

        data['Title'] = $('#title').val();
        
        data['Images'] = [];

        $('#images-container').children().each(function(index, element) {
            let imageData = $(element).attr('src');
            data['Images'].push(imageData);
        });

        let ingredientsData = extractIngredientsData();

        data['IngredientsNames'] = ingredientsData['IngredientsNames'];
        data['IngredientsMeasurementTypes'] = ingredientsData['IngredientsMeasurementTypes'];
        data['IngredientsQuantities'] = ingredientsData['IngredientsQuantities'];

        let stepsData = extractStepsData();

        data['StepsTexts'] = stepsData['StepsTexts'];
        data['StepsMinutes'] = stepsData['StepsMinutes'];

        data['MinutesRequiredForCooking'] = $(' #minutesRequiredToCook').val();

        return data;
    }
})();