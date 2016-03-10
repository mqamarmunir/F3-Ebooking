function LoginModel() {
    var self = this;

    self.IsAuthorized = ko.observable(false);
    self.UserName = ko.observable().extend({ required: { onlyIf: function () { return !self.IsAuthorized() } }, pattern: { params: patterns.email, message: 'Valid email required e.g. example@nhs.uk' } });
    self.Password = ko.observable().extend({ required: { onlyIf: function () { return !self.IsAuthorized() } } });
    self.PrivatePatient = ko.observable(false);
    self.NHSEligibilityCriteria = ko.observable(false);
    // self.IsPasswordForgotten = ko.observable(false);
    self.IsPasswordForgotten = ko.observable(true);
    self.Email = ko.observable().extend({ required: { onlyIf: function () { return (!self.IsPasswordForgotten()) } }, pattern: { params: patterns.email, message: 'Valid email required e.g. example@nhs.uk' } });//.extend({ required: { onlyif: function () { return !self.IsPasswordForgotten() } }, pattern: { params: patterns.email, message: 'Valid email required e-g example@nhs.net' } });
    self.PrivatePatient.extend({ checked: { onlyIf: function () { return (!self.NHSEligibilityCriteria() && self.IsAuthorized()); } } });
    self.NHSEligibilityCriteria.extend({ checked: { onlyIf: function () { return (!self.PrivatePatient() && self.IsAuthorized()); } } });
    
    //
    self.errors = ko.validation.group(self);

}