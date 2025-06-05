<script setup>
import { MyLinks } from '@/components/MyLinks';
import { useRoute } from 'vue-router';

const route = useRoute();

</script>

<template>
    <div v-if="!route.path.includes('/UserHome/Economy/SingleTransaction/')" class="economyLinksConteiner">
        <div v-for="link in MyLinks.Economy" :key="index" class="economyLinkDiv">
            <RouterLink  class="economyLink" :to="{ name: link.name}">{{ link.label }}</RouterLink>
        </div>
    </div>
    <router-view/>

</template>

<style scoped>

.economyLinksConteiner {
    display: flex;
    width: 80%;
    margin: auto;
    justify-content: center;
    text-align: center;
    border-bottom: 1px solid rgba(var(--bs-body-bg-rgb), 0.8);
}

.economyLinkDiv {
    width: 15%;
    margin: 3px;
    border-left: 1px solid rgba(var(--bs-body-bg-rgb), 0.8);
    transition: 1s;
}

.economyLinkDiv:first-child {
    border: none;
}

.economyLinkDiv:hover {
    width: 16%;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
    border-radius: 5px;
}

.economyLink {
    text-decoration: none;
    text-align: center;
    font-size: 0.8em;
    color: rgba(var(--bs-body-color-rgb), 0.8);
    transition: 1s;
}

.economyLink:hover {
    font-weight: bolder;
}

</style>