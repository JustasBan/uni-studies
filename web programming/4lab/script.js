let source

let validity = {
    title: false,
    incidents: false,
    warranty: false
}

function validateInputs() {
    validateTitle()
    if (validity.title) {
        validateIncidents()
        if (validity.incidents) {
            validateWarranty()
        }
    }
}

//1.1 Įvedimo laukas, kuriame kažkas turi būti įvesta (kuris negali būti paliktas tuščias)
function validateTitle() {
    if (document.getElementById("titleNewComponent").value.length === 0) {
        alert("Empty title")
    }
    else {
        validity.title = true
        $("#incidentsNewComponent").css({
            //2 HTML puslapio elementų paslėpimas/parodymas (ne išmetimas) panaudojant CSS savybę display
            display: "inline-block"
        })
    }
}

//1.2 Įvedimo laukas, kuriame turi būti įvestas sveikas teigiamas skaičius
function validateIncidents() {
    let thing = document.getElementById("incidentsNewComponent").value
    if (Number.isInteger(+thing) === false || thing < 0) {
        alert("not valid incidents")
    }
    else {
        validity.incidents = true
        $("#warrantyNewComponent").css({
            //2 HTML puslapio elementų paslėpimas/parodymas (ne išmetimas) panaudojant CSS savybę display
            display: "inline-block"
        })
    }
}

//1.3 Įvedimo laukas (-ai), kuriame (-iuose) turi būti įvesta teisinga data (metai, mėnuo, diena) (pvz. vasaris negali turėti 30 dienų); būtina naudoti Date objektą
function validateWarranty() {
    let thing = document.getElementById("warrantyNewComponent").value
    let thingy = Date.parse(thing)
    let reg = /^\d{4}-\d{2}-\d{2}$/

    if (isNaN(thingy) || !thing.match(reg)) {
        alert("not valid warranty")
    }
    else {
        validity.warranty = true
    }
}

$(document).ready(function () {
    //2 HTML puslapio elementų paslėpimas/parodymas (ne išmetimas) panaudojant CSS savybę display
    $("#incidentsNewComponent").css({
        display: "none"
    })
    $("#warrantyNewComponent").css({
        display: "none"
    })
})

function manipuliuoti() {
    //3.1 Egzistuojančių HTML dokumento žymių tekstinio turinio pakeitimas
    $(".textPart").text("Kitoks tekstas");

    //3.2 Egzistuojančių žymių stiliaus pakeitimas (atributui style priskiriant naują reikšmę)
    $("#rectanglePart").attr("style", "background-color: blue; width: 25px; height: 25px;")
}

function manipuliuoti2() {
    let element = $("#deleteTag").val()
    let string = `.textPart:nth-child(${element})`

    //3.3 Egzistuojančių žymių išmetimas (pvz. ištrinti įvedimo lauke numeriu nurodytą paragrafą)
    $(string).remove()
    $(`#deleteTag > option`).remove()

    let allElems = document.getElementsByClassName("textPart")
    for (let index = 0; index < allElems.length; index++) {

        //3.4 Naujų žymių įterpimas
        $("#deleteTag").append(`<option value='${index + 1}'>${index + 1}</option>`)
    }
}

function manipuliuoti3() {
    let allElems = document.getElementsByClassName("textPart")
    let string = $("#tekstoPridejimas").val()

    //3.4 Naujų žymių įterpimas
    $("#texts").append(`<p class="textPart">${string}</p>`)
    $("#deleteTag").append(`<option value="${allElems.length}">${allElems.length}</option>`)
}

//4.2 Duomenų (JSON formatu) išsitraukimas
function serverisGET() {
    let received = []
    $.ajax({
        type: 'GET',
        url: source,
        headers: { 'Access-Control-Allow-Origin': '*' },
        success: function (data) {
            received = data
            console.log(data);

            //4.3 Gautų duomenų atvaizdavimas HTML puslapio lentelėje
            $("#serveris > table tr:not(:first-child)").remove()
            let row = $(`<tr><td>${received.title}</td><td>${received.incidents}</td><td>${received.warranty}</td></tr>`)
            row.appendTo("#serveris > table")

        },
        error: function (data) {
            console.log("e");
            console.log(data);
        }
    })
}

//4.1 Įvedimo formoje pateiktų duomenų serializavimas JSON formatu ir patalpinimas
function serverisPOST() {
    let obj = {
        title: $('#titleNewComponent').val(),
        incidents: $('#incidentsNewComponent').val(),
        warranty: new Date($('#warrantyNewComponent').val())
    }
    validateInputs()
    if (validity.warranty && validity.incidents && validity.title) {
        $.ajax({
            type: 'POST',
            url: 'https://jsonblob.com/api/jsonBlob',
            data: JSON.stringify(obj),
            contentType: 'application/json',
            Accept: ' application/json',
            headers: { 'Access-Control-Allow-Origin': '*' },
            success: function (data, status, xhr) {
                source = xhr.getResponseHeader('location').replace("http", "https")
                console.log(source);
            },
            error: function (data) {
                console.log("e");
                console.log(data);
            }
        })
    }
}