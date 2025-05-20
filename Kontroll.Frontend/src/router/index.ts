import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UserHomeView from '@/views/UserHomeView.vue'
import FixedExpensesView from '@/views/FixedExpensesView.vue'
import TransactionView from '@/views/TransactionView.vue'

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
      name: 'userhome',
      component: UserHomeView,
      children: [
        { path: 'fixed', component: FixedExpensesView },
        { path: 'transaction', component: TransactionView },
      ]
    },
  ],
})

export default router
