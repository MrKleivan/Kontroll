<script setup>
import { colorMode, toggleColorMode } from './ColorMode';
</script>

<template>
  <button @click="toggleColorMode">colorMode{{ colorMode }}</button>
    <nav class="contentWindowNavBar">
      <RouterLink class="contentWindowNav" to="/UserHome">Logged Inn</RouterLink>
    </nav>
</template>