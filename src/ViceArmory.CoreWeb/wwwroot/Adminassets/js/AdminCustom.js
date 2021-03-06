$(document).ready(function () {
    $('#productImagefile').change(function () {
        const file = this.files[0];
        if (file) {
            let reader = new FileReader();
            reader.onload = function (event) {
                $('#editProductImage').attr('src', event.target.result);
                $("#editProductImage").attr('style', 'display:block');
                var imgPath = $("#ProductImage").val();
                var imgeName = imgPath.substring(imgPath.lastIndexOf('/') + 1);
                var folderPath = imgPath.substring(0, imgPath.length - imgeName.length);
                $("#ProductImage").val(folderPath + file.name);
            }
            reader.readAsDataURL(file);
        }
    });

    $("#updateProduct").click(function (e) {
        var img = $("#ProductImage").val();
        var file = document.getElementById("productImagefile");
        if (file.files.length == 0 && (img==""||img==null)) {
            e.preventDefault();
            $("#productImageValidation").html("product image required");
        }
    });

    $("#createProduct").click(function (e) {
        var file = document.getElementById("productImagecreate");
        if (file.files.length == 0) {
            e.preventDefault();
            $("#productImageValidation").html("product image required");
        }
    });

    $("#submitRegister").click(function (e) {
        var password = $("#Password").val();
        var ConfirmPassword = $("#confirmPassword").val();
        if (password !== ConfirmPassword) {
            e.preventDefault();
            $("#valConfirmPassword").text("password and confirm password must be same");
        }
        else {
            return true;
        }
    });
});

function activateAds(value) {
    $("#ActivateModelIdHidden").val(value);
    $("#ActivateModel").modal("show");
}

function deactivateAds(value) {
    $("#DeActivateModelIdHidden").val(value);
    $("#DeActivateModel").modal("show");
}

function deactivateAdsConfirm() {

    var deactivateId = $("#DeActivateModelIdHidden").val();

    $.ajax(
        {
            url: 'http://143.110.244.193:5001/Admin/Weeklyads/DeleteAds',
            data: { "id": deactivateId },
            success: function (response) {
                $("#DeActivateModel").modal("hide");
                if (response.result == true) {
                    window.location.reload();
                }
            },
            error: function (response) {
                $("#DeActivateModel").modal("hide");
                alert("Error occured while deactivate ads");
            },
        });
}

function activateAdsConfirm() {
    var activateId = $("#ActivateModelIdHidden").val();
    $.ajax(
        {
            url: 'http://143.110.244.193:5001/Admin/Weeklyads/ActivateAds',
            data: { "id": activateId },
            success: function (response) {
                $("#ActivateModel").modal("hide");
                if (response.result == true) {
                    window.location.reload();
                }
            },
            error: function (response) {
                $("#ActivateModel").modal("hide");
                alert("Error occured while activate ads");
            },
        });
}

