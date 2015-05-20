(function () {
    var setManager = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (isManager) {

                    //TODO:
                    var selectedEmployee = scope.item;
                    var managerId = scope.$parent.project.managerId;

                    //if (isManager) {
                    //    managerId = selectedEmployee.id;
                    //}

                    return isManager;
                });
            }
        };
    };

    angular.module('projectAssignEmployeesApp').directive('setManager', setManager);

}());
