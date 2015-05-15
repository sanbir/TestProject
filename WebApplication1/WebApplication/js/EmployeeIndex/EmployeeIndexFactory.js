(function () {
    var employeeIndexFactory = function () {

        var factory = {};

        factory.Id = 42;

        return factory;
    };

    angular.module('EmployeeIndexApp').factory('EmployeeIndexFactory', employeeIndexFactory);
}());
