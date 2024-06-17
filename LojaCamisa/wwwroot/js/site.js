document.addEventListener('DOMContentLoaded', function () {
    const marcasSlide = document.querySelector('.marcas-slide');
    const images = marcasSlide.innerHTML;
    marcasSlide.innerHTML += images; // Duplicate the images for infinite scrolling effect

    const slideWidth = marcasSlide.scrollWidth / 2; // Get the width of the original images set
    let start = 0;

    function animate() {
        start -= 1; // Adjust the speed by changing this value
        if (Math.abs(start) >= slideWidth) {
            start = 0;
        }
        marcasSlide.style.transform = `translateX(${start}px)`;
        requestAnimationFrame(animate);
    }

    animate();
});
