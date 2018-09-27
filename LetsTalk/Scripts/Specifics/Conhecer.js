
$(window).resize(function () {
    console.log('resizeou');
    setFonts();
})

function setFonts() {
    var divWidth = $('.card-usuario').width();
    $('.h3-nome-idade').css('fontSize', divWidth / 13);
    $('.p-frase').css('fontSize', divWidth / 18);
    $('.ul-tags').css('fontSize', divWidth / 20);
    $('.botao-ver-perfil').css('fontSize', divWidth / 20);
    $('.foto-usuarios').css('width', divWidth).css('height', divWidth);
}