function SpecialistTransportRequestModel()
{
    var self = this;

    self.IsHandledByProfessional = ko.observable(false);
    self.IsWhilstLayingDown = ko.observable(false);
    self.IsOxygenTheropy = ko.observable(false);
    self.IsPrecludesTravelling = ko.observable(false);

    self.IsAdmission = ko.observable(false);
    self.IsVisitOrAdmitted = ko.observable(false);
    self.IsVisit = ko.observable(false);

    self.AuthorisingConsultantOrGP = ko.observable().extend({ required: true });
    self.AuthorisingPracticeName = ko.observable().extend({ required: true });
    
    self.IsHandledByProfessional.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonOneAnswerSelected(self); } } });
    self.IsWhilstLayingDown.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonOneAnswerSelected(self); } } });
    self.IsOxygenTheropy.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonOneAnswerSelected(self); } } });
    self.IsPrecludesTravelling.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonOneAnswerSelected(self); } } });

    self.IsAdmission.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonTwoAnswerSelected(self); } } });
    self.IsVisitOrAdmitted.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonTwoAnswerSelected(self); } } });
    self.IsVisit.extend({ checked: { onlyIf: function () { return checkedAtLeastReasonTwoAnswerSelected(self); } } });
    
    self.errors = ko.validation.group(self);
}

function checkedAtLeastReasonOneAnswerSelected(self) {
    return !(self.IsHandledByProfessional() || self.IsWhilstLayingDown() || self.IsOxygenTheropy() || self.IsPrecludesTravelling());
}
function checkedAtLeastReasonTwoAnswerSelected(self) {
    return !(self.IsAdmission() || self.IsVisitOrAdmitted() || self.IsVisit());
}