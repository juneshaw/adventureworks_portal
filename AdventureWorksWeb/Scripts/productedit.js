// Javascript Object for the ProductEdit panel
var ProductEdit = new function () {
    var _currentProductId = null,
    
    loadProduct = function (productId) {
        // Show the product edit div
        $('#productEdit').show();

        // Hide the success message
        $('#productEdit .success').hide();

        var loadingPanel = new LoadingPanel($('#productEdit'));
        loadingPanel.show();        

        // Load the product details into the product edit template
        var url = 'Products/Details?id=' + productId;

        $.ajax(url, {
            type: "GET",
            dataType: "html",
            success: function (results) {                
                $('#productEdit').html(results);

                loadingPanel.hide();
                $('#productEdit').attr('data-display-mode', 'loaded');

                _currentProductId = productId;

                if (typeof ($.mobile) != "undefined") {
                    // Apply mobile styles
                    $('#productEdit [type="submit"]').button();
                    $('#productEdit input').textinput();
                    $('#productEdit select').selectmenu();

                    $("#productEditPanel").trigger('expand');

                    // Set the focus to the product name
                    $('#ProductNumber').focus();
                }
            },
            error: function (data) {
                loadingPanel.hide();
                $('#productEdit').attr('data-display-mode', 'loaded');
            }
        });
    },

    // Send the user input to the service
    updateProduct = function () {
        var loadingPanel = new LoadingPanel($('#productEdit'));
        loadingPanel.show();

        var name = $('#ProductName').val();
        var number = $('#ProductNumber').val();
        var standardCost = $('#StandardCost').val();
        var listPrice = $('#ListPrice').val();
        var productModel = $('#ProductModel').val();
        var productSubcategory = $('#ProductSubcategory').val();

        var product = {
            ProductId: _currentProductId,
            ProductName: name,
            ProductNumber: number,
            StandardCost: standardCost,
            ListPrice: listPrice,
            ProductModel: productModel,
            ProductSubcategory: productSubcategory
        };

        var url = 'Products/Update';

        $.ajax(url, {
            type: "POST",            
            data: product,
            dataType: "json",
            success: function (data) {                
                if (data.Success == true) {
                    // The update operation has succeeded
                    $('#productEdit .success').show();
                    $('#productEdit .error').hide();
                }
                else {
                    // The update operation has failed - display the error message
                    $('#productEdit .success').hide();
                    $('#productEdit .error').show();

                    var message = '';
                    for (var i = 0; i < data.Messages.length; i++) {
                        if (i > 0)
                            message += '</br>';

                        message += data.Messages[i];
                    }
                    $('#productEdit .errorMessage').html(message);
                }

                loadingPanel.hide();
            },
            error: function (data) {
                loadingPanel.hide();
            }
        });
    };

    return {
        loadProduct: loadProduct,
        updateProduct: updateProduct
    }
}();