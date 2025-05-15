<script setup>
import { ref, Transition } from 'vue';
import { RouterLink, RouterView } from 'vue-router'
import { colorMode, toggleColorMode } from './components/ColorMode';
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
  
      </div>
    </div>
    <div class="headerRight">
      <div class="headerMenu">
        <span>Meny </span>
        <button id="MenuButton" @click="toggleMenu">&#9776;</button>
      </div>
      <div class="mainNavConteiner">
        <nav class="MainNav" v-show="isMenuVisible" @mouseleave="hideMenuWithDelay" @mouseenter="cancelHide">
          <RouterLink class="mainNavButton" to="/">Hjem</RouterLink>
          <RouterLink class="mainNavButton" to="/myProfile">Profil</RouterLink>
          <RouterLink class="mainNavButton" to="/mySettings">Innstillinger</RouterLink>
          <RouterLink class="mainNavButton" to="/loggOut">Logg ut</RouterLink>
          <button class="mainNavButton" @click="toggleColorMode" variant="colorMode"> Color mode</button>
        </nav>
      </div>
    </div>
  </header>

  <RouterView />

  <footer>

  </footer>
</template>

<style scoped>


</style>
