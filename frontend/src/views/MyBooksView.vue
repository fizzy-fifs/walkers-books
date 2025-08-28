<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import BookCard from '../components/BookCard.vue'
import { bookService } from '../services/bookService'
import type { Book } from '../types/book'
import { pa } from 'element-plus/es/locales.mjs'

const books = ref<Book[]>([])
const loading = ref(false)
const error = ref<string | null>(null)

// Search, sort, pagination state
const search = ref('')
const sortBy = ref('title')
const page = ref(1)
const pageSize = ref(10)
const total = ref(0)

const fetchBooks = async () => {
  loading.value = true
  error.value = null
  try {
    const response = await bookService.getBooks({
      search: search.value,
      sortBy: sortBy.value,
      page: page.value,
      pageSize: pageSize.value,
    })
    console.log('Response:', response);
  books.value = response.items
  page.value = response.page
  pageSize.value = response.pageSize
  total.value = response.totalCount
  } catch (e: any) {
    // Convert technical errors into user-friendly messages
    const raw = e?.message || ''
    const friendly = (() => {
      const msg = String(raw).toLowerCase()
      if (msg.includes('failed to fetch') || msg.includes('ecoff') || msg.includes('ecoff') || msg.includes('econ') || msg.includes('network')) {
        return "Cannot reach the server. Check your network connection or start the backend and try again."
      }
      if (msg.includes('timeout')) return 'The server is taking too long to respond. Please try again later.'
      if (msg.includes('unauthorized') || msg.includes('401')) return "You're not authorized to perform this action. Please sign in with the right account."
      if (msg.includes('forbid') || msg.includes('403')) return "You don't have permission to access this resource."
      if (msg.includes('not found') || msg.includes('404')) return 'Requested resource not found.'
      // if server provided a friendly message already, prefer it (shorten long messages)
      if (raw && raw.length < 200) return raw
      return 'An unexpected error occurred. Please try again.'
    })()
    error.value = friendly
  } finally {
    loading.value = false
  }
}

onMounted(fetchBooks)
watch([search, sortBy, page], fetchBooks)

const handleSearch = () => {
  page.value = 1
  fetchBooks()
}

const clearError = () => {
  error.value = null
}

const retry = () => {
  clearError()
  fetchBooks()
}
</script>

<template>
  <div class="my-books-view">
    <div class="header-row">
      <div>
        <h2>My Books</h2>
        <p>Track, rate, and manage your book collection.</p>
      </div>
      <el-button type="primary">Add Book</el-button>
    </div>
  <div class="search-row">
      <el-input
        v-model="search"
        placeholder="Search by title or author..."
        class="search-input"
        @keyup.enter="handleSearch"
        clearable
      />
      <el-select v-model="sortBy" placeholder="Sort by" class="sort-select">
        <el-option label="Title" value="title" />
        <el-option label="Author" value="author" />
        <el-option label="Rating" value="rating" />
      </el-select>
    </div>
    <div v-if="loading" class="loading-row">
      <el-skeleton :rows="4" animated />
    </div>
    <div v-else class="book-list">
      <BookCard v-for="book in books" :key="book.id" :book="book" />
      <div v-if="books.length === 0 && !error" class="no-books">No books found.</div>
    </div>
    <el-pagination
      v-model:current-page="page"
      :page-size="pageSize"
      :total="total"
      layout="prev, pager, next"
      class="pagination"
    />
    <div v-if="error" class="error-alert">
      <el-alert :title="error" type="error" show-icon closable @close="clearError">
        <template #description>
          <div style="display:flex; align-items:center; gap:12px;">
            <el-button size="small" type="primary" @click="retry">Retry</el-button>
          </div>
        </template>
      </el-alert>
    </div>
  </div>
</template>

<style scoped>
.my-books-view {
  width: 100%;
  min-height: 100%;
  display: flex;
  flex-direction: column;
}
.header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}
.search-row {
  display: flex;
  gap: 16px;
  margin-bottom: 24px;
}
.search-input {
  flex: 1;
  max-width: 350px;
}
.sort-select {
  width: 180px;
}
.book-list {
  display: flex;
  flex-direction: column;
  gap: 18px;
  flex: 1 1 auto; /* allow list to grow and shrink */
  overflow: auto; /* scroll inside the card */
}
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
.book-card:hover {
  box-shadow: 0 4px 16px 0 rgba(37,99,235,0.10);
}
.book-icon {
  font-size: 2.2rem;
  color: #2563eb;
  margin-right: 12px;
}
.book-info {
  flex: 2;
  display: flex;
  flex-direction: column;
  gap: 4px;
}
.book-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: #222b45;
}
.book-meta {
  display: flex;
  gap: 18px;
  font-size: 0.95rem;
  color: #6b7280;
  align-items: center;
}
.book-notes {
  color: #67c23a;
  display: flex;
  align-items: center;
  gap: 2px;
}
.book-rating {
  flex: 1;
  display: flex;
  justify-content: flex-end;
  min-width: 120px;
}
.book-actions {
  display: flex;
  gap: 8px;
}
.no-books {
  text-align: center;
  color: #888;
  margin-top: 32px;
}
.loading-row {
  margin: 32px 0;
}
.pagination {
  margin-top: 32px;
  align-self: flex-end;
}
.error-alert {
  margin-top: 16px;
}
@media (max-width: 1100px) {
  .main-content-card {
    width: 100%;
    padding: 24px 8px;
  }
}
@media (max-width: 700px) {
  .my-books-view {
    padding: 0;
  }
  .main-content-card {
    width: 100%;
    padding: 8px 2px;
    min-height: unset;
  }
  .book-card {
    flex-direction: column;
    align-items: flex-start;
    gap: 10px;
    padding: 14px 10px;
  }
  .book-info, .book-rating, .book-actions {
    width: 100%;
    justify-content: flex-start;
  }
  .book-meta {
    flex-direction: column;
    gap: 2px;
    align-items: flex-start;
  }
}
</style>
