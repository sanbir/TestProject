(function () {
    var assignEmployee = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (isAssigned) {

                    var selectedEmployee = scope.item;
                    scope.$parent.assignEmployee(selectedEmployee, isAssigned);

                    return isAssigned;
                });
            }
        };
    };

    angular.module('projectAssignEmployeesApp').directive('assignEmployee', assignEmployee);

}());
