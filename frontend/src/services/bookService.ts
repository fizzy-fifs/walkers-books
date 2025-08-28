import type { Book, CreateBookRequest, UpdateBookRequest } from '../types/book'

const API_BASE = '/api/v1.0/books'

export const bookService = {
  async getBooks(params?: { search?: string; sortBy?: string; page?: number; pageSize?: number }): Promise<Book[]> {
    const url = new URL(API_BASE, window.location.origin)
    if (params) {
      if (params.search) url.searchParams.append('search', params.search)
      if (params.sortBy) url.searchParams.append('sortBy', params.sortBy)
      if (params.page) url.searchParams.append('page', params.page.toString())
      if (params.pageSize) url.searchParams.append('pageSize', params.pageSize.toString())
    }
    const res = await fetch(url.toString())
    if (!res.ok) throw new Error('Failed to fetch books')
    return res.json()
  },

  async getBookById(id: string): Promise<Book> {
    const res = await fetch(`${API_BASE}/${id}`)
    if (!res.ok) throw new Error('Book not found')
    return res.json()
  },

  async createBook(data: CreateBookRequest): Promise<Book> {
    const res = await fetch(`${API_BASE}/create`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    })
    if (!res.ok) throw new Error('Failed to create book')
    return res.json()
  },

  async updateBook(data: UpdateBookRequest): Promise<Book> {
    const res = await fetch(`${API_BASE}/update`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    })
    if (!res.ok) throw new Error('Failed to update book')
    return res.json()
  },

  async deleteBook(id: string): Promise<void> {
    const res = await fetch(`${API_BASE}/${id}`, { method: 'DELETE' })
    if (!res.ok) throw new Error('Failed to delete book')
  },
}
