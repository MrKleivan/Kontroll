<script setup>
import { colorMode, toggleColorMode } from './ColorMode';
import LifeControllLogo from './LifeControllLogo.vue';
import { MyLinks } from './MyLinks';
</script>

<template>
  <header class="headerHomePage">
    <div class="headerHomePageLeft">
      <div class="LogoheaderHomePage">
        <LifeControllLogo /> 
      </div>
    </div>
    <div class="headerHomePageCenter">
      <div class="upper"></div>
      <div class="lower"></div>
    </div>
    <div class="headerHomePageright">
      <div class="headerHomePagerightupper">
        <nav class="homePageNavBar">
          <div v-for="link in MyLinks.HomePageLinks" class="HomeNavigationLinks" :key="link.name">
            <div class="linkConteiner">
              <RouterLink class="links" :to="{name: link.name}">{{link.label}}</RouterLink>
              <div v-if="link.svg" v-html="link.svg" class="icon">
              </div>
  
            </div>
            </div>
        </nav>
      </div>
      <div class="headerHomePagerightlower">
        Fargevelger: <button class="colorMOdeButton" @click="toggleColorMode">{{ colorMode }}</button>
      </div>
    </div>
  </header>
</template>

<style scoped>

.headerHomePage {
  display: flex;
  width: 100%;
  min-height: 100px;
  height: 10vh;
}

.LogoheaderHomePage {
  display: flex;
  width: 80%;
  height: 100%;
  margin: auto;
  align-items: center;
}

.headerHomePageLeft {
  width: 40%;
  height: 80%;
  background-color: rgb(var(--bs-header-bg-rgb));
}

.headerHomePageCenter {
  width: 20%;
  height: 100%;
  background-color: rgb(var(--bs-header-bg-rgb));
}

.headerHomePageright {
  width: 40%;
  height: 100%;
  background-color: rgb(var(--bs-header-bg-rgb));
  border-radius: 0 0 0 10px;
}

.upper {
  width: 100%;
  height: 80%;
}

.homePageNavBar {
  display: flex;
  width: 90%;
  height: 50%;
  margin: auto;
  padding-left: 20px;
  align-items: center;
  justify-content: center;
  background-image: linear-gradient(to left, rgb(var(--bs-header-bg-rgb)), rgba(var(--bs-body-color-rgb), 0.1), rgb(var(--bs-header-bg-rgb)));
  border-radius: 5px;
}

.linkConteiner {
  display: flex;
  width: 90%;
  height: 90%;
  align-items: center;
  justify-content: center;
  margin: auto;
  /* border: 1px ridge rgba(var(--bs-body-color-rgb), 0.2); */
  border-radius: 5px;
}

.linkConteiner:hover {
  background-image: linear-gradient(to right, rgb(var(--bs-header-bg-rgb)), rgba(var(--bs-body-color-rgb), 0.2));
}
.HomeNavigationLinks {
  display: flex;
  width: 20%;
  height: 60%;
  border-left: 1px solid rgb(var(--bs-body-color-rgb));
  text-decoration: none;
  font-style: normal;
  align-items: center;
  justify-content: center;
}

.icon {
  width: 15%;
  height: 100%;
  align-content: center;
  text-align: center;
}

.HomeNavigationLinks:first-child {
  border: none;
}


.links {
  text-decoration: none;
  font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  font-weight: bold;
  color: rgb(var(--bs-body-color-rgb));
}

.lower {
  width: 100%;
  height: 20%;
  background-color: rgb(var(--bs-body-bg-rgb));
  border-radius: 0 10px 0 0;
}

.headerHomePagerightupper {
  width: 100%;
  height: 80%;
  align-content: center;
}
.headerHomePagerightlower {
  width: 100%;
  height: fit-content;
  text-align: end;
}

.colorMOdeButton {
  border: none;
  background-color: rgb(var(--bs-btn-bg-b-rgb));
  border-radius: 5px;
  margin-right: 10px;
}

.colorMOdeButton:hover {
  background-color: rgb(var(--bs-btn-hover-bg-rgb));
  cursor: pointer;
}

</style>