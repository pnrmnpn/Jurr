// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Highlight active nav-link based on current location
(function () {
    function setActiveNav() {
        var links = document.querySelectorAll('.header-nav .nav-link');
        var path = window.location.pathname.toLowerCase();
        links.forEach(function (link) {
            // Resolve href to pathname (ignore query/hash)
            var href = link.getAttribute('href');
            if (!href) return;
            try {
                var url = new URL(href, window.location.origin);
                var lp = url.pathname.toLowerCase();
                if (lp === path || (lp !== '/' && path.startsWith(lp))) {
                    link.classList.add('active');
                } else {
                    link.classList.remove('active');
                }
            } catch (e) { /* ignore */ }
        });
    }

    document.addEventListener('DOMContentLoaded', setActiveNav);
    window.addEventListener('popstate', setActiveNav);
})();
