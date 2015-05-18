(function () {

    var employeeIndexController = function ($scope, employeeIndexFactory) {

        $scope.Id = employeeIndexFactory.Id;

    };

    employeeIndexController.$inject = ['$scope', 'EmployeeIndexFactory'];

    angular.module('EmployeeIndexApp').controller('EmployeeIndexController', employeeIndexController);
}());