<script setup>
import { ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js';

const FixedExpenses = ref([]);
const FixedExpense = ref(null);
const loading = ref(false);
const error = ref(null);

const userId = '1e21c816-5591-40ca-b418-fd4c7c8ef188';

const GetAll = async () => {
    const url = `https://localhost:7287/FixedExpenseApi?userId=${userId}`;
    FixedExpenses.value = await fetchData(url, 'GET', null, loading, error);
    console.log(FixedExpenses.value)
}

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

    <div class="multipleFixedExpenseConteiner" v-if="FixedExpense == null">
        <div class="FixedExpenseConteiner" v-for="fixedExpense in FixedExpenses" :key="fixedExpense.Id" @click="GetSingle(fixedExpense)">
            <div class="FixedExpenseInfoBox">{{ fixedExpense.supplierName }}</div>
            <div class="FixedExpenseInfoBox">{{ fixedExpense.description }}</div>
            <div class="FixedExpenseInfoBox">{{ fixedExpense.monthlyAmount }}</div>
            <div class="FixedExpenseInfoBox">{{ (fixedExpense.monthlyAmount * 12) }}</div>
            <div class="FixedExpenseInfoBox">Trekk dato <br/>{{ fixedExpense.monthlyDeadlineDay }}</div>
        </div>
    </div>
    <div class="singleFixedExpenseConteiner" v-if="FixedExpense != null">
        <button @click="FixedExpense = null">Tilbake</button>
        <div>
            {{ FixedExpense.supplierName }}
        </div>
    </div>

</template>

<style scoped>
/* @import './main.css'; */

.multipleFixedExpenseConteiner {
  width: 100%;
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

