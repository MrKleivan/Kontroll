<script setup>
import { RouterLink, RouterView } from 'vue-router';
import LoggedInnHeader from './LoggedInnHeader.vue';
import { ref } from 'vue';
import router from '@/router';
import { MyLinks } from './MyLinks';

const watching = ref(false);

function handleClick() {
  watching.value = !watching.value;
}

function goBack() {
    watching.value = false;
    router.push({ name: 'UserHome' });
}

</script>

<template>
<LoggedInnHeader/>
<div class="MainContenWindow">
    <div class="contentWindow">
        <div class="droppdownFullMenueConteiner">
            <div class="droppdownFullMenue" @click="showFullMenu">&#9660;</div>
            <div></div>
        </div>
        <div class="backToMain" v-if="watching == true">
            <button class="backToMainButton" @click="goBack"> ◄ Tilbake</button>
        </div>
        <div v-if="watching == false" class="MainUserNavConteiner">
            <div v-for="mylink in MyLinks.Main" class="MainUserNav">
                <div class="MainUserNavTopp">
                    <RouterLink class="MainUserNavLink" :to="{ name: mylink.name }" @click="handleClick">{{ mylink.label }}</RouterLink>
                </div>
                <div class="MainUserNavBottom">
                    <RouterLink v-for="link in MyLinks.Economy" :to="{ name: link.name }" @click="handleClick" class="MainUserNavLinkLink">{{ link.label }}</RouterLink>
                </div>
            </div>
        </div>
        <div v-if="watching == true" class="contentWindowContent">
            <router-view />
        </div>
    </div>
    <div class="infoStripe">
        <div class="UserInfoConteiner">
            <div class="UserInfoConteinerHeader">
            <span>Per Ivar Kleivan</span>
            </div>
            <div class="UserInfoConteinerContent">
            <span></span>
            </div>
        </div>
        <br />
        <div class="UserInfoConteiner">
            
        </div>
    </div>
</div>
<footer>

</footer>
</template>

<style scoped>

.MainUserNavConteiner {
    display: flex;
    width: 100%;
    height: 15vh;
}

.MainUserNav {
    width: 15%;
    height: 90%;
    font-size: 200%;
    margin: auto;
    background-color: rgba(var(--bs-body-bg-rgb), 0.7);
    border-radius: 10px 35px;
}

.MainUserNavTopp {
    width: 80%;
    margin: auto;
    border-bottom: 1px solid rgba(var(--bs-body-color-rgb), 0.7);
}

.MainUserNavLink {
    text-decoration: none;
    color: rgba(var(--bs-body-color-rgb), 0.7);
}

.MainUserNavLinkLink {
    font-size: 50%;
    text-decoration: none;
    color: rgba(var(--bs-body-bg-rgb), 0.9);
    margin: 3px;
    padding: 2px;
    border-radius: 5px;
    background-color: rgba(var(--bs-body-color-rgb), 0.7);
}

.MainUserNavLinkLink:hover {
    background-color: rgba(var(--bs-body-color-rgb), 0.2);
    color: rgba(var(--bs-body-color-rgb), 0.9);
}

.backToMain {
    width: 100%;
    text-align: start;
    padding: 10px;
}

.backToMainButton {
    width: fit-content;
    height: fit-content;
    padding: 3px;
    background-color: rgb(var(--bs-btn-bg-b-rgb));
    color: rgb(var(--bs-body-color-rgb));
    border-radius: 5px;
    border: 1px solid rgba(var(--bs-body-bg-rgb), 0.7);
}

.backToMainButton:hover {
    background-color: rgb(var(--bs-btn-hover-bg-rgb));
}

.droppdownFullMenueConteiner {
    width: 100%;
    height: 17px;
    overflow: visible;
}

.droppdownFullMenue {
    width: 8%;
    height: 10px;
    margin: auto;
    font-size: 50%;
    background-color: rgb(var(--bs-body-bg-rgb));
    border-radius: 0 0 10px 10px;
    opacity: 0.2;
    transition: 1s;
}

.droppdownFullMenue:hover {
    height: 17px;
    font-size: 100%;
    opacity: 1;
    overflow: visible;
}

footer {
  width: 100%;
  height: 15vh;
  background-color: rgb(var(--bs-body-bg-rgb));
  border-top: 2px ridge rgba(var(--bs-body-bg-rgb), 0.3);
}
</style>