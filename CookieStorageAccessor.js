(function () {
    'use strict';

    angular
        .module('app')
        .controller('CookieStorageAccessor', CookieStorageAccessor);

    CookieStorageAccessor.$inject = ['$location'];

    function CookieStorageAccessor($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'CookieStorageAccessor';

        activate();

        function activate() { }

        export function get() {
            return document.cookie;
        }

        export function set(key, value) {
            document.cookie = `${key}=${value}`;
        }
    }
})();