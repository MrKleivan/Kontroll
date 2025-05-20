<script setup>
import { RouterLink, RouterView } from 'vue-router';
import LoggedInnHeader from './LoggedInnHeader.vue';
import { ref } from 'vue';

const watching = ref(null);

</script>

<template>
<LoggedInnHeader/>
<main>
    <div class="contentWindow">
        <div v-if="watching == null" class="MainUserNavConteiner">
            <RouterLink class="MainUserNav" to="/UserHome/transaction">Transaksjoner</RouterLink>
            <RouterLink class="MainUserNav" to="/UserHome/fixed">Faste Utgifter</RouterLink>
            <RouterLink class="MainUserNav" to="/UserHome/">Fakturaoversikt</RouterLink>
            <RouterLink class="MainUserNav" to="/UserHome/">Leverandøroversikt</RouterLink>
        </div>
        
        <div class="contentWindowHeader">
            <nav class="contentWindowNavBar">
                <RouterLink class="contentWindowNav" to="/UserHome/transaction">Transaksjoner</RouterLink>
                
            </nav>
        </div>
        <div class="contentWindowContent">
        <router-view />
        </div>
    </div>
    <div class="infoStripe">
        <div class="UserInfoConteiner">
            <div class="UserInfoConteinerHeader">
            <span>Per Ivar Kleivan</span>
            </div>
            <div class="UserInfoConteinerContent">
            <span></span>
            </div>
        </div>
        <br />
        <div class="UserInfoConteiner">
            
        </div>
    </div>
</main>
</template>