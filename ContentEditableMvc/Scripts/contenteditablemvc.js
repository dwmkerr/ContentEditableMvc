function saveChanges(cemContent) {

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

function discardChanges(cemContent) {
    alert('discard changes ' + cemContent.id);
}

$(function () {

    $('.cem-wrapper').focusin(function () {
        $(this).toggleClass('cem-editing');
        $(this).children('.cem-toolbar').toggleClass('cem-editing');
    });

    $('.cem-wrapper').focusout(function () {
        // without a timeout, as soon as we click on the save button, we lose focus, hide it, and lose the click.
        window.setTimeout(function (wrapper) {
            wrapper.toggleClass('cem-editing');
            wrapper.children('.cem-toolbar').toggleClass('cem-editing');
        }, 100, $(this));
    });

    $('.cem-savechanges').click(function () {
        var cemcontent = $(this).closest('.cem-wrapper').children('.cem-content').first();
        saveChanges(cemcontent);
    });

    $('.cem-discardchanges').click(function () {
        var cemcontent = $(this).closest('.cem-wrapper').children('.cem-content').first();
        discardChanges(cemcontent);
    });

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
});