<script setup>

defineProps({
  links: {
    type: Array,
    required: true
  }
});

</script>

<template>
<div class="underMenyContainer">
    <div v-for="(link, index) in links" :key="index" class="underMenyLinkContainer">
        <RouterLink  class="underMenyLink" :to="{ name: link.name}">{{ link.label }}</RouterLink>
    </div>
</div>
</template>

<style scoped>


.underMenyContainer {
    display: flex;
    width: 80%;
    margin: auto;
    justify-content: center;
    text-align: center;
}

.underMenyLinkContainer {
    width: 12%;
    margin: 3px;
    border-left: 1px solid rgba(var(--bs-body-bg-rgb), 0.8);
}

.underMenyLinkContainer:first-child {
    border: none;
}

/* .economyLinkDiv:hover {
    width: 16%;
    background-color: rgba(var(--bs-btn-hover-bg-rgb), 0.8);
    border-radius: 5px;
}  */

.underMenyLink {
    text-decoration: none;
    text-align: center;
    font-size: 0.7em;
    color: rgba(var(--bs-body-color-rgb), 0.8);
    box-sizing: border-box;
}

.underMenyLink:hover {
    font-weight: bolder;
    font-size: 0.75em;
}

</style>