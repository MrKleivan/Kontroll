<script setup>
import { reactive, ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js'
import { useRouter } from 'vue-router';

const router = useRouter();
const bankAccounts = ref({});
const Transactions = ref([]);
const SortRequest = reactive({
  userId: "1e21c816-5591-40ca-b418-fd4c7c8ef188",
  transactionId: "string",
  request: "Date",
  externalDescription: "string",
  userDescription: "string",
  userAccountNumber: "",
  supplierAccountNumber: "",
  minAmount: 0,
  maxAmount: 0,
  amountDirection: "string",
  startDate: "2025-01-01",
  endDate: "2025-01-20"
});
const loading = ref(false);
const error = ref(null);


const GetAllTransactionsByCriteria = async () => {
    
    Transactions.value = await fetchData('https://localhost:7287/TransactionApi/transactionSort', 'POST', SortRequest,
    loading, error);
}

const GoToTransactionSite = async (transactionId) => {
    router.push({ name: 'SingleTransaction', params: { id: transactionId } });
};

onMounted(() => {
    GetAllTransactionsByCriteria();
});

const formatDate = (dateString) => {
  const d = new Date(dateString);
  return `${d.getDate().toString().padStart(2, '0')}.${(d.getMonth() + 1)
    .toString()
    .padStart(2, '0')}.${d.getFullYear()}`;
};

function GetResults(transactions) {
    let income = 0;
    let outcome = 0;
    transactions.forEach(element => {
        income += element.income;
        outcome += Math.abs(element.outcome);
    });
    let result = income - outcome;

    return result;
}

const selectedFilter = ref(null);

function SetFilterSelection(text) {
    selectedFilter.value = text;
    SortRequest.value.request = text;
}

</script>

<template>
    <br/>
    <div class="filterConteiner">
        <div class="filterDivHeader">
            Filter
        </div>
        <div class="filterOptionsConteiner">
            <div class="filterButton" @click="SetFilterSelection('Dato')">Dato</div>
            <div class="filterButton" @click="SetFilterSelection('Description')">Beskrivelse</div>
            <div class="filterButton" @click="SetFilterSelection('Amount')">Beløp</div>
            <div class="filterButton" @click="SetFilterSelection('Account')">Konto</div>
        </div>
        <div class="filterDivContainer" v-if="selectedFilter === 'Dato'">
            Start Dato: <input v-model="SortRequest.startDate" type="date" />
            Slutt Dato: <input v-model="SortRequest.endDate" type="date" />
            <button @click="GetAllTransactionsByCriteria">Søk</button>
        </div>
        <div class="filterDivContainer" v-if="selectedFilter === 'Amount'">
            Minimum: <input v-model="SortRequest.minAmount" type="number" />
            Maksimum: <input v-model="SortRequest.maxAmount" type="number" />
            <button @click="GetAllTransactionsByCriteria">Søk</button>
        </div>
        <div class="filterDivContainer" v-if="selectedFilter === 'AccountNumber'">
            <div>
                <section class="bankAccountFilter">
                    <option v-for="bankAccount in bankAccounts">{{ bankAccount.accountNumber }}</option>
                </section>
            </div>
            <button @click="GetAllTransactionsByCriteria">Søk</button>
        </div>
    </div>
    <br />
    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>
    <div class="multipleTransactionsConteiner" v-if="!Transaction">
        <div class="TransactionConteinerHeader">
            <div class="TransactionInfoBox">Dato</div>
            <div class="TransactionInfoBox">Beskrivelse</div>
            <div class="TransactionInfoBox">Inn/ut</div>
            <div class="TransactionInfoBox">Motparts kontonummer</div>
        </div>
        <div class="transactions">
            <div class="TransactionConteiner" v-for="transaction in Transactions" :key="transaction.Id" @click="GoToTransactionSite(transaction.transactionId)">
                <div class="TransactionInfoBox">{{ formatDate(transaction.date) }}</div>
                <div class="TransactionInfoBox">{{ (transaction.userDescription != null ? transaction.userDescription : transaction.externalDescription) }}</div>
                <div class="TransactionInfoBox">{{ transaction.income == 0 ? transaction.outcome : transaction.income }} kr</div>
                <div class="TransactionInfoBox">{{ transaction.income == 0 ? transaction.toAccount : transaction.fromAccount }}</div>
            </div>
            <div class="resultconteiner" >
                <div class="resultLeftDiv">
                    Resultat for periode: 
                </div>
                <div v-if="Transactions && Transactions.length > 0" class="resultRightDiv">
                    {{ GetResults(Transactions).toLocaleString('no-NO') }}
                </div>
            </div>
        </div>
    </div>

</template>

<style scoped>


.filterConteiner {
    width: 80%;
    height: 10vh;
    margin: auto;
    border-radius: 10px;
    background-color: rgba(var(--bs-body-bg-rgb), 0.7);
}

.filterDivHeader {
    width: 80%;
    padding: 2px;
    margin: auto;
    border-bottom: 1px solid rgba(var(--bs-body-color-rgb), 0.7);
}

.filterOptionsConteiner {
    display: flex;
    width: 100%;
    font-size: 70%;
    padding: 2px;
    justify-content: center;
}

.filterButton {
    width: 10%;
    border-left: 1px solid rgba(var(--bs-body-color-rgb), 0.7);
}

.filterButton:hover {
    background-color: rgba(var(--bs-body-color-rgb), 0.3);
    border-radius: 3px;
}

.filterButton:first-child {
    border: none;
}

.filterDivContainer {
    width: 80%;
    margin: auto;
    padding-top: 10px;
}

.multipleTransactionsConteiner {
    width: 80%;
}

.transactions {
    width: 100%;
    height: 70vh;
    overflow: scroll;
    scrollbar-width: none;
}

.singleFixedExpenseConteiner {
  width: 100%;
  text-align: left;
  justify-items: left;
}

.TransactionConteinerHeader {
  display: flex;
  width: 100%;
  margin-bottom: 5px;
  padding-top: 7px;
  padding-bottom: 7px;
  background-color: rgba(var(--bs-body-bg-rgb), 0.7);
  border-radius: 5px;
  border-bottom: 2px solid rgba(var(--bs-body-bg-rgb), 0.7);
}

.TransactionConteiner {
  display: flex;
  width: 98%;
  margin: auto;
  padding: 5px;
}

.TransactionConteiner:hover {
    padding: 8px;
    cursor: pointer;
}

.TransactionConteiner:nth-child(odd){
    background-color: rgba(var(--bs-body-bg-rgb), 0.9);
}

.TransactionInfoBox {
    width: 15%;
    height: 100%;
    margin: auto;
    text-align: center;
    align-content: center;
    font-size: 0.8em;
}

.TransactionInfoBox:nth-child(2){
    width: 40%;
}

.resultconteiner {
    display: flex;
    width: 100%;
    height: fit-content;
    border-top: 2px solid rgba(var(--bs-body-bg-rgb), 0.9);
}

.resultLeftDiv {
    width: 50%;
}

.resultRightDiv {
    width: 50%;
}


</style>

