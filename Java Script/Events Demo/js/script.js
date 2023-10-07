// // Variable Declaration
// const canvas = document.getElementById("canvas");
// const context = canvas.getContext("2d");
// const colorPicker = document.getElementById("colorPicker");
// const lineWidth = document.getElementById("lineWidth");
// const undoButton = document.getElementById("undoButton");
// const redoButton = document.getElementById("redoButton");
// const saveButton = document.getElementById("saveButton");
// const newFileButton = document.getElementById("newFileButton");

// // Drawing lines in Canvas
// let painting = false;
// let paths = []; // Store drawn paths
// let undonePaths = []; // Store undone paths

// // Height and Width of canvas
// canvas.width = window.innerWidth;
// canvas.height = window.innerHeight;

// // Context area
// context.lineWidth = lineWidth.value;
// context.lineCap = "round";
// context.strokeStyle = colorPicker.value;

// // Change Color in app
// colorPicker.addEventListener("input", (e) => {
//     context.strokeStyle = e.target.value;
// });

// // change size of pointer
// lineWidth.addEventListener("input", (e) => {
//     context.lineWidth = e.target.value;
// });

// // Drawing lines
// function draw(e) {
//     if (!painting) return;

//     context.lineTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
//     context.stroke();
// }

// // Draw with mouse
// canvas.addEventListener("mousedown mouseup", () => {
//     if (painting == true) {
//         trial()
//     } else {
//         context.closePath();
//     }
// })

// canvas.addMultipleListeners("mousedown", "mouseup", () => {
//     trial()
// })

// function trial() {
//     context.beginPath();
//     context.moveTo(event.clientX - canvas.offsetLeft, event.clientY - canvas.offsetTop);
//     paths.push([]);
// }

// canvas.addEventListener("mousedown", () => {
//     painting = true;
//     context.beginPath();
//     context.moveTo(event.clientX - canvas.offsetLeft, event.clientY - canvas.offsetTop);
//     paths.push([]);
// });

// // Draw with mouse
// canvas.addEventListener("mouseup", () => {
//     painting = false;
//     context.closePath();
// });

// // Draw with mouse
// canvas.addEventListener("mousemove", (event) => {
//     if (!painting) return;

//     context.lineTo(event.clientX - canvas.offsetLeft, event.clientY - canvas.offsetTop);
//     context.stroke();
//     paths[paths.length - 1].push({
//         x: event.clientX - canvas.offsetLeft,
//         y: event.clientY - canvas.offsetTop,
//     });
// });

// canvas.addEventListener("mouseleave", () => {
//     painting = false;
//     context.closePath();
// });

// // undo button behaviour
// undoButton.addEventListener("click", undo);

// function undo() {
//     if (paths.length > 0) {
//         const pathToUndo = paths.pop();
//         undonePaths.push(pathToUndo);
//         clearCanvas();
//         redrawPaths();
//     }
// }

// // redo button behaviour
// redoButton.addEventListener("click", redo);

// function redo() {
//     if (undonePaths.length > 0) {
//         const pathToRedo = undonePaths.pop();
//         paths.push(pathToRedo);
//         clearCanvas();
//         redrawPaths();
//     }
// }

// // clear drawing area
// function clearCanvas() {
//     context.clearRect(0, 0, canvas.width, canvas.height);
// }

// // redraw paths
// function redrawPaths() {
//     context.beginPath();
//     context.strokeStyle = colorPicker.value;
//     context.lineWidth = lineWidth.value;

//     paths.forEach((path) => {
//         context.moveTo(path[0].x, path[0].y);

//         path.forEach((point) => {
//             context.lineTo(point.x, point.y);
//             context.stroke();
//         });
//     });
// }

// // save button behaviour
// saveButton.addEventListener("click", saveCanvas);

// function saveCanvas() {
//     const dataURL = canvas.toDataURL("image/png");
//     const a = document.createElement("a");
//     a.href = dataURL;
//     a.download = "drawing.png";
//     a.click();
// }

// // new file button behaviour
// newFileButton.addEventListener("click", () => {
//     clearCanvas();
//     paths = [];
//     undonePaths = [];
// });


// Get the canvas and context
const canvas = document.getElementById("canvas");
const context = canvas.getContext("2d");

// Store drawing paths and undone paths for undo/redo
let paths = [];
let undonePaths = [];

// Set initial stroke style and line width
context.strokeStyle = "#000000";
context.lineWidth = 2;

// Variable to track if the mouse is being clicked for drawing
let isDrawing = false;

// Function to handle mouse down event
canvas.addEventListener("mousedown", (event) => {
    startDrawing(event.clientX, event.clientY);
});

// Function to handle touch start event for mobile devices
canvas.addEventListener("touchstart", (event) => {
    const touch = event.touches[0];
    startDrawing(touch.clientX, touch.clientY);
});

// Function to start drawing a path
function startDrawing(x, y) {
    context.beginPath();
    context.moveTo(x - canvas.getBoundingClientRect().left, y - canvas.getBoundingClientRect().top);
    isDrawing = true;

    // Store the initial path point for undo/redo
    paths.push(context.getImageData(0, 0, canvas.width, canvas.height));
    undonePaths.length = 0;
}

// Function to handle mouse move event
canvas.addEventListener("mousemove", (event) => {
    draw(event.clientX, event.clientY);
});

// Function to handle touch move event for mobile devices
canvas.addEventListener("touchmove", (event) => {
    const touch = event.touches[0];
    draw(touch.clientX, touch.clientY);
});

// Function to draw a line
function draw(x, y) {
    if (!isDrawing) return;

    context.lineTo(x - canvas.getBoundingClientRect().left, y - canvas.getBoundingClientRect().top);
    context.stroke();
}

// Function to handle mouse up event
canvas.addEventListener("mouseup", stopDrawing);

// Function to handle touch end event for mobile devices
canvas.addEventListener("touchend", stopDrawing);

// Function to handle mouse leave event
canvas.addEventListener("mouseleave", stopDrawing);

// Function to stop drawing
function stopDrawing() {
    if (isDrawing) {
        isDrawing = false;
        // Store the updated path for undo/redo
        paths.push(context.getImageData(0, 0, canvas.width, canvas.height));
        undonePaths.length = 0;
    }
}

// Function to undo the last drawing action
const undoButton = document.getElementById("undoButton");
undoButton.addEventListener("click", undo);

function undo() {
    if (paths.length > 1) {
        undonePaths.push(paths.pop());
        context.putImageData(paths[paths.length - 1], 0, 0);
    }
}

// Function to redo the last undone action
const redoButton = document.getElementById("redoButton");
redoButton.addEventListener("click", redo);

function redo() {
    if (undonePaths.length > 0) {
        context.putImageData(undonePaths[undonePaths.length - 1], 0, 0);
        paths.push(undonePaths.pop());
    }
}

// Function to clear the canvas
const clearButton = document.getElementById("clearButton");
clearButton.addEventListener("click", clearCanvas);

function clearCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    paths = [];
    undonePaths = [];
}

// Function to change the line color
const colorPicker = document.getElementById("colorPicker");
colorPicker.addEventListener("input", changeColor);

function changeColor(event) {
    context.strokeStyle = event.target.value;
}

// Function to change the line width
const lineWidth = document.getElementById("lineWidth");
lineWidth.addEventListener("input", changeLineWidth);

function changeLineWidth(event) {
    context.lineWidth = event.target.value;
}

// Function to handle the "Open File" button
const openFileButton = document.getElementById("openFileButton");
const fileInput = document.createElement("input");
fileInput.type = "file";
fileInput.style.display = "none"; // Hide the file input element

openFileButton.addEventListener("click", () => {
    fileInput.click();
});

const saveButton = document.getElementById("saveButton");

saveButton.addEventListener("click", () => {
    // Create an anchor element to trigger the download
    const downloadLink = document.createElement("a");
    downloadLink.href = canvas.toDataURL("image/png"); // Convert canvas content to a data URL (PNG format)
    downloadLink.download = "my_drawing.png"; // Set the file name for the download

    // Trigger the download
    downloadLink.click();
});

// Listen for changes in the file input
fileInput.addEventListener("change", (event) => {
    const selectedFile = event.target.files[0];

    if (selectedFile) {
        // Load the selected file onto the canvas
        const fileReader = new FileReader();
        fileReader.onload = function (e) {
            const img = new Image();
            img.onload = function () {
                context.drawImage(img, 0, 0);
            };
            img.src = e.target.result;
        };
        fileReader.readAsDataURL(selectedFile);
    }
});
