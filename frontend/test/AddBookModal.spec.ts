import { describe, it, expect, vi, beforeEach } from 'vitest'

// mock service before importing the component so the component receives the mocked module
vi.mock('../src/services/bookService', () => ({
  bookService: {
    createBook: vi.fn(),
  }
}))

import { mount } from '@vue/test-utils'
import AddBookModal from '../src/components/AddBookModal.vue'
import { bookService } from '../src/services/bookService'

// minimal stubs for Element Plus components
const globalStubs = {
  'el-dialog': { template: '<div><slot /></div>' },
  'el-form': {
    template: '<form><slot /></form>',
    methods: {
  validate(cb: any) { return new Promise((resolve) => { const r = cb(true); if (r && typeof r.then === 'function') r.then(() => resolve(undefined)); else resolve(undefined) }) },
      clearValidate() {},
    }
  },
  'el-form-item': { template: '<div><slot /></div>' },
  'el-input': { template: '<input />', props: ['modelValue'], emits: ['update:modelValue'] },
  'el-rate': { template: '<div />', props: ['modelValue'], emits: ['update:modelValue'] },
  'el-button': { template: '<button><slot /></button>' },
}

describe('AddBookModal', () => {
  beforeEach(() => {
    ;(bookService.createBook as any).mockReset()
  })

  it('submits the form and emits success on create', async () => {
    ;(bookService.createBook as any).mockResolvedValue({ id: '1', title: 'T', author: 'A', isbn: '1234567890' })

    const wrapper = mount(AddBookModal as any, {
      props: { visible: true },
      global: { stubs: globalStubs },
    })

    const vm: any = wrapper.vm
    vm.form.title = 'Test Title'
    vm.form.author = 'Test Author'
    vm.form.isbn = '1234567890'

  // stub formRef API used by component: set validate on the existing ref value
  const internal = (wrapper.vm as any).$.setupState
  if (internal && internal.formRef) {
    internal.formRef.value = { validate: (cb: any) => { return new Promise((resolve) => { const r = cb(true); if (r && typeof r.then === 'function') r.then(() => resolve(undefined)); else resolve(undefined) }) }, clearValidate: () => {} }
  } else {
    ;(wrapper.vm as any).$.setupState.formRef = { value: { validate: (cb: any) => { return new Promise((resolve) => { const r = cb(true); if (r && typeof r.then === 'function') r.then(() => resolve(undefined)); else resolve(undefined) }) }, clearValidate: () => {} } }
  }

  // call the submit handler exposed on the instance
  await (wrapper.vm as any).handleSubmit()

    expect(bookService.createBook).toHaveBeenCalled()
    expect(wrapper.emitted('success')).toBeTruthy()
  })
})
