// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var timeouthandle;
    $("#currentshuttlediv").hide();
    var refreshComponent = function () {
        var shuttleNo = $('#shuttlenumber').text();
        var speedlevel = $("#shuttlespeedlevel").text()
        window.clearInterval(timeouthandle);
        $.get("/Home/GetStopWatchViewModel", { shuttleNumber: shuttleNo, speedLevel: speedlevel }, function (data) {

            timeouthandle = window.setInterval(refreshComponent, data.levelTime * 1000)
            $("#shuttlenumber").html(data.shuttleNo);
            $("#shuttlespeedlevel").html(data.speedLevel)
            $("#totalDistance").html(data.totalDistance)
            $("#totalTime").html(data.totalTime)
            $("#nextShuttle").html(data.nextShuttle)
            $("#speed").html(data.speed)

        });
    };

    var getAthletes = function (athleteId, testresult) {
        var shuttleNo = $("#shuttlenumber").text()
        var speedlevel = $("#shuttlespeedlevel").text()

        $.get("/Home/GetUpdatedAthleteViewModel",
            {
                athleteId: athleteId,
                testresult: testresult,
                shuttlenumber: shuttleNo,
                shuttlespeedlevel: speedlevel
            },
            function (data) {

                $("#tblathlete tbody tr").remove();
                var items = '';

                var body = "<tbody> </tbody>";
                $('#tblathlete').append(body);

                $.each(data.athletes, function (i, item) {
                    var row = '';

                    if (item.shuttleState == 'Start') {

                        row = "<tr>" +
                            "<td>" + item.name + "</td>" +

                            "<td><input type='button' class='btn btn-outline-primary' style='width: 80px; height: 25px;' value='Warn' data-id='" + item.id + "' data-value='warn' /></td>" +
                            "<td><input type='button'  class='btn btn-outline-danger' style='width: 80px; height: 25px;' value='Stop' data-id='" + item.id + "'  data-value='stop'  /></td>" +
                            "</tr>";
                    }
                    else if (item.shuttleState == 'warn') {
                        row = "<tr>" +
                            "<td>" + item.name + "</td>" +
                            "<td><input type='button' class='btn btn-outline-danger' style='width: 80px; height: 25px;' value='Stop' data-id='" + item.id + "'  data-value='stop'  /></td>" +
                            "</tr>";
                    }
                    else if (item.shuttleState == 'stop') {
                        var s = '<select id="athleteShuttleResult">';
                        $.each(item.finishedShuttles, function (j, shuttle) {
                            var option = '';
                            if (item.currentShuttle.shuttleNo == shuttle.shuttleNo
                                && item.currentShuttle.speedLevel == shuttle.speedLevel) {
                                option = '<option value="' + item.id +'" selected>' + item.currentShuttle.shuttleNo + "-" + item.currentShuttle.speedLevel + "</option>"
                            }
                            else {
                                option = '<option value="' + item.id +'">' + shuttle.shuttleNo + "-" + shuttle.speedLevel + "</option>"
                            }

                            s = s + (option);

                        });
                        s = s + "</select>";
                        row = "<tr>" +
                            "<td>" + item.name + "</td>" +
                            "<td>" + s + "</td>" +
                            "</tr>";
                    }

                    $('#tblathlete').append(row);
                });

              
            });
    };
    $(document).on('change', "#athleteShuttleResult", function () {
        var selectedshuttle = $('option:selected', this).text();
        var shuttleNo = selectedshuttle.split('-')[0];
        var speedlevel = selectedshuttle.split('-')[1];
        var athleteid = $(this).val();
        $.post("/Home/UpdateAthleteResult",
            {
                athleteId: athleteid,
                testresult: "stop",
                shuttlenumber: shuttleNo,
                shuttlespeedlevel: speedlevel
            },
            function (data) {

            });
    });


    $(document).on("click", "tr input[value=Warn],input[value=Stop]", function () {
        var dataID = $(this).attr("data-id");
        var datavval = $(this).attr("data-value");
        getAthletes(dataID, datavval);
    });

    getAthletes(0, "start");

    $("#btnstopwatchstart").click(function () {
        $("#playbuttondiv").hide();
        $("#currentshuttlediv").show();
        refreshComponent();
    });

});