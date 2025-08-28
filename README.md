
# Walkers Books — Book Tracker

This repository contains a small full-stack book-tracking application used for a technical assessment.

- Frontend: Vue 3 + TypeScript, Vite, Element Plus UI
- Backend: .NET 8 Web API (in-memory storage)
- Tests: Vitest + @vue/test-utils for frontend unit tests
- Deployment: Docker (multi-stage images) + docker-compose

---

## Quickstart (development)

Prerequisites:
- Node.js (recommended 18+ / tested with Node 20 in container)
- .NET SDK 8.0
- npm (or yarn)

1) Start the backend API locally

```bash
cd backend/Walkers.Books/Walkers.Books.Api
dotnet run --project Walkers.Books.Api.csproj
```

The API will listen on the ASP.NET configured URL (defaults to http://localhost:5000 in development launch settings).

2) Start the frontend dev server

```bash
cd frontend
npm install
npm run dev
```

The Vite dev server will open at http://localhost:5173 (or another port it selects). The frontend is configured to call the backend under `/api/`.

---

## Building for production

Frontend (build):

```bash
cd frontend
npm run build

# serve the `dist` folder (or use the provided Dockerfile/nginx config)
```

Backend (publish):

```bash
cd backend/Walkers.Books/Walkers.Books.Api
dotnet publish -c Release -o ./publish
```

---

## Tests

Frontend unit tests use Vitest. Run them from the `frontend` folder:

```bash
cd frontend
npm install
npm test
```

There is at least one unit test covering the Add Book modal flow.

Backend tests (if present) can be run with `dotnet test` from the solution folder.

---

## Docker & docker-compose

This repo includes Dockerfiles for the frontend and backend and a `docker-compose.yml` to run both together for local testing.

Common workflow:

```bash
# from repo root
docker compose build
docker compose up -d

# check containers
docker compose ps
```

Notes:
- The frontend image serves a production build with nginx on container port 80.
- The backend listens on container port 5000. If port 5000 is already in use on your machine, the compose file maps the backend to an alternate host port (49513) to avoid collisions — check `docker compose ps` or `docker-compose.yml` for the exact mapping.

Simple smoke checks:

```bash
curl -I http://localhost:8080/      # frontend (nginx)
curl -I http://localhost:49513/api/v1.0/books  # backend API (host port may vary)
```

---

## Project structure (high level)

- backend/Walkers.Books/Walkers.Books.Api — .NET Web API project
- frontend/ — Vue 3 application (Vite) with Element Plus and tests

Key frontend files:
- `src/views/MyBooksView.vue` — main book listing and modal wiring
- `src/components/{AddBookModal.vue, EditBookModal.vue, ViewBookModal.vue, DeleteBookModal.vue}` — modal components
- `src/services/bookService.ts` — API client

---

## Validation and business rules (implemented)

- Max 25 books total.
- Ratings must be integers 1–5.
- Comments are required if a rating is provided; comments must not include the word "horrible".
- String fields enforce sensible length limits in the UI.

These rules are enforced on the frontend and validated on the backend API.

---

## UX notes & known improvements

See `VISUAL_IMPROVEMENTS.md` for suggested UX and visual polish items that improve the product for a real user.

---

## Deliverables

- Complete Git repo with source code (this repo)
- README with setup instructions (this file)
- `VISUAL_IMPROVEMENTS.md` with suggested UI/UX improvements
- At least one unit test (frontend)

---