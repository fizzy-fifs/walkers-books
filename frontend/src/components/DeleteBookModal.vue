```vue
<script setup lang="ts">
import { ref } from 'vue'
import { bookService } from '../services/bookService'
import type { Book } from '../types/book'
import { ElMessage } from 'element-plus'

const props = defineProps<{ visible: boolean; book: Book | null }>()
const emits = defineEmits(['close', 'success'])

const loading = ref(false)

const handleDelete = async () => {
  if (!props.book) return
  loading.value = true
  try {
    await bookService.deleteBook(props.book.id)
    ElMessage.success('Book deleted')
    emits('success')
    emits('close')
  } catch (e: any) {
    ElMessage.error(e?.message || 'Failed to delete book')
  } finally {
    loading.value = false
  }
}

const handleClose = () => {
  emits('close')
}
</script>

<template>
  <el-dialog :model-value="props.visible" title="Delete Book" width="420px" @close="handleClose" :close-on-click-modal="false">
    <div v-if="props.book">
      <p>Are you sure you want to delete "<strong>{{ props.book.title }}</strong>" by {{ props.book.author }}?</p>
    </div>
    <template #footer>
      <el-button @click="handleClose">Cancel</el-button>
      <el-button type="danger" :loading="loading" @click="handleDelete">Delete</el-button>
    </template>
  </el-dialog>
</template>

<style scoped>
</style>

```