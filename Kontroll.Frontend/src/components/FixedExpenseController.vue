<script setup>
import { ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js';

const FixedTransactions = ref([]);
const FixedTransaction = ref(null);
const loading = ref(false);
const error = ref(null);
const FixedTransactionsSorted = ref();

const userId = '1e21c816-5591-40ca-b418-fd4c7c8ef188';

const GetAll = async () => {
    const url = `https://localhost:7287/FixedTransactionApi?userId=${userId}`;
    FixedTransactions.value = await fetchData(url, 'GET', null, loading, error);
    FixedTransactionsSorted.value = FixedTransactions.value;
}

const SortExpenses = async (text) => {
  if (text === 'inn'){
    FixedTransactionsSorted.value = FixedTransactions.value.filter(s = s.type === 'Inncome');
    SortHeader.value = 'Inntekter'
  }
  if (text === 'out'){
    FixedTransactionsSorted.value = FixedTransactions.value.filter(s = s.type === 'Expense');
  }
  else {
    FixedTransactionsSorted.value = FixedTransactions.value;
  }
};

const GetSingle = async (fixedExpense) => {
    FixedTransaction.value = fixedExpense;
};

onMounted(() => {
    GetAll();
});

</script>

<template>
    <br />
    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>
    <div v-else class="FixedExpenses">
      <div class="multipleFixedExpenseConteiner" v-if="FixedTransaction == null">
        <div class="FixedTransactionMeny">
          <div class="FixedTransactionSortHeading">Filtrering</div>
          <button class="FixedTransactionSortButton" @click="SortExpenses('inn')">Inntekt</button>
          <button class="FixedTransactionSortButton" @click="SortExpenses('out')">Utgift</button>
          <button class="FixedTransactionSortButton" @click="SortExpenses('all')">Alle</button>
        </div>  
        <div class="FixedTransactionList">
          <div class="FixedTransactionConteiner" v-for="fixedExpense in FixedTransactionsSorted" :key="fixedExpense.Id" @click="GetSingle(fixedExpense)">
              <div class="FixedTransactionInfoBox">{{ fixedExpense.supplierName }}</div>
              <div class="FixedTransactionInfoBox">{{ fixedExpense.description }}</div>
              <div class="FixedTransactionInfoBox">{{ fixedExpense.monthlyAmount }}</div>
              <div class="FixedTransactionInfoBox">{{ (fixedExpense.monthlyAmount * 12) }}</div>
              <div class="FixedTransactionInfoBox">Trekk dato <br/>{{ fixedExpense.monthlyDeadlineDay }}</div>
          </div>
        </div>
      </div>
      <div class="singleFixedTransactionConteinerr" v-if="FixedTransaction != null">
          <button class="backButton" @click="FixedTransaction = null">◄ Tilbake</button>
          <div>
              {{ FixedTransaction.supplierName }}
          </div>
      </div>
    </div>

</template>

<style scoped>
/* @import './main.css'; */

.FixedExpenses{
  width: 100%;
  height: fit-content;
}

.multipleFixedExpenseConteiner {
  display: flex;
  width: 90%;
  margin: auto;
}

.FixedTransactionMeny {
  width: 10%;
}

.FixedTransactionSortHeading {
  width: 90%;
  margin: auto;
  background-color: rgba(var(--bs-body-bg-rgb), 0.8);
  border-bottom: 2px solid rgb(var(--bs-content-bg-rgb));
  border-radius: 5px 5px 0 0;
}

.FixedTransactionSortButton {
  width: 90%;
  margin: auto;
  color: rgb(var(--bs-body-color-rgb));
  background-color: rgba(var(--bs-body-bg-rgb), 0.5);
  border: none;
  border-bottom: 1px solid rgb(var(--bs-content-bg-rgb));
}

.FixedTransactionSortButton:last-child {
  border: none;
  border-radius: 0 0 5px 5px;
}

.FixedTransactionSortButton:hover {
  cursor: pointer;
  font-weight: bold;
}

.FixedTransactionList {
  width: 90%;
}

.singleFixedTransactionConteinerr {
  width: 100%;
  text-align: left;
  justify-items: left;
}

.FixedTransactionConteiner {
  display: flex;
  width: 100%;
  height: 7vh;
  min-height: 70px;
  padding: 5px;
  background-color: rgba(var(--bs-body-bg-rgb), 0.8);
  border-radius: 10px 20px;
}

.FixedTransactionInfoBox {
  width: 22%;
  height: 100%;
  margin: auto;
  text-align: center;
  align-content: center;
  font-size: 150%;
}

.FixedTransactionInfoBox:first-child {
  background-color: rgba(var(--bs-body-color-rgb), 0.1);
  border-radius: 10px 20px;
}

</style>

