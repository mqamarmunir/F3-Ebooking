function TransportRequirementModel()
{
    var self = this;
    //debugger;
    self.Id = ko.observable(0);
    self.TransportRequestReasonId = ko.observable().extend({ required: true });
    self.TransportSelectionId = ko.observable().extend({ required: true });
    
    //Additional Patient Informations
    self.IsTravellingWithOwnOxygen = ko.observable(false);
    
    self.IsInfectious = ko.observable(false); 
    self.InfectiousId = ko.observable().extend({ required: { onlyIf: function () { return self.IsInfectious(); } } });
    self.IsInfectious.subscribe(function (value) { if (value == false) { self.InfectiousId(null); } });
    self.AdditionalPatientInfo = ko.observable('').extend({ required: { onlyIf: function () { return (self.InfectiousId() == 4 && self.IsInfectious() == true ? true : false); } }, minLength: 10 });
    self.IsEscortTravelling = ko.observable(false);
    self.EscortTypeId = ko.observable().extend({ required: { onlyIf: function () { return self.IsEscortTravelling(); } } });
    self.EscortNumberId = ko.observable().extend({ required: { onlyIf: function () { return self.IsEscortTravelling(); } } });
    self.IsEscortTravelling.subscribe(function (value) { if (value == false) { self.EscortTypeId(null); self.EscortNumberId(null); } });
    self.IsBariatric = ko.observable(false);
    self.IsFullLegPlasterPOP = ko.observable(false);
    self.IsElectricWheelchair = ko.observable(false);
    self.IsWheelchairAndLegExtension = ko.observable(false);
    self.IsWaterlow = ko.observable(false);
    self.IsDNACPR = ko.observable(false);
    self.IsDiabetic = ko.observable(false);
    self.IsNuclearMedicineRadioActiveRisk = ko.observable(false);
    self.IsNoneOfAbove = ko.observable(false);

    self.IsInfectious.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsTravellingWithOwnOxygen.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsEscortTravelling.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsBariatric.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsFullLegPlasterPOP.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsElectricWheelchair.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsWheelchairAndLegExtension.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsWaterlow.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsDNACPR.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsDiabetic.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsNuclearMedicineRadioActiveRisk.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.IsNoneOfAbove.extend({ checked: { onlyIf: function () { return CheckAtleastOneOptionSelected(self); }, message: "Please select at least one option to proceed." } });
    self.Is24HourAmendment = ko.observable(false);
    self.errors = ko.validation.group(self);
}

function CheckAtleastOneOptionSelected(self) {
    return !(self.IsInfectious() || self.IsEscortTravelling() || self.IsTravellingWithOwnOxygen() || self.IsBariatric() || self.IsFullLegPlasterPOP() || self.IsElectricWheelchair() || self.IsWheelchairAndLegExtension() || self.IsWaterlow() || self.IsDNACPR() || self.IsDiabetic() || self.IsNuclearMedicineRadioActiveRisk() || self.IsNoneOfAbove());
}
