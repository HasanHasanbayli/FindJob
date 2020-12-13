

$(document).ready(function () {
    //Search
    let hidden = $("#hidden").val().trim();
    $(document).on("keyup", "#input-search", function () {
        let search = $(this).val().trim();
        $("#searchList a").remove();
        if (search.length != 0) {
            $.ajax({
                url: "/Admin/Dashboard/Search?search=" + search + "&hidden=" + hidden,
                type: "Get",
                success: function (res) {
                    if (hidden == "postjob") {
                        $("#searchList").append(res);
                    }
                    else if (hidden == "popularjob") {
                        $("#searchList").append(res);
                    }
                    console.log(res)
                }
            })
        }
    })
})