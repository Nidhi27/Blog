// Defining angularjs module
var app = angular.module('CategoryModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('CategoryCtrl', function ($scope, $http, CategoryService) {

    $scope.categoryData = null;
    // Fetching records from the factory created at the bottom of the script file
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
        $scope.Category = { Id: data.Id, Name: data.Name };
    };

    // Cancel department details
    $scope.cancel = function () {
        $scope.clear();
    };

    // Update department details
    $scope.update = function () {
        if ($scope.Category.Name != "" ) {
        
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

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('CategoryService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:57649/api/category/');
    }
    return fac;
});

