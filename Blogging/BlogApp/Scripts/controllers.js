angular.module('app.controllers', ['app.services', 'angularUtils.directives.dirPagination', 'app.directives', 'LocalStorageModule', 'am.multiselect','angularjs-dropdown-multiselect'])
    
    .controller('LoginController', ['$scope',  '$http', '$window', 'authService', function ($scope, $http, $window, authService) {
        $scope.init = function () {
            $scope.isProcessing = false;
            $scope.LoginBtnText = "Sign In";
        }

        $scope.init();

        $scope.loginData = {
            userName: "",
            password: ""
        }

  
        $scope.Login = function () {
            $scope.isProcessing = true;
            $scope.LoginBtnText = "Signing in.....";

            authService.login($scope.loginData).then(function (response) {
                alert("Login Successfully");
                $window.location.href = "Index1.html";
            }, function (err) {
                alert("Failed.Please try again.");
               
                $scope.init();
            })
        }

    }])
    .controller('registrationController', ['$scope', '$http','$window', 'RegistrationService', function ($scope, $http, $window, RegistrationService) {
        $scope.init = function () {
            $scope.isProcessing = false;
            $scope.RegisterBtnText = "Register";
        };

        $scope.init();

        $scope.registration = {
            Email: "",
            Password: "",
            ConfirmPassword: ""
        };

        $scope.signUp = function () {
            $scope.isProcessing = true;
            $scope.RegisterBtnText = "Please wait...";
            RegistrationService.saveRegistration($scope.registration).then(function (response) {
                alert("Registration Successfully Completed. Please sign in to Continue.");
                $window.location.href = "Index1.html";
            }, function () {
                alert("Error occured. Please try again.");
                $scope.isProcessing = false;
                $scope.RegisterBtnText = "Register";
            });
        };


    }])
    .controller('PostController', ['$scope', '$http', 'PostService', 'CategoryService', 'TagService', '$routeParams','$window', function ($scope, $http, PostService, CategoryService, TagService, $routeParams, $window) {
        $scope.postData = null;

        PostService.GetAllRecords().then(function (d) {
            $scope.postData = d.data;
            // Success
        }, function () {
            alert('Error Occured in post!!!'); // Failed
        });

        CategoryService.GetAllRecords().then(function (d) {
            $scope.categoryData = d.data;
            // Success
        }, function () {
            alert('Error Occured in category!!!'); // Failed
        });

      
        TagService.GetAllRecords().then(function (d) {
            $scope.tagData = d.data;
          
            
        }, function () {
            alert('Error Occured in Tag!!!');
        });

        $scope.Tag = {
            Id: '',
            Name: ''
            //PostId: '',
        };

        $scope.postLimit = 300;

        $scope.sortPost = function () {
            var date = new Date($scope.PostedOn);
            return date;
        };
        $scope.selectedtags = [];
        $scope.submittedtags = [];
        $scope.Post = {
            Id: '',
            Title: '',
            UserName: '',
            Content: '',
            PostedOn: new Date(),
            CategoryId: '',
            //TagIds: [],
          
            Tags: ''
        };

        $scope.filterByCategory = function (categoryId)
        {
            PostService.GetAllRecordsByCategory(categoryId).then(function (d) {
                $scope.postData = d.data;
                // Success
            }, function () {
                alert('Error Occured in category!!!'); // Failed
            });
        }

        $scope.filterByTag = function (tagId)
        {
            PostService.GetAllRecordsByTag(tagId).then(function (d) {
                $scope.postData = d.data;
                // Success
            }, function () {
                alert('Error Occured in category!!!'); // Failed
            });
        }

      
        //$scope.Category = {
        //    Id: '',
        //    Name: ''
        //}

      

        $scope.clear = function () {
            $scope.Post.Id = '';
            $scope.Post.Title = '';
            $scope.Post.Content = '';
            $scope.Post.CategoryId = '';
            $scope.Post.Tags = '';
        }

        $scope.save = function () {
            if ($scope.Post.Title != "") {
                $scope.Post.CategoryId = $scope.Category.Id;
                //for (var i = 0; i < $scope.Tag.Id.length; i++)
                //{
                //    $scope.Post.TagIds.push($scope.Tag.Id[i]);
                //}
                //for (var i = 0; i < $scope.selectedtags.length; i++)
                //{

                //    $scope.Post.Tags.push($scope.selectedtags[i]);
                //}
              $scope.Post.Tags = $scope.selectedtags;

             
                // Call Http request using $.ajax

                //$.ajax({
                //    type: 'POST',
                //    contentType: 'application/json; charset=utf-8',
                //    data: JSON.stringify($scope.Product),
                //    url: 'api/Product/PostProduct',
                //    success: function (data, status) {
                //        $scope.$apply(function () {
                //            $scope.productsData.push(data);
                //            alert("Product Added Successfully !!!");
                //            $scope.clear();
                //        });
                //    },
                //    error: function (status) { }
                //});

                // or you can call Http request using $http
                $http({
                    method: 'POST',
                    //contentType: 'application/json; charset=utf-8',
                    url: 'http://localhost:57649/api/post/PostPosts/',
                    data: $scope.Post
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.postData.push(response.data);
                    $scope.clear();
                    alert("Post Added Successfully !!!");
                    $window.location.href = "Index1.html";
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };
       // $scope.stringSettings = { template: '{{option}}', smartButtonTextConverter(skip, option) { return option; }, };
        // Edit post details
        $scope.edit = function (data) {

            $scope.Post = {
                Id: data.Id,
                Title: data.Title,
                Content: data.Content,
                PostedOn: data.PostedOn,
                CategoryId: data.CategoryId,
                CategoryName: data.CategoryName,
                Tags: data.Tags,
              // selectedtags: data.TagName
            };

       
            //$scope.Post.Tags = $scope.Post.Tags;
         

           
        };

       
        // Cancel post details
        $scope.cancel = function () {
            $scope.clear();
        };

        // Update post details
        $scope.update = function () {
            if ($scope.Post.Title != "") {


            //    $scope.Post.Tags = $scope.Post.Tags;
         
                $http({
                    method: 'PUT',
                    url: 'http://localhost:57649/api/post/' + $scope.Post.Id,
                    data: $scope.Post
                }).then(function successCallback(response) {
                    $scope.postData = response.data;
                    $scope.clear();
                    alert("Post Updated Successfully !!!");
                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };

        // Delete Post details
        $scope.delete = function (index) {
            $http({
                method: 'DELETE',
                url: 'http://localhost:57649/api/post/' + $scope.postData[index].Id,
            }).then(function successCallback(response) {
                $scope.postData.splice(index, 1);
                alert("Post Deleted Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        };


    }])

    .controller('CategoryController', ['$scope', '$http', 'CategoryService', function ($scope, $http, CategoryService) {

        CategoryService.GetAllRecords().then(function (d) {
            $scope.categoryData = d.data;
            // Success
        }, function () {
            alert('Error Occured in category!!!'); // Failed
        });

        $scope.Category = {
            Id: '',
            Name: ''
        };


        // Reset department details
        $scope.clear = function () {
            $scope.Category.Id = '';
            $scope.Category.Name = '';


        }

        //Add New Item
        $scope.save = function () {
            if ($scope.Category.Name != "") {

                // Call Http request using $.ajax

                //$.ajax({
                //    type: 'POST',
                //    contentType: 'application/json; charset=utf-8',
                //    data: JSON.stringify($scope.Product),
                //    url: 'api/Product/PostProduct',
                //    success: function (data, status) {
                //        $scope.$apply(function () {
                //            $scope.productsData.push(data);
                //            alert("Product Added Successfully !!!");
                //            $scope.clear();
                //        });
                //    },
                //    error: function (status) { }
                //});

                // or you can call Http request using $http
                $http({
                    method: 'POST',
                    url: 'http://localhost:57649/api/category/PostCategories',
                    data: $scope.Category
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.categoryData.push(response.data);
                    $scope.clear();
                    alert("Category Added Successfully !!!");
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };

        // Edit department details
        $scope.edit = function (data) {
            $scope.Category = { Id: data.Id, Name: data.Name};
        };

        // Cancel department details
        $scope.cancel = function () {
            $scope.clear();
        };

        // Update department details
        $scope.update = function () {
            if ($scope.Category.Name != "") {

                $http({
                    method: 'PUT',
                    url: 'http://localhost:57649/api/category/' + $scope.Category.Id,
                    data: $scope.Category
                }).then(function successCallback(response) {
                    $scope.categoryData = response.data;
                    $scope.clear();
                    alert("Category Updated Successfully !!!");
                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };

        // Delete department details
        $scope.delete = function (index) {
            $http({
                method: 'DELETE',
                url: 'http://localhost:57649/api/category/' + $scope.categoryData[index].Id,
            }).then(function successCallback(response) {
                $scope.categoryData.splice(index, 1);
                alert("Category Deleted Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        };
    }])

    .controller('TagController', ['$scope', '$http', 'TagService', function ($scope, $http, TagService) {
        $scope.tagData = null;
        // Fetching records from the factory created at the bottom of the script file
        TagService.GetAllRecords().then(function (d) {
            $scope.tagData = d.data;
            // Success
        }, function () {
            alert('Error Occured in tag!!!'); // Failed


        });


        $scope.Tag = {
            Id: '',
            Name: ''
        };


        // Reset department details
        $scope.clear = function () {
            $scope.Tag.Id = '';
            $scope.Tag.Name = '';


        }

        //Add New Item
        $scope.save = function () {
            if ($scope.Tag.Name != "") {

                // Call Http request using $.ajax

                //$.ajax({
                //    type: 'POST',
                //    contentType: 'application/json; charset=utf-8',
                //    data: JSON.stringify($scope.Product),
                //    url: 'api/Product/PostProduct',
                //    success: function (data, status) {
                //        $scope.$apply(function () {
                //            $scope.productsData.push(data);
                //            alert("Product Added Successfully !!!");
                //            $scope.clear();
                //        });
                //    },
                //    error: function (status) { }
                //});

                // or you can call Http request using $http
                $http({
                    method: 'POST',
                    url: 'http://localhost:57649/api/tag/PostTags',
                    data: $scope.Tag
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.tagData.push(response.data);
                    $scope.clear();
                    alert("Tag Added Successfully !!!");
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };

        // Edit department details
        $scope.edit = function (data) {
            $scope.Tag = { Id: data.Id, Name: data.Name };
        };

        // Cancel department details
        $scope.cancel = function () {
            $scope.clear();
        };

        // Update department details
        $scope.update = function () {
            if ($scope.Tag.Name != "") {

                $http({
                    method: 'PUT',
                    url: 'http://localhost:57649/api/tag/' + $scope.Tag.Id,
                    data: $scope.Tag
                }).then(function successCallback(response) {
                    $scope.tagData = response.data;
                    $scope.clear();
                    alert("Tag Updated Successfully !!!");
                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });
            }
            else {
                alert('Please Enter All the Values !!');
            }
        };

        // Delete department details
        $scope.delete = function (index) {
            $http({
                method: 'DELETE',
                url: 'http://localhost:57649/api/tag/' + $scope.tagData[index].Id,
            }).then(function successCallback(response) {
                $scope.tagData.splice(index, 1);
                alert("Tag Deleted Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        };

   



    }])
//$routeParams.Id
    .controller('SinglePostController', ['$scope', '$http', 'PostService', '$routeParams', function ($scope, $http, PostService, $routeParams) {


        PostService.GetPost($routeParams.Id).then(function (d) {
            $scope.postData = d.data;
          //  $scope.postData.Title = d.data;
            $scope.message = 'Hello';
            // Success
        }, function () {
            alert('Error Occured in post!!!'); // Failed
            });

       
    }]);

  