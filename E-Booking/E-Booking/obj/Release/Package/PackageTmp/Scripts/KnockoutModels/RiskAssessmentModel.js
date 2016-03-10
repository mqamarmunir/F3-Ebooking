function RiskAssessmentModel() {

    var self = this;

    self.PropertyRiskAssessment = ko.observable();
    self.PatientRiskAssessment = ko.observable();
    self.IsManualHandlingProfileCarriedOutYes = ko.observable(false);
    self.IsManualHandlingProfileCarriedOutNo = ko.observable(false);

    self.PatientRiskAssessment.extend({ required: { onlyIf: function () { return !self.PropertyRiskAssessment(); }, message: 'Either this or above field is required' }, minLength: 10, maxLength:800 });
    self.PropertyRiskAssessment.extend({ required: { onlyIf: function () { return !self.PatientRiskAssessment(); }, message: 'Either this or next field is required' }, minLength: 10, maxLength:800 });

    self.IsManualHandlingProfileCarriedOutYes.extend({ checked: { onlyIf: function () { return !self.IsManualHandlingProfileCarriedOutNo(); }, message: 'Either this or next checkbox is required' } });
    self.IsManualHandlingProfileCarriedOutNo.extend({ checked: { onlyIf: function () { return !self.IsManualHandlingProfileCarriedOutYes(); }, message: 'Either this or previous checkbox is required' } });
       
    self.errors = ko.validation.group(self);
}