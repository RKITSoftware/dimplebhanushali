let colors = ['White', 'Black', 'Blue', 'Orange'];
const addBtn = document.getElementById('addButton')
const removeBtn = document.getElementById('removeButton')
const removeLast = document.getElementById('removeLastButton')
const addFirst = document.getElementById('addToBeginningButton')
const removeFirst = document.getElementById('removeFirstButton')

// Function to update the displayed results
function updateResults() {
    // Displaying the results on the webpage
    document.getElementById('output').innerHTML = `
         <p>Updated Array: [${colors.join(', ')}]</p>
     `;
}

// Add an event listener to the "Add Value" button
addBtn.addEventListener('click', function () {
    const newValue = prompt('Enter a value to add:');
    if (newValue !== null && newValue.trim() !== '') {
        colors.push(newValue);
        updateResults();
    }
});

// Add an event listener to the "Remove Value" button
removeBtn.addEventListener('click', function () {
    const removeValue = prompt('Enter a value to remove:');
    if (removeValue !== null) {
        const index = colors.indexOf(removeValue);
        if (index !== -1) {
            colors.splice(index, 1);
            updateResults();
        } else {
            alert("element not found in array")
        }
    }
});

// Add an event listener to the "Remove Last Element" button
removeLast.addEventListener('click', function () {
    if (colors.length > 0) {
        colors.pop(); // Remove the last element
        updateResults();
    } else {
        alert("Array is Empty")
    }
});

// Add an event listener to the "Add to Beginning" button
addFirst.addEventListener('click', function () {
    const addToBeginningValue = prompt('Enter a value to add at the beginning:');
    if (addToBeginningValue !== null && addToBeginningValue.trim() !== '') {
        colors.unshift(addToBeginningValue); // Add to the beginning
        updateResults();
    }
});

// Add an event listener to the "Remove First Element" button
removeFirst.addEventListener('click', function () {
    if (colors.length > 0) {
        colors.shift(); // Remove the first element
        updateResults();
    } else {
        alert("Array is Empty")
    }
});

// Initialize results
updateResults();