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

        // post
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
                timedOut = false,
                httpRequest;

            setTimeout(function () {
                timedOut = true;
                timeout.resolve();
            }, 10000);

            httpRequest = $http({
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

        $scope.sortDirection = sortDirections.ASC;
        $scope.sortPropertyName = "";
        $scope.searchString = "";
        $scope.page = 1;

        $scope.getEmployees = function () {
            $scope.response = '';

            var httpRequest = httpRequestHandler('GET', '/Project/GetEmployees', JSON.stringify($scope.project));

            httpRequest.then(function (data) {
                $scope.employeesPage = data;

            }, function (error) {
                $scope.response = error;
            });
        };
        $scope.getEmployees();



        // init
        $scope.sort = {
            sortingOrder: 'id',
            reverse: false
        };

        $scope.gap = 3;

        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 3;
        $scope.pagedItems = [];
        $scope.currentPage = 1;
        $scope.items = $scope.employeesPage;
        $scope.query = "";

        // init the filtered items
        $scope.search = function () {

        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 1) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 2) {
                $scope.currentPage++;
            }
        };

    };

    projectAssignEmployeesController.$inject = ['$scope', '$filter', '$http', '$q', 'projectFactory', 'employeesPageFactory'];

    angular.module('projectAssignEmployeesApp').controller('projectAssignEmployeesController', projectAssignEmployeesController);

}());