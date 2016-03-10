function SignupUserModel() {

    var self = this;

    self.Id = ko.observable('');
    self.RequestDate = ko.observable();
    self.Position = ko.observable().extend({ required: true });
    self.GenderTitleId = ko.observable().extend({ required: true });
    self.FirstName = ko.observable('').extend({ required: true, pattern: { params: patterns.regexnames, message:'Please enter valid name'} });
    self.Surname = ko.observable().extend({ required: true, pattern: { params: patterns.regexnames, message:'Please enter valid name' } });
    self.FacilityId = ko.observable().extend({ required: true });
    self.DepartmentId = ko.observable().extend({ required: { onlyIf: function () { return (self.FacilityId() && parseInt(self.facilityTypeID()) == 4); } } });
    self.PostalAddress = ko.observable('').extend({ required: true, minLength:5, maxLength:350});
    self.ServiceTypeId = ko.observable().extend({ required: true });
    self.EmailAddress = ko.observable().extend({ required: true, pattern: { params: patterns.emailnhsnet, message: 'Valid email required e-g example@nhs.net' } });
    self.ConfirmEmailAddress = ko.observable().extend({ required: true, pattern: { params: patterns.emailnhsnet, message: 'Valid email required e-g example@nhs.net' }, equal: { params: self.EmailAddress, message: 'Email and confirm email should be the same' } });
    self.ContactTelNo = ko.observable('').extend({ required: true, pattern: { params: patterns.phoneex, message: 'Enter valid Phone no. e.g 01983 409292 or 01983409292' } });
    self.Username = ko.observable().extend({ required: true, pattern: { params: patterns.email, message: 'Valid email required e-g example@nhs.uk' }, userNameExists:self.Id });
    self.Password = ko.observable().extend({ required: true, minLength: 6 });
    self.ConfirmPassword = ko.observable().extend({ required: true, minLength: 6, equal: { params: self.Password, message: 'Password and Confirm Password must be equal' } });
    self.facilityTypeID = ko.observable().extend({ required: true });
    self.Extension = ko.observable().extend({ pattern: { params: patterns.extension, message: 'Extension Number Should not be less than 1 and greater than 5 decimal characters.' } });

    self.Facilities = ko.observableArray([]);
    self.FacilityDepartments = ko.observableArray([]);
    self.errors = ko.validation.group(self);
}

function facilityDepartmentsModel(facilityDepartment) {
    var self = this;
    //debugger;
    self.DepartmentId = ko.observable(facilityDepartment.DepartmentId);
    self.DepartmentName = ko.observable(facilityDepartment.DepartmentName);

    self.errors = ko.validation.group(self);
}

function facilitiesModel(facility) {
    var self = this;
    //debugger;
    self.FacilityId = ko.observable(facility.FacilityId);
    self.FacilityName = ko.observable(facility.FacilityName);

    self.errors = ko.validation.group(self);
}