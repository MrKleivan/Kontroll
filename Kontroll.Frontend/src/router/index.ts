import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UserHomeView from '@/views/UserHomeView.vue'
import FixedExpensesView from '@/views/FixedExpensesView.vue'
import TransactionView from '@/views/TransactionView.vue'
import EconomyView from '@/views/EconomyView.vue'
import SingleTransactionView from '@/views/SingleTransactionView.vue'
import Logout from '@/components/LogOut.vue'
import Suppliers from '@/components/Suppliers.vue'
import MyProfile from '@/components/MyProfile.vue'
import AccountingView from '@/views/AccountingView.vue'
import Loan from '@/components/Loan.vue'
import Savings from '@/components/Savings.vue'
import Subscriptions from '@/components/Subscriptions.vue'
import Account from '@/components/Account.vue'

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
          path: 'MySettings',
          name: 'MySettings',
          component: EconomyView, 
        },
        { 
          path: 'Economy', 
          name: 'Economy',
          component: EconomyView, 
          children: [
            {
              path: 'Accounting',
              name: 'Accounting',
              component: AccountingView,
              children: [
                {
                  path: 'Account', 
                  name: 'Account', 
                  component: Account,
                },
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
                  path: 'Status', 
                  name: 'Status', 
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
                },
              ]
            },
            {
              path: 'Loan', 
              name: 'Loan', 
              component: TransactionView,
            },
            {
              path: 'Savings', 
              name: 'Savings', 
              component: TransactionView,
            },
            {
              path: 'Subscriptions', 
              name: 'Subscriptions', 
              component: TransactionView,
            },
          ]
        },
        {
          path: 'Property', 
          name: 'Property',
          component: EconomyView,
        },
        {
          path: 'EducationAndCareer', 
          name: 'EducationAndCareer',
          component: EconomyView,
        },
        {
          path: 'FreePlan', 
          name: 'FreePlan',
          component: EconomyView,
        },
      ]
    },
    {
      path: '/myProfile',
      name: 'MyProfile',
      component: MyProfile, 
    },
    {
      path: '/logout',
      name: 'LogOut',
      component: Logout,
    }
  ],
})

export default router
