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

// Services: expand/collapse bullet lists for ALL sections
(function(){
    function ready(fn){ if(document.readyState!=="loading") fn(); else document.addEventListener('DOMContentLoaded', fn); }
    ready(function(){
        var toggles = document.querySelectorAll('.svc-toggle');
        if(!toggles.length) return;

        toggles.forEach(function(toggle){
            // Find the associated list within the same services grid block
            var grid = toggle.closest('.svc-grid');
            var list = null;
            if(grid){
                // Prefer a direct child list of this grid
                list = grid.querySelector(':scope > .svc-list');
            }
            // Fallback: find the next .svc-list after the content block
            if(!list){
                var content = toggle.closest('.svc-content');
                var cur = content ? content.nextElementSibling : null;
                while(cur && !cur.classList.contains('svc-list')) cur = cur.nextElementSibling;
                list = cur || null;
            }
            if(!list) return; // nothing to wire

            toggle.addEventListener('click', function(){
                var expanded = this.getAttribute('aria-expanded') === 'true';
                this.setAttribute('aria-expanded', String(!expanded));
                list.hidden = expanded;
            });
        });
    });
})();
