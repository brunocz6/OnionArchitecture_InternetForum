$(() => {
    $(".redirector").click(function () {
        window.location = this.dataset.url;
    });
    
    let cleanValidationErrors = ($el) => {
        let $container = $el.find(".c-validation-errors");
        
        $container.length > 0 && $container.empty();
    }
    
    let addValidationError = ($el, error) => {
        let $container = $el.find(".c-validation-errors");

        if ($container.length < 1) {
            $container = $("<div class='c-validation-errors'></div>").appendTo($el);
        }
        
        $container.append("<span class='c-validation-errors__msg'>" + error + "</span>");
    }

    let showErrors = ($form, errors) => {
        cleanValidationErrors($form);

        for (const [name, messages] of Object.entries(errors)) {
            messages.forEach((msg) => addValidationError($form, msg));
        } 
    };
    
    $(".c-form").submit((e) => {
        e.preventDefault();
        
        let $form = $(e.target);
        let action = $form.attr("action");
        
        let result = null;
        
        $.post(action, $form.serialize())
            .done(() => {
                location.reload();
            })
            .fail((data) => {
                data.responseJSON?.errors && showErrors($form, data.responseJSON.errors);
                result = false;
            })
    });
});