function CancelReasonsModel() {
    //debugger;
    var self = this;
    self.CancelReasonId = ko.observable().extend({ required: true });
    self.cancelReasonName = ko.observable('');

    self.is24HourCancellation = ko.observable();
    
    self.errors = ko.validation.group(self);
}