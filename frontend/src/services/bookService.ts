import type { Book, CreateBookRequest, UpdateBookRequest } from '../types/book.ts'
import type { PagedResponse } from '../types/paged-response.ts';

const API_BASE = '/api/v1.0/books'

export const bookService = {
  async getBooks(params?: { search?: string; sortBy?: string; page?: number; pageSize?: number }): Promise<PagedResponse<Book>> {
    const url = new URL(API_BASE, window.location.origin)
    if (params) {
      if (params.search) url.searchParams.append('search', params.search)
      if (params.sortBy) url.searchParams.append('sortBy', params.sortBy)
      if (params.page) url.searchParams.append('page', params.page.toString())
      if (params.pageSize) url.searchParams.append('pageSize', params.pageSize.toString())
    }
    try {
      const res = await fetch(url.toString())
      if (!res.ok) {
        // try to parse error body for a meaningful message
        let msg = `Request failed with status ${res.status}`
        try {
          const body = await res.json()
          if (body && (body.message || body.error)) msg = body.message ?? body.error
        } catch {
          // ignore JSON parse errors
        }
        const err: any = new Error(msg)
        err.status = res.status
        throw err
      }
      return res.json()
    } catch (e: any) {
      // Network or parsing error
      if (e instanceof Error) throw e
      throw new Error('Failed to fetch books')
    }
  },

  async getBookById(id: string): Promise<Book> {
    const res = await fetch(`${API_BASE}/${id}`)
    if (!res.ok) {
      let msg = `Request failed with status ${res.status}`
      try { const body = await res.json(); if (body && (body.message || body.error)) msg = body.message ?? body.error } catch {}
      const err: any = new Error(msg); err.status = res.status; throw err
    }
    return res.json()
  },

  async createBook(data: CreateBookRequest): Promise<Book> {
    const res = await fetch(`${API_BASE}/create`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    })
    if (!res.ok) {
      let msg = `Request failed with status ${res.status}`
      try { const body = await res.json(); if (body && (body.message || body.error)) msg = body.message ?? body.error } catch {}
      const err: any = new Error(msg); err.status = res.status; throw err
    }
    return res.json()
  },

  async updateBook(data: UpdateBookRequest): Promise<Book> {
    const res = await fetch(`${API_BASE}/update`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    })
    if (!res.ok) {
      let msg = `Request failed with status ${res.status}`
      try { const body = await res.json(); if (body && (body.message || body.error)) msg = body.message ?? body.error } catch {}
      const err: any = new Error(msg); err.status = res.status; throw err
    }
    return res.json()
  },

  async deleteBook(id: string): Promise<void> {
    const res = await fetch(`${API_BASE}/${id}`, { method: 'DELETE' })
    if (!res.ok) {
      let msg = `Request failed with status ${res.status}`
      try { const body = await res.json(); if (body && (body.message || body.error)) msg = body.message ?? body.error } catch {}
      const err: any = new Error(msg); err.status = res.status; throw err
    }
  },
}
