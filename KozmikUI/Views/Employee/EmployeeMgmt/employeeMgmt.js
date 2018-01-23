appKCS.factory("employeeMgmtService", function ($http, $rootScope) {
    var empMgmtObj = {};

    empMgmtObj.getAll = function () {
        var emps = $http({ method: "Get", url: $rootScope.ServiceUrl + "Employee" }).
            then(function (response) {
                return response.data;

            });

        return emps;
    };

    empMgmtObj.createEmployee = function (emp) {
        emp = $http({ method: "Post", url: $rootScope.ServiceUrl + "Employee", data: emp }).
            then(function (response) {

                return response.data;

            }, function (error) {
                return error.data;
            });
        return emp;
    };

    empMgmtObj.deleteEmployeeById = function (eid) {
        var emps = $http({ method: "Delete", url: $rootScope.ServiceUrl + "Employee", params: { id: eid } }).
            then(function (response) {
                return response.data;

            });

        return emps;
    };

    return empMgmtObj;
});

appKCS.controller("employeeMgmtController", function ($scope, employeeMgmtService, utilityService, $window) {

    $scope.Sort = function (col) {
        $scope.key = col;
        $scope.AscOrDesc = !$scope.AscOrDesc;
    };

    employeeMgmtService.getAll().then(function (result) {
        $scope.Emps = result;
    });

    $scope.CreateEmployee = function (emp, isValid) {
        if (isValid) {
            $scope.Emp.Password = utilityService.randomPassword();

            employeeMgmtService.createEmployee(emp).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You have successfully created " + result.EmployeeId;
                    $scope.Flg = true;
                    utilityService.myAlert();

                    $scope.serverErrorMsgs = "";

                    employeeMgmtService.getAll().then(function (result) {
                        $scope.Emps = result;
                    });
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    };

    $scope.DeleteEmployeeById = function (emp) {
        if ($window.confirm("Do you want to delete Employee with Id:" + emp.EmployeeId + "?")) {
            employeeMgmtService.deleteEmployeeById(emp.EmployeeId).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You have successfully deleted " + result.EmployeeId;
                    $scope.Flg = true;
                    utilityService.myAlert();

                    employeeMgmtService.getAll().then(function (result) {
                        $scope.Emps = result;
                    });
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    };


    $scope.CreateMultiEmployee = function () {
        var file = $scope.myFile;
        var uploadUrl = "http://localhost:54103/api/Upload/";
        utilityService.uploadFile(file, uploadUrl, $scope.eid).then(function (result) {
            $scope.image = result;
        });
    };
});