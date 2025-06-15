<script setup>
import { ref } from 'vue';
import { onMounted, watch } from 'vue';
import { fetchData } from '../composables/useFetch.js'
import { useRoute, useRouter } from 'vue-router';
import { Links } from './MyLinks.js';

const route = useRoute();
const router = useRouter();
const loading = ref(false);
const error = ref(null);
const BankAccounts = ref([]);
const BankAccount = ref();
const UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188";
const status = ref();

const GetBankAccounts = async () => {
    const url = `https://localhost:7287/BankAccountApi?UserId=${UserId}`;
    BankAccounts.value = await fetchData(url, 'GET', null, loading, error);
};

const GetStatus = async () => {
    if(BankAccount){

    }
};

function SelectAccount(account) {
    BankAccount.value = BankAccount.value != account ? account : null;
}

onMounted(() => {
    GetBankAccounts();
})

watch(
  () => route.name,
  (newName) => {
    if (newName === 'Account') {
      GetBankAccounts();
    }
  }
);

</script>

<template>
    <div class="economyLinksConteiner">
        <div v-for="link in Links.UserHomePageMain.MainLinks.Economy.links.Accounting.links.Account.links" :key="index" class="economyLinkDiv">
            <RouterLink  class="economyLink" :to="{ name: link.name}">{{ link.label }}</RouterLink>
        </div>
    </div>
    <br/>
    <div v-if="loading">Laster...</div>
    <div v-if="error">Feil: {{ error }}</div>
    <div class="AccountContainer">
        <div class="topp">
            <div class="AccountContainerContentLeft">
                <div class="bankAccountContainerTopp">
                    <div v-if="BankAccounts && BankAccounts.length" v-for="account in BankAccounts" :key="account.id" class="bankAccount">
                        <div class="left">
                            {{ account.accountNumber }}
                        </div>
                        <div class="center">
                            {{ account.name }}
                        </div>
                        <div class="right" @click="SelectAccount(account)">
                            ↔
                        </div>
                    </div>
                    <div v-else>
                        Ingen kontoer funnet.
                    </div>
                </div>
                <div class="bankAccountContainerBottom">
                    {{ BankAccount ? BankAccount.name : 'jnkj' }}
                </div>
            </div>
            <div class="AccountContainerContentRight">
                <div class="accountStatus">
                    <div class="accountStatusTopp">Saldo</div>
                    <div class="accountStatusBottom"></div>
                </div>
                <div class="accountStatus">
                    <div class="accountStatusTopp">Balanse</div>
                    <div class="accountStatusBottom"></div>
                </div>
                <div class="accountStatus">
                    <div class="accountStatusTopp">Forventet resultat</div>
                    <div class="accountStatusBottom"></div>
                </div>
            </div>
        </div>
        <div class="bottom">
            <router-view />
        </div>
    </div>
</template>

<style scoped>

.economyLinksConteiner {
    display: flex;
    width: 80%;
    margin: auto;
    justify-content: center;
    text-align: center;
}

.economyLinkDiv {
    width: 12%;
    margin: 3px;
    border-left: 1px solid rgba(var(--bs-body-bg-rgb), 0.8);
}

.economyLinkDiv:first-child {
    border: none;
}

/* .economyLinkDiv:hover {
    width: 16%;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
    border-radius: 5px;
}  */

.economyLink {
    text-decoration: none;
    text-align: center;
    font-size: 0.7em;
    color: rgba(var(--bs-body-color-rgb), 0.8);
    box-sizing: border-box;
}

.economyLink:hover {
    font-weight: bolder;
    font-size: 0.75em;
}

.AccountContainer {
    width: 100%;
    margin: auto;
    height: fit-content;
}

.topp {
    display: flex;
}

.AccountContainerContentLeft {
    width: 30%;
    height: 20vh;
    min-height: 180px;
    margin: auto;
    border-radius: 15px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.bankAccountContainerTopp {
    width: 100%;
    height: 30%;
    padding-top: 15px;
    box-sizing: border-box;
}

.bankAccountContainerBottom {
    width: 50%;
    height: 70%;
    margin: auto;
    padding-top: 10px;
    box-sizing: border-box;
}

.newAccountButton {
    width: 80%;
    border-radius: 5px;
    border: none;
    color: rgba(var(--bs-body-color-rgb));
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
}

.newAccountButton:hover {
    cursor: pointer;
    border: 1px ridge rgba(var(--bs-header-bg-rgb), 0.8);
}

.bankAccount {
    display: flex;
    width: 80%;
    height: 20px;
    margin: auto;
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
    display: flex;
    width: 65%;
    height: 20vh;
    min-height: 180px;
    margin: auto;
    border-radius: 15px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.accountStatus {
    width: 30%;
    height: 90%;
    margin: auto;
}

.accountStatusTopp {
    width: 80%;
    height: 31px;
    margin: auto;
    text-align: center;
    align-content: center;
    font-size: 15px;
    font-family: 'Courier New', Courier, monospace;
    border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.7);
}

.accountStatusBottom {
    width: 100px;
    height: 100px;
    margin: auto;
    margin-top: 10px;
    border-radius: 50%;
    background-color: aliceblue;
}

.bottom {
    width: 98%;
    height: fit-content;
    margin: auto;
    margin-top: 30px;
    margin-bottom: 30px;
    border-radius: 15px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}


</style>