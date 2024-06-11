
$(function () {
    $("#btnContainer").dxButton({
        text: "Click me!",
        type: "success",
        stylingMode: "contained",
        icon: "comment",
        width: 150,
        onClick: function () {
            DevExpress.ui.notify("The button was clicked");
        }
    });

    // Create and configure a second button widget
    $("#buttonContainer2").dxButton({
        text: "Another Button",
        onClick: function () {
            alert("Another button clicked!");
        }
    });

    // Get the button instance
    var buttonInstance = $("#getOptionButton").dxButton({
        text: "Get Button Text",
    }).dxButton("instance");

    $('#setOptionButton').dxButton({
        text: "Get Option",
    });

    $('#destroyButton').dxButton({
        text: "Destroy Button",
    });

    // Get and set options
    $("#getOptionButton").on("click", function () {
        var buttonText = buttonInstance.option("text");
        alert("Button text: " + buttonText);
    });

    $("#setOptionButton").on("click", function () {
        buttonInstance.option("text", "New Text");
        alert("Button text has been changed");
    });

    // Call methods

    // Here we use the repaint method to re-render the button if needed
    $("#repaintButton").on("click", function () {
        buttonInstance.repaint();
    });

    // Handle events
    buttonInstance.on("optionChanged", function (e) {
        if (e.name === "text") {
            console.log("Button text changed to: " + e.value);
        }
    });

    // Destroy the button
    $("#destroyButton").on("click", function () {
        buttonInstance.dispose();
        alert("Button destroyed");
    });

});