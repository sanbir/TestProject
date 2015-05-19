﻿(function () {

    var employeeIndexController = function ($scope, $filter, employeeIndexFactory) {

        function project() {
            this.projectName = "";
            this.customerCompanyName = "";
            this.startDate = new Date();
            this.endDate = new Date();
            this.priority = 0;
            this.comment = "";

            this.managerId = 0;
            this.assignedEmployeesIds = [];
        }

        function employee() {
            this.id = 0;
            this.firstName = "";
            this.lastName = "";
            this.middleName = "";
            this.email = "";
            this.contractorCompanyName = "";

            this.isManager = false;
            this.isAssigned = false;
        }

        $scope.project = new project();
        $scope.employeesPage = [
            new employee(),
            new employee(),
            new employee()
        ];

        $scope.setManager = function() {
            for (var i = 0; i < $scope.employeesPage.length; i++) {
                if ($scope.employeesPage[i].isManager) {
                    $scope.project.managerId = $scope.employeesPage[i].id;
                    break;
                }
            }
        };

        $scope.assignEmployee = function () {
            for (var i = 0; i < $scope.employeesPage.length; i++) {
                if ($scope.employeesPage[i].isAssigned) {
                    $scope.project.assignedEmployeesIds.push($scope.employeesPage[i].id);
                    $scope.assignedEmployees.push($scope.employeesPage[i]);
                }
            }
        };

        $scope.assignedEmployees = [];




        // init
        $scope.sort = {
            sortingOrder: 'id',
            reverse: false
        };

        $scope.gap = 5;

        //employeeIndexFactory.getEmployees().then(function (employees) {
        //    $scope.employees = employees;
        //});

        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 5;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = [
            { "id": 1, "name": "name 1", "description": "description 1", "field3": "field3 1", "field4": "field4 1", "field5 ": "field5 1" },
            { "id": 2, "name": "name 2", "description": "description 1", "field3": "field3 2", "field4": "field4 2", "field5 ": "field5 2" },
            { "id": 3, "name": "name 3", "description": "description 1", "field3": "field3 3", "field4": "field4 3", "field5 ": "field5 3" },
            { "id": 4, "name": "name 4", "description": "description 1", "field3": "field3 4", "field4": "field4 4", "field5 ": "field5 4" },
            { "id": 5, "name": "name 5", "description": "description 1", "field3": "field3 5", "field4": "field4 5", "field5 ": "field5 5" },
            { "id": 6, "name": "name 6", "description": "description 1", "field3": "field3 6", "field4": "field4 6", "field5 ": "field5 6" },
            { "id": 7, "name": "name 7", "description": "description 1", "field3": "field3 7", "field4": "field4 7", "field5 ": "field5 7" },
            { "id": 8, "name": "name 8", "description": "description 1", "field3": "field3 8", "field4": "field4 8", "field5 ": "field5 8" },
            { "id": 9, "name": "name 9", "description": "description 1", "field3": "field3 9", "field4": "field4 9", "field5 ": "field5 9" },
            { "id": 10, "name": "name 10", "description": "description 1", "field3": "field3 10", "field4": "field4 10", "field5 ": "field5 10" },
            { "id": 11, "name": "name 11", "description": "description 1", "field3": "field3 11", "field4": "field4 11", "field5 ": "field5 11" },
            { "id": 12, "name": "name 12", "description": "description 1", "field3": "field3 12", "field4": "field4 12", "field5 ": "field5 12" },
            { "id": 13, "name": "name 13", "description": "description 1", "field3": "field3 13", "field4": "field4 13", "field5 ": "field5 13" },
            { "id": 14, "name": "name 14", "description": "description 1", "field3": "field3 14", "field4": "field4 14", "field5 ": "field5 14" },
            { "id": 15, "name": "name 15", "description": "description 1", "field3": "field3 15", "field4": "field4 15", "field5 ": "field5 15" },
            { "id": 16, "name": "name 16", "description": "description 1", "field3": "field3 16", "field4": "field4 16", "field5 ": "field5 16" },
            { "id": 17, "name": "name 17", "description": "description 1", "field3": "field3 17", "field4": "field4 17", "field5 ": "field5 17" },
            { "id": 18, "name": "name 18", "description": "description 1", "field3": "field3 18", "field4": "field4 18", "field5 ": "field5 18" },
            { "id": 19, "name": "name 19", "description": "description 1", "field3": "field3 19", "field4": "field4 19", "field5 ": "field5 19" },
            { "id": 20, "name": "name 5", "description": "description 1", "field3": "field3 5", "field4": "field4 5", "field5 ": "field5 5" },
            { "id": 21, "name": "name 6", "description": "description 1", "field3": "field3 6", "field4": "field4 6", "field5 ": "field5 6" },
            { "id": 22, "name": "name 7", "description": "description 1", "field3": "field3 7", "field4": "field4 7", "field5 ": "field5 7" },
            { "id": 23, "name": "name 8", "description": "description 1", "field3": "field3 8", "field4": "field4 8", "field5 ": "field5 8" },
            { "id": 24, "name": "name 9", "description": "description 1", "field3": "field3 9", "field4": "field4 9", "field5 ": "field5 9" },
            { "id": 25, "name": "name 10", "description": "description 1", "field3": "field3 10", "field4": "field4 10", "field5 ": "field5 10" },
            { "id": 26, "name": "name 11", "description": "description 1", "field3": "field3 11", "field4": "field4 11", "field5 ": "field5 11" },
            { "id": 27, "name": "name 12", "description": "description 1", "field3": "field3 12", "field4": "field4 12", "field5 ": "field5 12" },
            { "id": 28, "name": "name 13", "description": "description 1", "field3": "field3 13", "field4": "field4 13", "field5 ": "field5 13" },
            { "id": 29, "name": "name 14", "description": "description 1", "field3": "field3 14", "field4": "field4 14", "field5 ": "field5 14" },
            { "id": 30, "name": "name 15", "description": "description 1", "field3": "field3 15", "field4": "field4 15", "field5 ": "field5 15" },
            { "id": 31, "name": "name 16", "description": "description 1", "field3": "field3 16", "field4": "field4 16", "field5 ": "field5 16" },
            { "id": 32, "name": "name 17", "description": "description 1", "field3": "field3 17", "field4": "field4 17", "field5 ": "field5 17" },
            { "id": 33, "name": "name 18", "description": "description 1", "field3": "field3 18", "field4": "field4 18", "field5 ": "field5 18" },
            { "id": 34, "name": "name 19", "description": "description 1", "field3": "field3 19", "field4": "field4 19", "field5 ": "field5 19" },
            { "id": 35, "name": "name 5", "description": "description 1", "field3": "field3 5", "field4": "field4 5", "field5 ": "field5 5" },
            { "id": 36, "name": "name 6", "description": "description 1", "field3": "field3 6", "field4": "field4 6", "field5 ": "field5 6" },
            { "id": 37, "name": "name 7", "description": "description 1", "field3": "field3 7", "field4": "field4 7", "field5 ": "field5 7" },
            { "id": 38, "name": "name 8", "description": "description 1", "field3": "field3 8", "field4": "field4 8", "field5 ": "field5 8" },
            { "id": 39, "name": "name 9", "description": "description 1", "field3": "field3 9", "field4": "field4 9", "field5 ": "field5 9" },
            { "id": 40, "name": "name 10", "description": "description 1", "field3": "field3 10", "field4": "field4 10", "field5 ": "field5 10" },
            { "id": 41, "name": "name 11", "description": "description 1", "field3": "field3 11", "field4": "field4 11", "field5 ": "field5 11" },
            { "id": 42, "name": "name 12", "description": "description 1", "field3": "field3 12", "field4": "field4 12", "field5 ": "field5 12" },
            { "id": 43, "name": "name 13", "description": "description 1", "field3": "field3 13", "field4": "field4 13", "field5 ": "field5 13" },
            { "id": 44, "name": "name 14", "description": "description 1", "field3": "field3 14", "field4": "field4 14", "field5 ": "field5 14" },
            { "id": 45, "name": "name 15", "description": "description 1", "field3": "field3 15", "field4": "field4 15", "field5 ": "field5 15" },
            { "id": 46, "name": "name 16", "description": "description 1", "field3": "field3 16", "field4": "field4 16", "field5 ": "field5 16" },
            { "id": 47, "name": "name 17", "description": "description 1", "field3": "field3 17", "field4": "field4 17", "field5 ": "field5 17" },
            { "id": 48, "name": "name 18", "description": "description 1", "field3": "field3 18", "field4": "field4 18", "field5 ": "field5 18" },
            { "id": 49, "name": "name 19", "description": "description 1", "field3": "field3 19", "field4": "field4 19", "field5 ": "field5 19" },
            { "id": 50, "name": "name 5", "description": "description 1", "field3": "field3 5", "field4": "field4 5", "field5 ": "field5 5" },
            { "id": 51, "name": "name 6", "description": "description 1", "field3": "field3 6", "field4": "field4 6", "field5 ": "field5 6" },
            { "id": 52, "name": "name 7", "description": "description 1", "field3": "field3 7", "field4": "field4 7", "field5 ": "field5 7" },
            { "id": 53, "name": "name 8", "description": "description 1", "field3": "field3 8", "field4": "field4 8", "field5 ": "field5 8" },
            { "id": 54, "name": "name 9", "description": "description 1", "field3": "field3 9", "field4": "field4 9", "field5 ": "field5 9" },
            { "id": 55, "name": "name 10", "description": "description 1", "field3": "field3 10", "field4": "field4 10", "field5 ": "field5 10" },
            { "id": 56, "name": "name 11", "description": "description 1", "field3": "field3 11", "field4": "field4 11", "field5 ": "field5 11" },
            { "id": 57, "name": "name 12", "description": "description 1", "field3": "field3 12", "field4": "field4 12", "field5 ": "field5 12" },
            { "id": 58, "name": "name 13", "description": "description 1", "field3": "field3 13", "field4": "field4 13", "field5 ": "field5 13" },
            { "id": 59, "name": "name 14", "description": "description 1", "field3": "field3 14", "field4": "field4 14", "field5 ": "field5 14" },
            { "id": 60, "name": "name 15", "description": "description 1", "field3": "field3 15", "field4": "field4 15", "field5 ": "field5 15" },
            { "id": 61, "name": "name 16", "description": "description 1", "field3": "field3 16", "field4": "field4 16", "field5 ": "field5 16" },
            { "id": 62, "name": "name 17", "description": "description 1", "field3": "field3 17", "field4": "field4 17", "field5 ": "field5 17" },
            { "id": 63, "name": "name 18", "description": "description 1", "field3": "field3 18", "field4": "field4 18", "field5 ": "field5 18" },
            { "id": 64, "name": "name 19", "description": "description 1", "field3": "field3 19", "field4": "field4 19", "field5 ": "field5 19" },
            { "id": 65, "name": "name 20", "description": "description 1", "field3": "field3 20", "field4": "field4 20", "field5 ": "field5 20" }
        ];

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

    employeeIndexController.$inject = ['$scope', '$filter', 'EmployeeIndexFactory'];

    angular.module('EmployeeIndexApp').controller('EmployeeIndexController', employeeIndexController);

}());