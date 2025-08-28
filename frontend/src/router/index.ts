import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/books',
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () => import('../views/DashboardView.vue'),
  },
  {
    path: '/books',
    name: 'MyBooks',
    component: () => import('../views/MyBooksView.vue'),
  },
  // Analytics and Settings routes removed (out of scope)
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
