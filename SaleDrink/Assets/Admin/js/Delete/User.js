var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.Delete').on('click', function (e) {
            swal({
                title: "Bạn có chắc không?",
                text: "Sau khi đã bị xóa, bạn sẽ không thể khôi phục tập tin này!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        //e.preventDefault();
                        var btn = $(this);
                        var id = btn.data('id');
                        /*DELETE*/
                        $.ajax({
                            type: "POST",
                            url: "/Admin/UserAdministrators/Delete",
                            data: JSON.stringify({ ID: id }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (json) {
                                swal("OK! Tập tin của bạn đã bị xóa!", {
                                    icon: "Thành công",
                                });
                                if (json.isRedirect) {
                                    window.location.href = json.redirectUrl;
                                }
                            },
                            error: function () {
                                swal("Bạn không có quyền xóa tập tin này");
                                
                            }
                        });
                        
                    } else {
                        swal("Tập tin của bạn không xóa!");
                    }
                });

        });
    }
}
    user.init();