$('.ver-perfil-detalhado').click(function () {
    var $divCard = $(this).parent().parent().parent();
    var userID = $divCard.attr('id').replace("user", '');

    $.ajax({
        url: '/Conhecer/GetUser',
        type: 'POST',
        data: JSON.stringify({ id: userID }),
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result.sucesso) {
                inserirUserNoModal(result);
            } else {
                // mostrarAlerta(result.mensagem, 'danger');
            }
        },
        error: function (request, status, error) {
            mostrarAlerta('Algo de errado ocorreu ao visualizar este perfil', 'danger');
        }
    });
});
function inserirUserNoModal(result) {
    $('#stack1 #nome').text(result.nome);
    $('#stack1 #idade').text(result.idade);
    $('#stack1 #foto').attr('src', result.foto);
    $('#stack1 #frase-apresentacao').text(result.frase == null? "" : result.frase);
    $('#stack1 #descricao').text(result.descricao == null ? "" : result.descricao);

    $('#stack1 #tags').empty();
    $('#stack2 #carousel-fotos').empty();
    $('#stack2 #indicadores-carousel').empty();

    $(result.tags).each(function (i) {
        var iclass = '<i class="fa fa-tags"></i>' + this;
        $('#stack1 #tags').append(iclass);
    });

    var ativo = true;
    $(result.album).each(function (i) {
        var foto;
        if (ativo) {
            foto = '<div class="item active">' +
                '<img src="' + this + '" class="img-responsive" width="100%"/></div>';
            ativo = false;
        } else {
            foto = '<div class="item">' +
                '<img src="' + this + '" class="img-responsive" width="100%"/></div>';
        }
        $('#stack2 #carousel-fotos').append(foto);
    });
    for (var i = 0; i < result.album.length; i++) {
        var li = '<li data-target="#myCarousel" data-slide-to="' + i + '" class="active"></li>'
        $('#stack2 #indicadores-carousel').append(li);
    }
}