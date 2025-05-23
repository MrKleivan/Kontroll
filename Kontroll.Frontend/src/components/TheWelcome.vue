<script setup>
import { colorMode, toggleColorMode } from './ColorMode';
</script>

<template>
  <header>
    <button @click="toggleColorMode">colorMode{{ colorMode }}</button>
  </header>
    <nav class="contentWindowNavBar">
      <RouterLink class="contentWindowNav" to="/UserHome">Logged Inn</RouterLink>
    </nav>
</template>