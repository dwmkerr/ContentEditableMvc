$(function () {

    var currentEditingWrapper;

    $('.cem-content').focus(function () {
        var cemWrapper = $(this).parent();
        if (currentEditingWrapper != cemWrapper)
            startEditing(cemWrapper);
    });

    function blurTimeout(cemContent) {
        var cemWrapper = cemContent.parent();
        stopEditing(cemWrapper);
    }

    $('.cem-content').blur(function () {
        var cemContent = $(this);
        window.setTimeout(function () {
            blurTimeout(cemContent);
        }, 100);
    });

    $('.cem-savechanges').click(function () {
        var cemWrapper = $(this).closest('.cem-wrapper');
        saveChanges(cemWrapper);
    });

    //  No need for .cem-discardchanges - clicking it blurs the input, so it discards the changes anyway.

    $('.cem-content').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            var allowMultiline = $(this).attr('data-multiline');

            //  If we're not allowing multiline, save changes instead.
            if (allowMultiline != "true") {
                event.preventDefault();
                saveChanges($(this));
                $(this).blur();
                return false;
            }
        }
        return true;
    });

    function saveChanges(cemWrapper) {

        //  Clear the original value, so we don't reset it.
        var cemContent = cemWrapper.find('.cem-content');
        cemWrapper.data('original', null);

        var data = {
            PropertyName: cemContent.attr('data-property-name'),
            NewValue: cemContent.html(),
            RawModelData: cemContent.attr('data-model-data')
        };

        $.ajax({
            type: 'POST',
            url: cemContent.attr('data-edit-url'),
            data: data,
            error: function () {
                throw new Error('Failed to save changes, check the controller.');
            }
        });
    }

    function startEditing(cemWrapper) {
        cemWrapper.addClass('cem-editing');
        cemWrapper.children('.cem-toolbar').show();
        var cemContent = cemWrapper.find('cem-content');

        //  Store the current state.
        currentEditingWrapper = cemWrapper;
        cemWrapper.data('original', cemContent.html());
    }

    function stopEditing(cemWrapper) {
        cemWrapper.removeClass('cem-editing');
        cemWrapper.children('.cem-toolbar').hide();

        //  Get the content.
        var cemContent = cemWrapper.find('.cem-content');

        //  If we have an original value, set it.
        if (cemWrapper.data('original') != null)
            cemContent.html(cemWrapper.data('original'));
    }
});