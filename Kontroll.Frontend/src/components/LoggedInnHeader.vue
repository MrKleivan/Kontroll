<script setup>
import { RouterLink, RouterView } from 'vue-router';
import { ref, Transition } from 'vue';
import { colorMode, toggleColorMode } from './ColorMode';
import LifeControllLogo from './LifeControllLogo.vue';

const isMenuVisible = ref(false);
const headerMenu = ref('headerMenuActive');
const toggleMenu  = () => {
  isMenuVisible.value = !isMenuVisible.value;
  headerMenu.value = 'headerMenuActive';
};

let hideTimeout = null;

const hideMenuWithDelay = () => {
  hideTimeout = setTimeout(() => {
    isMenuVisible.value = false;
  }, 300); // 300 ms delay
};

const cancelHide = () => {
  clearTimeout(hideTimeout);
};


</script>

<template>
<header>
    <div class="headerLeft">
        <div class="headerLogo">
            <LifeControllLogo />
        </div>
    </div>
    <div class="headerRight">
        <div class="headerMenu" :style="{ width: isMenuVisible ? '20%' : '15%', backgroundColor: isMenuVisible ? 'rgba(var(--bs-body-color-rgb), 1)' : 'rgba(var(--bs-body-color-rgb), 0.4)'}">
            <div class="headerMenuLeft">
                <span :style="{display: isMenuVisible ? 'none' : 'block'}">Meny </span>
                <span :style="{display: !isMenuVisible ? 'none' : 'block'}">Valg</span>
            </div>
            <div class="headerMenuRight">
                <button id="MenuButton" @click="toggleMenu">&#9776;</button>
            </div>
      </div>
      <div class="mainNavConteiner">
        <nav class="MainNav" v-show="isMenuVisible" @mouseleave="hideMenuWithDelay" @mouseenter="cancelHide">
          <RouterLink class="mainNavButton" to="/">Hjem</RouterLink>
          <RouterLink class="mainNavButton" to="/myProfile">Profil</RouterLink>
          <RouterLink class="mainNavButton" to="/mySettings">Innstillinger</RouterLink>
          <RouterLink class="mainNavButton" :to="{name: 'LogOut'}">Logg ut</RouterLink>
          <button class="mainNavButton" @click="toggleColorMode" variant="colorMode"> Color mode</button><br/>
          <div class="colorModeConteiner">
            <div class="colormodeText">
                ColorMode:
            </div>
            <div class="colormodeIndikator">{{ colorMode }}

            </div>
        </div>
        </nav>
      </div>
    </div>
</header>
</template>

<style scoped>

header {
  display: flex;
  width: 100%;
  min-height: 100px;
  height: 10vh;
  background-color: rgba(var(--bs-header-bg-rgb), 0.8);
}

.WebLogo {
  width: 50%;
  height: 100%;
  align-content: center;
  text-align: center;
  color: rgba(var(--bs-body-color-rgb), 0.7);
}

.headerLeft {
  width: 50%;
  height: 100%;
}

.headerRight {
  width: 50%;
  height: 100%;
  text-align: end;
  justify-items: end;
}

.headerLogo {
  width: 100%;
  height: 100%;
}

.headerMenu {
  display: flex;
  margin-top: 5px;
  margin-right: 15px;
  padding: 5px;
  padding-right: 20px;
  padding-left: 20px;
  background-color: rgba(var(--bs-body-color-rgb), 0.4);
  color: rgb(var(--bs-body-bg-rgb));
  border-radius: 10px 20px;
  overflow: hidden;
  transition: 0.5s;
}

.headerMenuLeft {
  width: 50%;
  text-align: start;
}

.headerMenuRight {
  width: 50%;
  text-align: end;
}

#MenuButton {
  border: none;
  color: rgb(var(--bs-body-color-rgb));
  background-color: rgb(var(--bs-btn-bg-b-rgb));
  border-radius: 5px;
  z-index: 1;
}

#MenuButton:hover {
  background-color: rgb(var(--bs-btn-hover-bg-rgb));
}

.MainNav {
  display: grid;
  width: 15%;
  padding: 3px;
  margin-right: 20px;
  text-align: end;
  background-color: rgb(var(--bs-body-color-rgb));
  border-radius: 0 0 5px 15px;
  z-index: 1;
}

.mainNavConteiner {
  width: 100%;
  height: fit-content;
  margin-right: 5px;
  justify-items: end;
}

.mainNavButton {
  padding: 4px;
  background-color: rgb(var(--bs-btn-bg-b-rgb));
  color: rgb(var(--bs-body-color-rgb));
  margin-bottom: 1px;
  text-decoration: none;
  text-align: end;
  font-size: 0.7em;
  border: none;
  border-radius: 5px;
  z-index: 1;
}

.mainNavButton:hover {
  background-color: rgb(var(--bs-btn-hover-bg-rgb));
}

.colorModeConteiner {
    display: flex;
    width: 100%;
    font-size: 90%;
    justify-content: center;
}

.colormodeText {
    width: fit-content;
    color: rgb(var(--bs-body-bg-rgb));
}

.colormodeIndikator {
    width: fit-content;
    padding-left: 2px;
    padding-right: 2px;
    border-radius: 5px;
    background-color: rgb(var(--bs-body-bg-rgb));
    color: rgb(var(--bs-body-color-rgb));
    border: 1px solid black;
    font-size: 90%;
}

</style>