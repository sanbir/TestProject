(function () {
    var assignEmployee = function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                ngModelController.$parsers.push(function (isAssigned) {

                    //TODO:
                    if (isAssigned) {
                        if ($scope.project.assignedEmployeesIds.indexOf($scope.employeesPage[selectedIndex].id) === -1) {
                            $scope.project.assignedEmployeesIds.push($scope.employeesPage[selectedIndex].id);
                            $scope.assignedEmployees.push($scope.employeesPage[selectedIndex]);
                        }
                    } else {
                        if ($scope.project.assignedEmployeesIds.indexOf($scope.employeesPage[selectedIndex].id) !== -1) {
                            for (var i = 0; i < $scope.project.assignedEmployeesIds.length; i++) {
                                if ($scope.project.assignedEmployeesIds[i] === $scope.employeesPage[selectedIndex].id) {
                                    $scope.project.assignedEmployeesIds.splice(i, 1);
                                    $scope.assignedEmployees.splice(i, 1);
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
