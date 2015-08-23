; skimurui.flair = (function () {

    
    var deleteFlairRow = function (flairId) {

        if (!skimurui.login.checkLoggedIn("You must be logged in to change flair."))
            return;
        skimur.removeFlairById(flairId, function (result) {

            if (result.success) {
                return true;
            } else {
                skimur.displayError(result.error);
            }
        });
    };

    var openFlairBoxLink = function (element) {

        if (!skimurui.login.checkLoggedIn("You must be logged in to change flair."))
            return;
        skimur.GetAllFlairByUserAndType("2", function (result) {

            if (result.success) {
                $("#divlinkflair").html(result.html);
            } else {
                skimur.displayError(result.error);
            }
        });
    };
    var openFlairBoxUser = function (element) {
       
        if (!skimurui.login.checkLoggedIn("You must be logged in to change flair."))
            return;
        skimur.GetAllFlairByUserAndType("1",function (result) {
           
            if (result.success) {
                $("#divuserflair").html(result.html);
            } else {
                skimur.displayError(result.error);
            }
        });
    };

     
    var insertFlair = function (text, cssClass, textEditable, type) {
       
        skimur.insertFlair(text, cssClass, textEditable, type, function (result) {
            if (result.success) {
                
            } else {
                skimur.displayError(result.error);
            } 
        }); 
      
    };

    var editFlairInBox = function (element) {
        var flairId = $(element).data("flair-id");

        skimur.getFlairById(flairId, function (result) {
            if (result.success) {
                $('#showEditInBox').show();
                $("#flairUserName").html(result.usename);
                $("#flairId").val(result.userid);
                $("#flairText").val(result.text);
                $(".delete-selected-flair").attr("data-flair-to-delete-id", flairId);
                $("#flairUserNameDelete").html(result.usename);
                $("#flairTextDelete").html(result.text);

            } else {
                skimur.displayError(result.error);
            }
        });

    };


    var saveCurrentFlair = function (element) {
        var flairId = $(element).data("flair-to-delete-id");
      

        skimur.confirmDelete(function (result) {
            if (result.confirmed) {
        skimur.removeFlairById(flairId, function (result) {
            if (result.success) {
                openFlairBox(element);
            } else {
                skimur.displayError(result.error);
            } 
        }); 
            }
        });
    };
    return {
        openFlairBoxUser: openFlairBoxUser,
       editFlairInBox: editFlairInBox,     
       saveCurrentFlair, saveCurrentFlair,
       openFlairBoxLink: openFlairBoxLink,
       deleteFlairRow: deleteFlairRow,
       insertFlair: insertFlair
    };

})();
 

//   removeFlairInBox: removeFlairInBox,