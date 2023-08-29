// Variable Declaration
const canvas = document.getElementById("canvas");
const context = canvas.getContext("2d");
const colorPicker = document.getElementById("colorPicker");
const lineWidth = document.getElementById("lineWidth");
const undoButton = document.getElementById("undoButton");
const redoButton = document.getElementById("redoButton");
const saveButton = document.getElementById("saveButton");
const newFileButton = document.getElementById("newFileButton");

// Drawing lines in Canvas
let painting = false;
let paths = []; // Store drawn paths
let undonePaths = []; // Store undone paths

// Height and Width of canvas
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

// Context area
context.lineWidth = lineWidth.value;
context.lineCap = "round";
context.strokeStyle = colorPicker.value;

// Change Color in app
colorPicker.addEventListener("input", (e) => {
    context.strokeStyle = e.target.value;
});

// change size of pointer
lineWidth.addEventListener("input", (e) => {
    context.lineWidth = e.target.value;
});

// Drawing lines
function draw(e) {
    if (!painting) return;

    context.lineTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
    context.stroke();
}

// Draw with mouse
canvas.addEventListener("mousedown", () => {
    painting = true;
    context.beginPath();
    context.moveTo(event.clientX - canvas.offsetLeft, event.clientY - canvas.offsetTop);
    paths.push([]);
});

// Draw with mouse
canvas.addEventListener("mouseup", () => {
    painting = false;
    context.closePath();
});

// Draw with mouse
canvas.addEventListener("mousemove", (event) => {
    if (!painting) return;

    context.lineTo(event.clientX - canvas.offsetLeft, event.clientY - canvas.offsetTop);
    context.stroke();
    paths[paths.length - 1].push({
        x: event.clientX - canvas.offsetLeft,
        y: event.clientY - canvas.offsetTop,
    });
});

canvas.addEventListener("mouseleave", () => {
    painting = false;
    context.closePath();
});

// undo button behaviour
undoButton.addEventListener("click", undo);

function undo() {
    if (paths.length > 0) {
        const pathToUndo = paths.pop();
        undonePaths.push(pathToUndo);
        clearCanvas();
        redrawPaths();
    }
}

// redo button behaviour
redoButton.addEventListener("click", redo);

function redo() {
    if (undonePaths.length > 0) {
        const pathToRedo = undonePaths.pop();
        paths.push(pathToRedo);
        clearCanvas();
        redrawPaths();
    }
}

// clear drawing area
function clearCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height);
}

// redraw paths
function redrawPaths() {
    context.beginPath();
    context.strokeStyle = colorPicker.value;
    context.lineWidth = lineWidth.value;

    paths.forEach((path) => {
        context.moveTo(path[0].x, path[0].y);

        path.forEach((point) => {
            context.lineTo(point.x, point.y);
            context.stroke();
        });
    });
}

// save button behaviour
saveButton.addEventListener("click", saveCanvas);

function saveCanvas() {
    const dataURL = canvas.toDataURL("image/png");
    const a = document.createElement("a");
    a.href = dataURL;
    a.download = "drawing.png";
    a.click();
}

// new file button behaviour
newFileButton.addEventListener("click", () => {
    clearCanvas();
    paths = [];
    undonePaths = [];
});
