$(function () {
    carregarFotosPequenasDoBanco();
});

function carregarFotosPequenasDoBanco() {
    $.ajax({
        url: '/Perfil/GetPathFoto',
        type: 'POST',
        data: JSON.stringify({  }),
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result.sucesso) {
                gerarFotoPequena(resutado.pathFoto);
            } else {
                mostrarAlerta(result.mensagem, 'danger');
            }
        },
        error: function (request, status, error) {
            mostrarAlerta('Algo de errado ocorreu.', 'danger');
        }
    });
}


$('#salvar-informacoes').click(function () {
    var frase = $('#frase-apresentacao').val();
    var descricao = $("#descricao").val();
    var tags = [];
    if ($('#tags_2_tagsinput > span.tag >span').length < 3) {
        mostrarAlerta('Insira pelo menos 3 tags', 'info');
        return;
    }
    $('#tags_2_tagsinput > span.tag >span').each(function (tag) {
        tags.push($.trim(this.textContent));
    });
    tags = tags.join(',');
    $.ajax({
        url: '/Perfil/SalvarInformacoesPessoais',
        type: 'POST',
        data: JSON.stringify({ Frase: frase, Descricao: descricao, Tags: tags }),
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result.sucesso) {
                mostrarAlerta(result.mensagem, 'success');
            } else {
                mostrarAlerta(result.mensagem, 'danger');
            }
        },
        error: function (request, status, error) {
            mostrarAlerta('Algo de errado ocorreu.', 'danger');
        }
    });
});

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
            data: JSON.stringify({ senhaNova: nova }),
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

$("#botao-salvar-fotos").click(enviarFotos);

function enviarFotos() {

    $.ajax({
        url: '/Perfil/SalvarFotoDiretorio',
        type: 'POST',
        data: JSON.stringify({ Frase: frase, Descricao: descricao, Tags: tags }),
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result.sucesso) {
                mostrarAlerta(result.mensagem, 'success');
            } else {
                mostrarAlerta(result.mensagem, 'danger');
            }
        },
        error: function (request, status, error) {
            mostrarAlerta('Algo de errado ocorreu.', 'danger');
        }
    });
}


function gerarFotoPequena(pathFoto) {
    var li = $("<li>");

    var a = $("<a>").addClass("fancybox-button").attr("data-rel", "fancybox-button").attr("href", "#");

    var img = $("<img>").attr("src", pathFoto);

    a.append(img);

    li.append(a);

    $("#lista-fotos").append(li);
}

