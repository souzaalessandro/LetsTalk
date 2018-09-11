function salvar() {
    var frase = $("#fraseApresentacao").val();
    var tags = "";

    $("#tags_2_tagsinput").find("span.tag").each(function (i) {
        var temp = $(this).text();
        tags += temp.substring(0, temp.length - 1);
    });

    $.ajax({
        url: 'SalvarInformacoesPessoais',
        type: 'POST',
        data: JSON.stringify({ Frase: frase, Tags: tags }),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert("foi!");
        },
        error: function (request, status, error) {
            alert('oh, errors here. The call to the server is not working.')
        }
    });
}