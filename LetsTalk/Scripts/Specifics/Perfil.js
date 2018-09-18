function salvar() {
    var frase = $("#fraseApresentacao").val();
    var descricao = $("#maxlength_textarea").val();
    var tags = "";

    $("#tags_2_tagsinput").find("span.tag").each(function (i) {
        var temp = $(this).text();
        tags += temp.substring(0, temp.length - 1);
    });

    $.ajax({
        url: 'SalvarInformacoesPessoais',
        type: 'POST',
        data: JSON.stringify({ Frase: frase, Descricao: descricao, Tags: tags }),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert("foi!");
        },
        error: function (request, status, error) {
            alert('oh, errors here. The call to the server is not working.')
        }
    });
}

$('#atualizar-senha').click(function () {
    var atual = $('#senha-atual').val();
    var nova = $('#nova-senha').val();
    var repetida = $('#nova-senha-repetido').val();

    if (nova === repetida) {
        $.ajax({
            url: '/Perfil/AtualizarSenha',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ senhaNova: nova}),
            success: function (result) {
                if (result.sucesso) {
                    mostrarAlerta(result.mensagem, 'success');
                } else {
                    mostrarAlerta(result.mensagem, 'danger');
                }
            },
            error: function (xmlresponse, status, error) {
                    mostrarAlerta('Algo de errado ocorreu.', 'danger');
            }
        })
    } else {
        mostrarAlerta('Senhas digitadas não são iguais. Digite senhas iguais e tente novamente', 'info');
    }

})
//function atualizarSenha() {
//    var senhaNova = $("#fraseApresentacao").val();


//    $.ajax({
//        url: 'AtualizarSenha',
//        type: 'POST',
//        data: JSON.stringify({ Frase: senhaNova, novasenha }),
//        contentType: "application/json;charset=utf-8",
//        success: function (data) {
//            alert("foi!");
//        },
//        error: function (request, status, error) {
//            alert('oh, errors here. The call to the server is not working.')
//        }
//    });
//}

function mostrarAlerta(mensagem, tipoAlerta) {
    $.bootstrapGrowl(mensagem, {
        ele: 'body', // which element to append to
        type: tipoAlerta, // (null, 'info', 'danger', 'success')
        offset: { from: 'top', amount: 200 }, // 'top', or 'bottom'
        align: 'center', // ('left', 'right', or 'center')
        width: 'auto', // (integer, or 'auto')
        delay: 5000, // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
        allow_dismiss: true, // If true then will display a cross to close the popup.
        stackup_spacing: 10 // spacing between consecutively stacked growls.
    });
}