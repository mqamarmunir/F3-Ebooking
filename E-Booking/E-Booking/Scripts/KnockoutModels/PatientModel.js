function PatientModel() {

    var self = this;

    //Appointment Details
    self.PatientID = ko.observable(0);
    self.IsRiskAssessmentRequired = ko.observable(false);
    self.IsMainlandRepatriation = ko.observable(false);
    self.JourneyDate = ko.observable().extend({ required: { onlyIf: function () { return (!self.IsMainlandRepatriation() && !self.IsRiskAssessmentRequired()); } } });
    self.AppointmentTimeId = ko.observable().extend({ required: { onlyIf: function () { return (!self.IsMainlandRepatriation() && !self.IsRiskAssessmentRequired()); } } });
    self.EstimatedAppointmentDurationId = ko.observable();
    self.ActualAppointmentTime = ko.observable().extend({ pattern: { params: patterns.time, message: 'Valid time required e-g (10:00)' } });
    self.BookingNo = ko.observable();
    //Patient Details
    self.GenderTitleId = ko.observable().extend({ required: true });
    self.FirstName = ko.observable().extend({ required: true, pattern: {params:patterns.regexnames, message:'Valid name required.'} });
    self.Surname = ko.observable().extend({ required: true, pattern: { params: patterns.regexnames, message: 'Valid name required.' } });
    self.FullName = ko.computed(function () {
        return self.FirstName() + ' ' + self.Surname();
    });
    self.BirthDate = ko.observable().extend({ required: true });//, pattern: {params:patterns.dates, message: 'Enter Valid Date or select from calender' }
    self.NHSNumber = ko.observable().extend({required:true, pattern: { params: patterns.NHSNumber, message: 'Valid NHS Number required e-g 000 000 0000' } });
    self.IsleOfWightNo = ko.observable().extend({ pattern: { params: patterns.iowNumber, message: 'Valid Isle of Wight Number required e-g IW000000' } });
    self.NameOfGP = ko.observable().extend({ required: true, pattern: { params: patterns.regexnames, message: 'Valid name required.' } });
    self.LastRecordedPatientWeight = ko.observable().extend({ pattern: { params: patterns.weight, message: 'Please Enter valid Weight. Weight must be Between 0-999 and upto 2 decimal places.' } });
    self.WeighingDate = ko.observable();//.extend({ greaterThan:self.BirthDate });//, pattern: { params: patterns.dates, message: 'Enter Valid Date or select from calender' }
    self.GPPracticeId = ko.observable().extend({ required: true });
    self.GPPracticeAddressLineOne = ko.observable().extend({ minLength: 1, maxLength: 100 });
    self.GPPracticeAddressLineTwo = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.GPPracticeAddressLineThree = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.GPPracticeAddressLineFour = ko.observable().extend({ required: true, minLength: 1, maxLength: 100 });
    self.GPPracticeAddressPostCode = ko.observable().extend({ pattern: { params: patterns.postcode, message: 'Enter valid post code e.g SW42 4RG' } });//.extend({ required: true });
    self.GPPracticeEasting = ko.observable();
    self.GPPracticeNorthing = ko.observable();
    self.GPPracticeURPN = ko.observable();
    self.Easting = ko.observable();
    self.Northing = ko.observable();
    self.UPRN = ko.observable();
    self.ContactTelephoneNo = ko.observable('').extend({
        required: true, pattern: { params: patterns.phoneex, message: 'Enter valid Phone no. e.g 01983 409292 or 01983409292' }
    });
    self.BookingStatusName = ko.observable('');

    self.RequesterSubjectiveCode = ko.observable();
    self.RequesterCostCenter = ko.observable();
    self.RequesterAuthorizingClinician = ko.observable();
    self.RequesterAuthorizingRoleId = ko.observable();
    self.ComplexJourney = ko.observable(false);
    self.JourneyDate2 = ko.observable();//.extend({ required: { onlyIf: function () { return self.ComplexJourney() } } });//, pattern: { params:patterns.dates, message: 'Enter Valid Date or select from calender' }
    self.RejectionReason = ko.observable();
    self.LastStatusAt = ko.observable();
    self.JourneyDateBeforeUpdate = ko.observable();
    self.CADCaseID = ko.observable();
    self.isPrivatePatient = ko.observable();
    self.errors = ko.validation.group(self);
}


//, pattern: { params:patterns.dates, message: 'Enter Valid Date or select from calender' }