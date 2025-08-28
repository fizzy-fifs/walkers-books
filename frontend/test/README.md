This folder contains unit tests for the frontend.

Run tests:

```bash
cd frontend
npm install
npm test
```

Notes:
- Tests use Vitest and @vue/test-utils.
- Element Plus components are stubbed in tests; these are shallow unit tests that mock the `bookService`.
