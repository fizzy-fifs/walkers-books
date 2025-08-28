<script setup lang="ts">
import { ref, watch } from 'vue'
import { bookService } from '../services/bookService'
import type { CreateBookRequest } from '../types/book'
import { ElMessage } from 'element-plus'

const props = defineProps<{ visible: boolean }>()
const emits = defineEmits(['close', 'success'])

const form = ref<CreateBookRequest>({
  title: '',
  author: '',
  isbn: '',
  rating: undefined,
  comment: '',
  coverImageUrl: '',
})

const loading = ref(false)
const rules = {
  title: [
    { required: true, message: 'Title is required', trigger: 'blur' },
    { min: 2, max: 100, message: 'Title must be 2-100 characters', trigger: 'blur' },
  ],
  author: [
    { required: true, message: 'Author is required', trigger: 'blur' },
    { min: 2, max: 60, message: 'Author must be 2-60 characters', trigger: 'blur' },
  ],
  isbn: [
    { required: true, message: 'ISBN is required', trigger: 'blur' },
    { min: 10, max: 13, message: 'ISBN must be 10-13 characters', trigger: 'blur' },
  ],
  rating: [
    { type: 'number',  max: 5, message: 'Rating must be 1-5', trigger: 'change' },
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

const handleSubmit = async () => {
  // Validate before submit
  await formRef.value.validate(async (valid: boolean) => {
    if (!valid) return
    loading.value = true
    try {
      await bookService.createBook(form.value)
      ElMessage.success('Book added!')
      emits('success')
      handleClose()
    } catch (e: any) {
      ElMessage.error(e?.message || 'Failed to add book')
    } finally {
      loading.value = false
    }
  })
}

const handleClose = () => {
  emits('close')
  // Reset form on close
  form.value = { title: '', author: '', isbn: '', rating: undefined, comment: '', coverImageUrl: '' }
  if (formRef.value) formRef.value.clearValidate()
}

watch(() => props.visible, (val) => {
  if (!val) handleClose()
})
</script>

<template>
  <el-dialog :model-value="props.visible" title="Add Book" width="480px" @close="handleClose" :close-on-click-modal="false">
    <el-form :model="form" :rules="rules" ref="formRef" label-width="100px" status-icon>
      <el-form-item label="Title" prop="title">
        <el-input v-model="form.title" maxlength="100" show-word-limit />
      </el-form-item>
      <el-form-item label="Author" prop="author">
        <el-input v-model="form.author" maxlength="60" show-word-limit />
      </el-form-item>
      <el-form-item label="ISBN" prop="isbn">
        <el-input v-model="form.isbn" minlength="10" maxlength="13" show-word-limit />
      </el-form-item>
      <el-form-item label="Rating" prop="rating">
        <el-rate v-model="form.rating" :max="5" />
      </el-form-item>
      <el-form-item label="Comment" prop="comment">
        <el-input v-model="form.comment" type="textarea" maxlength="300" show-word-limit />
      </el-form-item>
      <el-form-item label="Cover URL" prop="coverImageUrl">
        <el-input v-model="form.coverImageUrl" maxlength="200" show-word-limit />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="handleClose">Cancel</el-button>
      <el-button type="primary" :loading="loading" @click="handleSubmit">Add Book</el-button>
    </template>
  </el-dialog>
</template>

<style scoped>
</style>
