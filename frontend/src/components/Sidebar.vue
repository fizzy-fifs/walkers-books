<template>
  <div class="sidebar">
    <div class="logo">
      <el-icon><i class="el-icon-menu"></i></el-icon>
      <span class="logo-text">BookHub</span>
    </div>
    <el-menu
      default-active="$route.path"
      class="el-menu-vertical-demo"
      router
      background-color="#2563eb"
      text-color="#fff"
      active-text-color="#ffd04b"
      style="border-right: none;"
    >
      <el-sub-menu index="/dashboard">
        <template #title>
          <el-icon><i class="el-icon-s-home"></i></el-icon>
          <span>Dashboard</span>
        </template>
        <el-menu-item index="/books">
          <el-icon><i class="el-icon-notebook-1"></i></el-icon>
          <span>My Books</span>
        </el-menu-item>
      </el-sub-menu>
    </el-menu>
  </div>
</template>

<script setup lang="ts">
import { ElIcon } from 'element-plus'
</script>

<style scoped>
.sidebar {
  width: 260px;
  min-height: 100vh;
  background: #2563eb;
  display: flex;
  flex-direction: column;
  align-items: stretch;
}
.logo {
  display: flex;
  align-items: center;
  padding: 32px 24px 24px 24px;
  font-size: 1.5rem;
  font-weight: bold;
  color: #fff;
}
.logo-text {
  margin-left: 12px;
  letter-spacing: 1px;
}
.el-menu-vertical-demo {
  background: transparent;
  border-right: none;
}

@media (max-width: 900px) {
  .sidebar {
    width: 72px; /* collapse to icons only */
    position: sticky;
    top: 0;
  }
  .logo-text {
    display: none;
  }
  .el-menu-vertical-demo span {
    display: none; /* hide menu labels */
  }
}
</style>
