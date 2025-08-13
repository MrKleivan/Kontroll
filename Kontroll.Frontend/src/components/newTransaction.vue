<script setup>
import { ref, onMounted } from 'vue'
import dragDrop from './dragDrop.vue';
import { fetchData } from '../composables/useFetch.js'
import newTransactinList from './newTransactinList.vue';

const uploadedFile = ref(null);
const Transactions = ref([]);
const BankAccounts = ref([]);
const bankAccount = ref(null);
const AccountNumber = ref('');
const FixedTransaction = ref(null);
const loading = ref(false);
const error = ref(null);
const UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188";

const selectAll = ref(false);

async function handleConfirmedFile(file) {
  uploadedFile.value = file
  console.log('Mottatt fil:', file)
  GetTransactions(file);
}

async function GetTransactions(file) {
    const url = `https://localhost:7287/TransactionApi/convert-csv`;
    const formData = new FormData();
    formData.append('file', file); // <-- korrekt her også
    formData.append('userId', UserId);
    formData.append('accountNumber', AccountNumber.value); // 🔁 husk .value
    Transactions.value = await fetchData(url, 'POST', formData, loading, error);
    
    if (Transactions.value.length > 0) {
        for (const transaction of Transactions.value) {
            if (transaction.fixedTransactionId) {
                const fixedInfo = await GetFixedTransactionInfoById(transaction.fixedTransactionId);
                transaction.fixedInfo = fixedInfo;
                console.log(fixedInfo);
            }
        }
    }
}

const formatDate = (dateString) => {
  const d = new Date(dateString);
  return `${d.getDate().toString().padStart(2, '0')}.${(d.getMonth() + 1)
    .toString()
    .padStart(2, '0')}.${d.getFullYear()}`;
};

const GetBankAccounts = async () => {
    const url = `https://localhost:7287/BankAccountApi?UserId=${UserId}`;
    BankAccounts.value = await fetchData(url, 'GET', null, loading, error);
};

const GetFixedTransactionInfoById = async (transactionId) => {
    const url = `https://localhost:7287/FixedTransactionApi/${transactionId}`;
    return await fetchData(url, 'GET', null, loading, error);
};

onMounted(() => {
    GetBankAccounts();
});

function toggleAll() {
  if (Transactions.value && Transactions.value.length) {
    Transactions.value.forEach(t => {
      t.forceAdd = selectAll.value;
    });
  }
}

async function UploadTransaction(transaction) {
  const url = 'https://localhost:7287/TransactionApi';
  const result = await fetchData(url, 'POST', transaction, loading, error);
  return result == null; // returner true hvis det faktisk gikk bra
}

async function uploadAll(transactions) {
    error.value = null;
    for (const transaction of transactions.slice()) {
        if (transaction.fixedInfo) {
            const fixedInfo = transaction.fixedInfo;
            delete transaction.fixedInfo;
            let isUploaded = await UploadTransaction(transaction);
            console.log(isUploaded);
            if(isUploaded){
                RemoveTransaction(transaction);
            }
            else {
                transaction.fixedInfo = fixedInfo;
            }
        }
        else {
            let isUploaded = await UploadTransaction(transaction);
            if(isUploaded){
                RemoveTransaction(transaction);
            }
            else {
                error.value = "feil";
            }
        }
    }
}

async function uploadByCriteria() {
    const transactions = Transactions.value.filter(t => t.forceAdd);
    if (transactions.length > 0) {
        await uploadAll(transactions);
    }
}

function RemoveTransaction(transaction) {
  Transactions.value = Transactions.value.filter(t => t.transactionId !== transaction.transactionId);

}

function removeAll() {
    Transactions.value = [];
};

function removeCriteria() {
    Transactions.value = Transactions.value.filter(t => t.forceAdd !== true);
}

const getDayAsInt = (dateString) => {
  if (!dateString) return null; // eller 0, avhengig av hva du vil gjøre ved tom verdi
  const date = new Date(dateString);
  return date.getDate(); // returnerer dag som tall (eks: 22)
};

function updateAccountNumber() {
  AccountNumber.value = bankAccount.value?.accountNumber ?? '';
}

function resetAccount() {
  bankAccount.value = null;
  Transactions.value = [];
  AccountNumber.value = '';
}

</script>

<template>
    <div class="newTransactionContainer">
        <div class="upper">
            <div class="leftSide">
                <div v-if="BankAccounts && BankAccounts.length" class="bankAccount">
                    <div v-if="!bankAccount">
                        <select v-model="bankAccount" @change="updateAccountNumber" class="inputField">
                            <option v-for="option in BankAccounts" :value="option">{{ option.accountNumber }}</option>
                        </select>← Velg Konto<br/>
                    </div>
                    <div v-if="bankAccount" class="account-number">
                        <button class="backButton" @click="resetAccount">◄</button><br/>
                        {{ bankAccount.name}}
                    </div>
                </div>
                <div v-else>
                    Ingen kontoer funnet.
                </div>
                <div v-if="bankAccount" class="bankAccount-info">
                    <div class="col-16 bankAccount-info-field-left">
                        Konto nr:<br/>
                        Konto navn: 
                    </div>
                    <div class="col-18 bankAccount-info-field-center">
                        {{ bankAccount.accountNumber }}<br/>
                        {{ bankAccount.name }}
                    </div>
                    <div class="col-62 bankAccount-info-field-right">
                        <button class="AddButton">Fyll inn en enkelt transaksjon</button>
                    </div>
                </div>
                <div v-else class="Insert-info">
                    Legg til transaksjoner i regnskapet ditt<br/>
                    via manuell utfylling av enkeltvis transaksjoner <br/>
                    eller last opp kontoutskrift fra banken (.csv fil)
                </div>
            </div>
            <div class="rightSide">
                <div class="dragDropContainer" v-if="bankAccount">
                    <dragDrop @file-confirmed="handleConfirmedFile" />
                </div>
                <div v-else>
                    <div style="width: 70%;margin: auto;border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.7);">
                        Her kan du laste opp <br/> 
                        kontoutskrift (filtype:  .csv)
                    </div>
                    <div>
                        Velg først hvilket kontonummer <br/>
                        kontoutskriften gjelder <br/>
                        og trykk last opp
                    </div>
                </div>
            </div>
        </div>
        <br/>
        <newTransactinList />
        <div v-if="Transactions.length > 0" class="lower">
            <div class="col-88 lower-left">
                <div class="TransactionsContainer">
                    <div class="TransactionHeader">
                        <div class="col-10 TransactionInfoBox">Kryss av<br/><span style="font-weight: normal;">Alle</span> <input type="checkbox" v-model="selectAll" @change="toggleAll" /></div>
                        <div class="col-16 TransactionInfoBox">Dato for transaksjon</div>
                        <div class="col-30 TransactionInfoBox">Fordringshaver</div>
                        <div class="col-16 TransactionInfoBox">Beløp</div>
                        <div class="col-20 TransactionInfoBox">Utenforstående sit kontonr</div>
                        <div class="col-8 TransactionInfoBox"></div>
                    </div>
                    <div v-for="transaction in Transactions" class="transaction-container">
                        <div :class="transaction.fixedInfo ?  'transaction-ekstra' : 'transaction'" >
                            <div class="col-10 TransactionInfoBox"><input type="checkbox" v-model="transaction.forceAdd"></div>
                            <div class="col-16 TransactionInfoBox">{{ formatDate(transaction.date) }}</div>
                            <div class="col-30 TransactionInfoBox">{{ (transaction.userDescription != null ? transaction.userDescription : transaction.externalDescription) }}</div>
                            <div class="col-16 TransactionInfoBox">{{ transaction.income == 0 ? transaction.outcome : transaction.income }} kr</div>
                            <div class="col-20 TransactionInfoBox">{{ transaction.income == 0 ? transaction.toAccount : transaction.fromAccount }}</div>
                            <div class="col-8 TransactionInfoBox"><button class="col-80 RemoveTransactionButton" @click="RemoveTransaction(transaction)">Fjern</button></div>
                        </div>
                        <div v-if="transaction.fixedInfo" class="transaktinon-info">
                            <div class="transaktinon-info-upper">
                                Funnet match i system<br/>
                                {{ transaction.fixedInfo.description }}
                            </div>
                            <div class="transaktinon-info-lower">
                                <div class="col-50 transaktinon-info-lower-left">
                                    <div v-if="getDayAsInt(transaction.date) != transaction.fixedInfo.monthlyDeadlineDay">
                                        Betalt dato: {{ formatDate(transaction.date) }} / 
                                        Forfallsdato: {{ transaction.fixedInfo.monthlyDeadlineDay }}
                                    </div>
                                </div>
                                <div class="col-50 transaktinon-info-lower-right">
                                    Vil du koble disse sammen
                                    <button class="transaktinon-info-lower-right-button">Ja</button>
                                    <button class="transaktinon-info-lower-right-button">Nei</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="TransactionFooter">
                        <div class="col-10 TransactionInfoBox"></div>
                        <div class="col-16 TransactionInfoBox"></div>
                        <div class="col-30 TransactionInfoBox"></div>
                        <div class="col-16 TransactionInfoBox"></div>
                        <div class="col-20 TransactionInfoBox"></div>
                        <div class="col-8 TransactionInfoBox"></div>
                    </div>
                </div>
            </div>
            <div class="col-10 lower-right">
                <div class="UpploadMeny">
                    <button class="col-80 upload-meny-button" @click="uploadAll(Transactions)">Legg til alle</button>
                    <button class="col-80 upload-meny-button" @click="uploadByCriteria()">Legg til merkede</button>
                </div>
                <div class="UpploadMeny">
                    <button class="col-80 upload-meny-button" @click="removeAll()">Fjern til alle</button>
                    <button class="col-80 upload-meny-button" @click="removeCriteria()">Fjern merkede</button>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

.newTransactionContainer {
    width: 100%;
    height: fit-content;
    margin: auto;
}

.upper {
    display: flex;
    width: 100%;
}

.lower {
    display: flex;
    width: 100%;
    height: 70vh;
    margin-top: 15px;
}

.leftSide {
    width: 70%;
    height: 20vh;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.rightSide {
    width: 28%;
    height: 20vh;
    margin-left: 2%;
    align-content: center;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

.bankAccount {
    width: 95%;
    height: 30%;
    text-align: start;
    align-content: end;
    box-sizing: border-box;
    margin: auto;
    padding-top: 5px;
    border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
}

.inputField {
    width: 20%;
    height: 20px;
    border-radius: 5px;
    margin-bottom: 5px;
    background-color: rgb(var(--bs-content-bg-rgb));
    color: rgb(var(--bs-body-color-rgb));
}

.account-number {
    font-family: 'Courier New', Courier, monospace;
}

.bankAccount-info {
    display: flex;
    width: 95%;
    height: 60%;
    margin: auto;
    box-sizing: border-box;
    padding-top: 10px;
}

.bankAccount-info-field-left {
    text-align: start;
    box-sizing: border-box;
    padding-left: 10px;
}

.bankAccount-info-field-center {
    text-align: start;
    box-sizing: border-box;
    font-family: 'Courier New', Courier, monospace;
}

.bankAccount-info-field-right {
    margin-top: auto;
    text-align: end;
    align-content: end;
}

.AddButton {
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 5px;
    margin-left: 10px;
    color: rgb(var(--bs-body-color-rgb));
    background-color: rgba(var(--bs-btn-bg-b-rgb), 0.3);
}

.AddButton:hover {
    cursor: pointer;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
}

.dragDropContainer {
    width: 100%;
    height: 100%;
    align-content: center;
}

.lower-right {
    height: fit-content;
    margin-left: 2%;
}

.TransactionsContainer {
    width: 100%;
    height: 60vh;
    border-radius: 15px;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
    overflow: scroll;
    scrollbar-width: none;
    box-sizing: border-box;
    padding: 10px;
}

.TransactionHeader {
    display: flex;
    width: 95%;
    margin: auto;
    margin-top: 2px;
    padding: 2px;
    box-sizing: border-box;
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 5px 5px 0 0;
    font-weight: bold;
}

.TransactionFooter {
    display: flex;
    width: 95%;
    margin: auto;
    margin-top: 2px;
    padding: 2px;
    box-sizing: border-box;
    background-color: rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 0 0 5px 5px;
    font-weight: bold;
}

.transaction {
    display: flex;
    width: 100%;
    height: fit-content;
    margin: auto;
    padding: 2px;
    background-color: rgba(var(--bs-header-bg-rgb), 0.3);
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.3);
    box-sizing: border-box;
}

.transaction-ekstra {
    display: flex;
    width: 100%;
    height: fit-content;
    margin: auto;
    padding: 2px;
    background-color: rgba(var(--bs-secondary-rgb), 0.8);
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    box-sizing: border-box;
}


.transaction-container {
    width: 95%;
    height: fit-content;
    margin: auto;
    margin-top: 2px;
    box-sizing: border-box;
}

.transaktinon-info {
    width: 100%;
    height: fit-content;
    border: 2px solid rgba(var(--bs-secondary-rgb), 0.8);
    border-top: none;
    border-radius: 0 0 5px 5px;
    padding: 2px;
    box-sizing: border-box;
    background-color: rgb(var(--bs-content-bg-rgb));
}

 .transaktinon-info-lower-right-button {
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    border-radius: 5px;
    margin-left: 10px;
    background-color: rgba(var(--bs-btn-bg-b-rgb), 0.3);
}
 .transaktinon-info-lower-right-button:hover {
    cursor: pointer;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
}

.transaction-container:last-child {
    border-radius: 0 0 5px 5px;
}

.TransactionInfoBox {
    height: fit-content;
    word-wrap: break-word;
    overflow-wrap: break-word;
    white-space: normal;
}

.RemoveTransactionButton {
    margin: auto;
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    color: rgb(var(--bs-body-color-rgb));
    background-color: rgba(var(--bs-btn-bg-b-rgb), 0.3);
    border-radius: 5px;
}

.UpploadMeny {
    width: 100%;
    height: 10vh;
    margin-bottom: 15px;
    border-radius: 15px;
    align-content: center;
    background-color: rgb(var(--bs-content-bg-rgb));
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
    box-sizing: border-box;
}

.upload-meny-button {
    width: 80%;
    height: 40%;
    margin-bottom: 2px;
    color: rgb(var(--bs-body-color-rgb));
    border: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
    background-color: rgba(var(--bs-btn-bg-b-rgb), 0.3);
    border-radius: 15px;
}

.upload-meny-button:hover, .RemoveTransactionButton:hover {
    cursor: pointer;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
}

.transaktinon-info-upper {
    width: 95%;
    margin: auto;
    text-align: start;
    border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.8);
}

.transaktinon-info-lower {
    display: flex;
    width: 95%;
    margin: auto;
}

.transaktinon-info-lower-left {
    text-align: start;
}
.transaktinon-info-lower-right {
    text-align: end;
}



</style>