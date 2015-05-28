(function () {

    var projectIndexController = function ($scope) {

        var commonFilter = function () {
            this.isShown = get("SearchString");
            this.show = function() {
                this.isShown = !this.isShown;
            }
        };

        $scope.projectNameFilter = new commonFilter();
        $scope.customerCompanyNameFilter = new commonFilter();
        $scope.managerFilter = new commonFilter();
        $scope.startDateFilter = new commonFilter();
        $scope.endDateFilter = new commonFilter();
        $scope.priorityFilter = new commonFilter();
        $scope.commentFilter = new commonFilter();

        function get(name) {
            if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search)) {
                if (decodeURIComponent(name[1]) !== "") return true;
            }
            return false;
        }

    };

    projectIndexController.$inject = ['$scope'];

    angular.module('projectIndexApp').controller('projectIndexController', projectIndexController);

}());