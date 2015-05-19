(function () {
    var employeeIndexFactory = function ($http) {
        return {
            getEmployees: function () {
                return $http.get('/Project/GetEmployees')
                    .then(function (result) {
                        return result.data;
                    });
            }
        }
    };

    employeeIndexFactory.$inject = ['$http'];

    angular.module('employeeIndexApp').factory('employeeIndexFactory', employeeIndexFactory);
}());

