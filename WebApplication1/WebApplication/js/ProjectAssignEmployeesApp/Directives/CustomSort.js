(function () {
    var customSort = function () {
        return {
            restrict: 'A',
            transclude: true,
            scope: {
                order: '=',
                sort: '='
            },
            template:
              ' <a ng-click="sort_by(order)" style="color: #555555;">' +
              '    <span ng-transclude></span>' +
              '    <i ng-class="selectedCls(order)"></i>' +
              '</a>',
            link: function (scope) {

                // change sorting order
                scope.sort_by = function (newSortingOrder) {
                    var sort = scope.sort;

                    if (sort.propertyName == newSortingOrder) {
                        sort.reverse = !sort.reverse;
                    }

                    sort.propertyName = newSortingOrder;
                };


                scope.selectedCls = function (column) {
                    if (column == scope.sort.propertyName) {
                        return ('icon-chevron-' + ((scope.sort.reverse) ? 'down' : 'up'));
                    }
                    else {
                        return 'icon-sort';
                    }
                };
            }
        }
    };

    angular.module('projectAssignEmployeesApp').directive('customSort', customSort);

}());
