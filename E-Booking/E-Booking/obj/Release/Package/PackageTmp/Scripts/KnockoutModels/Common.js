/// <reference path="../jquery-ui-1.11.2.min.js" />

$(function () {

    $("#AppointmentDateTimeAcceptanceDialog, #PrivatePatientConfirmationDialog, #TransportSelectionConfirmationDialog, #TransportSelectionConfirmationYesSelectedDialog, #TransportSelectionConfirmationNoSelectedDialog,#PatientIsRiskAssessmentRequiredConfirmationDialog, #PatientIsRiskAssessmentRequiredNonConfirmationDialog, #EBookingCompleteDialog,#NewJourneyAcceptanceDialog, #AmendJourneyAcceptanceDialog, #CancelJourneyAcceptanceDialog, #ReferenceNumberSearchDialog, #ForgetPasswordDialog, #ErrorDialog, #CancelJourneyReasonDialog, #NewJourneyAbandonConfirmationDialog, #UserSignUpSuccessDialog, #CancellationReasonSaveSuccessDialog, #UserSignUpUpdateSuccessDialog, #RecordNotFoundDialog, #RecordNotEditableDialog, #PatientFoundDialog, #DataClearConfirmationDialog, #EmailSentSuccessDialog").dialog({
        modal: true,
        autoOpen: false,
        draggable: true,
        resizable: false,
        width: 'auto',
        close: function () { }
    });

    $(".ui-dialog-titlebar").hide();
   // ko.validation.init({ decorateInputElement: true, errorElementClass: 'fieldError', insertMessages: false, errorsAsTitle: false });
    ko.validation.configure({
        decorateInputElement: true,
        insertMessages: false,
        errorsAsTitle: false,
        errorElementClass: "fieldError",
        registerExtenders: true,
        messagesOnModified: true,
        decorateElementOnModified:false,
        decorateElement: true
    });
    ko.validation.rules['greaterThan'] = {
        validator: function (val, otherVal) {
            //alert(new Date(val));
            //alert(otherVal());
            debugger;
            if (typeof val == 'undefined' || !val)//=='undefined' || val == '' || val == 'null'
            {
                return true;
            }
            return new Date(val) >= new Date(otherVal());
        },
        message: 'The Weighing Date should not be less than Date of Birth'
    };
    ko.validation.rules['userNameExists'] = {

        validator: function (val,userid) {
            debugger;
            var xyx = userid();
            if (parseInt(userid()) == 0) {
                var isUsed;
                $.when(
                    $.ajax({
                        url: '../api/Accountwebapi/ValidateUserName',
                        async: false,
                        data: { username: val },
                        type: "GET",
                    })
                        ).then(
                        function (data, textStatus, jqXhr) {
                            debugger;
                            isUsed = (textStatus === 'success') ? data : null;
                        });
                return !isUsed;
            }
            else
                return true;

        },
        message: 'User name already in use.'

    }
    ko.validation.registerExtenders();
  //  ko.makeBindingHandlerValidatable("textInput");

    ko.bindingHandlers.showMessageToolTip = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            //debugger;
            SetElementToolTip(true, '', $(element));

            valueAccessor().subscribe(function () {
               
                SetElementToolTip(valueAccessor().isValid(), valueAccessor().error, $(element));
            });
        }
    };
    
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var $el = $(element);
            //initialize datepicker with some optional options
            var options = allBindingsAccessor().datepickerOptions || {};
            $el.datepicker(options);

            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($el.datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $el.datepicker("destroy");
            });

            ko.bindingHandlers['validationCore'].init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                $el = $(element),
                current = $el.datepicker("getDate");

            if (value - current !== 0) {
                $el.datepicker("setDate", value);
            }
        }
    };

    //ko.bindingHandlers.masked = {
    //    init: function (element, valueAccessor, allBindingsAccessor) {
    //        var mask = allBindingsAccessor().mask || {};
    //        $(element).mask(mask);
    //        ko.utils.registerEventHandler(element, 'focusout', function () {
    //            var observable = valueAccessor();
    //            observable($(element).val());
    //        });
    //    },
    //    update: function (element, valueAccessor) {
    //        var value = ko.utils.unwrapObservable(valueAccessor());
    //        $(element).val(value);
    //    }
    //};

    ko.bindingHandlers.mask = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            debugger;
            var options = ko.utils.unwrapObservable(valueAccessor()) || "";
            $(element).mask(options);
        }
    };
   
    //ko.bindingHandlers.mask = {
    //    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    //        var mask = valueAccessor() || {};
    //        $(element).inputmask({ "mask": mask, 'autoUnmask': false });
    //        ko.utils.registerEventHandler(element, 'focusout', function () {
    //            var value = $(element).inputmask('unmaskedvalue');
    //            if (!value) {
    //                viewModel[$(element).attr("id")]("");
    //            }
    //        });
    //    }
    //};
});



function IsValidKnockoutModel(model) {
    debugger;
    if (model.errors().length > 0) {
        //debugger;
        model.errors.showAllMessages();
        for (var key in model) {
            if (key == 'errors' || key == 'isValid' || key == 'isAnyMessageShown')
                continue;
            try{
                if (ko.isObservable(model[key]) && !model[key].isValid())
                   model[key].notifySubscribers(false);
            }
            catch (exception) {
                console.log(exception.toString());
            }
        }

        return false;
    }
    else
        return true;
}
///^[a-zA-Z]+@nhs\.net$/


var patterns = {
    email: /^([\w-]+(?:\.[\w-]+)*)@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i,
    phone: /^[\d ]+$/,
    postcode: /^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)|(no fixed abode)$/ig,
    time: /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/,
    iowNumber: /^[IWiw]{2}[0-9]{6}$/,
    NHSNumber: /^\d{3}\s\d{3}\s\d{4}$/,
    extension: /^\d{1,5}$/,
    phoneex: /^(\+44)?\s?((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$/g,///^(\+44)?\d[\d ]{5,20}\d$/,
    dates: /^\d{2}\/\d{2}\/\d{4}$/,
    weight: /^\d{1,3}(\.\d{1,2})?$/,
    emailnhsnet: /^([\w-]+(?:\.[\w-]+)*)@(nhs)\.(net)$/ig,
    regexnames: /^[a-z ,.'-]{1,30}$/ig
    //dates: /^(((0[1-9]|[12][0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$|^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$/
//((iow\.))?(nhs)\.(uk)$/i
};

function ShowProgressBar() {
    $("#ajaxLoadingBar").show();
}

function HideProgressBar() {
    $("#ajaxLoadingBar").hide();
}

function ShowErrorMessage() {
    
    $("#ErrorDialog").dialog("open");
}

function captureEnter(Nextfunction,event)
{
    //alert('I am called');
    if (event.keyCode == 13)
    {
       // alert(Nextfunction);
        window[Nextfunction]();
        //$(Nextfunction).trigger("click");
    }
}

function SetElementToolTip(isValid, tipMsg, element) {
    //debugger;
    if (element.data('qtip'))
        element.qtip('destroy', true);
    var position = { my: 'bottom left', at: 'top left' };//top left

    if (!isValid) {

        element.qtip({
            //overwrite: true,
            content: { text: tipMsg },
            
            position: position,           
            style: { classes: 'qtip-red qtip-shadow qtip-rounded qtip-custom', color: 'black' }
        });
    }
    else {
        //console.log(element[0].id.toString());
        if (element[0].id == 'chkLoginUserNHSEligibilityCriteria' || element[0].id == 'chkLoginUserPrivatePatient')
            tipMsg = 'You are required to select of these boxes';
        else if (element[0].id == 'chkPatientIsMainlandRepatriation') {
            //debugger;
            //position = { target: 'mouse' };
            tipMsg = '<u>Transportation provided for a non I.W registered patient away from the Island.</u> <br /><br /> In the majority of cases PCT’s are responsible for the cost of transportation of patients in their registered population. Thus permission to charge must be gained before the travel takes place, and time must be allowed for costing the journey and authority obtained. If in doubt please phone 1983 822099 Ext 3504 ';
        } else if (element[0].id == 'txtPatientJourneyDate')
            tipMsg = 'If you need to arrange a patient journey outside of the available dates <br>please contact the PTS service - using 01983 822099 Ext 3504';
        else if (element[0].id == 'ddlPatientAppointmentTime')
            tipMsg = 'This is NOT a booking time - it is an estimated time to facilitate easier<br>planning of journeys';
        else if (element[0].id == 'ddlTransportRequirementTransportRequestReason') {
            position = { my: 'top left', at: 'bottom left' }
            tipMsg = '<b>REASON FOR TRANSPORT - QUICK REFERENCE GUIDE</b><br><b>NOTE:</b> Transportation using the Patient Transport service is not a right but is based upon medical requirements (refer to the DOH eligibility criteria for patient transport 2008) The transfer by patient transport vehicles of patients who do not require this type of transport is inappropriate.<br><b><u>Admission:</u></b><br>All admissions must be authorised by a Doctor, Consultant or other allied health professional with prior agreement from the Patient Transport Service. <br><b><u>Discharge</u></b><br>As part of the discharge process from Hospital all Service Users must be ready for discharge from 0900hrs or 13:00hrs and will be collected from the discharge area.These discharges will be combined with other journeys to ensure maximum use of resources. Patients requiring transport outside of these hours, will be conveyed on an ‘as and when’ basis.<br><b><u>Intermediate care:</u></b><br>The intermediate care nursing beds (provided within the private sector) and the Independent Living Rescource Centre (IRLC) beds provide short stay, structured, goal orientated programmes for adults who are recovering from an illness, injury or operation, and who are willing and able to work with the Intermediate Care Team to improve their level of fitness.<br><b><u>Outpatient:</u></b><br>Out patient and clinic appointment times for patients conveyed by patient transport will be either 10:00hrs for morning clinics or 14:00hrs for afternoon clinics. It is the responsibility of the person/department making the booking to ensure the patient is informed of the appointment time. A return journey will be created automatically.';
        }
        else if (element[0].id == 'ddlTransportRequirementTransportSelection') {
            if (element[0].options[element[0].selectedIndex].value == "1")
                tipMsg = "Can walk with the assistance of one person";
            else if (element[0].options[element[0].selectedIndex].value == "2")
                tipMsg = "Can be transferred with the assistance of two people";
            else if (element[0].options[element[0].selectedIndex].value == "3")
                tipMsg = "Own Wheel chair";
            else if (element[0].options[element[0].selectedIndex].value == "4")
                tipMsg = "Own Bariatric Wheelchair";
            else if (element[0].options[element[0].selectedIndex].value == "5")
                tipMsg = "Ambulance Wheelchair";
            else if (element[0].options[element[0].selectedIndex].value == "6")
                tipMsg = "Ambulance Bariatric Wheelchair";
            else if (element[0].options[element[0].selectedIndex].value == "7")
                tipMsg = "Wheelchair to and from Ambulance. Stretcher for the journey";
            else if (element[0].options[element[0].selectedIndex].value == "8")
                tipMsg = "Stretcher patient";
            //tipMsg = 'Data available from the ‘Patient Transport Service Information Pack on the Intranet<br>ttp://intranet/uploads/ambulance/pdfs/PR300913-Patient_Transport_Services_Information_Pack_June_2013-PROOF.pdf<br><br>TRANSPORT SELECTION<br>THIS QUICK REFERENCE GUIDE DOES NOT REPLACE THE USE OF THE FLOW CHART TO ASSIST WITH CHOOSING THE<br>CORRECT TRANSPORT SELECTION, FOR THE JOURNEY<br><b>C1A:</b><br>For Service Users who are able to walk and require the assistance of 1 PTS member of staff<br><b>C2:</b><br>For Service Users who require the assistance of 2 PTS staff<br><b>OWC:</b><br>For Service Users who can only travel in their own wheelchair for the journey, with ramped or flat access at collection address and destination<br>Assistance of 1 or 2 PTS members of staff<br><b>AWC:</b><br>For Service Users who require vehicle and wheelchair for the journey, or require hoisting into wheelchair with hoisting facilities at destination and<br>who do not have their own wheelchair. Ramped or flat access at collection address and destination required.  Assistance of 2 PTS members of staff<br><b>WC/STR:</b><br>For Service Users who are suitable to be taken to the ambulance in a wheelchair but then must lie down for the journey.(must be able to transfer with 2 PTS members of staff or less)<br><b>STR:</b><br>Service User requires leg(s) to be elevated or to lay down, for the entire journey. Is access suitable for a stretcher at pick up and destination<br>i.e. No steps, sharp turns, small lifts before area where Service user is to be transferred.';
        }
        else if (element[0].id == 'chkTransportRequirementEscortTravelling')
            tipMsg = '<b>ESCORT TRAVELLING</b><br>Escorts may be medical or non-medical<br>Medical escorts are determined by those booking PTS where the Service Users condition is such they require constant medical attention of a medical escort beyond the capability of the PTS crew throughout their journey.<br>Non-medical escorts are not expected to be routinely authorised. Department of Health guidance states that non-medical escorts (i.e. friend or relative) are to be the exception.<br>Typically non-medical escorts are only appropriate for circumstances where:<br>- The Service User is under 18 years of age.<br>- The Service User requires a translator.<br>- The Service User has severe communication difficulties e.g. has a profound sight or hearing/speech impairment and cannot travel safely without a known carer.<br>- The Service User suffers from a physical or mental health problem e.g. Alzheimer’s disease, Dementia etc which prevents them from travelling unaccompanied   	safely without a known carer.<br>Other than these few cases Service Users are not entitled to an escort. The PTS does not exist to provide transport for escorts on social grounds. Partners/family members/friends etc whose support is not required by the Service User in the circumstances described are responsible for their own transport at their own cost.<br>Escorts will not be accepted at time of pick up unless already included as part of the booking. Only in exceptional circumstances may non-booked escorts be able to travel.All children under the age of 18 are required to have an escort for their journey and this must be arranged prior to booking the transport. There are very rare circumstances in which some Service Users under the age of 18, and/or their parents may wish them to travel without an escort. The responsibility of ensuring an escort has been arranged to travel with ALL children at the time of booking transport lies with the originating requesting department and the requester. Should any member of the PTS Team identify any concerns these must be raised at the time prior to transportation and the requesting department will be contacted. This may delay attendance at a hospital appointment or even result in refusal to transport.<br><b>ASSISTANCE DOGS</b><br>(including guide and hearing dogs) are permitted on the Service, providing this is specified in the booking, to ensure appropriate consideration can be made for other patients.';
        else if (element[0].id == 'TransportRequirementIsRiskAssessmentRequiredNonConfirmation') {
            tipMsg = 'By clicking OK you are confirming that a risk assessment is not required';
            position = { my: 'bottom right', at: 'top right' };

        }
        else if (element[0].id == 'cancelReasons') {
            // debugger;
            tipMsg = 'Select Reason for Cancelling the Journey. If not present then please select Other and mention the reason in the text field.';
            //position = { my: 'bottom right', at: 'top right' };
        }
        else if (element[0].id == 'ddlSignupFacility') {
            tipMsg = 'If your workplace is not listed – please contact PTS on 01983 822088 ext 3504';
        }

        if (tipMsg != null && tipMsg != '') {
            element.qtip({
                overwrite: true,
                content: { text: tipMsg },
                position: position,
                style: { classes: 'qtip-blue qtip-shadow qtip-rounded qtip-custom' }
            });
        }
    }
}


