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
     
    return { 
       deleteFlairRow: deleteFlairRow 
    };

})();
 

//   removeFlairInBox: removeFlairInBox,