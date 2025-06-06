<script setup>
import { RouterLink, RouterView } from 'vue-router';
import LoggedInnHeader from './LoggedInnHeader.vue';
import { ref } from 'vue';
import { MyLinks, Links } from './MyLinks';
import { useRouter, useRoute } from 'vue-router';

const route = useRoute();
const router = useRouter();


function goBack() {
    router.push({ name: 'UserHome' });
}

function goToLink(link){
  router.push({name: link.name});
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
        <div class="backToMain" v-if="route.name != 'UserHome'">
            <button class="backButton" @click="goBack"> ◄ Hjem</button>
        </div>
        <div v-if="route.name === 'UserHome'" class="MainUserNavConteiner">
            <div v-for="mylink in Links.UserHomePageMain.MainLinks" class="MainUserNav">
                <div class="MainUserNavTopp">
                    <RouterLink class="MainUserNavLink" :to="{ name: mylink.name }">{{ mylink.label }}</RouterLink>
                </div>
                <div class="MainUserNavBottom" v-if="mylink.links">
                    <div v-for="link in mylink.links" class="MainUserNavLinkConteiner"> 
                      <div @click="goToLink(link)" class="MainUserNavLinkLink">{{ link.label }}</div>
                      <div class="MainUserNavLinkInfo">{{ link.info }}</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="contentWindowContent">
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

.MainContenWindow {
  display: flex;
  width: 100%;
  height: 100vh;
  background-color: rgba(var(--bs-header-bg-rgb), 0.8);
}

.contentWindow {
  width: 85%;
  height: 100%;
  margin: auto;
  text-align: center;
  background-color: rgba(var(--bs-content-bg-rgb), 0.5);
  border-radius: 0 25px 0 0;
  box-shadow: 0px 0px 2px 2px rgba(var(--bs-body-color-rgb), 0.2) inset;
  overflow: scroll;
  scrollbar-width: none;
  z-index: 0;
}


.contentWindowHeader {
  width: 80%;
  height: 10%;
  margin: auto;
  align-content: center;
}

.contentWindowNavBar {
  display: flex;
  justify-content: center;
  width: 80%;
  margin: auto;
  padding: 5px;
  border-bottom: 1px solid rgba(var(--bs-body-color-rgb), 0.3);
}

.contentWindowNav {
  width: 19%;
  /* background-color: rgba(var(--bs-body-bg-rgb), 0.7); */
  color: rgba(var(--bs-body-color-rgb), 0.8);
  text-decoration: none;
  text-align: center;
  font-size: 0.7em;
  border-left: 1px solid rgba(var(--bs-body-color-rgb), 0.3);
  transition: 0.4s;
  z-index: 1;
}

.contentWindowNav:first-child {
  border: none;
}

.contentWindowNav:hover {
  width: 20%;
  background-color: rgba(var(--bs-body-color-rgb), 0.8);
  color: rgba(var(--bs-body-bg-rgb), 0.7);
  border: 1px ridge rgba(var(--bs-body-color-rgb), 0.8);
  border-radius: 3px;
}

.contentWindowContent {
  width: 80%;
  margin: auto;
  text-align: center;
  justify-items: center;
}

.infoStripe {
  width: 15%;
  height: 100%;
}

.UserInfoConteiner {
  width: 90%;
  height: 15dvh;
  min-height: 145px;
  margin: auto;
  text-align: center;
  background-color: rgba(var(--bs-content-bg-rgb), 0.5);
  box-shadow: 0px 0px 2px 2px rgba(var(--bs-body-color-rgb), 0.2) inset;
  border-radius: 25px;
}

.UserInfoConteinerHeader {
  width: 90%;
  margin: auto;
  padding-top: 5px;
  color: rgba(var(--bs-body-color-rgb), 0.7);
  border-bottom: 1px solid rgba(var(--bs-body-color-rgb), 0.2);
}

.MainUserNavConteiner {
    display: flex;
    width: 100%;
    min-height: 150px;
    height: 19vh;
    min-height: 188px;
}

.MainUserNav {
    width: 18%;
    height: 90%;
    font-size: 1.5vw;
    margin: auto;
    background-color: rgba(var(--bs-body-bg-rgb), 0.7);
    border-radius: 10px 35px;
    box-shadow: 0px 0px 2px 2px rgba(var(--bs-body-color-rgb), 0.2);
}


.MainUserNavTopp {
    width: 80%;
    margin: auto;
}

.MainUserNavBottom {
  width: 78%;
  height: 70%;
  margin: auto;
  border-top: 1px solid rgba(var(--bs-body-color-rgb), 0.7);
  border-radius: 5px;
  overflow: scroll;
  scrollbar-width: none;
}

.MainUserNavLink {
    text-decoration: none;
    color: rgba(var(--bs-body-color-rgb), 0.7);
}

.MainUserNavLinkConteiner {
  width: 100%;
  display: flex;
  justify-content: center;
}

.MainUserNavLinkLink {
    width: fit-content;
    font-size: 0.7vw;
    margin-top: 7px;
    padding: 2px;
    color: rgba(var(--bs-body-color-rgb), 0.9);
    border-bottom: 1px solid rgba(var(--bs-body-color-rgb), 0.4);
    background-color: rgba(var(--bs-body-bg-rgb), 0.7);
}

.MainUserNavLinkLink:hover {
    /* background-color: rgba(var(--bs-body-color-rgb), 0.2);
    color: rgba(var(--bs-body-color-rgb), 0.9); */
    font-weight: bold;
    cursor: pointer;
}

.MainUserNavLinkInfo {
  text-align: center;
  align-content: center;
  margin-top: 7px;
  margin-left: 4px;
  font-size: 0.6vw;
}

.backToMain {
    width: 100%;
    text-align: start;
    padding: 10px;
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