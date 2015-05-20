(function () {
    var assignIfManager = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (managerId) {

                    scope.item.isAssigned = true;

                    return managerId;
                });
            }
        };
    };

    angular.module('projectAssignEmployeesApp').directive('assignIfManager', assignIfManager);

}());
