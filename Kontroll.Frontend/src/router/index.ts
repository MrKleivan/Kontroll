import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UserHomeView from '@/views/UserHomeView.vue'
import FixedExpensesView from '@/views/FixedExpensesView.vue'
import TransactionView from '@/views/TransactionView.vue'
import EconomyView from '@/views/EconomyView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
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
              path: 'FixedExpense', 
              name: 'FixedExpense', 
              component: FixedExpensesView,
            },
          ]
        },
        {
          path: 'Property', 
          name: 'Property',
          component: EconomyView,
          
        }
      ]
    },
  ],
})

export default router
