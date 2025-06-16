<script setup>
import { reactive, ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js'
import { useRouter, useRoute } from 'vue-router';
import { Links } from './MyLinks.js';
import UnderMeny from './UnderMeny.vue';
import TransactionView from '@/views/TransactionView.vue';

const MyLinks = Links.UserHomePageMain.MainLinks.Economy.links.Accounting.links.Transaction.links;
const router = useRouter();
const route = useRoute();


</script>

<template>
    <UnderMeny :links="MyLinks" />
    <br/>
    <div v-if="route.name === 'Transaction'" class="TransactionViewContainer">
        <TransactionView />
    </div>
    <div v-else class="TransactionViewContainer">
        <router-view />
    </div>
    

</template>

<style scoped>

.TransactionViewContainer {
    width: 100%;
    height: 100%;
}

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
    margin: auto;
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

