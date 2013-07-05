$(function () {

    var currentEditingWrapper;
    var currentEditingContent;
    var originalContentDict = {};

    $('.cem-content').focus(function () {
        var cemWrapper = $(this).parent();
        if (currentEditingWrapper != cemWrapper)
            startEditing(cemWrapper);
    });

    function blurTimeout(cemContent) {
        var cemWrapper = cemContent.parent();
        stopEditing(cemWrapper);
        //  We can discard the changes safely - if we've lost focus by hitting save, they're already saved.
        discardChanges(cemWrapper);
    }

    $('.cem-content').blur(function () {
        var cemContent = $(this);
        window.setTimeout(function () {
            blurTimeout(cemContent);
        }, 100);
    });

    $('.cem-savechanges').click(function () {
        saveChanges(currentEditingContent);
    });

    $('.cem-discardchanges').click(function () {
        var cemWrapper = $(this).closest('.cem-wrapper');
        discardChanges(cemWrapper);
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

    function saveChanges(cemContent) {

        //  Clear the entry in the original content dictionary (we won't need to restore it).
        originalContentDict[cemContent.parent()] = null;

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

    // todo remove dependency on global variables for the content object, only store the old html
    function discardChanges(cemWrapper) {
        //  If we have content to restore for the cem, restore it now.
        if (originalContentDict != null && originalContentDict[cemWrapper] != null)
            cemWrapper.find('.cem-content').html(originalContentDict[cemWrapper]);
        cemWrapper.blur();
    }

    function startEditing(cemWrapper) {
        cemWrapper.addClass('cem-editing');
        cemWrapper.children('.cem-toolbar').show();

        //  Store the current state.
        currentEditingWrapper = cemWrapper;
        currentEditingContent = cemWrapper.children('.cem-content').first();
        originalContentDict[cemWrapper] = currentEditingContent.html();
    }

    function stopEditing(cemWrapper) {
        cemWrapper.removeClass('cem-editing');
        cemWrapper.children('.cem-toolbar').hide();
    }
});