appKCS.factory("loginService", function ($http, $rootScope) {
    var loginObj = {};

    loginObj.getByEmp = function (employee) {
        var emp = $http({
                method: "POST", url: $rootScope.ServiceUrl +"Login", data: employee
            }).
            then(function (response) {
                return response.data;
            }, function (error) {
                return error.data;
            });

        return emp;
    };

    return loginObj;
});

appKCS.controller("loginController", function ($scope, loginService, $cookies, $rootScope, $location) {
    $scope.Login = function (emp, isValid) {
        if (isValid) {
            loginService.getByEmp(emp).then(function (result) {
                if (result.ModelState == null) {

                    $scope.Emp = result;
                    $scope.errorMsgs = "";

                    $cookies.put("Auth", "true");
                    $rootScope.Auth = $cookies.get("Auth");

                    $cookies.put("EmpSignIn", JSON.stringify($scope.Emp));
                    $rootScope.EmpSignIn = JSON.parse($cookies.get("EmpSignIn"));

                    $location.path("/");
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    }
});























