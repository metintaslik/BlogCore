$(document).ready(async () => {
    var id = location.pathname.split('/')[2] ?? null;
    let input = $("input#category-name");
    if (!!id) {
        let categoriesResponse = await $.ajax({
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

        var category = categoriesResponse.Result.filter(x => x.Id == id)[0];
        input.val(category.CategoryName);
    }

    $("button#create-category").on("click", async () => {
        let response = await $.ajax({
            async: true,
            type: 'POST',
            contentType: 'application/json',
            url: 'https://localhost:44311/api/CreateOrUpdateCategory',
            data: JSON.stringify({ Id: (!!id ? parseInt(id) : 0), CreativeId: 1, CategoryName: input.val() }),
            success: (data, success) => {
                return data;
            },
            error: (html, status, error) => {
                alert(`Status: ${status} - Error: ${error}`);
                return null;
            }
        });

        if (!response?.Error) {
            alert("Kategori kayıt edildi!");
        }
    });
});