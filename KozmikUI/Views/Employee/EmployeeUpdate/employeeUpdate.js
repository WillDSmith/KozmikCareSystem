appKCS.factory("employeeUpdateService", function ($http, $rootScope) {

    var empUpdateObj = {};

    empUpdateObj.getByEid = function (eid) {
        var emp = $http({ method: "Get", url: $rootScope.ServiceUrl + "Employee", params: { id: eid } }).
            then(function (response) {
                return response.data;
            });

        return emp;
    };

    empUpdateObj.updateEmployee = function (emp) {
        emp = $http({ method: "Put", url: $rootScope.ServiceUrl + "Employee", data: emp }).
            then(function (response) {
                return response.data;
            }, function (error) {
                return error.data;
            });
        return emp;
    };

    return empUpdateObj;
});

appKCS.controller("employeeUpdateController", function ($scope, $rootScope, $routeParams, employeeUpdateService, utilityService) {

    $scope.eid = $routeParams.EmployeeId != null ? $routeParams.EmployeeId : $rootScope.EmpSignIn.EmployeeId;

    utilityService.getFile($rootScope.ServiceUrl + "Upload", $scope.eid).then(function (result) {
        $scope.image = result;
    });

    employeeUpdateService.getByEid($scope.eid).then(function (result) {
        $scope.Emp = result;
        $scope.Emp.DOJ = new Date($scope.Emp.DOJ);
    });

    $scope.UpdateEmployee = function (emp, isValid) {
        console.log(isValid);
        if (isValid) {
            employeeUpdateService.updateEmployee(emp).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You have successfully updated " + result.EmployeeId;
                    $scope.Flg = true;
                    $scope.serverErrorMsgs = "";
                    utilityService.myAlert();
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    };

    $("#profilePanel a").click(function (e) {
        e.preventDefault();
    });

    $scope.msg = " This is a Profile Update " + $scope.eid;

    $scope.UploadFile = function () {
        var file = $scope.myFile;
        var uploadUrl = $rootScope.ServiceUrl + "Upload/";
        utilityService.uploadFile(file, uploadUrl, $scope.eid).then(function (result) {
            $scope.image = result;
        });
    };
});