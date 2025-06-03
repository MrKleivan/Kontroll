<script setup>
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

onMounted(() => {
    console.log('LogOut mounted');
  setTimeout(() => {
    console.log('LogOut mounted');
    router.replace({ name: 'home' });
  }, 50); // 50 ms delay
});

</script>

<template>
<div>
    Logger ut...
</div>
</template>

<style>
</style>