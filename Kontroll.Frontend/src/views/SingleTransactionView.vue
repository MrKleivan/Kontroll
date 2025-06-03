<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { fetchData } from '../composables/useFetch.js'

const route = useRoute();
const router = useRouter();

const Transaction = ref(null);
const loading = ref(true);
const error = ref(null);

const id = route.params.id;

onMounted(async () => {
    const data = await fetchData(`https://localhost:7287/TransactionApi/${id}`, 'Get', null, loading, error)

    if (data) {
        Transaction.value = data;
    }

    console.log(id)
});

</script>

<template>
    <div v-if="loading">🔄 Laster...</div>
    <div v-else-if="error">⚠️ Feil: {{ error }}</div>
    <div v-else-if="!Transaction">
        <button class="backButton" @click="router.push({ name: 'Transaction'})"> ◄ Transaksjoner </button>
    </div>
    <div class="SingleTransactionConteiner" v-else-if="Transaction">
        <button class="backButton" @click="router.push({ name: 'Transaction'})"> ◄ Transaksjoner </button>
        <h2>{{ Transaction.userDescription || Transaction.externalDescription }}</h2>
        <p>Dato: {{ Transaction.date }}</p>
        <p>Beløp: {{ Transaction.income === 0 ? Transaction.outcome : Transaction.income }} kr</p>
    </div>

</template>

<style>

.SingleTransactionConteiner {
    width: 100%;
    justify-items: start;
    align-content: start;
    text-align: left;
}

</style>