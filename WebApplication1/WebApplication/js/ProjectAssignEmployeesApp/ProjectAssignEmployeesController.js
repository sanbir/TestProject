(function () {

    var projectAssignEmployeesController = function ($scope, $filter, $http, $q, projectFactory, employeesPageFactory) {

        $scope.project = projectFactory;
        $scope.employeesPage = employeesPageFactory;
        $scope.assignedEmployees = [];

        $scope.managerFullName = "";

        $scope.sendData = function () {
            $scope.response = '';

            var httpRequest = httpRequestHandler('post', '/Project/Persist', JSON.stringify($scope.project));

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

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };

        // init the filtered items
        $scope.search = function () {
            $scope.filteredItems = $filter('filter')($scope.items, function (item) {
                for (var attr in item) {
                    if (searchMatch(item[attr], $scope.query))
                        return true;
                }
                return false;
            });
            // take care of the sorting order
            if ($scope.sort.sortingOrder !== '') {
                $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sort.sortingOrder, $scope.sort.reverse);
            }
            $scope.currentPage = 0;
            // now group by pages
            $scope.groupToPages();
        };


        // calculate page in place
        $scope.groupToPages = function () {
            $scope.pagedItems = [];

            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        };

        $scope.range = function (size, start, end) {
            var ret = [];
            console.log(size, start, end);

            if (size < end) {
                end = size;
                start = size - $scope.gap;
            }
            for (var i = start; i < end; i++) {
                ret.push(i);
            }
            console.log(ret);
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage++;
            }
        };

        $scope.setPage = function () {
            $scope.currentPage = this.n;
        };

        // functions have been describe process the data for display
        $scope.search();
    };

    projectAssignEmployeesController.$inject = ['$scope', '$filter', '$http', '$q', 'projectFactory', 'employeesPageFactory'];

    angular.module('projectAssignEmployeesApp').controller('projectAssignEmployeesController', projectAssignEmployeesController);

}());