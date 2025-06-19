<script setup>
import { ref, onMounted } from 'vue'
import dragDrop from './dragDrop.vue';
import { fetchData } from '../composables/useFetch.js'

const uploadedFile = ref(null);
const Transactions = ref([]);
const BankAccounts = ref([]);
const AccountNumber = ref('');
const loading = ref(false);
const error = ref(null);
const UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188";

const selectAll = ref(false);

async function handleConfirmedFile(file) {
  uploadedFile.value = file
  console.log('Mottatt fil:', file)
  GetTransactions(file);
}

async function GetTransactions(file) {
    const url = `https://localhost:7287/TransactionApi/convert-csv`;
    const formData = new FormData();
    formData.append('file', file); // <-- korrekt her også
    formData.append('userId', UserId);
    formData.append('accountNumber', AccountNumber.value); // 🔁 husk .value
    Transactions.value = await fetchData(url, 'POST', formData, loading, error);
    console.log(Transactions.value[1]);
}

const formatDate = (dateString) => {
  const d = new Date(dateString);
  return `${d.getDate().toString().padStart(2, '0')}.${(d.getMonth() + 1)
    .toString()
    .padStart(2, '0')}.${d.getFullYear()}`;
};

const GetBankAccounts = async () => {
    const url = `https://localhost:7287/BankAccountApi?UserId=${UserId}`;
    BankAccounts.value = await fetchData(url, 'GET', null, loading, error);
};

onMounted(() => {
    GetBankAccounts();
});

function toggleAll() {
  if (Transactions.value && Transactions.value.length) {
    Transactions.value.forEach(t => {
      t.forceAdd = selectAll.value;
    });
  }
}

function RemoveTransaction(transaction) {
  const index = Transactions.value.indexOf(transaction);
  if (index !== -1) {
    Transactions.value.splice(index, 1); // ✅ Vue ser endringen
  }
}

</script>

<template>
    <div class="newTransactionContainer">
        <div class="upper">
            <div class="leftSide">
                <div>
                    <div v-if="BankAccounts && BankAccounts.length" class="bankAccount">
                        <select v-model="AccountNumber" class="inputField">
                            <option v-for="option in BankAccounts" :value="option.accountNumber">{{ option.accountNumber }}</option>
                        </select>
                    </div>
                    <div v-else>
                        Ingen kontoer funnet.
                    </div>
                </div>
            </div>
            <div class="rightSide">
                <div class="dragDropContainer" v-if="AccountNumber">
                    <dragDrop @file-confirmed="handleConfirmedFile" />
                </div>
                <div v-else>
                    <div style="width: 70%;margin: auto;border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.7);">
                        Her kan du laste opp <br/> 
                        kontoutskrift i csv fil
                    </div>
                    <div>
                        Velg et kontonummer i venstre meny
                    </div>
                </div>
            </div>
        </div>
        <div class="lower">
            <div v-if="Transactions.length > 0" class="TransactionsContainer">
                <div class="TransactionHeader">
                    <div class="col-10 TransactionInfoBox">Kryss av<br/><span style="font-weight: normal;">Alle</span> <input type="checkbox" v-model="selectAll" @change="toggleAll" /></div>
                    <div class="col-15 TransactionInfoBox">Dato for transaksjon</div>
                    <div class="col-30 TransactionInfoBox">Fordringshaver</div>
                    <div class="col-15 TransactionInfoBox">Beløp</div>
                    <div class="col-20 TransactionInfoBox">Utenforstående sit kontonr</div>
                    <div class="col-10 TransactionInfoBox"></div>
                </div>
                <div v-for="transaction in Transactions" class="Transaction">
                    <div class="col-10 TransactionInfoBox"><input type="checkbox" v-model="transaction.forceAdd"></div>
                    <div class="col-15 TransactionInfoBox">{{ formatDate(transaction.date) }}</div>
                    <div class="col-30 TransactionInfoBox">{{ (transaction.userDescription != null ? transaction.userDescription : transaction.externalDescription) }}</div>
                    <div class="col-15 TransactionInfoBox">{{ transaction.income == 0 ? transaction.outcome : transaction.income }} kr</div>
                    <div class="col-20 TransactionInfoBox">{{ transaction.income == 0 ? transaction.toAccount : transaction.fromAccount }}</div>
                    <div class="col-10 TransactionInfoBox"><button class="RemoveTransactionButton" @click="RemoveTransaction(transaction)">Fjern</button></div>
                </div>
            </div>
            <div class="UpploadMeny">
                <button></button>
                <button></button>
            </div>
        </div>
    </div>
</template>

<style scoped>

.newTransactionContainer {
    width: 100%;
    height: fit-content;
    margin: auto;
}

.upper {
    display: flex;
    width: 100%;
}

.lower {
    width: 100%;
    height: 70vh;
    margin-top: 15px;
}

.leftSide {
    width: 70%;
    height: 20vh;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.rightSide {
    width: 28%;
    height: 20vh;
    margin-left: 2%;
    align-content: center;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.dragDropContainer {
    width: 100%;
    height: 100%;
    align-content: center;
}

.TransactionsContainer {
    width: 100%;
    height: 60vh;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
    overflow: scroll;
    scrollbar-width: none;
    box-sizing: border-box;
    padding: 10px;
}

.TransactionHeader {
    display: flex;
    width: 80%;
    margin: auto;
    margin-top: 2px;
    padding: 2px;
    box-sizing: border-box;
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 5px 5px 0 0;
    font-weight: bold;
}

.Transaction {
    display: flex;
    width: 80%;
    margin: auto;
    margin-top: 2px;
    padding: 2px;
    box-sizing: border-box;
    background-color: rgba(var(--bs-header-bg-rgb), 0.3);
}


.Transaction:nth-child(odd) {
    background-color: rgba(var(--bs-header-bg-rgb), 0.1);
}

.Transaction:last-child {
    border-radius: 0 0 5px 5px;
}

.RemoveTransactionButton {
    width: 80%;
    margin: auto;
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    background-color: rgba(var(--bs-header-bg-rgb), 0.3);
    border-radius: 5px;
}





</style>