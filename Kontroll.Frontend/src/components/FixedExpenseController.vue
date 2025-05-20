<script setup>
import { ref, onMounted } from 'vue';

const FixedExpenses = ref([]);
const FixedExpense = ref(null);
const loading = ref(false);
const error = ref(null);

const fetchData = async (url, method = "GET", body = null) => {
    loading.value = true;
    error.value = null;

    try {
        const options = { method };
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

const GetAll = async () => {
    FixedExpenses.value = await fetchData('https://localhost:7287/FixedExpenseApi?userId=1e21c816-5591-40ca-b418-fd4c7c8ef188');
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

    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>

    <div class="multipleFixedExpenseConteiner" v-if="FixedExpense == null">
        <div class="FixedExpenseConteiner" v-for="fixedExpense in FixedExpenses" :key="fixedExpense.Id" @click="GetSingle(fixedExpense)">
            <div class="FixedExpenseInfoBox">{{ fixedExpense.supplierName }}</div>
            <div class="FixedExpenseInfoBox">{{ fixedExpense.description }}</div>
            <div class="FixedExpenseInfoBox">{{ fixedExpense.monthlyAmount }}</div>
            <div class="FixedExpenseInfoBox">{{ (fixedExpense.monthlyAmount * 12) }}</div>
        </div>
    </div>
    <div class="singleFixedExpenseConteiner" v-if="FixedExpense != null">
        <button @click="FixedExpense = null">Tilbake</button>
        <div>
            {{ FixedExpense.supplierName }}
        </div>
    </div>

</template>

