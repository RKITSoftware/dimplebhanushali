// Define a simple Account class
class Account {
    constructor(customerName, initialBalance, accountType) {
        this.customerName = customerName;
        this.accountNumber = Account.generateAccountNumber();
        this.balance = initialBalance;
        this.accountType = accountType;
    }

    static generateAccountNumber() {
        return Math.floor(Math.random() * 100000);
    }

    deposit(amount) {
        this.balance += amount;
    }

    withdraw(amount) {
        if (this.balance >= amount) {
            this.balance -= amount;
            return true; // Successful withdrawal
        }
        return false; // Insufficient balance
    }

    getAccountInfo() {
        return `Account Number: ${this.accountNumber}<br>
                        Customer Name: ${this.customerName}<br>
                        Account Type: ${this.accountType}<br>
                        Balance: $${this.balance.toFixed(2)}`;
    }
}

// Handle the account creation form
const accountForm = document.getElementById('accountForm');
accountForm.addEventListener('submit', function (e) {
    e.preventDefault();
    const customerName = document.getElementById('customerName').value;
    const initialBalance = parseFloat(document.getElementById('initialBalance').value);
    const accountType = document.getElementById('accountType').value;
    const account = new Account(customerName, initialBalance, accountType);

    // Display account information
    const accountInfo = document.getElementById('accountInfo');
    accountInfo.innerHTML = account.getAccountInfo();

    // Clear form fields
    document.getElementById('customerName').value = '';
    document.getElementById('initialBalance').value = '';
    document.getElementById('accountType').value = 'Savings';
});

// Handle the deposit form
const depositForm = document.getElementById('depositForm');
depositForm.addEventListener('submit', function (e) {
    e.preventDefault();
    const accountNumber = parseInt(document.getElementById('accountNumber').value);
    const depositAmount = parseFloat(document.getElementById('depositAmount').value);

    // Find the account and deposit the amount
    if (accountNumber === account.accountNumber) {
        account.deposit(depositAmount);

        // Update and display account information
        const accountInfo = document.getElementById('accountInfo');
        accountInfo.innerHTML = account.getAccountInfo();

        // Clear form fields
        document.getElementById('accountNumber').value = '';
        document.getElementById('depositAmount').value = '';
    } else {
        alert('Account not found. Please enter a valid account number.');
    }
});

// Handle the withdraw form
const withdrawForm = document.getElementById('withdrawForm');
withdrawForm.addEventListener('submit', function (e) {
    e.preventDefault();
    const accountNumber = parseInt(document.getElementById('withdrawAccountNumber').value);
    const withdrawAmount = parseFloat(document.getElementById('withdrawAmount').value);

    // Find the account and attempt to withdraw the amount
    if (accountNumber === account.accountNumber) {
        const withdrawalSuccessful = account.withdraw(withdrawAmount);

        if (withdrawalSuccessful) {
            // Update and display account information
            const accountInfo = document.getElementById('accountInfo');
            accountInfo.innerHTML = account.getAccountInfo();

            // Clear form fields
            document.getElementById('withdrawAccountNumber').value = '';
            document.getElementById('withdrawAmount').value = '';
        } else {
            alert('Insufficient balance. Please enter a valid withdrawal amount.');
        }
    } else {
        alert('Account not found. Please enter a valid account number.');
    }
});