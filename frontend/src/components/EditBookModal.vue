<script setup lang="ts">
import { ref, watch } from 'vue'
import { bookService } from '../services/bookService'
import type { Book, UpdateBookRequest } from '../types/book'
import { ElMessage } from 'element-plus'

const props = defineProps<{ visible: boolean; book: Book | null }>()
const emits = defineEmits(['close', 'success'])

const form = ref<UpdateBookRequest>({
  bookId: '',
  rating: undefined,
  comment: '',
})

const loading = ref(false)
const rules = {
  rating: [
    { type: 'number', min: 1, max: 5, message: 'Rating must be 1-5', trigger: 'change' },
  ],
  comment: [
    { validator: (rule: any, value: string, callback: any) => {
      if (form.value.rating && !value) {
        callback(new Error('Comment is required if rating is supplied'))
      } else if (value && value.toLowerCase().includes('horrible')) {
        callback(new Error('Comment must not contain the word "horrible"'))
      } else if (value && (value.length < 2 || value.length > 300)) {
        callback(new Error('Comment must be 2-300 characters'))
      } else {
        callback()
      }
    }, trigger: 'blur' },
  ],
}

const formRef = ref()

watch(() => props.visible, (val) => {
  if (val && props.book) {
    form.value = {
      bookId: props.book.id,
      rating: props.book.rating,
      comment: '',
    }
    if (formRef.value) formRef.value.clearValidate()
  }
})

const handleSubmit = async () => {
  await formRef.value.validate(async (valid: boolean) => {
    if (!valid) return
    loading.value = true
    try {
      await bookService.updateBook(form.value)
      ElMessage.success('Book updated!')
      emits('success')
      handleClose()
    } catch (e: any) {
      ElMessage.error(e?.message || 'Failed to update book')
    } finally {
      loading.value = false
    }
  })
}

const handleClose = () => {
  emits('close')
  if (formRef.value) formRef.value.clearValidate()
}
</script>

<template>
  <el-dialog :model-value="props.visible" title="Edit Book" width="420px" @close="handleClose" :close-on-click-modal="false">
    <el-form :model="form" :rules="rules" ref="formRef" label-width="100px" status-icon>
      <el-form-item label="Rating" prop="rating">
        <el-rate v-model="form.rating" :max="5" />
      </el-form-item>
      <el-form-item label="Comment" prop="comment">
        <el-input v-model="form.comment" type="textarea" maxlength="300" show-word-limit />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="handleClose">Cancel</el-button>
      <el-button type="primary" :loading="loading" @click="handleSubmit">Save</el-button>
    </template>
  </el-dialog>
</template>

<style scoped>
</style>
