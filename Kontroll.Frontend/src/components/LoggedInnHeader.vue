<script setup>
import { RouterLink, RouterView } from 'vue-router';
import { ref, Transition } from 'vue';
import { colorMode, toggleColorMode } from './ColorMode';
import LifeControllLogo from './LifeControllLogo.vue';

const isMenuVisible = ref(false);
const toggleMenu  = () => {
  isMenuVisible.value = !isMenuVisible.value;
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
        <div class="headerMenu">
            <div class="headerMenuLeft">
                <span>Meny </span>
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
          <RouterLink class="mainNavButton" to="/loggOut">Logg ut</RouterLink>
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