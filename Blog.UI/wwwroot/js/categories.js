$(document).ready(async () => {
    let response = await $.ajax({
        async: true,
        type: 'GET',
        contentType: 'application/json',
        url: 'https://localhost:44311/api/Categories',
        data: null,
        success: (data, success) => {
            return data;
        },
        error: (html, status, error) => {
            alert(`Status: ${status} - Error: ${error}`);
            return null;
        }
    });

    if (!response?.Error) {
        for (var i of response.Result) {
            console.log(i);
            var cardBody = $("<div />").addClass("card-body");
            $("div.categories").css("margin-top", "25px")
                .append($("<div />").css("margin-bottom", "1px").addClass("card")
                    .append(cardBody
                        .append($("<h5 />").css("text-align", "left").text(i.Id))
                    )
                );

            var buttons = $("<div />").css("text-align", "left");
            cardBody.append($("<div />").addClass("card-text").text(i.CategoryName));
            cardBody.append(
                buttons
                    .append($("<a />")
                        .addClass("btn btn-primary navbar-brand")
                        .css("color", "white")
                        .text("Düzenle")
                        .attr("href", `/Category/${i.Id}`)
                    )
            )
            buttons.append(
                $("<a />")
                    .addClass("article-remove btn btn-danger navbar-brand")
                    .css("color", "white")
                    .text("Sil")
            );

            $("a.article-remove").on("click", async () => {
                await $.ajax({
                    async: true,
                    type: 'POST',
                    contentType: 'application/json',
                    url: 'https://localhost:44311/api/DeleteCategory',
                    data: JSON.stringify(i),
                    success: (data, success) => {
                        if (!data?.Error) {
                            alert("Silme işlemi başarılı.");
                            location.reload();
                        }
                    },
                    error: (html, status, error) => {
                        alert(`Status: ${status} - Error: ${error}`);
                        return null;
                    }
                });
            });
        }
    }
});