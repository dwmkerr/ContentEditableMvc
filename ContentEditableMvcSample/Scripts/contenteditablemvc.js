$(function () {

    var currentEditingWrapper;
    var currentEditingContent;
    var originalContent;
    var focusTarget;
    
    $('.cem-content').focus(function () {
        var cemWrapper = $(this).parent();
        if (currentEditingWrapper != cemWrapper)
            startEditing(cemWrapper);
    });

    $('.cem-content').blur(function (event) {
        //// unless the focus target is part of the wrapper, we'll stop editing.
        //var cemWrapper = $(this).parent();
        //if (cemWrapper.contains(focusTarget) == false)
        //    stopEditing();
    });

    $('.cem-savechanges').click(function () {
        saveChanges(currentEditingWrapper);
    });

    $('.cem-discardchanges').click(function () {
        discardChanges();
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

    function discardChanges() {
        if (currentEditingContent!= null) {
            currentEditingContent.html(originalContent);
            currentEditingContent.blur();
        }
    }
    
    function startEditing(cemWrapper) {
        cemWrapper.addClass('cem-editing');
        cemWrapper.children('.cem-toolbar').slideDown();

        //  Store the current state.
        currentEditingWrapper = cemWrapper;
        currentEditingContent = cemWrapper.children('.cem-content').first();
        originalContent = currentEditingContent.html();
    }
    
    function stopEditing() {
        if (currentEditingWrapper == null)
            return;
        currentEditingWrapper.removeClass('cem-editing');
        currentEditingWrapper.children('.cem-toolbar').slideUp();
        currentEditingWrapper = null;
        currentEditingContent = null;
    }
});