(function () {
    var assignIfManager = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (managerId) {

                    var manager = scope.item;
                    manager.isAssigned = false;
                    scope.$parent.managerFullName = manager.lastName
                        + " " + manager.firstName
                        + " " + manager.middleName;

                    return managerId;
                });
            }
        };
    };

    angular.module('projectAssignEmployeesApp').directive('assignIfManager', assignIfManager);

}());
