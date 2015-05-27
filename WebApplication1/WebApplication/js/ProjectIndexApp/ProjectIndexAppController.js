(function () {

    var projectIndexController = function ($scope) {

        $scope.temp = "temp!!!";

    };

    projectIndexController.$inject = ['$scope'];

    angular.module('projectIndexApp').controller('projectIndexController', projectIndexController);

}());