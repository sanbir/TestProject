(function () {

    var employeesPageFactory = function () {

        var employeesPage = [
            { "id": 750, "firstName": "FirstName1", "lastName": "LastName1", "middleName": "MiddleName1", "email": "Email1", "contractorCompanyName": "СontractorCompanyName1", "isManager": null, "isAssigned": false },
            { "id": 751, "firstName": "FirstName2", "lastName": "LastName2", "middleName": "MiddleName2", "email": "Email2", "contractorCompanyName": "СontractorCompanyName2", "isManager": null, "isAssigned": false },
            { "id": 752, "firstName": "FirstName3", "lastName": "LastName3", "middleName": "MiddleName3", "email": "Email3", "contractorCompanyName": "СontractorCompanyName3", "isManager": null, "isAssigned": false }
        ];

        return employeesPage;
    };

    function employee() {
        this.id = 0;
        this.firstName = "";
        this.lastName = "";
        this.middleName = "";
        this.email = "";
        this.contractorCompanyName = "";

        this.isManager = false;
        this.isAssigned = false;
    }

    angular.module('projectAssignEmployeesApp').factory('employeesPageFactory', employeesPageFactory);
}());

