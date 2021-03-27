$(document).ready(async () => {
    var id = location.pathname.split('/')[2] ?? null;
    var editor = new FroalaEditor('#editor');
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
    if (!categoriesResponse?.Error && categoriesResponse.Result.length > 0) {
        for (var i of categoriesResponse.Result) {
            $("select#category").append(
                $("<option />").attr("value", i.Id).text(i.CategoryName)
            );
        }
    }

    if (!!id) {
        var response = await $.ajax({
            async: true,
            type: 'GET',
            contentType: 'application/json',
            url: 'https://localhost:44311/api/Articles',
            data: null,
            success: (data, success) => {
                return data;
            },
            error: (html, status, error) => {
                alert(`Status: ${status} - Error: ${error}`);
                return null;
            }
        });

        var article = response.Result.filter(x => x.Id == id)[0];
        $("select#category").val(article.CategoryId);
        editor.el.innerHTML = article.Theme;
    }

    $("button#create-article").on("click", async () => {
        let select = $("select#category").val();
        var content = editor.el.innerHTML;
        if (!select) {
            alert("Kategori Boş Bırakılamaz...");
            return;
        }

        let response = await $.ajax({
            async: true,
            type: 'POST',
            contentType: 'application/json',
            url: 'https://localhost:44311/api/CreateOrUpdateArticle',
            data: JSON.stringify({ Id: (!!id ? parseInt(id) : 0), CreativeId: 1, CategoryId: parseInt(select), Theme: content }),
            success: (data, success) => {
                return data;
            },
            error: (html, status, error) => {
                alert(`Status: ${status} - Error: ${error}`);
                return null;
            }
        });

        if (!response?.Error) {
            alert("Makale kayıt edildi!");
        }
    });
});