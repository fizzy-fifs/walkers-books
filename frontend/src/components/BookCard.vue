<script setup lang="ts">
import type { PropType } from 'vue'
import type { Book } from '../types/book'

const props = defineProps({
  book: {
    type: Object as PropType<Book>,
    required: true,
  },
})
</script>

<template>
  <div class="book-card">
    <div class="book-icon">
      <el-icon><i class="el-icon-notebook-1"></i></el-icon>
    </div>
    <div class="book-info">
      <div class="book-title">{{ book.title }}</div>
      <div class="book-meta">
        <span class="book-author">{{ book.author }}</span>
        <span class="book-isbn">ISBN: {{ book.isbn }}</span>
        <span v-if="book.hasNotes" class="book-notes"><el-icon><i class="el-icon-document"></i></el-icon> Notes</span>
      </div>
    </div>
    <div class="book-rating">
      <el-rate v-model="book.rating" disabled show-score allow-half />
    </div>
    <div class="book-actions">
      <el-button size="small" type="primary">Edit</el-button>
      <el-button size="small" type="info">View</el-button>
      <el-button size="small" type="danger">Delete</el-button>
    </div>
  </div>
</template>

<style scoped>
.book-card {
  display: flex;
  align-items: center;
  background: #f6f8fc;
  border-radius: 12px;
  box-shadow: 0 2px 8px 0 rgba(0,0,0,0.03);
  padding: 18px 24px;
  gap: 24px;
  transition: box-shadow 0.2s;
  width: 100%;
}
.book-card:hover { box-shadow: 0 4px 16px 0 rgba(37,99,235,0.10); }
.book-icon { font-size: 2.2rem; color: #2563eb; margin-right: 12px; }
.book-info { flex: 2; display: flex; flex-direction: column; gap: 4px; }
.book-title { font-size: 1.1rem; font-weight: 600; color: #222b45; }
.book-meta { display: flex; gap: 18px; font-size: 0.95rem; color: #6b7280; align-items: center; }
.book-notes { color: #67c23a; display: flex; align-items: center; gap: 2px; }
.book-rating { flex: 1; display: flex; justify-content: flex-end; min-width: 120px; }
.book-actions { display: flex; gap: 8px; }

@media (max-width: 700px) {
  .book-card { flex-direction: column; align-items: flex-start; gap: 10px; padding: 14px 10px; }
  .book-info, .book-rating, .book-actions { width: 100%; justify-content: flex-start; }
  .book-meta { flex-direction: column; gap: 2px; align-items: flex-start; }
}
</style>
