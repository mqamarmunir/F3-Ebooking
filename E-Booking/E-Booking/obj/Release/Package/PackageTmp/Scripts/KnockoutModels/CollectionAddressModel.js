function CollectionAddressModel() {

    var self = this;

    self.IsThisPatientHomeAddress = ko.observable(false);    
    self.FacilityTypeId = ko.observable().extend({ required: { onlyIf: function () { return !self.IsThisPatientHomeAddress() } } });
    self.FacilityId = ko.observable().extend({ required: { onlyIf: function () { return (!self.IsThisPatientHomeAddress() && parseInt(self.FacilityTypeId())!=5); } } });
    self.DepartmentId = ko.observable().extend({ required: { onlyIf: function () { return (!self.IsThisPatientHomeAddress() && self.FacilityId() && parseInt(self.FacilityTypeId())==4); } } });

    self.FacilityDepartments = ko.observableArray([]);
    self.Facilities = ko.observableArray([]);
    self.LineOne = ko.observable().extend({minLength:1,maxLength:100});
    self.LineTwo = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.LineThree = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.LineFour = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.Easting = ko.observable();
    self.Northing = ko.observable();
    self.UPRN = ko.observable();
    self.PostCode = ko.observable().extend({ pattern: { params: patterns.postcode, message: 'Enter valid post code e.g SW42 4RG' } });//.extend({ required: true });
    self.ContactTelNo = ko.observable('').extend({ required: true, pattern: { params: patterns.phoneex, message: 'Enter valid Phone no. e.g 01983 409292 or 01983409292' } });
    self.ExtensionNo = ko.observable('').extend({ pattern: { params: patterns.extension, message: 'Extension Number Should not be less than 1 and greater than 5 decimal characters.' } });

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
    self.FacilityId= ko.observable(facility.FacilityId);
    self.FacilityName = ko.observable(facility.FacilityName);

    self.errors = ko.validation.group(self);
}