<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { colorMode, toggleColorMode } from './components/ColorMode';

const ToggleColorMode = toggleColorMode;
</script>

<template>
  <header>

    <div :class="colorMode.value">
      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
        <button @click="toggleColorMode" variant="colorMode"> Color mode</button>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

</style>
