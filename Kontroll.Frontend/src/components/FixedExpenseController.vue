<script setup>
import { ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js';

const FixedExpenses = ref([]);
const FixedExpense = ref(null);
const loading = ref(false);
const error = ref(null);
const FixedExpensesSorted = ref();

const userId = '1e21c816-5591-40ca-b418-fd4c7c8ef188';

const GetAll = async () => {
    const url = `https://localhost:7287/FixedExpenseApi?userId=${userId}`;
    FixedExpenses.value = await fetchData(url, 'GET', null, loading, error);
    FixedExpensesSorted.value = FixedExpenses.value;
}

const SortExpenses = async (text) => {
  if (text === 'inn'){
    FixedExpensesSorted.value = FixedExpenses.value.filter(s = s.type === 'Inncome');
    SortHeader.value = 'Inntekter'
  }
  if (text === 'out'){
    FixedExpensesSorted.value = FixedExpenses.value.filter(s = s.type === 'Outcome');
  }
  else {
    FixedExpensesSorted.value = FixedExpenses.value;
  }
};

const GetSingle = async (fixedExpense) => {
    FixedExpense.value = fixedExpense;
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
      <div class="multipleFixedExpenseConteiner" v-if="FixedExpense == null">
        <div class="FixedExpenseMeny">
          <div class="FixedExpenseSortHeading">Filtrering</div>
          <button class="FixedExpenseSortButton" @click="SortExpenses('inn')">Inntekt</button>
          <button class="FixedExpenseSortButton" @click="SortExpenses('out')">Utgift</button>
          <button class="FixedExpenseSortButton" @click="SortExpenses('all')">Alle</button>
        </div>  
        <div class="FixedExpenseList">
          <div class="FixedExpenseConteiner" v-for="fixedExpense in FixedExpensesSorted" :key="fixedExpense.Id" @click="GetSingle(fixedExpense)">
              <div class="FixedExpenseInfoBox">{{ fixedExpense.supplierName }}</div>
              <div class="FixedExpenseInfoBox">{{ fixedExpense.description }}</div>
              <div class="FixedExpenseInfoBox">{{ fixedExpense.monthlyAmount }}</div>
              <div class="FixedExpenseInfoBox">{{ (fixedExpense.monthlyAmount * 12) }}</div>
              <div class="FixedExpenseInfoBox">Trekk dato <br/>{{ fixedExpense.monthlyDeadlineDay }}</div>
          </div>
        </div>
      </div>
      <div class="singleFixedExpenseConteiner" v-if="FixedExpense != null">
          <button class="backButton" @click="FixedExpense = null">◄ Tilbake</button>
          <div>
              {{ FixedExpense.supplierName }}
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

.FixedExpenseMeny {
  width: 10%;
}

.FixedExpenseSortHeading {
  width: 90%;
  margin: auto;
  background-color: rgba(var(--bs-body-bg-rgb), 0.8);
  border-bottom: 2px solid rgb(var(--bs-content-bg-rgb));
  border-radius: 5px 5px 0 0;
}

.FixedExpenseSortButton {
  width: 90%;
  margin: auto;
  color: rgb(var(--bs-body-color-rgb));
  background-color: rgba(var(--bs-body-bg-rgb), 0.5);
  border: none;
  border-bottom: 1px solid rgb(var(--bs-content-bg-rgb));
}

.FixedExpenseSortButton:last-child {
  border: none;
  border-radius: 0 0 5px 5px;
}

.FixedExpenseSortButton:hover {
  cursor: pointer;
  font-weight: bold;
}

.FixedExpenseList {
  width: 90%;
}

.singleFixedExpenseConteiner {
  width: 100%;
  text-align: left;
  justify-items: left;
}

.FixedExpenseConteiner {
  display: flex;
  width: 100%;
  height: 7vh;
  min-height: 70px;
  padding: 5px;
  background-color: rgba(var(--bs-body-bg-rgb), 0.8);
  border-radius: 10px 20px;
}

.FixedExpenseInfoBox {
  width: 22%;
  height: 100%;
  margin: auto;
  text-align: center;
  align-content: center;
  font-size: 150%;
}

.FixedExpenseInfoBox:first-child {
  background-color: rgba(var(--bs-body-color-rgb), 0.1);
  border-radius: 10px 20px;
}

</style>

