// Defining angularjs module
var app = angular.module('PostModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('PostCtrl', function ($scope, $http, PostService, CategoryService, TagService) {

    $scope.postData = null;
    // Fetching records from the factory created at the bottom of the script file
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

    $scope.postLimit = 300;

    $scope.Post = {
        Id: '',
        Title: '',

        Content: '',
        PostedOn: new Date(),
        CategoryId: '',
        TagIds: [],
    };

    //$scope.Category = {
    //    Id: '',
    //    Name: ''
    //}

    //$scope.Tag = {
    //    Id: '',
    //    Name: ''
    //    //PostId: '',
    //};

    $scope.clear = function () {
        $scope.Post.Id = '';
        $scope.Post.Title = '';
    }

    $scope.save = function () {
        if ($scope.Post.Title != "") {
            $scope.Post.CategoryId = $scope.Category.Id;
            $scope.Post.TagIds.push($scope.Tag.Id);


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
                url: 'http://localhost:57649/api/post/PostPosts/',
                data: $scope.Post
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.postData.push(response.data);
                $scope.clear();
                alert("Post Added Successfully !!!");
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

    // Edit post details
    $scope.edit = function (data) {
        $scope.Post = { Id: data.Id, Title: data.Title, Content: data.Content, PostedOn: data.PostedOn, CategoryId: data.CategoryId };
    };

    // Cancel post details
    $scope.cancel = function () {
        $scope.clear();
    };

    // Update post details
    $scope.update = function () {
        if ($scope.Post.Title != "") {

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


});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.
app.factory('PostService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:57649/api/post');
    }
    return fac;
});

app.factory('CategoryService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:57649/api/category/');
    }
    return fac;
});

app.factory('TagService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:57649/api/tag');
    }
    return fac;
});


