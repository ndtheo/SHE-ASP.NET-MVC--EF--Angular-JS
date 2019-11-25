var inputsArray = [];
$(document).ready(function () {
    $("#pdfReport").click(function (event) {
        //GenerateWordDocument();
        CreatePDF();
    });
    ImagefromCanvas();
});
/*
    function PopulateInputArray() {
        inputsArray = [];
        $("#Fields :input").not(':button,:hidden').each(function () {
            var tmp1 = $("label[for='" + this.id + "']").text();
            if (!$("label[for='" + this.id + "']").length) {
                tmp1 = $(this).parent().prev().text();
                tmp1 = $.trim(tmp1.replace(/[\t\n]+/g, ' '));
            }
            if ($(this).is('select')) {
                var tmp2 = $('#'+$(this).attr('id') + " option:selected").text();
            } else {
                var tmp2 = $(this).val();
            }

            inputsArray.push([tmp1, tmp2]);
        });
        inputsArray.splice(0, 0, "GENERAL INFORMATION");
        if ((FindInInputArray("Body Type:") + 1) > 0)
            inputsArray.splice(FindInInputArray("Body Type:") + 1, 0, "DOCUMENTS LIST");
        if ((FindInInputArray("Tyre Repair Kit:") + 1) > 0)
            inputsArray.splice(FindInInputArray("Tyre Repair Kit:") + 1, 0, "EXTERIOR");
        if ((FindInInputArray("EXTERIOR") + 1) > 0)
            inputsArray.splice(FindInInputArray("EXTERIOR") + 1, 0, "INTERIOR");
        if ((FindInInputArray("Smoke or other smells:") + 1) > 0)
            inputsArray.splice(FindInInputArray("Smoke or other smells:") + 1, 0, "TYRES");

    }*/
function FindInInputArray(value) {
    for (i = 0; i < inputsArray.length; i++) {
        if (!$j.isArray(inputsArray[i])) {
            if (inputsArray[i] == value)
                return i;
        }
        if (inputsArray[i][0] == value || inputsArray[i][1] == value)
            return i;
    }
    return -1;
}
function ImagefromCanvas() {
    var canvas = document.getElementById('Canvas'),
        dataUrl = canvas.toDataURL(),
        imageFoo = document.createElement('img');
    imageFoo.src = dataUrl;

    // Style your image here
    //imageFoo.style.width = '100px';
    //imageFoo.style.height = '100px';
    $("#tempImageDiv").append(imageFoo);
    return imageFoo;
}
function GetCanvasDataUrl() {
    var canvas = document.getElementById('Canvas');
    return canvas.toDataURL();
}
function ClearTempImg() {
    $("#tempImageDiv").empty();
}

function GenerateWordDocument() {
    var imageFoo = ImagefromCanvas();
    $(imageFoo).load(function () {
        //$("#Fields").wordExport("VehicleReport");
        //ClearTempImg
    });
}
/*
function CreatePDF() {
    //  PopulateInputArray();
    var doc = new jsPDF();
    var splitTitle = "";
    var splitText = "";
    //doc.setFontSize(16);
    //doc.setFont("times");
    //var text = "GENERAL INFORMATION";
    //doc.text(text, GetjsPDFCenteredXOfText(doc, text), 10);
    doc.setFontSize(11);
    var y = 10;
    var x = 10;
    var addToNextLine = 0;
    for (i = 0; i < inputsArray.length; i++) {
        //if Title
        if (!$j.isArray(inputsArray[i])) {
            y += 20;
            doc.setFontSize(16);
            doc.setFontType("bold");
            var text = inputsArray[i];
            doc.text(text, GetjsPDFCenteredXOfText(doc, text), y);
            x = 10;
            y += 10;
            doc.setFontSize(11);
            doc.setFontType("normal");
            if (text == 'EXTERIOR') {
                imgwidth = 150;
                imgHeight = 75;
                if ((y + imgHeight) >= doc.internal.pageSize.height) {
                    doc.addPage();
                    var y = 10;
                }
                doc.addImage(GetCanvasDataUrl(), 'JPEG', (doc.internal.pageSize.width - imgwidth) / 2, y, imgwidth, imgHeight);
                y += imgHeight;
            }
        } else {
            //lbl
            splitTitle = doc.splitTextToSize(inputsArray[i][0], 30);
            //value
            splitText = doc.splitTextToSize(inputsArray[i][1], doc.internal.pageSize.width / 2);
            //if big text, go to page start
            if (splitText.length > 1) {
                if (x != 10) {
                    y += 10;
                    x = 10;
                }
            }
            var c = Math.max(splitTitle.length, splitText.length);
            if ((y + (doc.internal.getFontSize() * c) / 2 + 20) > doc.internal.pageSize.height) {
                doc.addPage();
                var y = 10;
            }
            doc.setFontType("bold");
            doc.text(x, y, splitTitle);
            doc.setFontType("normal");
            doc.text(x + 35, y, splitText);

            if (splitText.length > 1) {
                y += (c * doc.internal.getFontSize()) / 2;
                x = 10;
            } else {
                if (splitTitle.length > 1) {
                    addToNextLine = (c * doc.internal.getFontSize()) / 4;
                }
                if (x == 10) {
                    x = 105;
                } else {
                    x = 10;
                    y += 10 + addToNextLine;
                    addToNextLine = 0;
                }
            }
        }
        if ((y + 20) > doc.internal.pageSize.height) {
            doc.addPage();
            var y = 10;
        }
    }
    //footer
    if ((y + 20) >= doc.internal.pageSize.height) {
        doc.addPage();
    }
    doc.setFontType("italic");
    var idtext = '';
    if ($('#HiddenEntityID').val() > 0) {
        idtext = "Vehicle inspection number: " + $('#HiddenEntityID').val();
    } else {
        doc.setTextColor(255, 0, 0); //set font color to red
        idtext = "Inspection has not been submitted yet.";
    }
    doc.text(idtext, GetjsPDFCenteredXOfText(doc, idtext), doc.internal.pageSize.height - 20);
    doc.setTextColor(0, 0, 0);
    var currentdate = new Date();
    var datetime = 'Report issued on ' + currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + "  "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    doc.text(datetime, GetjsPDFCenteredXOfText(doc, datetime), doc.internal.pageSize.height - 10);
    doc.save('vehicleReport.pdf');
}

function jsPDFShouldTextGoToNewLine(doc, x, text) {
    var fontSize = doc.internal.getFontSize();
    var pageWidth = doc.internal.pageSize.width;
    txtWidth = doc.getStringUnitWidth(text) * fontSize / doc.internal.scaleFactor;
    if ((x + txtWidth) >= pageWidth)
        return true;
    else {
        return false;
    }
}

function GetjsPDFCenteredXOfText(doc, text) {
    var fontSize = doc.internal.getFontSize();
    var pageWidth = doc.internal.pageSize.width;
    var txtWidth = doc.getStringUnitWidth(text) * fontSize / doc.internal.scaleFactor;
    return (pageWidth - txtWidth) / 2;
}

*/
///Canvas


function RemoveLastMarker() {
    Markers.splice(Markers.length - 1, 1);
}


var canvas = document.getElementById('Canvas');
var context = canvas.getContext("2d");


// Map sprite
var mapSprite = new Image();
mapSprite.src = '\\Content\\Images\\reportCar.png';

var Marker = function () {
    this.Sprite = new Image();
    this.Sprite.src = '\\Content\\Images\\x.png';
    this.Width = 20;
    this.Height = 20;
    this.XPos = 0;
    this.YPos = 0;
}

var Markers = new Array();
//fill array with existing markers
if ($("#ExteriorImageData").length) {
    var existingMarks = $j.parseJSON($("#ExteriorImageData").val());

    $(existingMarks).each(function (index, listItem) {

        var marker = new Marker();
        marker.XPos = this.XPos;
        marker.YPos = this.YPos;
        console.log(marker);
        Markers.push(marker);
    });

}

var mouseClicked = function (mouse) {
    if (mouse.which == 1) { //left click
        // Get corrent mouse coords
        var rect = canvas.getBoundingClientRect();
        var mouseXPos = (mouse.x - rect.left);
        var mouseYPos = (mouse.y - rect.top);

        console.log("Marker added");

        // Move the marker when placed to a better location
        var marker = new Marker();
        marker.XPos = mouseXPos - (marker.Width / 2);
        marker.YPos = mouseYPos - (marker.Height / 2);

        Markers.push(marker);
    }
}

// Add mouse click event listener to canvas
canvas.addEventListener("mousedown", mouseClicked, false);

var firstLoad = function () {
    context.font = "15px Georgia";
    context.textAlign = "center";
}

firstLoad();

var main = function () {
    draw();
};

var draw = function () {
    // Clear Canvas
    context.fillStyle = "#000";
    context.fillRect(0, 0, canvas.width, canvas.height);

    // Draw map
    // Sprite, X location, Y location, Image width, Image height
    // You can leave the image height and width off, if you do it will draw the image at default size
    context.drawImage(mapSprite, 0, 0, 453, 277);

    // Draw markers

    for (var i = 0; i < Markers.length; i++) {
        var tempMarker = Markers[i];
        // Draw marker
        context.drawImage(tempMarker.Sprite, tempMarker.XPos, tempMarker.YPos, tempMarker.Width, tempMarker.Height);

        // Calculate postion text
        //var markerText = "Postion (X:" + tempMarker.XPos + ", Y:" + tempMarker.YPos;

        // Draw a simple box so you can see the position
        //var textMeasurements = context.measureText(markerText);
        //context.fillStyle = "#666";
        //context.globalAlpha = 0.7;
        //context.fillRect(tempMarker.XPos - (textMeasurements.width / 2), tempMarker.YPos - 15, textMeasurements.width, 20);
        //context.globalAlpha = 1;

        // Draw position above
        //context.fillStyle = "#000";
        //context.fillText(markerText, tempMarker.XPos, tempMarker.YPos);
    }
    //$("#marks").data("markers", Markers);
    $("#ExteriorImageData").val(JSON.stringify(Markers));
};

setInterval(main, (1000 / 60)); // Refresh 60 times a second