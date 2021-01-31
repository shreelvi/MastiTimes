// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(window).load(function () {
    $('#carouselExample').on('slide.bs.carousel', function (e) {
        /*
        CC 2.0 License Iatek LLC 2018
        Attribution required 
        */
        var $e = $(e.relatedTarget);
        var idx = $e.index();
        console.log("IDX :  " + idx);

        var itemsPerSlide = 8;
        var totalItems = $('.carousel-item').length;

        if (idx >= totalItems - (itemsPerSlide - 1)) {
            var it = itemsPerSlide - (totalItems - idx);
            for (var i = 0; i < it; i++) {
                // append slides to end
                if (e.direction == "left") {
                    $('.carousel-item').eq(i).appendTo('.carousel-inner');
                }
                else {
                    $('.carousel-item').eq(0).appendTo('.carousel-inner');
                }
            }
        }
    })
});



