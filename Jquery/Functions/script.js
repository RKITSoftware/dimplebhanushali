$(document).ready(function () {
    // Array Manipulation
    $("#doubleArray").click(function () {
        var arr = [1, 2, 3, 4, 5];
        var doubled = $.map(arr, function (item) {
            return item * 2;
        });

        $("#doubledResult").text("Doubled Array: " + doubled.join(", "));
    });

    $("#filterEven").click(function () {
        var arr = [1, 2, 3, 4, 5];
        var evenNumbers = $.grep(arr, function (item) {
            return item % 2 === 0;
        });
        $("#evenNumbersResult").text("Even Numbers: " + evenNumbers.join(", "));
    });

    $("#iterateArray").click(function () {
        var arr = [1, 2, 3, 11, 4, 5];
        var result = "";
        $.each(arr, function (index, value) {
            result += "Index: " + index + ", Value: " + value + "<br>";
        });
        $("#iterateResult").html(result);
    });

    // Deferred Objects
    const deferred = $.Deferred();

    $("#deferredSuccess").click(function () {
        deferred.resolve("Operation completed successfully");
    });

    $("#deferredFail").click(function () {
        deferred.reject("Operation failed");
    });

    deferred.promise()
        .done(function (result) {
            $("#deferredResult").text("Success: " + result).removeClass("text-danger").addClass("text-success");
        })
        .fail(function (error) {
            $("#deferredResult").text("Error: " + error).removeClass("text-success").addClass("text-danger");
            // Print "Operation failed" explicitly
            console.error("Operation failed");
        });
});