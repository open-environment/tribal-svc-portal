$(function () {
    $("a.delete-link").click(function () {
        //DESCRIPTION ****************************************
        // Used in conjunction with toastr.js, this javascript performs 3 actions: 
        //    (1) prompts user with deletion confirmation
        //    (2) calls deletion action upon confirmation
        //    (3) displays deletion confirmation message via toastr popup

        //VERSION*********************************************
        // 1.0.0 5/17/2018

        //USAGE **********************************************
        // delete-link: class applied to hyperlink that is originally clicked for deletion
        // delete-confirm: class applied to sibling div that contains the additional attributes needed for deletion
        // data-delete-p: (attribute) URI path to action for deletion
        // data-delete-id: (attribute) key for record being deleted
        // data-delete-id2: (attribute) secondary id to optionally pass to the deletion action. This is sometimes used to redirect back to original page after deletion action finishes
        // data-delete-array: (attribute) indicates if ID is to be handled as an array (set to Y for Yes)
        // data-delete-type: (attribute) helps indicate which parent html block should be removed upon deletion. If blank, assume a table row . If 'cells' then delete the row contents but keep row
        // data-success-url: (attribute) optional redirect to another page upon successful deletion, otherwise will stay on same page


        var deleteLink = $(this);
        deleteLink.hide();
        var confirmButton = deleteLink.siblings(".delete-confirm");
        confirmButton.delay(100).fadeIn(700);

        var cancelDelete = function () {
            removeEvents();
            showDeleteLink();
        };


        //appPath needed to overcome difference in localhost path versus deployed in MVC environment 
        var appPath = (window.location.pathname.split("/")[1] == confirmButton.attr('data-delete-p').split("/")[1] ? '' : window.location.pathname.split("/")[1]);
        var delPath = window.location.protocol + "//" + window.location.host + "/" + appPath + confirmButton.attr('data-delete-p');

        var deleteItem = function () {
            removeEvents();
            confirmButton.fadeOut(700);

            var idval = confirmButton.attr('data-delete-array') == "Y" ? confirmButton.attr('data-delete-id').split(',') : confirmButton.attr('data-delete-id');

            $.ajax({
                type: "POST",
                url: delPath,
                //contentType: "application/json",
                dataType: "json",
                traditional: true,
                data: {
                    id: idval,
                    id2: confirmButton.attr('data-delete-id2')
                },
                success: function (response) {
                    console.log(response);
                    if (response == "Success") {
                        if (confirmButton.attr('data-success-url') != null && confirmButton.attr('data-success-url').length > 0) {
                            var redirPath = window.location.protocol + "//" + window.location.host + "/" + appPath + confirmButton.attr('data-success-url');
                            window.location.replace(redirPath);
                        }

                        //remove html for deleted records
                        var idType = confirmButton.attr('data-delete-type');
                        var parentRow = deleteLink.parents("tr:first");

                        if (idType === 'team') //special handling for tree control
                            parentRow = deleteLink.parents(".treecont:first");
                        else if (idType === 'cells')  //special handling delete column but not row
                            parentRow = deleteLink.parents("td:first").next().nextAll();

                        parentRow.fadeOut('slow', function () {
                            parentRow.remove();
                        });
                    }
                    else {
                        toastr.warning(response);
                    }
                },
                error: function (response) {
                    toastr.warning(response || "Record cannot be deleted.");
                }
            });

            return false;
        };

        var removeEvents = function () {
            confirmButton.off("click", deleteItem);
            $(document).on("click", cancelDelete);
        };

        var showDeleteLink = function () {
            confirmButton.hide();
            deleteLink.fadeIn(700);
        };

        confirmButton.on("click", deleteItem);
        $(document).on("click", cancelDelete);

        return false;
    });

    AddAntiForgeryToken = function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    };
});