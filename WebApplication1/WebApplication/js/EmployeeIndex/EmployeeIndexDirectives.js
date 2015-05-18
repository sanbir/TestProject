(function () {
    var employeeIndexDirectives = function () {

        var factory = {};

        factory.Id = 42;

        return factory;
    };

    angular.module('EmployeeIndexApp').directive('EmployeeIndexDirectives', employeeIndexDirectives);
}());
