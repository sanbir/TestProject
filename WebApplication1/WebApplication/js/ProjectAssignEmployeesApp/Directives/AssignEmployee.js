(function () {
    var assignEmployee = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (isAssigned) {

                    //TODO:
                    var selectedEmployee = scope.item;
                    var assignedEmployeesIds = scope.$parent.project.assignedEmployeesIds;
                    var assignedEmployees = scope.$parent.assignedEmployees;

                    if (isAssigned) {
                        if (assignedEmployeesIds.indexOf(selectedEmployee.id) === -1) {
                            assignedEmployeesIds.push(selectedEmployee.id);
                            assignedEmployees.push(selectedEmployee);
                        }
                    } else {
                        if (assignedEmployeesIds.indexOf(selectedEmployee.id) !== -1) {
                            for (var i = 0; i < assignedEmployeesIds.length; i++) {
                                if (assignedEmployeesIds[i] === selectedEmployee.id) {
                                    assignedEmployeesIds.splice(i, 1);
                                    assignedEmployees.splice(i, 1);
                                    break;
                                }
                            }
                        }
                    }

                    return isAssigned;
                });
            }
        };
    };

    angular.module('projectAssignEmployeesApp').directive('assignEmployee', assignEmployee);

}());
