<script setup>
import { colorMode, toggleColorMode } from './ColorMode';
import LifeControllLogo from './LifeControllLogo.vue';
import { MyLinks } from './MyLinks';
</script>

<template>
  <header class="headerHomePage">
    <div class="headerHomePageLeft">
      <div class="upper">
        <div class="LogoheaderHomePage">
          <LifeControllLogo /> 
        </div>
      </div>
      <div class="lower"></div>
    </div>
    <div class="headerHomePageCenter">
      <div class="upper"></div>
      <div class="lower">
        <svg width="10%" height="100%">
          <rect width="200" height="200" fill="rgba(var(--bs-header-bg-rgb), 0)" />
  
          <polygon points="0,0 100,0 300,200" fill="rgba(var(--bs-header-bg-rgb), 0.8)" />
        </svg>
      </div>
    </div>
    <div class="headerHomePageright">
      <div class="headerHomePagerightupper">
        <nav class="homePageNavBar">
          <div v-for="link in MyLinks.HomePageLinks" class="HomeNavigationLinks" :key="link.name">
            <div class="linkConteiner">
              <RouterLink class="links" :to="{name: link.name}">{{link.label}}</RouterLink>
              <span v-if="link.svg" v-html="link.svg" class="icon"></span>
            </div>
          </div>
        </nav>
      </div>
      <div class="headerHomePagerightlower">
        <div>
          Fargevelger: <button class="colorMOdeButton" @click="toggleColorMode">{{ colorMode }}</button>
        </div>
      </div>
    </div>
  </header>
  <body>
    <router-view />
  </body>
</template>

<style scoped>

.headerHomePage {
  display: flex;
  width: 100%;
  height: 100px;
}

.headerHomePageLeft {
  width: 30%;
  height: 100%;
}

.LogoheaderHomePage {
  display: flex;
  width: 80%;
  height: 100%;
  margin: auto;
  align-items: center;
}

.headerHomePageCenter {
  width: 20%;
  height: 100%;
}


.headerHomePageright {
  width: 50%;
  height: 100%;
  background-color: rgba(var(--bs-header-bg-rgb), 0.8);
  /* border-radius: 0 0 0 10px; */
}

.upper {
  width: 100%;
  height: 80%;
  background-color: rgba(var(--bs-header-bg-rgb), 0.8);
  box-shadow: var(--bs-header-shadow);
}

.lower {
  width: 100%;
  height: 20%;
  text-align: end;
  /* border-radius: 0 10px 0 0; */
}

.homePageNavBar {
  position: relative;
  display: flex;
  width: 90%;
  height: 50%;
  margin: auto;
  padding-left: 20px;
  padding-bottom: 10px;
  align-items: center;
  justify-content: center;
  /* background-image: linear-gradient(to left, rgb(var(--bs-header-bg-rgb)), rgba(var(--bs-body-color-rgb), 0.1), rgb(var(--bs-header-bg-rgb))); */
  /* border-radius: 5px; */
}

.homePageNavBar::after {
  content: '';
  display: block;
  height: 10px;
  background: linear-gradient(to bottom, rgba(var(--bs-body-color-rgb), 0.2), rgb(var(--bs-header-bg-rgb)));
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  border-radius: 0 0 15px 15px;
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

.headerHomePagerightupper {
  width: 100%;
  height: 80%;
  align-content: center;
}

.headerHomePagerightlower {
  width: 100%;
  height: 20%;
  text-align: end;
  box-shadow: var(--bs-header-shadow);
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