(function () {

    var projectAssignEmployeesController = function ($scope, projectFactory, employeesPageFactory) {

        $scope.searchString = "";
        $scope.project = projectFactory;
        $scope.assignedEmployees = [];

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

        $scope.resetPagingGap = function() {
            $scope.paging.gap = 10;
            if ($scope.paging.gap > $scope.paging.pageCount) {
                $scope.paging.gap = $scope.paging.pageCount;
            }
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

                        for (var j = 0; j < $scope.project.assignedEmployeesIds.length; j++) {

                            if (employees[i].id == $scope.project.assignedEmployeesIds[j]) {
                                employees[i]["isAssigned"] = true;

                                var needToAssign = true;
                                for (var k = 0; k < $scope.assignedEmployees.length; k++) {
                                    if ($scope.assignedEmployees[k].id === employees[i].id) {
                                        needToAssign = false;
                                        break;
                                    }
                                }
                                if (needToAssign) {
                                    $scope.assignedEmployees.push(employees[i]);
                                }
                            }

                            if (employees[i].id == $scope.project.managerId) {
                                $scope.setManagerFullName(employees[i]);
                            }
                        }
                    }
                    
                    $scope.employeesPage = employees;
                    $scope.paging.pageSize = data.pageSize;
                    $scope.paging.pageNumber = data.pageNumber;
                    $scope.paging.pageCount = data.pageCount;
                    $scope.resetPagingGap();
                },
                function (data) {
                    $scope.error = "Failed to get employees";
                });
        };

        $scope.getEmployees();

        $scope.setManagerFullName = function(employee) {
            $scope.project.managerFullName = employee.lastName
                + " " + employee.firstName
                + " " + employee.middleName;
        };

        $scope.assignEmployee = function(selectedEmployee, isAssigned) {

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
                            break;
                        }
                    }

                    for (var j = 0; j < $scope.assignedEmployees.length; j++) {
                        if ($scope.assignedEmployees[j].id === selectedEmployee.id) {
                            $scope.assignedEmployees.splice(j, 1);
                            break;
                        }
                    }
                }
            }

        };

        $scope.sendData = function () {

            employeesPageFactory.sendData($scope.project)
                .then(function (data) {
                    window.location.replace("/");
                },
                function (data) {
                    $scope.error = "Failed to send the project";
                });
        };

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

        $scope.range = function () {
            var pageNumbers = [];
            var start = $scope.paging.pageNumber;

            if (start > $scope.paging.pageCount - $scope.paging.gap) {
                start = $scope.paging.pageCount - $scope.paging.gap + 1;
            }

            for (var i = start; i < start + $scope.paging.gap; i++) {
                pageNumbers.push(i);
            }

            return pageNumbers;
        };

    };

    projectAssignEmployeesController.$inject = ['$scope', 'projectFactory', 'employeesPageFactory'];

    angular.module('projectAssignEmployeesApp').controller('projectAssignEmployeesController', projectAssignEmployeesController);

}());