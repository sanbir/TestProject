(function () {

    var employeesPageFactory = function ($http, $q) {

        // interface
        var factory = {
            employeesPage: [],
            getEmployees: getEmployees,
            sendData: sendData
        };
        return factory;

        // implementation
        function getEmployees(sortDirection, sortPropertyName, searchString, page) {
            var def = $q.defer();

            var dataToSend = {
                sortDirection: sortDirection,
                sortPropertyName: sortPropertyName,
                searchString: searchString,
                page: page
            };

            $http.post("/Project/GetEmployees", dataToSend)
                .success(function (data, status, headers, config) {
                    factory.employeesPage = data;
                    def.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    def.reject("Failed to get employees");
                });

            return def.promise;
        }

        function sendData(project) {
            var def = $q.defer();

            $http.post("/Project/Persist", JSON.stringify(project))
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function () {
                    def.reject("Failed to send the project");
                });
            return def.promise;
        }
    };

    employeesPageFactory.$inject = ['$http', '$q'];

    angular.module('projectAssignEmployeesApp').factory('employeesPageFactory', employeesPageFactory);
}());

