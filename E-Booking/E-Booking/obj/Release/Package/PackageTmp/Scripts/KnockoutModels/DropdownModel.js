function DropdownModel() {

    var self = this;

    self.Departments = ko.observableArray([]);
    self.ServiceTypes = ko.observableArray([]);
    self.AuthorisingRoles = ko.observableArray([]);
    self.Titles = ko.observableArray([]);
    self.AppointmentTimes = ko.observableArray([]);
    self.EstimatedAppointmentDurations = ko.observableArray([]);
    self.RelationshipToPatient = ko.observableArray([]);   
    self.Facilities = ko.observableArray([]);
    self.FacilityTypes = ko.observableArray([]);
    self.GPPractices = ko.observableArray([]);
    self.PatientTypes = ko.observableArray([]);
    self.RequestTypes = ko.observableArray([]);
    self.EscortTypes = ko.observableArray([]);
    self.EscortNumbers = ko.observableArray([]);
    self.TransportRequestReasons = ko.observableArray([]);
    self.TransportSelections = ko.observableArray([]);
    self.Infectious = ko.observableArray([]);
    self.YourPosition = ko.observableArray([]);
    self.CancelReason = ko.observableArray([]);

    self.errors = ko.validation.group(self);
}

function CancelReasonModel(reason) {
    var self = this;
    self.CancelReasonId = ko.observable(reason.CancelReasonId);
    self.CancelReasonName = ko.observable(reason.CancelReasonName);

    self.errors = ko.validation.group(self);
}

function DepartmentModel(department) {

    var self = this;

    self.DepartmentId = ko.observable(department.DepartmentId);
    self.DepartmentName = ko.observable(department.DepartmentName);

    self.errors = ko.validation.group(self);
}

function FacilityModel(facility) {

    var self = this;

    self.FacilityId = ko.observable(facility.FacilityId);
    self.FacilityName = ko.observable(facility.FacilityName);

    self.errors = ko.validation.group(self);
}

function FacilityTypeModel(facility) {

    var self = this;

    self.FacilityTypeId = ko.observable(facility.FacilityTypeId);
    self.FacilityTypeName = ko.observable(facility.FacilityTypeName);

    self.errors = ko.validation.group(self);
}

function GPPracticeModel(gpPractice) {

    var self = this;

    self.GPPracticeId = ko.observable(gpPractice.GPPracticeId);
    self.GPPracticeName = ko.observable(gpPractice.GPPracticeName);

    self.errors = ko.validation.group(self);
}

function ServiceTypeModel(serviceType) {

    var self = this;

    self.ServiceTypeId = ko.observable(serviceType.ServiceTypeId);
    self.ServiceTypeName = ko.observable(serviceType.ServiceTypeName);

    self.errors = ko.validation.group(self);
}

function AuthorisingRoleModel(authorisingRole) {

    var self = this;

    self.AuthorisingRoleId = ko.observable(authorisingRole.AuthorisingRoleId);
    self.AuthorisingRoleName = ko.observable(authorisingRole.AuthorisingRoleName);

    self.errors = ko.validation.group(self);
}

function TitleModel(title) {

    var self = this;
    
    self.TitleId = ko.observable(title.TitleId);
    self.TitleName = ko.observable(title.TitleName);

    self.errors = ko.validation.group(self);
}

function AppointmentTimeModel(appointmenttime) {

    var self = this;

    self.AppointmentTimeId = ko.observable(appointmenttime.AppointmentTimeId);
    self.AppointmentTimeName = ko.observable(appointmenttime.AppointmentTimeName);

    self.errors = ko.validation.group(self);
}

function EstimatedAppointmentDurationModel(estimatedAppointmentDuration) {

    var self = this;

    self.EstimatedAppointmentDurationId = ko.observable(estimatedAppointmentDuration.EstimatedAppointmentDurationId);
    self.EstimatedAppointmentDurationName = ko.observable(estimatedAppointmentDuration.EstimatedAppointmentDurationName);

    self.errors = ko.validation.group(self);
}


function RelationshipToPatientModel(relationshiptopatient) {

    var self = this;

    self.RelationshipToPatientId = ko.observable(relationshiptopatient.RelationshipToPatientId);
    self.RelationshipToPatientName = ko.observable(relationshiptopatient.RelationshipToPatientName);

    self.errors = ko.validation.group(self);
}

function PatientTypeModel(patientType) {

    var self = this;

    self.PatientTypeId = ko.observable(patientType.PatientTypeId);
    self.PatientTypeName = ko.observable(patientType.PatientTypeName);

    self.errors = ko.validation.group(self);
}

function RequestTypeModel(requestType) {

    var self = this;

    self.RequestTypeId = ko.observable(requestType.RequestTypeId);
    self.RequestTypeName = ko.observable(requestType.RequestTypeName);

    self.errors = ko.validation.group(self);
}

function EscortTypeModel(escortType) {

    var self = this;

    self.EscortTypeId = ko.observable(escortType.EscortTypeId);
    self.EscortTypeName = ko.observable(escortType.EscortTypeName);

    self.errors = ko.validation.group(self);
}

function EscortNumberModel(escortNumber) {

    var self = this;

    self.EscortNumberId = ko.observable(escortNumber.EscortNumberId);
    self.EscortNumberName = ko.observable(escortNumber.EscortNumberName);

    self.errors = ko.validation.group(self);
}

function InfectiousModel(infectious) {
    var self = this;

    self.InfectiousId = ko.observable(infectious.InfectiousId);
    self.InfectiousName = ko.observable(infectious.InfectiousName);

    self.errors = ko.validation.group(self);

}

function TransportRequestReasonModel(transportRequestReason) {

    var self = this;

    self.TransportRequestReasonId = ko.observable(transportRequestReason.TransportRequestReasonId);
    self.TransportRequestReasonName = ko.observable(transportRequestReason.TransportRequestReasonName);

    self.errors = ko.validation.group(self);
}

function TransportSelectionModel(transportSelection) {

    var self = this;

    self.TransportSelectionId = ko.observable(transportSelection.TransportSelectionId);
    self.TransportSelectionName = ko.observable(transportSelection.TransportSelectionName);

    self.errors = ko.validation.group(self);
}
function YourPositionModel(yourPosition) {

    var self = this;

    self.YourPositionId = ko.observable(yourPosition.YourPositionId);
    self.YourPositionName = ko.observable(yourPosition.YourPositionName);

    self.errors = ko.validation.group(self);
}