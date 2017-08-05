angular.module('app', ['ngRoute', 'app.controllers', 'app.services'])
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    })
    .config(['$routeProvider', function ($routeProvider) {

    
        $routeProvider.when('/', {
            templateUrl: 'http://localhost:57649/BlogApp/Views/post.html',
            controller: 'PostController'
            //  access: { isFreeAccess: true }
        })
            .when('/post/:Id', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/singlePost.html',
                controller: 'SinglePostController'
                // access: { isFreeAccess: true }
            })


            .when('/create', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/create.html',
                controller: 'PostController'
                //  access: { isFreeAccess: false }
            })

            .when('/manageCategory', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/manageCategory.html',
                controller: 'CategoryController'
                // access: { isFreeAccess: false }
            })

            .when('/managePosts', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/managePosts.html',
                controller: 'PostController'
                // access: { isFreeAccess: false }
            })
            .when('/manageTag', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/manageTag.html',
                controller: 'TagController'
                // access: { isFreeAccess: false }
            })

            .when('/registration', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/registration.html',
                controller: 'registrationController'
                //  access: { isFreeAccess: true }
            })

            .when('/login', {
                templateUrl: 'http://localhost:57649/BlogApp/Views/login.html',
                controller: 'LoginController'
                // access: { isFreeAccess: true }

            })

            .otherwise({
                redirectTo: '/'
            });

            //.run(function ($rootScope, $location, authService) {
            //    $rootScope.$on('$routeChangeStart', function (currRoute, prevRoute) {
            //        if (prevRoute.access != undefined) {
            //            if (!prevRoute.access.isFreeAccess && !authService.login) {
            //                $location.path('/');
            //            }
            //        }
            //    });
            //});
    }])

    .run(['authService', function (authService) {

        authService.fillAuthData();
    }]);
