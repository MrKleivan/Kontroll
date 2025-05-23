<script setup>
import { ref, onMounted } from 'vue';

const Transactions = ref([]);
const Transaction = ref(null);
const loading = ref(false);
const error = ref(null);

const fetchData = async (url, method = "GET", body = null) => {
    loading.value = true;
    error.value = null;

    try {
        const headers = {
            'Content-Type': 'application/json'
        }
        const options = { method, headers };
        
        if (body) {
            options.body = JSON.stringify(body);
        }

        const result = await fetch(url, options);

        const text = await result.text();
        console.log(text);
        return text ? JSON.parse(text) : [];
    } catch (err) {
        error.value = err.message;
        return [];
    } finally {
        loading.value = false;
    }
    
};

const GetAllTransactionsByCriteriar = async () => {
    Transactions.value = await fetchData('https://localhost:7287/TransactionApi/transactionSort', 'POST', {
  "userId": "1e21c816-5591-40ca-b418-fd4c7c8ef188",
  "transactionId": "string",
  "request": "Date",
  "externalDescription": "string",
  "userDescription": "string",
  "accountNumber": "string",
  "minAmount": 0,
  "maxAmount": 0,
  "amountDirection": "string",
  "startDate": "2025-01-01",
  "endDate": "2025-01-20"
});
}

const GetSingle = async (transaction) => {
    Transaction.value = transaction;
};

onMounted(() => {
    GetAllTransactionsByCriteriar();
});

</script>

<template>

    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>
    <div class="multipleTransactionsConteiner" v-if="Transaction == null">
        <div class="TransactionConteinerHeader">
            <div class="TransactionInfoBox">Dato</div>
            <div class="TransactionInfoBox">Beskrivelse</div>
            <div class="TransactionInfoBox">Inn/ut</div>
            <div class="TransactionInfoBox">Motparts kontonummer</div>
        </div>
        <div class="transactions">
            <div class="TransactionConteiner" v-for="transaction in Transactions" :key="transaction.Id" @click="GetSingle(transaction)">
                <div class="TransactionInfoBox">{{ transaction.date }}</div>
                <div class="TransactionInfoBox">{{ (transaction.userDescription != null ? transaction.userDescription : transaction.externalDescription) }}</div>
                <div class="TransactionInfoBox">{{ transaction.income == 0 ? transaction.outcome : transaction.income }} kr</div>
                <div class="TransactionInfoBox">{{ transaction.income == 0 ? transaction.toAccount : transaction.fromAccount }}</div>
            </div>
        </div>
    </div>
    <div class="singleFixedExpenseConteiner" v-if="Transaction != null">
        <button @click="Transaction = null">Tilbake</button>
        <div>
            {{ Transaction.date }}
        </div>
    </div>

</template>

<style scoped>

.multipleTransactionsConteiner {
    width: 80%;
}

.transactions {
    width: 100%;
    height: 80vh;
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
  padding: 7px;
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


</style>

