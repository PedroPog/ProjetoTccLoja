document.addEventListener('DOMContentLoaded', function () {
    const brandImages = document.querySelectorAll('.img-slide');

    brandImages.forEach(function (img) {
        img.addEventListener('click', function () {
            const brand = this.getAttribute('data-brand');
            // Enviar a marca selecionada para o backend via AJAX
            const xhr = new XMLHttpRequest();
            xhr.open('POST', '/Home/FiltrarPorMarca', true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === XMLHttpRequest.DONE) {
                    // Lógica para lidar com a resposta do servidor, se necessário
                }
            };
            xhr.send(JSON.stringify({ marcaSelecionada: brand }));
        });
    });
});
