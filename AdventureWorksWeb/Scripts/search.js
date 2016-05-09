// Javascript Object for the Search panel
var Search = new function () {
    var _currentPage = 1,
    _currentSortColumn = 'Name',
    _currentSortDirection = 'ASC',
    _currentKeyword = '',
    _totalPages = 0,
    _loadedRow = null,

    // Bind to the keydown event to see if the user pressed the ENTER key    
    keyDown = function (event, keyword) {
        if (event.which == 13)
            search(keyword);
    },

    setTotalPages = function (totalPages) {
        _totalPages = totalPages;
    },

    search = function (keyword) {
        _currentKeyword = keyword;
        _currentPage = 1;
        _currentSortColumn = 'Name';
        _currentSortDirection = 'ASC';

        var loadingPanel = new LoadingPanel($('#searchContent'));
        loadingPanel.show();

        var url = "Products/Search?keyword=" + _currentKeyword;

        $.ajax(url, {
            type: "GET",
            dataType: "html",
            success: function (results) {
                $('#searchContent').html(results);

                _totalPages = parseInt($('#TotalPageCount').val());

                loadingPanel.hide();
                $('#searchContent').attr('data-display-mode', 'loaded');

                if (typeof ($.mobile) != "undefined") {
                    // Apply mobile styles
                    $('#searchContent ul').listview();
                    $('#searchContent [data-role="button"]').button();
                    $('#searchPager').selectmenu();
                }
            },
            error: function () {
                loadingPanel.hide();
                $('#searchContent').attr('data-display-mode', 'loaded');
            }
        });
    },

    doPaging = function () {
        var loadingPanel = new LoadingPanel($('#searchResults'));
        loadingPanel.show();

        var url = "Products/Page?pageNumber=" + _currentPage + "&sortColumn=" + _currentSortColumn + "&sortDirection=" + _currentSortDirection + "&keyword=" + _currentKeyword;

        $.ajax(url, {
            type: "GET",
            dataType: "html",
            success: function (results) {
                $('#searchResults').html(results);

                loadingPanel.hide();

                if (typeof ($.mobile) != "undefined") {
                    // Apply mobile styles
                    $('#searchContent ul').listview();
                }
            },
            error: function () {
                loadingPanel.hide();
            }
        });
    },

    // Update the sort solumn and direction and do the search
    sort = function (sortColumn) {
        if (_currentSortColumn == sortColumn) {
            if (_currentSortDirection == 'ASC')
                _currentSortDirection = 'DESC';
            else
                _currentSortDirection = 'ASC';
        }
        else {
            _currentSortDirection = 'ASC';
        }

        _currentSortColumn = sortColumn;
        _currentPage = 1;

        doPaging();
    },

    // Update the page number and do the search
    page = function (pageNumber) {
        _currentPage = pageNumber;

        doPaging();
    },

    // Update the page number and do the search
    previousPage = function () {
        if (_currentPage > 1)
            _currentPage = _currentPage - 1;

        doPaging();

        if (typeof ($.mobile) != "undefined") {
            $('#searchPager option[value=' + _currentPage.toString() + ']').attr('selected', 'selected');
            $('#searchPager').selectmenu('refresh', true);
        }
        else {
            $('#searchPager').val(_currentPage.toString());
        }
    },

    // Update the page number and do the search
    nextPage = function () {
        if (_currentPage < _totalPages)
            _currentPage = _currentPage + 1;

        doPaging();

        if (typeof ($.mobile) != "undefined") {
            $('#searchPager option[value=' + _currentPage.toString() + ']').attr('selected', 'selected');
            $('#searchPager').selectmenu('refresh', true);
        }
        else {
            $('#searchPager').val(_currentPage.toString());
        }
    },

    // The user has selected to view the details of a product
    // Load the product details and transaction history panels
    loadProduct = function (productId) {
        var loadedRow = $('#row_' + productId);

        if ($(_loadedRow) != null)
            $(_loadedRow).removeClass('loaded');

        _loadedRow = loadedRow;
        $(_loadedRow).addClass('loaded');

        ProductEdit.loadProduct(productId);
        TransactionHistory.loadTransactionHistory(productId);
    };

    return {
        search: search,
        keyDown: keyDown,
        sort: sort,
        page: page,
        previousPage: previousPage,
        nextPage: nextPage,
        loadProduct: loadProduct
    };
}();