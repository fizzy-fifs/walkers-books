```vue
<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'
import type { Book } from '../types/book'

const props = defineProps<{ visible: boolean; book: Book | null }>()
const emits = defineEmits(['close'])

const handleClose = () => {
  emits('close')
}
</script>

<template>
  <el-dialog :model-value="props.visible" title="Book Details" width="520px" @close="handleClose" :close-on-click-modal="false">
    <div v-if="props.book" class="view-body">
      <div class="view-header">
        <h3>{{ props.book.title }}</h3>
        <div class="author">by {{ props.book.author }}</div>
      </div>
      <div class="view-main">
        <div class="cover">
          <img v-if="props.book.coverImageUrl" :src="props.book.coverImageUrl" alt="cover" />
          <div v-else class="no-cover">No cover</div>
        </div>
        <div class="meta">
          <div><strong>ISBN:</strong> {{ props.book.isbn }}</div>
          <div><strong>Rating:</strong> <el-rate :model-value="props.book.rating" disabled show-score allow-half /></div>
            <div v-if="props.book.comments && props.book.comments.length"><strong>Comments:</strong>
              <div class="comments-list">
                <p v-for="(c, i) in props.book.comments" :key="i" class="comment">{{ c }}</p>
              </div>
            </div>
        </div>
      </div>
    </div>
    <template #footer>
      <el-button @click="handleClose">Close</el-button>
    </template>
  </el-dialog>
</template>

<style scoped>
.view-body {
  display: flex;
  flex-direction: column;
  gap: 12px;
}
.view-main {
  display: flex;
  gap: 16px;
  align-items: flex-start;
}
.cover img {
  width: 120px;
  height: 160px;
  object-fit: cover;
  border-radius: 6px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.08);
}
.no-cover {
  width: 120px;
  height: 160px;
  display:flex;align-items:center;justify-content:center;
  background:#f3f6fb;border-radius:6px;color:#6b7280
}
.meta { display:flex;flex-direction:column;gap:8px }
.comment { margin:6px 0 0 }
</style>

```