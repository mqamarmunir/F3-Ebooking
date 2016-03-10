function RequestertModel() {

    var self = this;

    self.Id = ko.observable();
    self.RequestDate = ko.observable('').extend({ required: true });
    self.StaffPosition = ko.observable('').extend({ required: true });
    self.GenderTitleId = ko.observable().extend({ required: true });
    self.FirstName = ko.observable('').extend({ required: true });
    self.Surname = ko.observable().extend({ required: true });
    self.FacilityId = ko.observable();//.extend({ required: true });
    self.DepartmentId = ko.observable();//.extend({ required: true });
    self.ServiceTypeId = ko.observable().extend({ required: true });
    self.CostCentre = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.SubjectiveCode = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.PostalAddress = ko.observable('').extend({ required: true});
    self.EmailAddress = ko.observable().extend({ required: true, pattern: { params: patterns.emailnhsnet, message:'Valid email required (example@nhs.net)' } });
    self.ContactTelNo = ko.observable('').extend({ required: true });
    self.AuthorisingClinician = ko.observable('').extend({ required: true });
    self.AuthorisingRoleId = ko.observable().extend({ required: true });
    self.FacilityName = ko.observable();
    self.DepartmentName = ko.observable();
    self.facilityType = ko.observable();
    self.Extension = ko.observable();

    self.errors = ko.validation.group(self);
}