
$(document).ready(function(){
    // Init draggable items functionality (with .js-draggable-items class)
    jQuery( '.js-draggable-items' ).sortable({
        connectWith: '.draggable-column',
        items: '.draggable-item',
        opacity: .75,
        handle: '.draggable-handler',
        placeholder: 'draggable-placeholder',
        tolerance: 'pointer',
        start: function( e, ui ) {
            ui.placeholder.css({
                'height': ui.item.outerHeight(),
                'margin-bottom': ui.item.css( 'margin-bottom' )
            });
        }
    });
});

