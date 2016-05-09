var LoadingPanel = function (contentDiv) {
    var _contentDiv = contentDiv,
    _initLoadingPanel = null,

    show = function () {
        if ($(_contentDiv).attr('data-display-mode') == 'init') {
            _initLoadingPanel = $('<div class="loadingdiv"><img src="Images/loading.gif"/></div>');
            $(_contentDiv).append(_initLoadingPanel);
        }
        else {
            $(_contentDiv).block({
                message: '<img src="Images/loading.gif"/>',
                css: {
                    padding: 0,
                    margin: 0,
                    textAlign: 'center',
                    color: '#000',
                    border: 0,
                    backgroundColor: 0,
                    cursor: 'wait'
                },
                overlayCSS: {
                    backgroundColor: '#fff',
                    opacity: 0.6
                }
            });
        }
    },

    hide = function () {
        if ($(_contentDiv).attr('data-display-mode') == 'init') {
            $(_initLoadingPanel).remove();
        }
        else {            
            $(_contentDiv).unblock();
        }
    };

    return {
        show: show,
        hide: hide
    }
}