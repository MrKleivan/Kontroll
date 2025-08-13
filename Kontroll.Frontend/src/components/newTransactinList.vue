<script setup>
import { Models } from '@/models/models';
</script>

<template>

    <div class="col-100 add-newTransaction-container">
        <div>
            <input 
                v-for="( field, key ) in Models.Transaction"
                :key="key"
                v-model="field.value" 
                :id="key" 
            />
        </div>

    </div>

</template>

<style scoped>

.add-newTransaction-container {
    display: flex;
    background-color: rgba(var(--bs-content-bg-rgb));
    height: 20vh;
    margin: auto;
    border-radius: 15px;
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-header-bg-rgb), 0.3);
}

</style>