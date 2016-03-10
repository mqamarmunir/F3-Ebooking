function AdditionalPatientInfoModel() {

    var self = this;

    self.AdditionalPatientInfo = ko.observable('').extend({ required: true });
    self.IsInfectious = ko.observable(false);
    self.IsTravellingWithOwnOxygen = ko.observable(false);
    self.IsEscortTravelling = ko.observable(false);
    self.EscortTypeId = ko.observable();
    self.EscortNumberId = ko.observable();
    self.IsBariatric = ko.observable(false);
    self.IsFullLegPlasterPOP = ko.observable(false);
    self.IsElectricWheelchair = ko.observable(false);
    self.IsWheelchairAndLegExtension = ko.observable(false);
    self.IsWaterlow = ko.observable(false);
    self.IsDNACPR = ko.observable(false);
    self.IsDiabetic = ko.observable(false);
    self.IsNuclearMedicineRadioActiveRisk = ko.observable(false);
    self.IsNoneOfAbove = ko.observable(false);

    self.errors = ko.validation.group(self);
}