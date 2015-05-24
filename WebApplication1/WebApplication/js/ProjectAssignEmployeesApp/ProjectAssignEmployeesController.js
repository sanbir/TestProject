(function () {

    var projectAssignEmployeesController = function ($scope, $filter, $http, $q, projectFactory, employeesPageFactory) {

        $scope.project = projectFactory;
        $scope.employeesPage = employeesPageFactory;
        $scope.assignedEmployees = [];

        $scope.managerFullName = "";

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
            $scope.response = '';

            var httpRequest = httpRequestHandler('POST', '/Project/Persist', JSON.stringify($scope.project));

            httpRequest.then(function (data) {
                $scope.response = data;

            }, function (error) {
                $scope.response = error;
            });
        };

        function httpRequestHandler(method, url, dataToSend) {
            var timeout = $q.defer(),
                result = $q.defer(),
                timedOut = false;

            setTimeout(function () {
                timedOut = true;
                timeout.resolve();
            }, 100000000);

            var httpRequest = $http({
                method: method,
                url: url,
                data: dataToSend,
                cache: false,
                timeout: timeout.promise
            });

            httpRequest.success(function (data, status, headers, config) {
                result.resolve(data);
            });

            httpRequest.error(function (data, status, headers, config) {
                if (timedOut) {
                    result.reject({
                        error: 'timeout'
                    });
                } else {
                    result.reject(data);
                }
            });

            return result.promise;
        }

        /////////////////////////////////////////

        var sortDirections = {
            ASC: "Ascending",
            DESC: "Descending"
        };

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

        $scope.getEmployees = function() {
            $scope.response = '';

            var data = {
                sortDirection: $scope.sort.reverse ? "Descending" : "Ascending",
                sortPropertyName: $scope.sort.propertyName,
                searchString: $scope.searchString,
                page: $scope.paging.pageNumber
            };

            var httpRequest = httpRequestHandler('GET', '/Project/GetEmployees', JSON.stringify(data));

            httpRequest.then(function (data) {
                $scope.employeesPage = data.employees;
                $scope.paging.pageSize = data.pageSize;
                $scope.paging.pageNumber = data.pageNumber;
                $scope.paging.pageCount = data.pageCount;

            }, function (error) {
                $scope.response = error;
            });
        };

        $scope.getEmployees();

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

        $scope.firstPage = function() {
            $scope.paging.pageNumber = 1;
            $scope.getEmployees();
        };

        $scope.lastPage = function() {
            $scope.paging.pageNumber = $scope.paging.pageCount;
            $scope.getEmployees();
        };

        $scope.setPage = function () {
            $scope.paging.pageNumber = this.n;
            $scope.getEmployees();
        };

    };

    projectAssignEmployeesController.$inject = ['$scope', '$filter', '$http', '$q', 'projectFactory', 'employeesPageFactory'];

    angular.module('projectAssignEmployeesApp').controller('projectAssignEmployeesController', projectAssignEmployeesController);

}());