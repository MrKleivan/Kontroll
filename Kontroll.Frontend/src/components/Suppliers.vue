<script setup>
import { ref, onMounted } from 'vue';
import { fetchData } from '../composables/useFetch.js';

const Suppliers = ref([]);
const Supplier = ref(null);
const loading = ref(false);
const error = ref(null);


const userId = '1e21c816-5591-40ca-b418-fd4c7c8ef188';

const GetAll = async () => {
    const url = `https://localhost:7287/SupplierApi?userId=${userId}`;
    Suppliers.value = await fetchData(url, 'GET', null, loading, error);

     await Promise.all(
    Suppliers.value.map(async supplier => {
      const balance = await GetBalanceSheetForSupplierByYear(supplier);
      supplier.balance = balance; // Legger til direkte
    })
  );
}

const GetSingle = async (supplier) => {
    Supplier.value = supplier;
};

async function GetBalanceSheetForSupplierByYear(supplier){
    const url = `https://localhost:7287/SupplierApi`;
    let request = {
        UserId: userId,
        SupplierId: supplier.supplierId, 
        Year: new Date().getFullYear(),
    };
    const type = 'POST';

    return await fetchData(url, type, request, loading, error);
}

onMounted(() => {
    GetAll();
});

</script>

<template>
    <br />
    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>

    <div class="multipleFixedExpenseConteiner" v-if="Supplier == null">
        <div class="FixedExpenseConteiner" v-for="supplier in Suppliers" :key="supplier.supplierId" @click="GetSingle(supplier)">
            <div class="FixedExpenseInfoBox">{{ supplier.supplierName }}</div>
            <div class="FixedExpenseInfoBox">{{ supplier.typeOfGoods }}</div>
            <div class="FixedExpenseInfoBox">{{ supplier.balance }}</div>
        </div>
    </div>
    <div class="singleFixedExpenseConteiner" v-if="Supplier != null">
        <button @click="Supplier = null">Tilbake</button>
        <div>
            {{ Supplier.supplierName }}
        </div>
    </div>

</template>

<style scoped>

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

