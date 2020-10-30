
var displayminutes;
var displayseconds;
var percent = 0;
var initializeTimer = 1.5 // enter in minutes
var minutesToSeconds = number;
var startCountDownTimer;
var rotationAngle = 360 / number;
var setTime;
$(document).ready(function () {
    var timeouthandle;

    $("#currentshuttlediv").hide();

    $(".btn-timer").html("Start")

    var refreshComponent = function () {
        var shuttleNo = $('#shuttlenumber').text();
        var speedlevel = $("#shuttlespeedlevel").text()
        window.clearInterval(timeouthandle);
        $.get("/Home/GetStopWatchViewModel", { shuttleNumber: shuttleNo, speedLevel: speedlevel }, function (data) {
           
            if (data.isFinalShuttle == false) {
                timeouthandle = window.setInterval(refreshComponent, data.levelTime * 1000)
            }
            else {
                window.clearInterval(timeouthandle);
                window.clearInterval(startCountDownTimer);
            }
            currentTimeLevel += data.levelTime;

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
                                option = '<option value="' + item.id + '" selected>' + item.currentShuttle.shuttleNo + "-" + item.currentShuttle.speedLevel + "</option>"
                            }
                            else {
                                option = '<option value="' + item.id + '">' + shuttle.shuttleNo + "-" + shuttle.speedLevel + "</option>"
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

    });

    var setProgress = function (percent) {
        var progressValue = Math.round((percent * 100) / number)
        $('.progress-bar').css('width', progressValue + '%').attr('aria-valuenow', progressValue).html(progressValue + '%');
    }

    var animate1 = function () {

        var  percent = 0;
        (function animate() {
            setProgress((percent += 1));
            if (percent < number)
                setTimeout(animate, 1000);
            else {
                $(".btn-timer").html("Start");
                $('.progress-bar').css('width', 0 + '%').attr('aria-valuenow', 0).html(0 + '%');
            }
        })();
    }

    $(".btn-timer").click(function () {

        $("#playbuttondiv").hide();
        $("#currentshuttlediv").show();
        setTime = getTime();
        $(".btn-timer").html(Math.round(setTime[0]) + ":" + Math.round(setTime[1]))
        animate1();
        refreshComponent();
        startCountDownTimer  = setInterval(function () {

            minutesToSeconds = minutesToSeconds - 1;
            var timer = getTime();
            $(".btn-timer").html(Math.round(timer[0]) + ":" + Math.round(timer[1]));
            if (minutesToSeconds == 0) {
                clearInterval(startCountDownTimer);
                console.log("completed");
            }
        }, 1000)
    });
    function getTime() {

        displayminutes = Math.floor(minutesToSeconds / 60);
        displayseconds = minutesToSeconds - (displayminutes * 60);
        if (displayseconds < 10) {
            displayseconds = "0" + displayseconds;
        }
        if (displayminutes < 10) {
            displayminutes = "0" + displayminutes;
        }

        return [displayminutes, displayseconds];


    }
});


