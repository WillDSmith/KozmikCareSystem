appKCS.factory("recoverPasswordService", function ($http, $rootScope) {
    var recoverPasswordObj = {};

    recoverPasswordObj.getByEmp = function (employee) {
        var emp = $http({
                method: "GET", url: $rootScope.ServiceUrl + "Login/RecoverPassword", params: { empEmail: employee.Email }
            }).
            then(function (response) {
                return response.data;
            }, function (error) {
                return error.data;
            });

        return emp;
    };

    return recoverPasswordObj;
});

appKCS.controller("recoverPasswordController", function ($scope, recoverPasswordService, utilityService) {
    $scope.RecoverPassword = function (emp, isValid) {
        console.log(emp);
        if (isValid) {
            recoverPasswordService.getByEmp(emp).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You login credentials has been emailed. Kindly check you email.";
                    $scope.Flg = true;
                    $scope.serverErrorMsgs = "";
                    utilityService.myAlert();
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    }
});