<template>
  <el-menu
    default-active="$route.path"
    class="el-menu-vertical-demo"
    router
    background-color="#2d3a4b"
    text-color="#fff"
    active-text-color="#ffd04b"
    style="height: 100vh; width: 220px; border-right: none;"
  >
    <el-menu-item index="/dashboard">
      <span>Dashboard</span>
    </el-menu-item>
    <el-menu-item index="/books">
      <span>My Books</span>
    </el-menu-item>
    <el-menu-item index="/analytics">
      <span>Analytics</span>
    </el-menu-item>
    <el-menu-item index="/settings">
      <span>Settings</span>
    </el-menu-item>
  </el-menu>
</template>

<script setup lang="ts">
</script>

<style scoped>
.el-menu-vertical-demo {
  min-height: 100vh;
  border-right: none;
}
</style>
