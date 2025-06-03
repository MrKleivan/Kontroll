import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UserHomeView from '@/views/UserHomeView.vue'
import FixedExpensesView from '@/views/FixedExpensesView.vue'
import TransactionView from '@/views/TransactionView.vue'
import EconomyView from '@/views/EconomyView.vue'
import SingleTransactionView from '@/views/SingleTransactionView.vue'
import Logout from '@/components/LogOut.vue';
import Suppliers from '@/components/Suppliers.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      children: [
        {
          path: 'homeserarch',
          name: 'HomeSearch',
          component: EconomyView,
        },
        {
          path: 'about',
          name: 'About',
          component: EconomyView,
        },
        {
          path: 'login',
          name: 'Login',
          component: UserHomeView,
        },
      ]
    },
    {
      path: '/UserHome',
      name: 'UserHome',
      component: UserHomeView,
      children: [
        { 
          path: 'Economy', 
          name: 'Economy',
          component: EconomyView, 
          children: [
            {
              path: 'Transaction', 
              name: 'Transaction', 
              component: TransactionView,
            },
            {
              path: 'SingleTransaction/:id', 
              name: 'SingleTransaction', 
              component: SingleTransactionView,
            },
            {
              path: 'FixedExpense', 
              name: 'FixedExpense', 
              component: FixedExpensesView,
            },
            {
              path: 'Suppliers',
              name: 'Suppliers',
              component: Suppliers,
            }
          ]
        },
        {
          path: 'Property', 
          name: 'Property',
          component: EconomyView,
        },
      ]
    },
    {
      path: '/logout',
      name: 'LogOut',
      component: Logout,
    }
  ],
})

export default router
