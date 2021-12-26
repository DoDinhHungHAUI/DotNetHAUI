var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
      
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    soLuong: $(item).val(),
                    sanPham: {
                        MaSP: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/ShoppingCart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Gio-hang";
                    }
                }
            })
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/ShoppingCart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Gio-hang";
                    }
                }
            })
        });
    }
}
cart.init();