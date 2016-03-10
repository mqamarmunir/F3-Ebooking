function GridPagedModelQamar() {
    var self = this;
    //debugger;
    // stuff that matters
    // ---------------------------------------------
    self.items = ko.observableArray([]);
    self.BookingNo = ko.observable();
    self.FirstName = ko.observable();
    self.Surname = ko.observable();
    self.NHSNumber = ko.observable();
    self.IsleOfWightNo = ko.observable();
    self.ContactTelephoneNo = ko.observable();
    self.BookingStatusName = ko.observable();
    self.JourneyDate = ko.observable();
    self.RejectionReason = ko.observable();
    self.CADCaseID = ko.observable();
  

    self.errors = ko.validation.group(self);
}

function addItems(result)
{
    var self = this;
    //debugger;
    self.BookingNo = ko.observable(result.BookingNo);
    self.FirstName = ko.observable(result.FirstName);
    self.Surname = ko.observable(result.Surname);
    self.NHSNumber = ko.observable(result.NHSNumber);
    self.IsleOfWightNo = ko.observable(result.IsleOfWightNo);
    self.ContactTelephoneNo = ko.observable(result.ContactTelephoneNo);
    self.BookingStatusName = ko.observable(result.BookingStatusName);
    self.JourneyDate = ko.observable(getDate_Journey(result.JourneyDate).toString().replace('01 Jan 1','n/a'));
    self.RejectionReason = ko.observable(result.RejectionReason);
    self.RequestDate = ko.observable(getDateTime_Request(result.LastStatusAt));
    self.CADCaseID = ko.observable(result.CADCaseID.toString().indexOf('0')==0?"":result.CADCaseID);






    self.errors = ko.validation.group(self);
}

function getDate_Journey(date)
{
 //   alert(JSON.stringify(get_browser_info()));
    //alert(date);
    var browserandversion=get_browser_info();
    if (browserandversion.name.toLowerCase().indexOf('msie')>=0 && parseFloat(browserandversion.version)<9.0)
    {
        //alert(NewDate(date));
        return NewDate(date);
        //alert(new Date(date.replace(/-/g, '/')));
        //return $.datepicker.formatDate('dd M yy', new Date(date.replace(/-/g, '/')));
    }
    else{
      //  alert(new Date(date));
        return $.datepicker.formatDate('dd M yy', new Date(date));
    }
}

function getDateTime_Request(date) {
    //   alert(JSON.stringify(get_browser_info()));
    //alert(date);
    var browserandversion = get_browser_info();
    debugger;
    if (browserandversion.name.toLowerCase().indexOf('msie') >= 0 && parseFloat(browserandversion.version) < 9.0) {
        //alert(NewDate(date));
        return NewDateTime(date);
        //alert(new Date(date.replace(/-/g, '/')));
        //return $.datepicker.formatDate('dd M yy', new Date(date.replace(/-/g, '/')));
    }
    else {
        var timeafteroffsetadjustment = new Date(date);
        if (browserandversion.name.toLowerCase().indexOf('msie') < 0 && browserandversion.name.toLowerCase().indexOf('ie') < 0)
            timeafteroffsetadjustment = new Date(timeafteroffsetadjustment.getTime() + timeafteroffsetadjustment.getTimezoneOffset() * 60 * 1000);
        return $.datepicker.formatDate('dd/mm/yy', timeafteroffsetadjustment) + ' ' + pad(timeafteroffsetadjustment.getHours().toString(), 2) + ':' + pad(timeafteroffsetadjustment.getMinutes().toString(), 2);
      //  alert(new Date(date));
      //  return $.datepicker.formatDate('dd M yy', new Date(date));
    }
}

function get_browser_info() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return { name: 'IE', version: (tem[1] || '') };
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR\/(\d+)/)
        if (tem != null) { return { name: 'Opera', version: tem[1] }; }
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) { M.splice(1, 1, tem[1]); }
    return {
        name: M[0],
        version: M[1]
    };
}
function NewDate(dateArg) {
    //debugger;
    var dateValues = dateArg.split('-');
    var date = new Date(dateValues[0],parseFloat(dateValues[1])-1,dateValues[2].substring(0,2));
    return $.datepicker.formatDate('dd M yy', date);
}

function NewDateTime(dateArg) {
    //debugger;
    var dateValues = dateArg.split('-');
    var timeanddayseperator = dateValues[2].split('T');
    var time=timeanddayseperator[1];
    var hoursMinutesSeconds=time.split(':');
    var timeafteroffsetadjustment = new Date(dateValues[0], parseFloat(dateValues[1]) - 1, timeanddayseperator[0], hoursMinutesSeconds[0], hoursMinutesSeconds[1], hoursMinutesSeconds[2], 0);
    return $.datepicker.formatDate('dd/mm/yy', timeafteroffsetadjustment) + ' ' + pad(timeafteroffsetadjustment.getHours().toString(), 2) + ':' + pad(timeafteroffsetadjustment.getMinutes().toString(), 2);
    //return $.datepicker.formatDate('dd M yy', date);
}
function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}
