(function () {

    var projectFactory = function () {

        var project = {};

        project.id = 0;
        project.projectName = "ProjectName_TEST";
        project.customerCompanyName = "CustomerCompanyName_TEST";
        project.startDate = new Date(2015, 05, 01);
        project.endDate = new Date(2015, 06, 10);
        project.priority = 42;
        project.comment = "Comment_TEST";

        project.managerId = 0;
        project.assignedEmployeesIds = [];

        return project;
    };

    angular.module('employeeIndexApp').factory('projectFactory', projectFactory);
}());

