function HomeAddressModel() {

    var self = this;

    self.IsNoFixAbode = ko.observable(false);
    self.LineOne = ko.observable().extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } }, minLength: 1, maxLength: 100 });
    self.LineTwo = ko.observable().extend({required: { onlyIf: function () { return !self.IsNoFixAbode(); } },  minLength: 1, maxLength: 100 });
    self.LineThree = ko.observable().extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } }, minLength: 1, maxLength: 100 });
    self.LineFour = ko.observable().extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } }, minLength: 1, maxLength: 100 });
    self.PostCode = ko.observable().extend({ pattern: { params: patterns.postcode, message: 'Enter valid post code e.g SW42 4RG' } });//.extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } } });
    self.Easting = ko.observable();
    self.Northing = ko.observable();
    self.UPRN = ko.observable();
    self.ContactTelNo = ko.observable('').extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } }, pattern: { params: patterns.phoneex, message: 'Enter valid Phone no. e.g 01983 409292 or 01983409292' } });
    self.AlternateContactTelNo = ko.observable('').extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } }, pattern: { params: patterns.phoneex, message: 'Enter valid Phone no. e.g 01983 409292 or 01983409292' } });
    self.RelationshipId = ko.observable().extend({ required: { onlyIf: function () { return !self.IsNoFixAbode(); } } });

    self.errors = ko.validation.group(self);
}