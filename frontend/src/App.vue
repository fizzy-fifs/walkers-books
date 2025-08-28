<script setup lang="ts">
import Sidebar from './components/Sidebar.vue'
import MyBooksView from './views/MyBooksView.vue';
</script>

<template>
  
  <div class="app-layout">
    <Sidebar />
    <div class="main-content-bg">
      <div class="main-content-card">
        <RouterView />
      </div>
    </div>
  </div>
</template>

<style scoped>
.app-layout {
  display: flex;
  min-height: 100vh;
  max-width: 100%;
  background: #ffffff;
}
.main-content-bg {
  flex: 1;
  background: #ffffff;
  display: flex;
  justify-content: center;
  display: flex;
  flex-direction: column;
  min-height: 100%; /* fill the available height */
  overflow: hidden; /* clip children/shadows so they don't escape rounded corners */
  width: 100%;
  /* max-width: 1100px; */
  overflow: hidden; /* clip children/shadows so they don't escape rounded corners */
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px 0 rgba(0,0,0,0.07);
  padding: 32px;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  min-height: 100%; /* fill the available height */
}

@media (max-width: 900px) {
  .main-content-bg {
    padding: 16px;
  }
  .main-content-card {
    border-radius: 12px;
    padding: 20px;
    max-width: 100%;
  }
}
</style>
