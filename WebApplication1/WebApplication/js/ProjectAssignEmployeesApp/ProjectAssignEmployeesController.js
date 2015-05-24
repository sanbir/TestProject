(function () {

    var projectAssignEmployeesController = function ($scope, projectFactory, employeesPageFactory) {

        $scope.project = projectFactory;
        $scope.assignedEmployees = [];
        $scope.managerFullName = "";
        $scope.searchString = "";

        $scope.sort = {
            reverse: false,
            propertyName: null
        };

        $scope.paging = {
            pageSize: null,
            pageNumber: null,
            pageCount: null,
            gap: 10
        };

        $scope.employeesPage = [];

        $scope.getEmployees = function () {

            var sortDirection = $scope.sort.reverse ? "Descending" : "Ascending";
            var sortPropertyName = $scope.sort.propertyName;
            var searchString = $scope.searchString;
            var page = $scope.paging.pageNumber;

            employeesPageFactory.getEmployees(sortDirection, sortPropertyName, searchString, page)
                .then(function (data) {
                    $scope.employeesPage = data.employees;
                    var employees = data.employees;
                    for (var i = 0; i < employees.length; i++) {
                        employees[i]["isAssigned"] = false;
                    }
                    $scope.employeesPage = employees;
                    $scope.paging.pageSize = data.pageSize;
                    $scope.paging.pageNumber = data.pageNumber;
                    $scope.paging.pageCount = data.pageCount;
                },
                function (data) {
                    $scope.error = "Failed to get employees";
                });
        };

        $scope.getEmployees();

        $scope.assignEmployee = function (selectedEmployee, isAssigned) {

            if (isAssigned) {
                if ($scope.project.assignedEmployeesIds.indexOf(selectedEmployee.id) === -1) {
                    $scope.project.assignedEmployeesIds.push(selectedEmployee.id);
                    $scope.assignedEmployees.push(selectedEmployee);
                }
            } else {
                if ($scope.project.assignedEmployeesIds.indexOf(selectedEmployee.id) !== -1) {
                    for (var i = 0; i < $scope.project.assignedEmployeesIds.length; i++) {
                        if ($scope.project.assignedEmployeesIds[i] === selectedEmployee.id) {
                            $scope.project.assignedEmployeesIds.splice(i, 1);
                            $scope.assignedEmployees.splice(i, 1);
                            break;
                        }
                    }
                }
            }

        }

        $scope.sendData = function () {

            employeesPageFactory.sendData($scope.project)
                .then(function (data) {
                    $scope.info = data;
                },
                function (data) {
                    $scope.error = "Failed to send the project";
                });
        };


        /////////////////////////////////////////

        $scope.prevPage = function () {
            if ($scope.paging.pageNumber > 1) {
                $scope.paging.pageNumber--;
                $scope.getEmployees();
            }
        };

        $scope.nextPage = function () {
            if ($scope.paging.pageNumber < $scope.paging.pageCount) {
                $scope.paging.pageNumber++;
                $scope.getEmployees();
            }
        };

        $scope.firstPage = function () {
            $scope.paging.pageNumber = 1;
            $scope.getEmployees();
        };

        $scope.lastPage = function () {
            $scope.paging.pageNumber = $scope.paging.pageCount;
            $scope.getEmployees();
        };

        $scope.setPage = function () {
            $scope.paging.pageNumber = this.n;
            $scope.getEmployees();
        };

    };

    projectAssignEmployeesController.$inject = ['$scope', 'projectFactory', 'employeesPageFactory'];

    angular.module('projectAssignEmployeesApp').controller('projectAssignEmployeesController', projectAssignEmployeesController);

}());