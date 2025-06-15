<script setup>
import { ref } from 'vue';
import { fetchData } from '../composables/useFetch.js'
import { useRouter } from 'vue-router';

const router = useRouter();
const loading = ref(false);
const error = ref(null);
const AccountModel = [
    {label: 'Konto nummer'},
    {label: 'Konto navn'},
    {label: 'Type konto'},
];

const Model = {
    Input: {
        AccountNumber: {
            value: ref(''),
            name: 'AccountNumber',
            label: 'Konto nummer'
        },
        AccountName: {
            value: ref(''),
            name: 'AccountName',
            label: 'Konto navn'
        }
    },
    OptionInput: {
        Type: ['Brukskonto', 'Lønnskonto', 'Regningskonto', 'Sparekonto', 'Mastercardkonto'],
        value: ref(''),
    }
};


async function getMyData() {
    if (confirm('vil du?')){
        const url = 'https://localhost:7287/BankAccountApi';
        const body = {
            id: null,
            userId: '1e21c816-5591-40ca-b418-fd4c7c8ef188',
            accountNumber: Model.Input.AccountNumber.value.value,
            name: Model.Input.AccountName.value.value,
            type: Model.OptionInput.value.value
        };
        await fetchData(url, 'POST', body, loading, error);
        router.replace({name: 'Account'})
    } 
    else return;
};

</script>
<template>
    <div class="newAccountContainer">
        <div class="newAccountHead">Legg til konto</div>
        <div class="inputcontainer" v-for="model in Model.Input">
            <div class="inputInfo">
                {{ model.label }}:
            </div>
            <div class="inputFieldContainer">
                <input class="inputField" v-model="model.value.value" />
            </div>
        </div>
        <div class="inputcontainer">
            <div class="inputInfo">
                Type konto:
            </div>
            <div class="inputFieldContainer">
                <select v-model="Model.OptionInput.value.value" class="inputField">
                    <option v-for="option in Model.OptionInput.Type" :value="option">{{ option }}</option>
                </select>
            </div>
        </div>
        <div class="buttoncontainer">
            <button class="newAccountButton" @click="getMyData()">Legg till</button>
        </div>
    </div>   
</template>

<style scoped>

.newAccountHead {
    width: 50%;
    margin: auto;
    border-bottom: 1px solid rgba(var(--bs-header-bg-rgb), 0.9);
}

.newAccountContainer {
    width: 50%;
    margin: auto;
    box-sizing: border-box;
    padding: 15px;
}

.inputcontainer {
    display: flex;
    width: 50%;
    margin: auto;
    margin-top: 10px;
    border: 2px solid rgba(var(--bs-header-bg-rgb), 0.9);
    border-radius: 5px;
    background-color: rgba(var(--bs-header-bg-rgb), 0.7);
    overflow: hidden;
}
.buttoncontainer {
    display: flex;
    width: 40%;
    margin: auto;
    margin-top: 10px;
    border: 2px solid rgba(var(--bs-header-bg-rgb), 0.9);
    border-radius: 5px;
    background-color: rgba(var(--bs-header-bg-rgb), 0.7);
    overflow: hidden;
}

.newAccountButton {
    width: 50%;
    margin: auto;
    border: none;
    border-radius: 5px;
    background-color: rgba(var(--bs-content-bg-rgb), 0.6);
}

.newAccountButton:hover {
    cursor: pointer;
    background-color: rgba(var(--bs-content-bg-rgb), 0.9);
    font-size: 0.85em;
}

.inputInfo {
    width: 40%;
    height: 20px;
    font-size: 0.8em;
    text-align: end;
    box-sizing: border-box;
    padding-right: 5px;
    align-content: center;
}

.inputFieldContainer {
    width: 60%;
    height: 20px;
    text-align: center;
}

.inputField {
    position: relative;
    width: 100%;
    height: 100%;
    color: rgba(var(--bs-body-color-rgb), 0.8);
    background-color: rgba(var(--bs-content-bg-rgb), 0.8);
    text-align: start;
    border: none;
}
.inputField:focus {
    position: relative;
    width: 100%;
    height: 100%;
    text-align: start;
    outline: none
}

</style>