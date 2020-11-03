
var displayminutes;
var displayseconds;
var percent = 0;
var initializeTimer = 1.5 // enter in minutes
var minutesToSeconds = totalTimeForTest;
var startCountDownTimer;
var rotationAngle = 360 / totalTimeForTest;
var setTime;

var firstTimeInitilize = true;

$(document).ready(function () {
    var timeouthandle;
    var nextShuttleTimer;
    $("#currentshuttlediv").hide();

    $("#nextShuttle").html("0 - 0")

    $(".btn-timer").html("Start")

    var refreshShuttle = function () {
        var shuttleNo = $('#shuttlenumber').text();
        var speedlevel = $("#shuttlespeedlevel").text()
        window.clearInterval(timeouthandle);
        window.clearInterval(nextShuttleTimer);
        $.get("/Home/GetStopWatchViewModel", { shuttleNumber: shuttleNo, speedLevel: speedlevel }, function (data) {
           
            if (data.isFinalShuttle == false) {
                timeouthandle = window.setInterval(refreshShuttle, data.levelTime * 1000)
            }
            else {
                window.clearInterval(timeouthandle);
             
                window.clearInterval(nextShuttleTimer);
            }
            currentTimeLevel += data.levelTime;

            $("#shuttlenumber").html(data.shuttleNo);
            $("#shuttlespeedlevel").html(data.speedLevel)
            $("#totalDistance").html(data.totalDistance)
            $("#totalTime").html(data.totalTime)

            var nextshuttledisplaytime = getTime(data.levelTime);

            $("#nextShuttle").html(Math.round(nextshuttledisplaytime[0]) + ":" + Math.round(nextshuttledisplaytime[1]))
            var nextshuttletime = data.levelTime;
            nextShuttleTimer = setInterval(function () {
                var min, sec;
                nextshuttletime = nextshuttletime - 1;
                $("#nextShuttle").html(data.levelTime)
                var timer = getTime(nextshuttletime);

                if (!Number.isNaN(Math.round(timer[0])))
                    min = Math.round(timer[0])
                if (!Number.isNaN(Math.round(timer[1]))) {
                    sec = Math.round(timer[1])
                }
                $("#nextShuttle").html(min + ":" + sec)
            }, 1000)
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

                            "<td><input  type='button' class='btn btn-outline-primary' style='width: 80px; height: 25px;' value='Warn' data-id='" + item.id + "' data-value='warn' /></td>" +
                            "<td><input  type='button'  class='btn btn-outline-danger' style='width: 80px; height: 25px;' value='Stop' data-id='" + item.id + "'  data-value='stop'  /></td>" +
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

                if (firstTimeInitilize) {

                    $('.btn-outline-primary').prop('disabled', true);
                    $('.btn-outline-danger').prop('disabled', true);
                }
            });
    };
    

    var setProgress = function (percent) {
        var progressValue = Math.round((percent * 100) / totalTimeForTest)
        $('.progress-bar').css('width', progressValue + '%').attr('aria-valuenow', progressValue).html(progressValue + '%');
    }

    var startTimerAndProgressBar = function () {

        var  percent = 0;
        (function animate() {
            setProgress((percent += 1));
            if (percent < totalTimeForTest)
                setTimeout(animate, 1000);
            else {
                resetUI();

            }
        })();
    }

    var resetUI = function () {
        $(".btn-timer").html("Start");
        $('.progress-bar').css('width', 0 + '%').attr('aria-valuenow', 0).html(0 + '%');
        $("#shuttlenumber").html('');
        $("#shuttlespeedlevel").html('')
        $("#totalDistance").html('')
        $("#totalTime").html('')
        $("#nextShuttle").html('')
        $("#speed").html('')
        $("#nextShuttle").html('')
        window.clearInterval(timeouthandle);
        window.clearInterval(nextShuttleTimer);
        firstTimeInitilize = true;

        $.post("/Home/SaveTestResult", {}, function (data) {
            $("#btnstopwatchstart").prop('disabled', false);
            getAthletes(0, "start")
            minutesToSeconds = totalTimeForTest;
        });
    }

    $(".btn-timer").click(function () {

        $("#playbuttondiv").hide();
        $("#currentshuttlediv").show();
        $("#btnstopwatchstart").prop('disabled', true);
        $('.btn-outline-primary').prop('disabled', false);
        $('.btn-outline-danger').prop('disabled', false);
        firstTimeInitilize = false;
        setTime = getTime(minutesToSeconds);
        $(".btn-timer").html(Math.round(setTime[0]) + ":" + Math.round(setTime[1]))
        startTimerAndProgressBar();
        refreshShuttle();
        startCountDownTimer = setInterval(function () {
            var min=0, sec=0;
            minutesToSeconds = minutesToSeconds - 1;
            var timer = getTime(minutesToSeconds);
            if (!Number.isNaN(Math.round(timer[0])))
                min = Math.round(timer[0])
            if (!Number.isNaN(Math.round(timer[1]))) {
                sec = Math.round(timer[1])
            }
            $(".btn-timer").html(min + ":" + sec);
            if (minutesToSeconds == 0) {
                clearInterval(startCountDownTimer);
                console.log("completed");
            }
        }, 1000)
    });

    getAthletes(0, "start");

    $('.btn-outline-primary').prop('disabled', true);

    $('.btn-outline-danger').prop('disabled', true);

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

    function getTime(mintosecond) {

        displayminutes = Math.floor(mintosecond / 60);
        displayseconds = mintosecond - (displayminutes * 60);
        if (displayseconds < 10) {
            displayseconds = "0" + displayseconds;
        }
        if (displayminutes < 10) {
            displayminutes = "0" + displayminutes;
        }

        return [displayminutes, displayseconds];


    }
});


