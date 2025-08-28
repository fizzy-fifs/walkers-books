<script setup lang="ts">
import Sidebar from './components/Sidebar.vue'
</script>

<template>
  <div style="display: flex; min-height: 100vh;">
    <Sidebar />
    <main style="flex: 1; background: #f5f6fa; padding: 32px;">
      <router-view />
    </main>
  </div>
</template>

<style scoped>
</style>
