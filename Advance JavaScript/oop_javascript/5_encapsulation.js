class BankAccount {

    customerName;
    static accountNumber = 12345;
    #balance = 0;

    constructor(customerName, balance = 0) {
        this.customerName = customerName;
        this.accountNumber = BankAccount.accountNumber++;
        this.#balance = balance;
    }

    deposit(amount) {
        this.#balance += amount;
    }

    withdraw(amount) {
        this.#balance -= amount;
    }

    set balance(amount) {
        if (isNaN(amount)) {
            throw new Error('Amount is not a valid input');
        }
        this.#balance = amount;
    }

    get balance() {
        return this.#balance;
    }
}

class CurrentAccount extends BankAccount {
    transactionLimit = 50000;

    constructor(customerName, balance = 0) {
        super(customerName, balance);
    }

    #calculateInterest(amount) {
        console.log('Calculating interest');
    }

    takeBusinessLoan(amount) {
        // Logic
        this.#calculateInterest(amount);
        console.log('Taking business loan: ' + amount);
    }
}

const accounts = [];
const accountForm = document.querySelector('#accountForm');
const customerName = document.querySelector('#customerName');
const balance = document.querySelector('#balance');


const acc = document.getElementById('accoundholder')
const bal = document.getElementById('balance')


const depositForm = document.getElementById('deposit');
const withdrawForm = document.getElementById('withdraw');
const accountNumber = document.querySelector('#accountNumber');
const amount = document.querySelector('#amount');

// Add an event listener for the account form submission
accountForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const account = new BankAccount(customerName.value, +balance.value);
    accounts.push(account);
    alert(`Account Number: ${account.accountNumber}`);

    acc_details(account)

    console.log(accounts);
});
// Add an event listener for the deposit button
depositForm.addEventListener('click', (e) => {
    e.preventDefault();
    const account = accounts.find(
        (account) => account.accountNumber === +accountNumber.value
    );
    account.deposit(+amount.value);
    console.log(accounts);

    // Update account details in the "details" div
    acc_details(account);
});

// Add an event listener for the withdraw button
withdrawForm.addEventListener('click', (e) => {
    e.preventDefault();
    const account = accounts.find(
        (account) => account.accountNumber === +accountNumber.value
    );
    account.withdraw(+amount.value);
    console.log(accounts);

    // Update account details in the "details" div
    acc_details(account);
});

// Create a function to update account details in the "details" div
const acc_details = (account) => {
    const accountHolderName = document.getElementById('accountHolderName');
    const accountBalance = document.getElementById('accountBalance');

    if (account) {
        accountHolderName.textContent = 'Account Holder: ' + account.customerName;
        accountBalance.textContent = 'Balance: ' + account.balance;
    } else {
        accountHolderName.textContent = '';
        accountBalance.textContent = '';
    }


};
