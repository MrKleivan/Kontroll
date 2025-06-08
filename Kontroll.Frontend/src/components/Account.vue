<script setup>
import { ref } from 'vue';
import { onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js'

const loading = ref(false);
const error = ref(null);
const BankAccounts = ref([]);
const BankAccount = ref();
const UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188";

const GetBankAccounts = async () => {
    const url = `https://localhost:7287/BankAccountApi?UserId=${UserId}`;
    BankAccounts.value = await fetchData(url, 'GET', null, loading, error);
};

onMounted(() => {
    GetBankAccounts();
})

</script>

<template>
    <br/>
    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>
    <div class="AccountContainer">
        <div class="AccountContainerContentLeft">
            <div v-if="BankAccounts && BankAccounts.length" v-for="account in BankAccounts" :key="account.id" class="bankAccount">
                <div class="left">
                    {{ account.accountNumber }}
                </div>
                <div class="center">
                    {{ account.name }}
                </div>
                <div class="right" @click="GoToSingleAccount(account)">
                    ↔
                </div>
            </div>
            <div v-else>
                Ingen kontoer funnet.
            </div>
        </div>
        <div class="AccountContainerContentRight">

        </div>
    </div>
</template>

<style scoped>

.AccountContainer {
    display: flex;
    width: 100%;
    margin: auto;
    height: fit-content;
}

.AccountContainerContentLeft {
    width: 30%;
    height: 30vh;
    margin: auto;
    border-radius: 15px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.bankAccount {
    display: flex;
    width: 80%;
    height: 20px;
    margin: auto;
    margin-top: 10px;
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 5px;
}


.left {
    width: 45%;
    height: 100%;
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
}

.center {
    width: 45%;
    height: 100%;
}

.right {
    width: 10%;
    height: 100%;
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
}

.right:hover {
    cursor: pointer;
    background-color: rgba(var(--bs-header-bg-rgb), 1);
}

.AccountContainerContentRight {
    width: 60%;
    height: 30vh;
    margin: auto;
    border-radius: 15px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}


</style>