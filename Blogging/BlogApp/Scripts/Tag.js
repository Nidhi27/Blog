// Defining angularjs module
var app = angular.module('TagModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('TagCtrl', function ($scope, $http, TagService) {

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

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('TagService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:57649/api/tag');
    }
    return fac;
});

