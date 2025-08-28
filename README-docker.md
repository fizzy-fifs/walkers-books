Docker setup

This repo includes Dockerfiles for the backend (.NET 8) and frontend (Node + nginx), plus a `docker-compose.yml` to run both together.

Build and run locally:

```bash
# from repo root
docker compose build
docker compose up
```

- Backend will be available at http://localhost:49513 (mapped to container 5000)
- Frontend will be available at http://localhost:8080 and will proxy `/api/` requests to the backend.

Notes:
- The frontend nginx config proxies `/api/` to the backend service name `backend:5000` inside Docker compose.
- For production, ensure secrets and configuration are handled securely.
