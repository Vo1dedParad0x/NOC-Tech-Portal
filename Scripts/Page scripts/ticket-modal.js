$('#createTicketModal').ready(() => {
    'use strict';

    $('#createTicketModal').on('show.bs.modal', function () {
        $('.pane-container section.current').removeClass('current');
        $('.pane-container section').first().addClass('current');
        $('.pane-scrollbar > progress').val(1);
    });

    $('#createTicketModal').on('shown.bs.modal', function () {
        if ($('.pane-container').has('section').length <= 0) {
            let curSec = $('<section class="current"></section>').appendTo($('.pane-container'));
            $('.pane-container').children().not('section').each(function (i, e) {
                let parent = $(e).parent();
                $(e).detach().appendTo(curSec);
                
                if (getChildHeights(curSec) > curSec.outerHeight(true)) {
                    curSec = $('<section></section>').appendTo(parent);
                    $(e).detach().appendTo(curSec);
                }
                

            });
            $('.pane-scrollbar > progress').attr('max', $('.pane-container section').length);

            $("[data-paneaction]").click(function () {
                let cur = $('section.current').first();
                switch (this.dataset.paneaction) {
                    case "forward":
                        if (cur.next().length > 0) {
                            cur.toggleClass('current');
                            cur.next().toggleClass('current');
                            $('section.current')[0].scrollIntoView({ behavior: 'smooth' });
                            $('.pane-scrollbar > progress').val(function (i, oldval) { return ++oldval });
                        }
                        break;
                    case "backward":
                        if (cur.prev().length > 0) {
                            cur.toggleClass('current');
                            cur.prev().toggleClass('current');
                            $('section.current')[0].scrollIntoView({ behavior: 'smooth' });
                            $('.pane-scrollbar > progress').val(function (i, oldval) { return --oldval });
                        }
                        break;
                }
            });

        }
        $('section.current')[0].scrollIntoView({ behavior: 'smooth' });
    });

    $('#ticketForm').submit(function (event) {
        console.log(this);
        event.preventDefault();
        let formData = $(this).serializeArray();
        if ($('.selected').length < 1) {
            formData.push({ name: 'cusNameGuid', value: $('#cusName').typeahead('getActive').guid }, { name: 'cusAddressGuid', value: $('#cusAddress').typeahead('getActive').guid })
        }
        else {
            formData.push({ name: 'cusNameGuid', value: $('.selected').data('nameGuid') }, { name: 'cusAddressGuid', value: $('.selected').data('addressGuid') });
        }
        //console.log(formData);
        $.post(this.action, formData).done(() => {
            location.reload();
        });

    });

    $('#cusAddress').typeahead({
        minLength: 3,
        autoSelect: true,
        afterSelect: (d) => {
            $('#ticketForm :submit').prop("disabled", true);
            $('.services').empty().load(baseHref + `Home/GetServices?cusid=${$('#cusName').typeahead('getActive').guid}&locid=${d.guid}`);
            $.getJSON(baseHref + `Home/GetPlantInfo?locid=${d.guid}`, (data) => {
                Object.entries(data).forEach(([key, value]) => {
                    $(`input[name='${key}']`).val(value);
                });
                $('#ticketForm :submit').prop("disabled", false);
            });

        }
    });

    $('tbody tr').click(function () {
        $('.selected').removeClass('selected');
        $(this).addClass('selected');
        $('#ticketForm :submit').prop("disabled", true);
        $('#cusName').val(this.cells[2].innerText.trim());
        $('#cusAddress').val(this.cells[3].innerText.trim());
        $('#alarmID').val(this.cells[5].innerText.trim())
        $('.services').empty().load(baseHref + `Home/GetServices?cusid=${$(this).data("nameGuid")}&locid=${$(this).data("addressGuid")}`);
        $.getJSON('http://gis.lusfiber.net:6080/arcgis/rest/services/Networks1_Auto_Provisioning_Production/MapServer/exts/ProvisioningSOE/GetPlant?AddressPointID=' + this.cells[6].innerText.trim() + '&f=pjson', (data) => {
            Object.entries(data).forEach(([key, value]) => {
                $(`input[name='${key}']`).val(value);
            });
            $('#ticketForm :submit').prop("disabled", false);
        });
    });

    $.getJSON(baseHref + 'Home/GetCustomers', (data) => {
        $('#cusName').typeahead({
            source: data,
            minLength: 3,
            autoSelect: true,
            afterSelect: (d) => {
                $.getJSON(baseHref + `Home/GetAddresses?query=${d.guid}`, (cusData) => {
                    $('#cusAddress').typeahead('setSource', cusData);
                });
            }
        });
    });

    

});

function getChildHeights(elem) {
    return Math.ceil($(elem).children().toArray().reduce((sum,e) => {
        return sum + $(e).outerHeight(true);
    },0));
}