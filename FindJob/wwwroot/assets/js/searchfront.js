$(document).ready(function () {
    //Search
    let hidden = $("#hidden").val().trim();
    $(document).on("keyup", "#input-search2", function () {
        let search = $(this).val().trim();
        $("#searchList2 a").remove();
        $("#searchList2 li").remove();
        if (search.length != 0) {
            $.ajax({
                url: "/Search/Search?search=" + search + "&hidden=" + hidden,
                type: "Get",
                success: function (res) {
                    if (hidden == "postjobfront") {
                        $("#searchList2").append(res);
                    }
                    else if (hidden == "userfront") {
                        $("#searchList2").append(res);
                    }
                    else if (hidden == "blogfront") {
                        $("#searchList2").append(res);
                    }
                    //else if (hidden == "blog") {
                    //    $("#searchList").append(res);
                    //}
                    //else if (hidden == "contact") {
                    //    $("#searchList").append(res);
                    //}
                    console.log(res)
                }
            })
        }
    })
})