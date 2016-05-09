// Javascript object for the TransactionHistory panel
var TransactionHistory = new function () {
    var _currentPage = 1,
    _totalPages = 0,
    _currentProductId,    

    // Pull down the TransactionHistory HTML into the TransactionHistory panel DOM
    loadTransactionHistory = function (productId) {
        _currentProductId = productId;

        // Show the product edit div
        $('#transactionHistory').show();

        var loadingPanel = new LoadingPanel($('#transactionHistory'));
        loadingPanel.show();

        var url = "TransactionHistory/List?id=" + _currentProductId;

        $.ajax(url, {
            type: "GET",
            dataType: "html",
            success: function (results) {
                $('#transactionHistory').html(results);

                _totalPages = parseInt($('#TransactionHistoryTotalPageCount').val());

                loadingPanel.hide();
                $('#transactionHistory').attr('data-display-mode', 'loaded');

                if (typeof ($.mobile) != "undefined") {
                    // Apply mobile styles
                    $('#transactionHistory ul').listview();
                    $('#transactionHistory [data-role="button"]').button();
                    $('#transactionHistoryPager').selectmenu();
                }
            },
            error: function () {
                loadingPanel.hide();
                $('#transactionHistory').attr('data-display-mode', 'loaded');
            }
        });
    },

    // Load the transaction history of the production fro the data source to the panel
    doPageTransactionHistory = function () {
        var loadingPanel = new LoadingPanel($('#transactionHistoryResults'));
        loadingPanel.show();

        var url = "TransactionHistory/Page?productId=" + _currentProductId + "&pageNumber=" + _currentPage;

        $.ajax(url, {
            type: "GET",
            dataType: "html",
            success: function (results) {
                $('#transactionHistoryResults').html(results);

                loadingPanel.hide();
            },
            error: function () {
                loadingPanel.hide();
            }
        });
    },

    page = function (pageNumber) {
        _currentPage = pageNumber;

        doPageTransactionHistory();
    },

    previousPage = function () {
        if (_currentPage > 1)
            _currentPage = _currentPage - 1;

        doPageTransactionHistory();

        if (typeof ($.mobile) != "undefined") {
            $('#transactionHistoryPager option[value=' + _currentPage.toString() + ']').attr('selected', 'selected');
            $('#transactionHistoryPager').selectmenu('refresh', true);
        }
        else {
            $('#transactionHistoryPager').val(_currentPage.toString());
        }
    },

    nextPage = function () {
        if (_currentPage < _totalPages)
            _currentPage = _currentPage + 1;

        doPageTransactionHistory();

        if (typeof ($.mobile) != "undefined") {
            $('#transactionHistoryPager option[value=' + _currentPage.toString() + ']').attr('selected', 'selected');
            $('#transactionHistoryPager').selectmenu('refresh', true);
        }
        else {
            $('#transactionHistoryPager').val(_currentPage.toString());
        }
    };

    return {
        loadTransactionHistory: loadTransactionHistory,
        page: page,
        previousPage: previousPage,
        nextPage: nextPage
    };
}();