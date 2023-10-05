function getCheese() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const cheese = "🧀"
            resolve(cheese)
        }, 2000)
    })

}

function makeDough(cheese) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const dough = cheese + "🍪"
            resolve(dough)
            // reject("Bad Cheese")
        }, 2000)
    })

}

function bakePizza(dough) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const pizza = dough + "🍕"
            resolve(pizza)
        }, 2000)
    })

}


// Promises

// getCheese()
//     .then((cheese) => {
//         console.log('Here is your Cheese', cheese)
//         return makeDough(cheese)
//     })
//     .then((dough) => {
//         console.log('Here is your Dough', dough)
//         return bakePizza(dough)
//     })
//     .then((pizza) => {
//         console.log('Here is your Pizza', pizza)
//     })
//     .catch((data) => {
//         console.log('Error :: ', data)
//     })
//     .finally(() => {
//         console.log('Finally Task is over')
//     })

// Async Await
// this function is acnchronous in nature
async function orderPizza() {
    try {
        const cheese = await getCheese();
        console.log('Here is your Cheese', cheese)
        const dough = await makeDough(cheese);
        console.log('Here is your Dough', dough)
        const pizza = await bakePizza(dough)
        console.log('Here is your Pizza', pizza)
    } catch (Error) {
        console.log('Error Occured :: ', Error)
    }
}

orderPizza()