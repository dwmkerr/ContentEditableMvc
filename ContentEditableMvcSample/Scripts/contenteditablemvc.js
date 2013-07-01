function saveChanges(cemContent) {
    $.ajax({
        type: 'POST',
        url: cemContent.attr('data-edit-url'),
        data: {
            id: cemContent.attr('data-entity-id'),
            name: cemContent.attr('data-property-name'),
            value: cemContent.html()
        },
            error: function() {
                throw new Error('Failed to save changes, check the controller.');
            }
    });
}
function discardChanges(cemContent) {
    alert('discard changes ' + cemContent.id);
}

$(function() {

    $('.cem-wrapper').focusin(function () {
        $(this).children('.cem-savechanges').toggleClass('cem-editing');
        $(this).children('.cem-discardchanges').toggleClass('cem-editing');
    });

    $('.cem-wrapper').focusout(function () {
        // without a timeout, as soon as we click on the save button, we lose focus, hide it, and lose the click.
        window.setTimeout(function () {
            $(this).children('.cem-savechanges').toggleClass('cem-editing');
            $(this).children('.cem-discardchanges').toggleClass('cem-editing');
        });
    });

    $('.cem-savechanges').click(function () {
        var cemcontent = $(this).siblings('.cem-content').first();
        saveChanges(cemcontent);
    });

    $('.cem-discardchanges').click(function () {
        var cemcontent = $(this).siblings('.cem-content').first();
        discardChanges(cemcontent);
    });
});


/*
document.addEventListener('keydown', function (event) {
    var esc = event.which == 27,
        nl = event.which == 13,
        el = event.target,
        input = el.nodeName != 'INPUT' && el.nodeName != 'TEXTAREA',
        data = {};

    if (input) {
        if (esc) {
            // restore state
            document.execCommand('undo');
            el.blur();
        } else if (nl) {
            // save
            data[el.getAttribute('data-name')] = el.innerHTML;

            // we could send an ajax request to update the field
            
            //$.ajax({
            //  url: window.location.toString(),
            //  data: data,
            //  type: 'post'
            //});
            
            log(JSON.stringify(data));

            el.blur();
            event.preventDefault();
        }
    }
}, true);

function log(s) {
    document.getElementById('debug').innerHTML = 'value changed to: ' + s;
}

*/